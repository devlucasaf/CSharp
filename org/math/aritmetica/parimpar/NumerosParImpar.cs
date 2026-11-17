using System;

namespace Math.Aritmetica.ParImpar
{
    public class NumerosParImpar
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Números de 1 a 10:");
            for (int i = 1; i <= 10; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Números pares de 1 a 50:\n");
            int number = 1;
            while (number <= 50)
            {
                if (number % 2 == 0)
                {
                    Console.Write(number + " ");
                }
                number++;
            }

            Console.WriteLine("Números pares de 1 a 50:\n");
            int impar = 1;
            while (impar <= 50)
            {
                if (impar % 2 != 0)
                {
                    Console.Write(impar + " ");
                }
                impar++;
            }
        }
    }
}
