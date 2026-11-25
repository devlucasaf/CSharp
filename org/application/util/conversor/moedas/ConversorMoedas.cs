using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Application.Util.Conversor.Moedas
{
    public class ConversorMoedas
    {
        private const string UrlBase = "https://economia.awesomeapi.com.br/last/";

        private readonly HttpClient http = new HttpClient();
        private readonly string historicoArquivo = Path.Combine("target", "historico-cotacoes.csv");

        public Dictionary<string, double> Consultar(params string[] pares)
        {
            string url = UrlBase + string.Join(",", pares);
            using var resp = http.GetAsync(url).GetAwaiter().GetResult();
            if ((int)resp.StatusCode != 200)
            {
                throw new InvalidOperationException("Erro HTTP: " + (int)resp.StatusCode);
            }
            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return Parsear(body);
        }

        private Dictionary<string, double> Parsear(string json)
        {
            var resultado = new Dictionary<string, double>();
            int i = 0;
            while (i < json.Length)
            {
                int codeStart = json.IndexOf("\"code\":\"", i, StringComparison.Ordinal);
                if (codeStart == -1) break;
                codeStart += 8;
                int codeEnd = json.IndexOf('"', codeStart);
                string code = json.Substring(codeStart, codeEnd - codeStart);

                int codeInStart = json.IndexOf("\"codein\":\"", codeEnd, StringComparison.Ordinal);
                codeInStart += 10;
                int codeInEnd = json.IndexOf('"', codeInStart);
                string codein = json.Substring(codeInStart, codeInEnd - codeInStart);

                int bidStart = json.IndexOf("\"bid\":\"", codeInEnd, StringComparison.Ordinal);
                bidStart += 7;
                int bidEnd = json.IndexOf('"', bidStart);
                string bid = json.Substring(bidStart, bidEnd - bidStart);

                resultado[code + codein] = double.Parse(bid, CultureInfo.InvariantCulture);
                i = bidEnd;
            }
            return resultado;
        }

        public void SalvarHistorico(Dictionary<string, double> cotacoes)
        {
            string? dir = Path.GetDirectoryName(historicoArquivo);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var sb = new StringBuilder();
            string data = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            foreach (var e in cotacoes)
            {
                sb.Append(data).Append(',')
                    .Append(e.Key).Append(',')
                    .Append(e.Value.ToString(CultureInfo.InvariantCulture))
                    .Append('\n');
            }

            File.AppendAllText(historicoArquivo, sb.ToString());
        }

        public string GetHistoricoArquivo() => historicoArquivo;
    }
}
