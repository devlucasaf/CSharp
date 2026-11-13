using System;

namespace Games.Texto.Criptografia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("          CRIPTOGRAFIA EM C#       ");

            while (true)
            {
                Console.WriteLine("\nEscolha o algoritmo:");
                Console.WriteLine("1. Cifra de César");
                Console.WriteLine("2. Cifra de Vigenère");
                Console.WriteLine("3. Força Bruta (quebrar César)");
                Console.WriteLine("4. Demonstração");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha: ");

                string escolha = Console.ReadLine() ?? string.Empty;

                switch (escolha)
                {
                    case "1":
                        MenuCesar();
                        break;
                    case "2":
                        MenuVigenere();
                        break;
                    case "3":
                        MenuForcaBruta();
                        break;
                    case "4":
                        Demonstracao();
                        break;
                    case "5":
                        Console.WriteLine("Até mais!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        private static void MenuCesar()
        {
            Console.WriteLine("\n--- CIFRA DE CÉSAR ---");
            Console.WriteLine("1. Criptografar");
            Console.WriteLine("2. Descriptografar");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine() ?? string.Empty;

            if (opcao != "1" && opcao != "2")
            {
                Console.WriteLine("Opção inválida!");
                return;
            }

            Console.Write("Digite o texto: ");
            string texto = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a chave (número): ");

            if (!int.TryParse(Console.ReadLine(), out int chave))
            {
                Console.WriteLine("Chave inválida!");
                return;
            }

            if (opcao == "1")
            {
                string resultado = CifraCesar.Criptografar(texto, chave);
                Console.WriteLine("\n  Original:      " + texto);
                Console.WriteLine("  Criptografado: " + resultado);
            }
            else
            {
                string resultado = CifraCesar.Descriptografar(texto, chave);
                Console.WriteLine("\n  Criptografado:    " + texto);
                Console.WriteLine("  Descriptografado: " + resultado);
            }
        }

        private static void MenuVigenere()
        {
            Console.WriteLine("\n--- CIFRA DE VIGENÈRE ---");
            Console.WriteLine("1. Criptografar");
            Console.WriteLine("2. Descriptografar");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine() ?? string.Empty;

            if (opcao != "1" && opcao != "2")
            {
                Console.WriteLine("Opção inválida!");
                return;
            }

            Console.Write("Digite o texto: ");
            string texto = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a palavra-chave: ");
            string chave = Console.ReadLine() ?? string.Empty;

            if (!ChaveVigenereValida(chave))
            {
                Console.WriteLine("Chave inválida! Utilize somente letras de A a Z.");
                return;
            }

            if (opcao == "1")
            {
                string resultado = CifraVigenere.Criptografar(texto, chave);
                Console.WriteLine("\n  Original:      " + texto);
                Console.WriteLine("  Chave:         " + chave);
                Console.WriteLine("  Criptografado: " + resultado);
            }
            else
            {
                string resultado = CifraVigenere.Descriptografar(texto, chave);
                Console.WriteLine("\n  Criptografado:    " + texto);
                Console.WriteLine("  Chave:            " + chave);
                Console.WriteLine("  Descriptografado: " + resultado);
            }
        }

        private static void MenuForcaBruta()
        {
            Console.WriteLine("\n--- FORÇA BRUTA (QUEBRAR CIFRA DE CÉSAR) ---");
            Console.Write("Digite o texto criptografado: ");
            string texto = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\nTentando todas as 26 chaves possíveis:\n");
            string[] tentativas = CifraCesar.ForcaBruta(texto);

            for (int i = 0; i < tentativas.Length; i++)
            {
                Console.WriteLine($"Chave {i,2}: {tentativas[i]}");
            }
        }

        private static void Demonstracao()
        {
            Console.WriteLine("\n=== DEMONSTRAÇÃO ===\n");

            string original = "Java e muito legal para aprender programacao!";

            // --- CIFRA DE CÉSAR ---
            Console.WriteLine("--- Cifra de César (chave = 3) ---");

            string cesarCriptografado = CifraCesar.Criptografar(original, 3);
            string cesarDescriptografado = CifraCesar.Descriptografar(cesarCriptografado, 3);

            Console.WriteLine("Original:         " + original);
            Console.WriteLine("Criptografado:    " + cesarCriptografado);
            Console.WriteLine("Descriptografado: " + cesarDescriptografado);
            Console.WriteLine("Funciona: " + (original == cesarDescriptografado ? "SIM" : "NÃO"));
            Console.WriteLine();

            // --- CIFRA DE VIGENÈRE ---
            string chave = "JAVA";

            Console.WriteLine($"--- Cifra de Vigenère (chave = '{chave}') ---");

            string vigenereCriptografado = CifraVigenere.Criptografar(original, chave);
            string vigenereDescriptografado = CifraVigenere.Descriptografar(vigenereCriptografado, chave);

            Console.WriteLine("Original:         " + original);
            Console.WriteLine("Criptografado:    " + vigenereCriptografado);
            Console.WriteLine("Descriptografado: " + vigenereDescriptografado);
            Console.WriteLine("Funciona: " + (original == vigenereDescriptografado ? "SIM" : "NÃO"));
            Console.WriteLine();
            Console.WriteLine("Note como Vigenère é mais segura: a mesma letra pode virar letras diferentes dependendo da posição na palavra-chave!");
        }

        private static bool ChaveVigenereValida(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave))
            {
                return false;
            }

            foreach (char caractere in chave)
            {
                if (!(caractere >= 'A' && caractere <= 'Z' || caractere >= 'a' && caractere <= 'z'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
