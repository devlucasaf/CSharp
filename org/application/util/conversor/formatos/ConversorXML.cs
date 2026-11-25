using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Util.Conversor.Formatos
{
    public static class ConversorXML
    {
        public static void Escrever(List<Registro> registros, string arquivo)
        {
            var sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            sb.Append("<registros>\n");
            foreach (Registro r in registros)
            {
                sb.Append("  <registro>\n");
                foreach (var e in r.GetCampos())
                {
                    string chave = SanitizarTag(e.Key);
                    sb.Append("    <").Append(chave).Append(">")
                        .Append(Escapar(e.Value))
                        .Append("</").Append(chave).Append(">\n");
                }
                sb.Append("  </registro>\n");
            }
            sb.Append("</registros>\n");
            File.WriteAllText(arquivo, sb.ToString());
        }

        public static List<Registro> Ler(string arquivo)
        {
            string texto = File.ReadAllText(arquivo);
            var padraoRegistro = new Regex("<registro>(.*?)</registro>", RegexOptions.Singleline);
            var padraoCampo = new Regex(@"<(\w+)>(.*?)</\1>", RegexOptions.Singleline);

            var registros = new List<Registro>();
            foreach (Match mReg in padraoRegistro.Matches(texto))
            {
                var r = new Registro();
                foreach (Match mCampo in padraoCampo.Matches(mReg.Groups[1].Value))
                {
                    r.Set(mCampo.Groups[1].Value, Desescapar(mCampo.Groups[2].Value.Trim()));
                }
                registros.Add(r);
            }
            return registros;
        }

        private static string SanitizarTag(string s)
        {
            return Regex.Replace(s, "[^a-zA-Z0-9_]", "_");
        }

        private static string Escapar(string? s)
        {
            if (s == null)
            {
                return "";
            }
            return s.Replace("&", "&amp;").Replace("<", "&lt;")
                    .Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        private static string Desescapar(string s)
        {
            return s.Replace("&quot;", "\"").Replace("&gt;", ">")
                    .Replace("&lt;", "<").Replace("&amp;", "&");
        }
    }
}
