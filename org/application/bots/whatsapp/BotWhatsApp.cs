using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bots.WhatsApp
{
    public class BotWhatsApp
    {
        private readonly HttpClient http;
        private readonly string     token;
        private readonly string     phoneNumberId;
        private readonly string     versaoApi;
        private readonly Dictionary<string, ManipuladorComando> comandos = new Dictionary<string, ManipuladorComando>(StringComparer.OrdinalIgnoreCase);

        public BotWhatsApp(HttpClient http, string token, string phoneNumberId, string versaoApi)
        {
            this.http = http;
            this.token = token;
            this.phoneNumberId = phoneNumberId;
            this.versaoApi = versaoApi;
            this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // --- REGISTRA UM NOVO COMANDO E ASSOCIA ESSE COMANDO A UMA AÇÃO. ---
        public void RegistrarComando(string comando, ManipuladorComando handler)
        {
            string nomeComando = comando.StartsWith("/") ? comando : "/" + comando;
            comandos[nomeComando] = handler;
        }

        // --- INTERPRETA A MENSAGEM RECEBIDA E IDENTIFICA O COMANDO SOLICITADO. ---
        public async Task ResponderAsync(string telefone, string texto, CancellationToken cancellationToken = default)
        {
            string mensagemRecebida = texto.Trim();

            if (string.IsNullOrWhiteSpace(mensagemRecebida))
            {
                return;
            }

            string[] partes = mensagemRecebida.Split(new[] { ' ', '\t', '\r', '\n' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string comando = partes[0].ToLowerInvariant();
            string argumentos = partes.Length > 1 ? partes[1] : string.Empty;

            string resposta = comandos.TryGetValue(comando, out ManipuladorComando handler)
                ? handler(telefone, argumentos)
                : "Comando desconhecido. Envie /help para ver a lista.";

            await EnviarAsync(telefone, resposta, cancellationToken);
        }

        // --- ENVIA UMA MENSAGEM DE TEXTO UTILIZANDO A WHATSAPP CLOUD API. ---
        public async Task EnviarAsync(string telefone, string texto, CancellationToken cancellationToken = default)
        {
            string url = $"https://graph.facebook.com/{versaoApi}/{phoneNumberId}/messages";

            object corpo = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = telefone,
                type = "text",
                text = new
                {
                    preview_url = false,
                    body = texto
                }
            };

            using HttpResponseMessage resposta = await http.PostAsJsonAsync(url, corpo, cancellationToken);
            string conteudo = await resposta.Content.ReadAsStringAsync(cancellationToken);

            if (!resposta.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Não foi possível enviar a mensagem. Código HTTP: {(int)resposta.StatusCode}. Resposta: {conteudo}"
                );
            }
        }
    }
}
