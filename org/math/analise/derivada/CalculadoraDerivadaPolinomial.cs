using System;
using System.Collections.Generic;
using System.Text;

namespace Math.Analise.Derivada
{
    public class CalculadoraDerivadaPolinomial
    {
        public static void Main(string[] argumentos)
        {
            Console.WriteLine("--- CÁLCULO DA DERIVADA DE UM POLINÔMIO ---");

            Console.Write("Digite o grau do polinômio: ");
            int grau = int.Parse(Console.ReadLine());

            List<double> coeficientes = new List<double>(grau + 1);

            // --- LE OS COEFICIENTES DO MAIOR EXPOENTE PARA O TERMO INDEPENDENTE ---
            for (int i = grau; i >= 0; i--)
            {
                Console.Write("Digite o coeficiente para x^" + i + ": ");
                double coeficiente = double.Parse(Console.ReadLine());
                coeficientes.Add(coeficiente);
            }

            Console.WriteLine("\nPolinômio original:");
            Console.WriteLine(FormatarPolinomio(coeficientes, grau));

            List<double> coeficientesDerivada = CalcularDerivada(coeficientes, grau);
            int novoGrau = grau - 1;

            Console.WriteLine("\nDerivada do polinômio:");
            if (novoGrau < 0)
            {
                Console.WriteLine("0");
            }
            else
            {
                Console.WriteLine(FormatarPolinomio(coeficientesDerivada, novoGrau));
            }
        }

        // --- APLICA A REGRA DA POTÊNCIA ---
        public static List<double> CalcularDerivada(List<double> coeficientes, int grau)
        {
            List<double> derivada = new List<double>();
            for (int i = 0; i <= grau; i++)
            {
                double coefOriginal = coeficientes[i];
                int expoente = grau - i;
                if (expoente > 0)
                {
                    double novoCoeficiente = expoente * coefOriginal;
                    derivada.Add(novoCoeficiente);
                }
            }
            return derivada;
        }

        // --- MONTA A STRING DO POLINÔMIO PARA EXIBIÇÃO ---
        public static string FormatarPolinomio(List<double> coeficientes, int grau)
        {
            StringBuilder polinomioStr = new StringBuilder();
            bool primeiroTermo = true;

            for (int i = 0; i <= grau; i++)
            {
                double coeficiente = coeficientes[i];
                int expoente = grau - i;

                if (Math.Abs(coeficiente) < 1e-10)
                {
                    continue;
                }

                if (!primeiroTermo)
                {
                    polinomioStr.Append(coeficiente > 0 ? " + " : " - ");
                }
                else if (coeficiente < 0)
                {
                    polinomioStr.Append("-");
                }

                double valorAbsoluto = Math.Abs(coeficiente);

                if (expoente == 0)
                {
                    polinomioStr.Append(FormatarNumero(valorAbsoluto));
                }
                else if (expoente == 1)
                {
                    if (Math.Abs(valorAbsoluto - 1.0) < 1e-10)
                    {
                        polinomioStr.Append("x");
                    }
                    else
                    {
                        polinomioStr.Append(FormatarNumero(valorAbsoluto)).Append("x");
                    }
                }
                else
                {
                    if (Math.Abs(valorAbsoluto - 1.0) < 1e-10)
                    {
                        polinomioStr.Append("x^").Append(expoente);
                    }
                    else
                    {
                        polinomioStr.Append(FormatarNumero(valorAbsoluto)).Append("x^").Append(expoente);
                    }
                }

                primeiroTermo = false;
            }

            if (primeiroTermo)
            {
                return "0";
            }
            return polinomioStr.ToString();
        }

        // --- REMOVE DECIMAIS DESNECESSÁRIOS EM NÚMEROS INTEIROS ---
        public static string FormatarNumero(double valor)
        {
            if (valor == (long)valor)
            {
                return ((long)valor).ToString();
            }
            else
            {
                return valor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
