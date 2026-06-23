using System;

namespace Math.Algebra
{
    public class EquacaoQuadratica
    {
        public static void Main(string[] args)
        {
            Console.Write("Digite o valor de a: ");
            double a = double.Parse(Console.ReadLine() ?? "0");
            while (Math.Abs(a) < 1e-10)  
            {
                Console.Write("Coeficiente 'a' inválido para equação quadrática. Digite novamente: ");
                a = double.Parse(Console.ReadLine() ?? "0");
            }

            Console.Write("Digite o valor de b: ");
            double b = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Digite o valor de c: ");
            double c = double.Parse(Console.ReadLine() ?? "0");

            double delta = CalcularDelta(a, b, c);
            Console.WriteLine($"Discriminante (Δ) = {delta}");

            double[] raizes = CalcularRaizes(a, b, c, delta);

            ExibirResultado(delta, raizes);
        }

        public static double CalcularDelta(double a, double b, double c)
        {
            return b * b - 4 * a * c;
        }

        public static double[] CalcularRaizes(double a, double b, double c, double delta)
        {
            double[] raizes = new double[4]; 

            if (delta >= 0)
            {
                double raiz1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double raiz2 = (-b - Math.Sqrt(delta)) / (2 * a);
                raizes[0] = raiz1;
                raizes[1] = 0.0;
                raizes[2] = raiz2;
                raizes[3] = 0.0;
            }
            else
            {
                double parteReal = -b / (2 * a);
                double parteImaginaria = Math.Sqrt(-delta) / (2 * a);
                raizes[0] = parteReal;
                raizes[1] = parteImaginaria;
                raizes[2] = parteReal;
                raizes[3] = -parteImaginaria;
            }

            return raizes;
        }

        public static void ExibirResultado(double delta, double[] raizes)
        {
            if (delta > 0)
            {
                Console.WriteLine("A equação possui duas raízes reais e distintas:");
                Console.WriteLine($"x₁ = {raizes[0]}");
                Console.WriteLine($"x₂ = {raizes[2]}");
            }
            else if (Math.Abs(delta) < 1e-10)  
            {
                Console.WriteLine("A equação possui uma raiz real (dupla):");
                Console.WriteLine($"x = {raizes[0]}");
            }
            else
            {
                Console.WriteLine("A equação possui duas raízes complexas conjugadas:");
                Console.WriteLine($"x₁ = {raizes[0]:F2} + {raizes[1]:F2}i");
                Console.WriteLine($"x₂ = {raizes[2]:F2} - {raizes[3]:F2}i");
            }
        }
    }
}
