using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRepositorio.org.application
{
    public class Votacao
    {
        private static List<int> votosTotais = new List<int>();
        private static string eleito = "";

        public static void Run()
        {
            Console.WriteLine(@"
Escolha entre as seguintes opções de candidatos:
22 - Leanderson
13 - Mario Bitcoin
14- Lucao
0 - NULO
9 - Encerrar
");
            eleicao();
        }

        public static void eleicao()
        {
            while (true)
            {
                Console.Write("Digite seu voto: ");
                int voto;

                if (!int.TryParse(Console.ReadLine(), out voto))
                {
                    Console.WriteLine("Entrada inválida! Digite um número.");
                    continue;
                }

                if (voto == 9)
                {
                    Console.WriteLine("Votação encerrada!");
                    break;
                }
                else
                {
                    Console.WriteLine("Número digitado não identificado a nenhum candidato! Tente outro número!");
                }

                if (votosTotais.Count(v => v == 22) > votosTotais.Count(v => v == 13)
                    && votosTotais.Count(v => v == 22) > votosTotais.Count(v => v == 14))
                {
                    eleito = "Leanderson";
                }
                else if (votosTotais.Count(v => v == 13) > votosTotais.Count(v => v == 22)
                    && votosTotais.Count(v => v == 13) > votosTotais.Count(v => v == 14))
                {
                    eleito = "Mario Bitcoin";
                }
                else if (votosTotais.Count(v => v == 14) > votosTotais.Count(v => v == 22)
                    && votosTotais.Count(v => v == 14) > votosTotais.Count(v => v == 13))
                {
                    eleito = "Lucao";
                }
            }

            if (votosTotais.Count != 0)
            {
                int votosLeanderson     = votosTotais.Count(v => v == 22);
                int votosMarioBitcoin   = votosTotais.Count(v => v == 13);
                int votosLucao          = votosTotais.Count(v => v == 14);
                int votosNulo           = votosTotais.Count(v => v == 0);
                int totalVotos          = votosTotais.Count;

                var resultados = new Dictionary<string, int>()
                {
                    { "Leanderson",     votosLeanderson     },
                    { "Mario Bitcoin",  votosMarioBitcoin   },
                    { "Lucao",          votosLucao          }
                };

                eleito = resultados.OrderByDescending(x => x.Value).First().Key;
                int votosEleito = resultados[eleito];

                double primeiraPorcentagem  = (votosLeanderson   / (double)totalVotos) * 100;
                double segundaPorcentagem   = (votosMarioBitcoin / (double)totalVotos) * 100;
                double terceiraPorcentagem  = (votosLucao        / (double)totalVotos) * 100;
                double porcentagemNulo      = (votosNulo         / (double)totalVotos) * 100;

                if (votosNulo > votosEleito)
                {
                    Console.WriteLine($"Votação anulada! Os votos nulos veceram com {votosNulo}");
                    Console.WriteLine("\nVotação recomeçada!\n");

                    Console.WriteLine(@"
Escolha entre as seguintes opções de candidatos:
22 - Leanderson
13 - Mario Bitcoin
14 - Lucao
0  - NULO
9  - Encerrar
");

                    votosTotais.Clear();
                    eleito = "";
                    eleicao();
                    return;
                }
                else
                {
                    if (totalVotos == 1)
                    {
                        Console.WriteLine($"Nesta eleição houve {totalVotos} voto!");
                    }
                    else
                    {
                        Console.WriteLine($"Nesta eleição houve {totalVotos} votos!");
                        Console.WriteLine($"Leanderson obteve {votosLeanderson} votos com {primeiraPorcentagem:F2}%");
                        Console.WriteLine($"Mario Bitcoin obteve {votosMarioBitcoin} votos com {segundaPorcentagem:F2}%");
                        Console.WriteLine($"Lucao obteve {votosLucao} votos com {terceiraPorcentagem:F2}%");
                        Console.WriteLine($"Obteve {votosNulo} votos nulos com {porcentagemNulo:F2}%");
                        Console.WriteLine($"O candidato {eleito} foi eleito com {votosEleito} votos!");
                    }

                }

            }
            else
            {
                Console.WriteLine("Nenhum voto registrado!");
            }

        }
    }
}
