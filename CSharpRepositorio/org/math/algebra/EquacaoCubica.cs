using System;
using System.Collections.Generic;

namespace Math.Algebra
{
    public class EquacaoCubica
    {
        private const double EPSILON = 1e-10;

        public static void Main(string[] args)
        {
            Console.WriteLine("Resolução de equação cúbica: a*x³ + b*x² + c*x + d = 0");
            Console.Write("Digite o coeficiente a (diferente de zero): ");
            double a = double.Parse(Console.ReadLine() ?? "0");

            if (Math.Abs(a) < EPSILON)
            {
                Console.WriteLine("O coeficiente 'a' não pode ser zero (não é uma equação cúbica).");
                Console.WriteLine("Tratando como equação de grau inferior...");
                ResolverGrauInferior();
                return;
            }

            Console.Write("Digite o coeficiente b: ");
            double b = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Digite o coeficiente c: ");
            double c = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Digite o coeficiente d: ");
            double d = double.Parse(Console.ReadLine() ?? "0");

            double[] raizesReais = CalcularRaizesReais(a, b, c, d);
            ExibirRaizes(raizesReais);
        }

        private static void ResolverGrauInferior()
        {
            Console.Write("Digite o coeficiente b: ");
            double b = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Digite o coeficiente c: ");
            double c = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Digite o coeficiente d: ");
            double d = double.Parse(Console.ReadLine() ?? "0");

            if (Math.Abs(b) < EPSILON && Math.Abs(c) < EPSILON)
            {
                if (Math.Abs(d) < EPSILON)
                {
                    Console.WriteLine("A equação é identicamente nula (infinitas soluções).");
                }
                else
                {
                    Console.WriteLine("Equação impossível (sem solução real).");
                }
            }
            else if (Math.Abs(b) < EPSILON)
            {
                double raiz = -d / c;
                Console.WriteLine($"Raiz real única: {raiz:F6}");
            }
            else
            {
                double[] raizes = ResolverQuadratica(b, c, d);
                ExibirRaizes(raizes);
            }
        }

        private static double[] ResolverQuadratica(double b, double c, double d)
        {
            double delta = c * c - 4 * b * d;
            if (delta < -EPSILON)
            {
                return new double[0]; 
            }

            if (Math.Abs(delta) < EPSILON)
            {
                double raiz = -c / (2 * b);
                return new double[] { raiz };
            }

            double sqrtDelta = Math.Sqrt(delta);
            double raiz1 = (-c + sqrtDelta) / (2 * b);
            double raiz2 = (-c - sqrtDelta) / (2 * b);
            return new double[] { raiz1, raiz2 };
        }

        public static double[] CalcularRaizesReais(double a, double b, double c, double d)
        {
            double a2 = b / a;
            double a1 = c / a;
            double a0 = d / a;

            double p = a1 - (a2 * a2) / 3.0;
            double q = (2.0 * a2 * a2 * a2) / 27.0 - (a2 * a1) / 3.0 + a0;

            double discriminante = (q / 2.0) * (q / 2.0) + (p / 3.0) * (p / 3.0) * (p / 3.0);

            List<double> raizes = new List<double>();
            double deslocamento = -a2 / 3.0;

            if (discriminante > EPSILON)
            {
                double sqrtDelta = Math.Sqrt(discriminante);
                double u = Cbrt(-q / 2.0 + sqrtDelta);
                double v = Cbrt(-q / 2.0 - sqrtDelta);
                double raizReal = u + v + deslocamento;
                raizes.Add(raizReal);
            }
            else if (Math.Abs(discriminante) < EPSILON)
            {
                double u = Cbrt(-q / 2.0);
                if (Math.Abs(p) < EPSILON && Math.Abs(q) < EPSILON)
                {
                    double raizTripla = deslocamento;
                    raizes.Add(raizTripla);
                }
                else
                {
                    double raiz1 = 2.0 * u + deslocamento;
                    double raiz2 = -u + deslocamento;
                    raizes.Add(raiz1);
                    if (Math.Abs(raiz1 - raiz2) > EPSILON)
                    {
                        raizes.Add(raiz2);
                    }
                }
            }
            else
            {
                double r = 2.0 * Math.Sqrt(-p / 3.0);
                double theta = Math.Acos((3.0 * q) / (2.0 * p) * Math.Sqrt(-3.0 / p));
                double raiz1 = r * Math.Cos(theta / 3.0) + deslocamento;
                double raiz2 = r * Math.Cos((theta + 2.0 * Math.PI) / 3.0) + deslocamento;
                double raiz3 = r * Math.Cos((theta + 4.0 * Math.PI) / 3.0) + deslocamento;
                raizes.Add(raiz1);
                raizes.Add(raiz2);
                raizes.Add(raiz3);
            }

            return raizes.ToArray();
        }

        private static double Cbrt(double x)
        {
            if (x < 0)
            {
                return -Math.Pow(-x, 1.0 / 3.0);
            }
            return Math.Pow(x, 1.0 / 3.0);
        }

        private static void ExibirRaizes(double[] raizes)
        {
            if (raizes.Length == 0)
            {
                Console.WriteLine("A equação não possui raízes reais.");
            }
            else
            {
                Console.WriteLine("\nRaízes reais encontradas:");
                for (int i = 0; i < raizes.Length; i++)
                {
                    Console.WriteLine($"x{i + 1} = {raizes[i]:F6}");
                }
            }
        }
    }
}