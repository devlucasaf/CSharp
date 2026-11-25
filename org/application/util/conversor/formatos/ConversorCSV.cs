using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Util.Conversor.Formatos
{
    public static class ConversorCSV
    {
        public static List<Registro> Ler(string arquivo)
        {
            string[] linhas = File.ReadAllLines(arquivo);
            if (linhas.Length == 0)
            {
                return new List<Registro>();
            }

            string[] cabecalho = Separar(linhas[0]);
            var registros = new List<Registro>();

            for (int i = 1; i < linhas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(linhas[i]))
                {
                    continue;
                }

                string[] valores = Separar(linhas[i]);
                var r = new Registro();
                for (int j = 0; j < cabecalho.Length && j < valores.Length; j++)
                {
                    r.Set(cabecalho[j], valores[j]);
                }
                registros.Add(r);
            }
            return registros;
        }

        public static void Escrever(List<Registro> registros, string arquivo)
        {
            if (registros.Count == 0)
            {
                File.WriteAllText(arquivo, "");
                return;
            }

            var sb = new StringBuilder();
            List<string> chaves = registros[0].GetCampos().Keys.ToList();
            sb.Append(string.Join(",", chaves)).Append('\n');

            foreach (Registro r in registros)
            {
                var valores = new List<string>();
                foreach (string c in chaves)
                {
                    valores.Add(Escapar(r.Get(c)));
                }
                sb.Append(string.Join(",", valores)).Append('\n');
            }
            File.WriteAllText(arquivo, sb.ToString());
        }

        private static string[] Separar(string linha)
        {
            var partes = new List<string>();
            var atual = new StringBuilder();
            bool dentroAspas = false;

            for (int i = 0; i < linha.Length; i++)
            {
                char c = linha[i];
                if (c == '"')
                {
                    dentroAspas = !dentroAspas;
                }
                else if (c == ',' && !dentroAspas)
                {
                    partes.Add(atual.ToString());
                    atual.Clear();
                }
                else
                {
                    atual.Append(c);
                }
            }
            partes.Add(atual.ToString());
            return partes.ToArray();
        }

        private static string Escapar(string? valor)
        {
            if (valor == null)
            {
                return "";
            }

            if (valor.Contains(",") || valor.Contains("\"") || valor.Contains("\n"))
            {
                return "\"" + valor.Replace("\"", "\"\"") + "\"";
            }
            return valor;
        }
    }
}
