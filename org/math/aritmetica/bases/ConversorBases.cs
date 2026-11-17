using System;
using System.Text;

namespace Math.Aritmetica.Bases
{
    public class ConversorBases
    {
        // --- CONVERTE DECIMAL PARA BINÁRIO POR DIVISÕES SUCESSIVAS POR 2 ---
        public static string DecimalParaBinario(int dec)
        {
            if (dec == 0)
            {
                return "0";
            }

            StringBuilder resultado = new StringBuilder();
            int numero = Math.Abs(dec);
            while (numero > 0)
            {
                resultado.Insert(0, numero % 2);
                numero /= 2;
            }
            return (dec < 0 ? "-" : "") + resultado;
        }

        // --- CONVERTE DECIMAL PARA OCTAL POR DIVISÕES SUCESSIVAS POR 8 ---
        public static string DecimalParaOctal(int dec)
        {
            if (dec == 0)
            {
                return "0";
            }

            StringBuilder resultado = new StringBuilder();
            int numero = Math.Abs(dec);
            while (numero > 0)
            {
                resultado.Insert(0, numero % 8);
                numero /= 8;
            }
            return (dec < 0 ? "-" : "") + resultado;
        }

        // --- CONVERTE DECIMAL PARA HEXADECIMAL POR DIVISÕES SUCESSIVAS POR 16 ---
        public static string DecimalParaHexadecimal(int dec)
        {
            if (dec == 0)
            {
                return "0";
            }

            char[] hexDigitos = "0123456789ABCDEF".ToCharArray();
            StringBuilder resultado = new StringBuilder();
            int num = Math.Abs(dec);
            while (num > 0)
            {
                resultado.Insert(0, hexDigitos[num % 16]);
                num /= 16;
            }
            return (dec < 0 ? "-" : "") + resultado;
        }

        // --- CONVERTE BINÁRIO PARA DECIMAL ---
        public static int BinarioParaDecimal(string binario)
        {
            return Convert.ToInt32(binario, 2);
        }

        // --- CONVERTE OCTAL PARA DECIMAL ---
        public static int OctalParaDecimal(string octal)
        {
            return Convert.ToInt32(octal, 8);
        }

        // --- CONVERTE HEXADECIMAL PARA DECIMAL ---
        public static int HexadecimalParaDecimal(string hexadecimal)
        {
            return Convert.ToInt32(hexadecimal, 16);
        }

        // --- CONVERTE DE QUALQUER BASE PARA OUTRA PASSANDO PELO DECIMAL ---
        public static string Converter(string numero, int baseOrigem, int baseDestino)
        {
            int dec = Convert.ToInt32(numero, baseOrigem);
            return BaseConverter(dec, baseDestino).ToUpper();
        }

        // --- AUXILIAR: CONVERTE UM INTEIRO DECIMAL PARA A BASE INFORMADA (2-36) ---
        private static string BaseConverter(int numero, int baseDestino)
        {
            if (numero == 0)
            {
                return "0";
            }

            const string digitos = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder resultado = new StringBuilder();
            int num = Math.Abs(numero);
            while (num > 0)
            {
                resultado.Insert(0, digitos[num % baseDestino]);
                num /= baseDestino;
            }
            return (numero < 0 ? "-" : "") + resultado.ToString();
        }

        // --- MENU INTERATIVO DAS CONVERSÕES DE BASE ---
        public static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.WriteLine("      CONVERSOR DE BASES NUMÉRICAS        ");
                Console.WriteLine("  1. Decimal  → Binário                   ");
                Console.WriteLine("  2. Decimal  → Octal                     ");
                Console.WriteLine("  3. Decimal  → Hexadecimal               ");
                Console.WriteLine("  4. Binário  → Decimal                   ");
                Console.WriteLine("  5. Octal    → Decimal                   ");
                Console.WriteLine("  6. Hexadecimal → Decimal                ");
                Console.WriteLine("  7. Conversão livre (base X → base Y)    ");
                Console.WriteLine("  0. Sair                                 ");
                Console.Write("Escolha: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                    {
                        Console.Write("Digite o número decimal: ");
                        int num = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Binário: {DecimalParaBinario(num)}\n");
                        break;
                    }
                    case 2:
                    {
                        Console.Write("Digite o número decimal: ");
                        int num = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Octal: {DecimalParaOctal(num)}\n");
                        break;
                    }
                    case 3:
                    {
                        Console.Write("Digite o número decimal: ");
                        int num = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Hexadecimal: {DecimalParaHexadecimal(num)}\n");
                        break;
                    }
                    case 4:
                    {
                        Console.Write("Digite o número binário: ");
                        string num = Console.ReadLine();
                        Console.WriteLine($"Decimal: {BinarioParaDecimal(num)}\n");
                        break;
                    }
                    case 5:
                    {
                        Console.Write("Digite o número octal: ");
                        string num = Console.ReadLine();
                        Console.WriteLine($"Decimal: {OctalParaDecimal(num)}\n");
                        break;
                    }
                    case 6:
                    {
                        Console.Write("Digite o número hexadecimal: ");
                        string num = Console.ReadLine();
                        Console.WriteLine($"Decimal: {HexadecimalParaDecimal(num)}\n");
                        break;
                    }
                    case 7:
                    {
                        Console.Write("Digite o número: ");
                        string num = Console.ReadLine();
                        Console.Write("Base de origem (2-16): ");
                        int baseOrigem = int.Parse(Console.ReadLine());
                        Console.Write("Base de destino (2-16): ");
                        int baseDestino = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Resultado: {Converter(num, baseOrigem, baseDestino)}\n");
                        break;
                    }
                    case 0:
                        Console.WriteLine("Encerrando...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 0);
        }
    }
}
