using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Games.Texto.Capitais
{
    public class CapitaisDoMundo
    {
        private readonly List<Pais> banco = new List<Pais>
        {
            new Pais("Brasil", "Brasilia", "América do Sul"),
            new Pais("Argentina", "Buenos Aires", "América do Sul"),
            new Pais("Chile", "Santiago", "América do Sul"),
            new Pais("Peru", "Lima", "América do Sul"),
            new Pais("Colombia", "Bogota", "América do Sul"),
            new Pais("Uruguai", "Montevideu", "América do Sul"),
            new Pais("Venezuela", "Caracas", "América do Sul"),
            new Pais("Estados Unidos", "Washington", "América do Norte"),
            new Pais("Canada", "Ottawa", "América do Norte"),
            new Pais("Mexico", "Cidade do Mexico", "América do Norte"),
            new Pais("Portugal", "Lisboa", "Europa"),
            new Pais("Espanha", "Madri", "Europa"),
            new Pais("Franca", "Paris", "Europa"),
            new Pais("Italia", "Roma", "Europa"),
            new Pais("Alemanha", "Berlim", "Europa"),
            new Pais("Reino Unido", "Londres", "Europa"),
            new Pais("Russia", "Moscou", "Europa"),
            new Pais("Holanda", "Amsterda", "Europa"),
            new Pais("Grecia", "Atenas", "Europa"),
            new Pais("Suecia", "Estocolmo", "Europa"),
            new Pais("Japao", "Toquio", "Ásia"),
            new Pais("China", "Pequim", "Ásia"),
            new Pais("Coreia do Sul", "Seul", "Ásia"),
            new Pais("India", "Nova Deli", "Ásia"),
            new Pais("Tailandia", "Bangcoc", "Ásia"),
            new Pais("Vietna", "Hanoi", "Ásia"),
            new Pais("Egito", "Cairo", "África"),
            new Pais("África do Sul", "Pretoria", "África"),
            new Pais("Nigeria", "Abuja", "África"),
            new Pais("Marrocos", "Rabat", "África"),
            new Pais("Australia", "Canberra", "Oceania"),
            new Pais("Nova Zelandia", "Wellington", "Oceania")
        };

        public static void Main(string[] args)
        {
            new CapitaisDoMundo().Iniciar();
        }

        public void Iniciar()
        {
            Console.WriteLine("=== CAPITAIS DO MUNDO ===");
            Console.WriteLine("1 - País -> Capital");
            Console.WriteLine("2 - Capital -> País");
            Console.WriteLine("3 - Filtrar por continente");
            Console.Write("Modo: ");

            string modo = Console.ReadLine()?.Trim() ?? string.Empty;
            List<Pais> rodada;

            if (modo == "3")
            {
                Console.WriteLine("Continentes: América do Sul | América do Norte | " + "Europa | Ásia | África | Oceania");

                Console.Write("Continente: ");

                string continente =
                    Console.ReadLine()?.Trim() ?? string.Empty;

                rodada = banco
                    .Where(pais => Normalizar(pais.Continente) == Normalizar(continente))
                    .ToList();

                if (rodada.Count == 0)
                {
                    Console.WriteLine("Nenhum pais encontrado.");
                    return;
                }

                Console.Write("1 - Pais -> Capital ou 2 - Capital -> Pais? ");

                modo = Console.ReadLine()?.Trim() ?? string.Empty;
            }
            else
            {
                rodada = new List<Pais>(banco);
            }

            bool paisParaCapital = modo != "2";

            Console.Write("Quantas perguntas? ");

            if (!int.TryParse(Console.ReadLine()?.Trim(), out int quantidade))
            {
                quantidade = 10;
            }

            if (quantidade < 1)
            {
                quantidade = 1;
            }
            else if (quantidade > rodada.Count)
            {
                quantidade = rodada.Count;
            }

            Embaralhar(rodada);

            int acertos = 0;

            for (int i = 0; i < quantidade; i++)
            {
                Pais pais = rodada[i];

                string enunciado;
                string esperado;
                string rotulo;

                if (paisParaCapital)
                {
                    enunciado = pais.Nome;
                    esperado = pais.Capital;
                    rotulo = "Capital de";
                }
                else
                {
                    enunciado = pais.Capital;
                    esperado = pais.Nome;
                    rotulo = "Pais cuja capital e";
                }

                Console.WriteLine($"\n[{i + 1}/{quantidade}] " + $"{rotulo} {enunciado}?");

                Console.Write("Resposta: ");

                string resposta = Console.ReadLine()?.Trim() ?? string.Empty;

                if (Normalizar(resposta) == Normalizar(esperado))
                {
                    Console.WriteLine("Correto!");
                    acertos++;
                }
                else
                {
                    Console.WriteLine("Errado. Resposta correta: " + esperado);
                }
            }

            double percentual = acertos * 100.0 / quantidade;

            Console.WriteLine("\n===== FIM =====");
            Console.WriteLine($"Acertos: {acertos}/{quantidade} " + $"({percentual:F1}%)");
        }

        private static string Normalizar(string texto)
        {
            string textoDecomposto = texto.Normalize(NormalizationForm.FormD);

            StringBuilder textoSemAcentos = new StringBuilder();

            foreach (char caractere in textoDecomposto)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);

                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    textoSemAcentos.Append(caractere);
                }
            }

            string[] palavras = textoSemAcentos.ToString()
                .ToLowerInvariant()
                .Trim()
                .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(" ", palavras);
        }

        private static void Embaralhar<T>(IList<T> itens)
        {
            Random random = new Random();

            for (int i = itens.Count - 1; i > 0; i--)
            {
                int indiceSorteado = random.Next(i + 1);

                T temporario = itens[i];
                itens[i] = itens[indiceSorteado];
                itens[indiceSorteado] = temporario;
            }
        }
    }
}
