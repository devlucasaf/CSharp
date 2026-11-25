using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Util.Conversor.Formatos
{
    public static class ConversorJSON
    {
        public static void Escrever(List<Registro> registros, string arquivo)
        {
            var sb = new StringBuilder("[\n");
            for (int i = 0; i < registros.Count; i++)
            {
                sb.Append("  {");
                var partes = new List<string>();
                foreach (var e in registros[i].GetCampos())
                {
                    partes.Add("\"" + Escapar(e.Key) + "\": \"" + Escapar(e.Value) + "\"");
                }

                sb.Append(string.Join(", ", partes));
                sb.Append("}");
                if (i < registros.Count - 1)
                {
                    sb.Append(",");
                }
                sb.Append("\n");
            }
            sb.Append("]\n");
            File.WriteAllText(arquivo, sb.ToString());
        }

        public static List<Registro> Ler(string arquivo)
        {
            string texto = File.ReadAllText(arquivo).Trim();
            if (texto.StartsWith("["))
            {
                texto = texto.Substring(1);
            }

            if (texto.EndsWith("]"))
            {
                texto = texto.Substring(0, texto.Length - 1);
            }

            var registros = new List<Registro>();
            int nivel = 0;
            var atual = new StringBuilder();
            bool dentroAspas = false;

            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];
                if (c == '"' && (i == 0 || texto[i - 1] != '\\'))
                {
                    dentroAspas = !dentroAspas;
                }

                if (!dentroAspas)
                {
                    if (c == '{')
                    {
                        nivel++;
                    }
                    else if (c == '}')
                    {
                        nivel--;
                        if (nivel == 0)
                        {
                            atual.Append(c);
                            registros.Add(ParseObjeto(atual.ToString()));
                            atual.Clear();
                            continue;
                        }
                    }
                }

                if (nivel > 0)
                {
                    atual.Append(c);
                }
            }
            return registros;
        }

        private static Registro ParseObjeto(string texto)
        {
            var registro = new Registro();
            texto = texto.Trim();
            if (texto.StartsWith("{"))
            {
                texto = texto.Substring(1);
            }

            if (texto.EndsWith("}"))
            {
                texto = texto.Substring(0, texto.Length - 1);
            }

            bool dentroAspas = false;
            var atual = new StringBuilder();
            var pares = new List<string>();

            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];
                if (c == '"' && (i == 0 || texto[i - 1] != '\\'))
                {
                    dentroAspas = !dentroAspas;
                }

                if (c == ',' && !dentroAspas)
                {
                    pares.Add(atual.ToString());
                    atual.Clear();
                }
                else
                {
                    atual.Append(c);
                }
            }
            pares.Add(atual.ToString());

            foreach (string par in pares)
            {
                int sep = par.IndexOf(':');
                if (sep < 0)
                {
                    continue;
                }

                string chave = LimparString(par.Substring(0, sep));
                string valor = LimparString(par.Substring(sep + 1));
                registro.Set(chave, valor);
            }
            return registro;
        }

        private static string LimparString(string s)
        {
            s = s.Trim();
            if (s.StartsWith("\""))
            {
                s = s.Substring(1);
            }

            if (s.EndsWith("\""))
            {
                s = s.Substring(0, s.Length - 1);
            }
            return s.Replace("\\\"", "\"").Replace("\\\\", "\\");
        }

        private static string Escapar(string? s)
        {
            if (s == null)
            {
                return "";
            }
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
