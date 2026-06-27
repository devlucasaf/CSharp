using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace games.forca
{
    public static class JogoDaForca
    {
        private static readonly string[] PalavrasBase = new[]
        {
            "banana", "lorenx", "batata", "amostradinho", "receba",
            "ceub", "apendicite", "guilherme", "santiago", "aula", "samambaia"
        };

        private static List<string> PalavraEscondida() => PalavrasBase.ToList();

        private static void Linha()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("+=+=", 56)));
        }

        private static string EscolherPalavra(List<string> palavras)
        {
            var random = new Random();
            return palavras[random.Next(palavras.Count)].Trim();
        }

        private static string MostrarPalavra(string palavra, HashSet<char> letrasAdivinhadas)
        {
            var resultado = new StringBuilder();
            foreach (char letra in palavra)
            {
                if (letrasAdivinhadas.Contains(letra))
                {
                    resultado.Append(letra).Append(' ');
                }
                else
                {
                    resultado.Append("_ ");
                }
            }
            return resultado.ToString().TrimEnd();
        }

        private static void Jogar()
        {
            int vida = 6;
            string coracao = "❤️";
            string palavraSecreta = EscolherPalavra(PalavraEscondida());
            var letrasAdivinhadas = new HashSet<char>();
            var letrasErradas = new HashSet<char>();

            Linha();
            Console.WriteLine($"{"Jogo da forca",112}");
            Linha();
            Console.WriteLine($"Essa sua palavra tem {palavraSecreta.Length} letras");

            while (vida > 0)
            {
                Console.WriteLine(MostrarPalavra(palavraSecreta, letrasAdivinhadas));
                Console.WriteLine($"Você tem {string.Concat(Enumerable.Repeat(coracao, vida))}");

                Console.Write("Digite uma letra: ");
                string input = Console.ReadLine()?.ToLower() ?? "";
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                char letra = input[0];

                if (letrasAdivinhadas.Contains(letra) || letrasErradas.Contains(letra))
                {
                    Console.WriteLine("Você já digitou essa letra! Tente novamente!");
                    continue;
                }

                if (palavraSecreta.Contains(letra))
                {
                    letrasAdivinhadas.Add(letra);

                    bool venceu = palavraSecreta.All(c => letrasAdivinhadas.Contains(c));
                    if (venceu)
                    {
                        Console.WriteLine($"Você adivinhou a palavra secreta! A palavra secreta era: {palavraSecreta}");
                        break;
                    }
                }
                else
                {
                    letrasErradas.Add(letra);
                    vida--;
                    if (vida == 0)
                    {
                        Console.WriteLine($"Você perdeu o jogo! A palavra era: {palavraSecreta}");
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            Jogar();

            while (true)
            {
                Console.Write("Digite [SIM] para continuar ou [NAO] para fechar o jogo: ");
                string escolha = Console.ReadLine()?.ToUpper() ?? "";

                if (escolha == "SIM")
                {
                    Jogar();
                }
                else if (escolha == "NAO")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("A palavra digitada não é aceita! Tente novamente!");
                }
            }
        }
    }
}