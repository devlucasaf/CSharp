using System;

namespace Math.Aritmetica.Primo
{
    public class VerificadorPrimo
    {
        public static void Main(string[] args)
        {
            Console.Write("Digite um número inteiro: ");
            int numero = int.Parse(Console.ReadLine());

            if (EhPrimo(numero))
            {
                Console.WriteLine(numero + " é um número primo.");
            }
            else
            {
                Console.WriteLine(numero + " não é um número primo.");
            }
        }

        public static bool EhPrimo(int num)
        {
            if (num <= 1)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
