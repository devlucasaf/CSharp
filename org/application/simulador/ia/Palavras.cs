using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Application.Simuladores.Ia
{
    public class Palavras
    {
        // --- REMOVE ESPAÇOS EXCEDENTES E NORMALIZA O TEXTO RECEBIDO ---
        public static string ExtrairInteligente(string? textoBruto)
        {
            if (string.IsNullOrWhiteSpace(textoBruto))
            {
                return "";
            }

            string textoLimpo = Regex.Replace(textoBruto.Trim(), "\\s+", " ");
            return textoLimpo;
        }

        // --- VERIFICA SE O ARQUIVO PDF EXISTE NO CAMINHO INFORMADO ---
        public static void Pdf()
        {
            string caminhoPdf = Path.Combine("C:", "VSCode", "GitHub", "IA",
                "leitor-de-documentos-IA", "teste.pdf");

            try
            {
                if (!File.Exists(caminhoPdf))
                {
                    Console.WriteLine("Arquivo PDF nao encontrado: " + caminhoPdf);
                    return;
                }
                Console.WriteLine("PDF localizado: " + caminhoPdf);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.Error.WriteLine("Sem permissão para acessar o PDF: " + e.Message);
            }
        }

        // --- CONSULTA E EXIBE O CONTEÚDO DE UM DOCUMENTO PÚBLICO DO GOOGLE DOCS ---
        public static void Docs()
        {
            Console.Write("Informe a URL pública/exportável do Google Docs: ");
            string url = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("URL inválida.");
                return;
            }

            using var cliente = new HttpClient();
            try
            {
                using var respostaHttp = cliente.GetAsync(url).GetAwaiter().GetResult();
                Console.WriteLine("Status HTTP: " + (int)respostaHttp.StatusCode);
                string body = respostaHttp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(ExtrairInteligente(body));
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine("Erro ao consultar o documento: " + e.Message);
            }
            catch (UriFormatException e)
            {
                Console.Error.WriteLine("URL invalida: " + e.Message);
            }
        }

        // --- SOLICITA O MODO DE LEITURA E EXECUTA A OPÇÃO SELECIONADA ---
        public static void Main(string[] args)
        {
            Console.Write("Qual modo deseja utilizar:\n[1] - Arquivo PDF\n[2] - Google Docs\nOpção: ");
            string opcao = (Console.ReadLine() ?? "").Trim();

            switch (opcao)
            {
                case "1":
                    Pdf();
                    break;
                case "2":
                    Docs();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }
}
