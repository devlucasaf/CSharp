using System;
using System.Collections.Generic;

namespace Org.Application.MegaSena
{
    class Program
    {
        static void Main(string[] args)
        {
            var megaSena = new MegaSena();

            Console.Write("Digite quantos jogos de Mega Sena quer fazer: ");
            int numJogos = int.Parse(Console.ReadLine());

            for (int i = 0; i < numJogos; i++)
            {
                Console.WriteLine("\nNovo jogo!");
                Console.Write("Digite a quantidade de dezenas (6-20): ");
                int qtd = int.Parse(Console.ReadLine());

                try
                {
                    List<int> jogo = megaSena.GerarJogo(qtd);
                    Console.WriteLine($"Seu jogo está pronto! Números: {string.Join(", ", jogo)}");
                    Console.WriteLine($"Custo da aposta: R${megaSena.CustoJogoMegaSena(qtd):F2}");

                    megaSena.GravarJogos();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }
    }
}