using System;

namespace Math
{
    public class Logaritmo
    {
        public static double CalcularLogaritmo(double numero, double baseValor)
        {
            if (numero <= 0)
            {
                throw new ArgumentException("O número deve ser maior que zero.");
            }

            if (baseValor <= 0)
            {
                throw new ArgumentException("A base deve ser maior que zero.");
            }

            if (Math.Abs(baseValor - 1.0) < 1e-10)
            {
                throw new ArgumentException("A base não pode ser igual a 1.");
            }

            return Math.Log(numero) / Math.Log(baseValor);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("=== CALCULADORA DE LOGARITMOS ===");

            try
            {
                Console.Write("Digite o número (positivo): ");
                double numero = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Digite a base (positiva e diferente de 1): ");
                double baseValor = double.Parse(Console.ReadLine() ?? "0");

                double resultado = CalcularLogaritmo(numero, baseValor);

                Console.WriteLine("\nRESULTADO:");
                Console.WriteLine($"log_{baseValor}({numero}) = {resultado:F4}");

            }
            catch (ArgumentException erro)
            {
                Console.Error.WriteLine($"Erro: {erro.Message}");
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Erro: Entrada inválida. Certifique-se de digitar números válidos.");
            }
        }
    }
}