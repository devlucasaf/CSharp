using System;
using System.Collections.Generic;
using System.Threading;

namespace games
{
    public static class Bingo
    {
        private const int TAM = 5;

        private static int[,] cartela = new int[TAM, TAM];
        private static bool[,] marcacoes = new bool[TAM, TAM];

        private static List<int> numerosSorteados = new List<int>();
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            GerarCartela();

            Console.WriteLine("BINGO");
            ImprimirCartela();

            while (true)
            {
                int numero = SortearNumero();
                Console.WriteLine($"\nNúmero sorteado: {numero}");

                MarcarNumero(numero);
                ImprimirCartela();

                if (VerificarVitoria())
                {
                    Console.WriteLine("\nBINGO! VOCÊ VENCEU!");
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        private static void GerarCartela()
        {
            List<int> numerosUsados = new List<int>();

            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (i == 2 && j == 2)
                    {
                        cartela[i, j] = 0;
                        marcacoes[i, j] = true;
                        continue;
                    }

                    int numero;
                    do
                    {
                        numero = random.Next(1, 76); 
                    } 
                    while (numerosUsados.Contains(numero));

                    numerosUsados.Add(numero);
                    cartela[i, j] = numero;
                }
            }
        }

        private static int SortearNumero()
        {
            int numero;
            do
            {
                numero = random.Next(1, 76);
            }
            while (numerosSorteados.Contains(numero));

            numerosSorteados.Add(numero);
            return numero;
        }

        private static void MarcarNumero(int numero)
        {
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (cartela[i, j] == numero)
                    {
                        marcacoes[i, j] = true;
                    }
                }
            }
        }

        private static void ImprimirCartela()
        {
            Console.WriteLine("-----------------------------");
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (i == 2 && j == 2)
                    {
                        Console.Write(" FREE ");
                    }
                    else if (marcacoes[i, j])
                    {
                        Console.Write($"[{cartela[i, j],2}] ");
                    }
                    else
                    {
                        Console.Write($" {cartela[i, j],2}  ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------------");
        }

        private static bool VerificarVitoria()
        {
            // Verifica linhas
            for (int i = 0; i < TAM; i++)
            {
                bool linhaCompleta = true;
                for (int j = 0; j < TAM; j++)
                {
                    if (!marcacoes[i, j])
                    {
                        linhaCompleta = false;
                        break;
                    }
                }
                if (linhaCompleta)
                {
                    return true;
                }
            }

            for (int j = 0; j < TAM; j++)
            {
                bool colunaCompleta = true;
                for (int i = 0; i < TAM; i++)
                {
                    if (!marcacoes[i, j])
                    {
                        colunaCompleta = false;
                        break;
                    }
                }
                if (colunaCompleta)
                {
                    return true;
                }
            }

            bool diagonal1 = true;
            for (int i = 0; i < TAM; i++)
            {
                if (!marcacoes[i, i])
                {
                    diagonal1 = false;
                    break;
                }
            }
            if (diagonal1)
            {
                return true;
            }

            // Diagonal secundária
            bool diagonal2 = true;
            for (int i = 0; i < TAM; i++)
            {
                if (!marcacoes[i, TAM - 1 - i])
                {
                    diagonal2 = false;
                    break;
                }
            }
            return diagonal2;
        }
    }
}