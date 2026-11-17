using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Utilitarios.Logs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string arquivo = args.Length > 0 ? args[0] : GerarLogExemplo();
            Regex? filtro = args.Length > 1 ? new Regex(args[1]) : null;
            string? nivel = args.Length > 2 ? args[2] : null;

            Console.WriteLine("Analisando: " + Path.GetFullPath(arquivo));
            if (filtro != null)
            {
                Console.WriteLine("Filtro regex: " + filtro);
            }

            if (nivel != null)
            {
                Console.WriteLine("Filtro nivel: " + nivel);
            }
            Console.WriteLine();

            var analisador = new AnalisadorLogs(arquivo);
            Resultado resultado = analisador.Analisar(filtro, nivel);

            Console.WriteLine("=== ESTATISTICAS ===");
            Console.WriteLine("Linhas totais: " + resultado.TotalLinhas);
            Console.WriteLine("Linhas validas: " + resultado.LinhasValidas);
            Console.WriteLine("Linhas casadas: " + resultado.LinhasCasadas);
            Console.WriteLine("Primeira: " + resultado.Primeira);
            Console.WriteLine("Ultima: " + resultado.Ultima);
            Console.WriteLine();

            Console.WriteLine("=== POR NIVEL ===");
            foreach (var (k, v) in resultado.PorNivel)
            {
                Console.WriteLine($"{k,-8} {v}");
            }
            Console.WriteLine();

            Console.WriteLine("=== POR HORA ===");
            foreach (var (k, v) in resultado.PorHora)
            {
                Console.WriteLine($"{k:D2}h  {new string('#', Math.Min(60, v))} ({v})");
            }
        }

        private static string GerarLogExemplo()
        {
            string arq = Path.Combine(Path.GetTempPath(),
                "exemplo-log-" + Guid.NewGuid().ToString("N") + ".log");
            string[] niveis = { "INFO", "INFO", "INFO", "WARN", "ERROR", "DEBUG" };
            string[] mensagens =
            {
                "Conexao estabelecida com banco de dados",
                "Usuario logado com sucesso",
                "Cache atualizado",
                "Timeout ao chamar servico externo",
                "Falha ao processar requisicao",
                "Iniciando job de indexacao",
                "Requisicao processada em 45ms",
                "Erro de autenticacao: token invalido"
            };

            var rnd = new Random(42);
            var sb = new StringBuilder();
            DateTime dt = DateTime.Now.AddHours(-24);
            for (int i = 0; i < 5000; i++)
            {
                dt = dt.AddSeconds(rnd.Next(20) + 1);
                string linha = "[" + dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "] "
                            + niveis[rnd.Next(niveis.Length)] + " "
                            + mensagens[rnd.Next(mensagens.Length)];
                sb.AppendLine(linha);
            }
            File.WriteAllText(arq, sb.ToString());
            Console.WriteLine("Arquivo de exemplo criado com 5000 linhas");
            return arq;
        }
    }
}
