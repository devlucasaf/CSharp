using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace Application.Bots.Telegram
{
    public class BotTelegram
    {
        private readonly string token;
        private readonly HttpClient http;
        private long ultimoUpdateId = 0;

        private readonly Dictionary<string, ManipuladorComando> comandos =
            new Dictionary<string, ManipuladorComando>();

        public BotTelegram(string token)
        {
            this.token = token;

            http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(40)
            };
        }

        // --- REGISTRA UM NOVO COMANDO E ASSOCIA ESSE COMANDO A UMA AÇÃO. ---
        public void RegistrarComando(string comando, ManipuladorComando handler)
        {
            string nomeComando = comando.StartsWith("/")
                ? comando
                : "/" + comando;

            comandos[nomeComando] = handler;
        }

        // --- MANTÉM O BOT EM EXECUÇÃO, VERIFICANDO CONTINUAMENTE SE EXISTEM NOVAS MENSAGENS. ---
        public void Executar()
        {
            Console.WriteLine("Bot Telegram iniciado. Aguardando updates...");

            // --- CRIA UM LAÇO INFINITO PARA MANTER O BOT FUNCIONANDO. ---
            while (true)
            {
                try
                {
                    ProcessarUpdates();
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Erro: " + e.Message);
                }
            }
        }

        // --- CONSULTA A API DO TELEGRAM E PROCESSA AS ATUALIZAÇÕES RECEBIDAS. ---
        private void ProcessarUpdates()
        {
            string url = $"https://api.telegram.org/bot{token}/getUpdates" +
                $"?offset={ultimoUpdateId + 1}&timeout=30";

            string respostaHttp = http.GetStringAsync(url).GetAwaiter().GetResult();

            using JsonDocument documento = JsonDocument.Parse(respostaHttp);

            if (!documento.RootElement.TryGetProperty("ok", out JsonElement ok) || !ok.GetBoolean())
            {
                throw new InvalidOperationException("A API do Telegram retornou uma resposta inválida.");
            }

            if (!documento.RootElement.TryGetProperty("result", out JsonElement atualizacoes))
            {
                return;
            }

            // --- PERCORRE TODAS AS ATUALIZAÇÕES RECEBIDAS DA API DO TELEGRAM. ---
            foreach (JsonElement atualizacao in atualizacoes.EnumerateArray())
            {
                if (!atualizacao.TryGetProperty("update_id", out JsonElement elementoUpdateId))
                {
                    continue;
                }

                long updateId = elementoUpdateId.GetInt64();
                ultimoUpdateId = Math.Max(ultimoUpdateId, updateId);

                if (!atualizacao.TryGetProperty("message", out JsonElement mensagem))
                {
                    continue;
                }

                if (!mensagem.TryGetProperty("chat", out JsonElement chat))
                {
                    continue;
                }

                if (!chat.TryGetProperty("id", out JsonElement elementoChatId))
                {
                    continue;
                }

                if (!mensagem.TryGetProperty("text", out JsonElement elementoTexto))
                {
                    continue;
                }

                long chatId = elementoChatId.GetInt64();
                string texto = elementoTexto.GetString() ?? string.Empty;

                Responder(chatId, texto);
            }
        }

        // --- INTERPRETA A MENSAGEM RECEBIDA E IDENTIFICA O COMANDO SOLICITADO PELO USUÁRIO. ---
        private void Responder(long chatId, string texto)
        {
            string[] partes = texto
                .Trim()
                .Split(new[] { ' ', '\t', '\r', '\n' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 0)
            {
                return;
            }

            string comando = partes[0].ToLowerInvariant();
            string argumentos = partes.Length > 1 ? partes[1] : string.Empty;

            string resposta = comandos.TryGetValue(comando, out ManipuladorComando handler)
                    ? handler(chatId, argumentos)
                    : "Comando desconhecido. Envie /help para ver a lista.";

            Enviar(chatId, resposta);
        }

        // --- ENVIA UMA MENSAGEM DE TEXTO PARA UM DETERMINADO CHAT DO TELEGRAM. ---
        public void Enviar(long chatId, string texto)
        {
            string url = $"https://api.telegram.org/bot{token}/sendMessage";

            Dictionary<string, string> campos = new Dictionary<string, string>
                {
                    ["chat_id"] = chatId.ToString(CultureInfo.InvariantCulture),
                    ["text"] = texto
                };

            using FormUrlEncodedContent conteudo = new FormUrlEncodedContent(campos);

            using HttpResponseMessage resposta = http
                .PostAsync(url, conteudo)
                .GetAwaiter()
                .GetResult();

            resposta.EnsureSuccessStatusCode();
        }

        // --- RESPONSÁVEL POR INICIAR A APLICAÇÃO. ---
        public static void Main(string[] args)
        {
            string token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

            if (string.IsNullOrWhiteSpace(token))
            {
                Console.Error.WriteLine("Defina TELEGRAM_BOT_TOKEN. " + "Crie um bot em @BotFather no Telegram.");
                return;
            }

            BotTelegram telegram = new BotTelegram(token);

            telegram.RegistrarComando("/start", (c, a) => "Olá! Comandos: /help, /oi, /soma <a> <b>, /hora");

            telegram.RegistrarComando("/help", (c, a) => "Comandos: /start /oi /soma /hora");

            telegram.RegistrarComando("/oi", (c, a) => "Oi! Tudo bem?");

            telegram.RegistrarComando("/hora", (c, a) => "Agora são " + DateTime.Now.ToString("HH:mm:ss"));

            telegram.RegistrarComando("/soma", (c, a) => 
                {
                    string[] numeros = a.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    if (numeros.Length < 2)
                    {
                        return "Uso: /soma <a> <b>";
                    }

                    bool primeiroNumeroValido = double.TryParse(
                        numeros[0],
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double primeiroNumero
                    );

                    bool segundoNumeroValido = double.TryParse(
                        numeros[1],
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double segundoNumero
                    );

                    if (!primeiroNumeroValido || !segundoNumeroValido)
                    {
                        return "Números inválidos.";
                    }

                    double resultado = primeiroNumero + segundoNumero;

                    return "Resultado: " + resultado.ToString(CultureInfo.InvariantCulture);
                }
            );

            telegram.Executar();
        }
    }
}
