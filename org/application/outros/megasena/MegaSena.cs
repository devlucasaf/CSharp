using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Outros.MegaSena
{
    public class MegaSena
    {
        private List<int> NumerosJogosAJogar;

        public List<int> GerarJogo(int quantidadeNumeros)
        {
            if (quantidadeNumeros < 6 || quantidadeNumeros > 20)
            {
                throw new ArgumentException("Para jogar, escolha entre 6 e 20 dezenas.");
            }

            var selecao = new HashSet<int>();
            var random = new Random();

            while (selecao.Count < quantidadeNumeros)
            {
                selecao.Add(random.Next(1, 61)); 
            }

            NumerosJogosAJogar = selecao.OrderBy(n => n).ToList();
            return NumerosJogosAJogar;
        }

        public double CustoJogoMegaSena(int tamanho)
        {
            return tamanho switch
            {
                6  => 5.00,
                7  => 35.00,
                8  => 140.00,
                9  => 420.00,
                10 => 1050.00,
                11 => 2310.00,
                12 => 4620.00,
                13 => 8580.00,
                14 => 15015.00,
                15 => 25035.00,
                16 => 40040.00,
                17 => 61880.00,
                18 => 92820.00,
                19 => 135600.00,
                20 => 193800.00,
                _  => 0.0
            };
        }

        public void GravarJogos()
        {
            try
            {
                using var writer = new StreamWriter("mega-teste.txt", append: true);
                writer.WriteLine("     Jogo para a Mega da Virada     ");
                writer.WriteLine("                |                   ");
                writer.WriteLine("------------------------------------");
                writer.WriteLine($"    {string.Join(", ", NumerosJogosAJogar)}");
                writer.WriteLine("------------------------------------");
                writer.WriteLine($"- Valor total: R${CustoJogoMegaSena(NumerosJogosAJogar.Count):F2} ------");
                writer.WriteLine("\n");
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"Erro ao salvar: {ex.Message}");
            }
        }
    }
}