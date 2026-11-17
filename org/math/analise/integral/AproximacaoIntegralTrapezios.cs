using System;
using System.Collections.Generic;

namespace Math.Analise.Integral
{
    public class AproximacaoIntegralTrapezios
    {
        // --- AVALIA A EXPRESSÃO PARA UM VALOR DE X ---
        public static double AvaliarFuncao(string expressao, double x)
        {
            var parser = new ParserExpressao(expressao, x);
            return parser.Avaliar();
        }

        // --- LE A FUNÇÃO, OS LIMITES E APROXIMA A INTEGRAL POR TRAPÉZIOS ---
        public static void Main(string[] args)
        {
            Console.WriteLine("=== APROXIMAÇÃO DE INTEGRAL DEFINIDA - MÉTODO DOS TRAPÉZIOS ===\n");

            Console.Write("Digite a função polinomial (use 'x' como variável): ");
            Console.WriteLine("Exemplos: 2*x^2 + 3*x - 5   ou   3*x^3 - 2*x + 1");
            Console.Write("Função: ");
            string funcao = Console.ReadLine();

            Console.Write("\nLimite inferior (a): ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Limite superior (b): ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Número de subdivisões (n): ");
            int n = int.Parse(Console.ReadLine());

            if (n <= 0)
            {
                Console.WriteLine("ERRO: O número de subdivisões deve ser positivo.");
                return;
            }

            double h = (b - a) / n;
            double soma = 0.0;

            try
            {
                double fa = AvaliarFuncao(funcao, a);
                double fb = AvaliarFuncao(funcao, b);
                soma = fa + fb;

                for (int i = 1; i <= n - 1; i++)
                {
                    double xi = a + i * h;
                    double fxi = AvaliarFuncao(funcao, xi);
                    soma += 2 * fxi;
                }

                double resultado = (h / 2.0) * soma;

                Console.WriteLine("\nRESULTADO:");
                Console.WriteLine($"Integral aproximada de {a:F6} até {b:F6} = {resultado:F8}");
                Console.WriteLine($"(com {n} subdivisão(ões), passo h = {h:F6})");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nERRO ao avaliar a função:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Verifique a sintaxe da expressão. Use '^' para potência (ex: x^2)");
                Console.WriteLine("e apenas as operações +, -, *, / e parênteses.");
            }
        }
    }

    /// <summary>
    /// Parser simples para expressões polinomiais com +, -, *, /, ^, parênteses, números e 'x'.
    /// </summary>
    internal class ParserExpressao
    {
        private readonly string texto;
        private readonly double valorX;
        private int pos;

        public ParserExpressao(string texto, double valorX)
        {
            this.texto = texto.Replace(" ", "");
            this.valorX = valorX;
            this.pos = 0;
        }

        public double Avaliar()
        {
            double resultado = ParseExpressao();
            if (pos < texto.Length)
                throw new FormatException($"Caractere inesperado: '{texto[pos]}' na posição {pos}.");
            return resultado;
        }

        // expressao := termo (('+' | '-') termo)*
        private double ParseExpressao()
        {
            double valor = ParseTermo();
            while (pos < texto.Length && (texto[pos] == '+' || texto[pos] == '-'))
            {
                char op = texto[pos++];
                double proximo = ParseTermo();
                valor = op == '+' ? valor + proximo : valor - proximo;
            }
            return valor;
        }

        // termo := fator (('*' | '/') fator)*
        private double ParseTermo()
        {
            double valor = ParseFator();
            while (pos < texto.Length && (texto[pos] == '*' || texto[pos] == '/'))
            {
                char op = texto[pos++];
                double proximo = ParseFator();
                if (op == '/' && proximo == 0)
                    throw new DivideByZeroException("Divisão por zero na expressão.");
                valor = op == '*' ? valor * proximo : valor / proximo;
            }
            return valor;
        }

        // fator := base ('^' fator)?   (associativo à direita)
        private double ParseFator()
        {
            double baseVal = ParseBase();
            if (pos < texto.Length && texto[pos] == '^')
            {
                pos++;
                double expoente = ParseFator();
                return Math.Pow(baseVal, expoente);
            }
            return baseVal;
        }

        // base := numero | 'x' | '(' expressao ')' | '-' base
        private double ParseBase()
        {
            if (pos >= texto.Length)
                throw new FormatException("Expressão inacabada.");

            char c = texto[pos];

            if (c == '(')
            {
                pos++;
                double valor = ParseExpressao();
                if (pos >= texto.Length || texto[pos] != ')')
                    throw new FormatException("Parêntese fechando esperado.");
                pos++;
                return valor;
            }

            if (c == '-')
            {
                pos++;
                return -ParseBase();
            }

            if (c == '+')
            {
                pos++;
                return ParseBase();
            }

            if (c == 'x' || c == 'X')
            {
                pos++;
                return valorX;
            }

            if (char.IsDigit(c) || c == '.')
            {
                int inicio = pos;
                while (pos < texto.Length && (char.IsDigit(texto[pos]) || texto[pos] == '.'))
                    pos++;
                string numero = texto.Substring(inicio, pos - inicio);
                return double.Parse(numero, System.Globalization.CultureInfo.InvariantCulture);
            }

            throw new FormatException($"Caractere inesperado: '{c}' na posição {pos}.");
        }
    }
}
