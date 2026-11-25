using System;
using System.Collections.Generic;
using System.Globalization;

namespace Application.Util.Conversor.Moedas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== CONVERSOR DE MOEDAS (API awesomeapi.com.br) ===\n");

            var c = new ConversorMoedas();
            Dictionary<string, double> cotacoes;

            try
            {
                cotacoes = c.Consultar("USD-BRL", "EUR-BRL", "GBP-BRL", "BTC-BRL", "ARS-BRL");
            }
            catch (Exception)
            {
                Console.WriteLine("Falha ao consultar API (offline?). Usando cotacoes fixas de demonstracao.");
                cotacoes = new Dictionary<string, double>
                {
                    ["USDBRL"] = 5.10,
                    ["EURBRL"] = 5.55,
                    ["GBPBRL"] = 6.50,
                    ["BTCBRL"] = 320000.0,
                    ["ARSBRL"] = 0.006
                };
            }

            Console.WriteLine("Cotacoes atuais:");
            foreach (var kv in cotacoes)
            {
                Console.WriteLine($"  {kv.Key} = R$ {kv.Value:F4}");
            }

            try
            {
                c.SalvarHistorico(cotacoes);
                Console.WriteLine("\nHistorico salvo em: " + c.GetHistoricoArquivo());
            }
            catch (Exception)
            {
                // ignorado
            }

            Console.WriteLine("\n=== CONVERSAO ===");
            Console.Write("Valor: ");
            double valor = double.Parse(Console.ReadLine()!.Replace(",", "."), CultureInfo.InvariantCulture);
            Console.Write("De (USD, EUR, GBP, BTC, ARS): ");
            string origem = Console.ReadLine()!.Trim().ToUpperInvariant();
            Console.Write("Para (BRL): ");
            string destino = Console.ReadLine()!.Trim().ToUpperInvariant();

            if (cotacoes.TryGetValue(origem + destino, out double taxa))
            {
                Console.WriteLine($"{valor:F4} {origem} = {valor * taxa:F2} {destino}");
            }
            else
            {
                Console.WriteLine("Par nao encontrado. Tente outro.");
            }
        }
    }
}
