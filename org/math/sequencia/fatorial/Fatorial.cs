using System;
using System.Numerics;

namespace Math.Sequencia.Fatorial
{
    public class Fatorial
    {
        // --- LE UM NÚMERO E CALCULA SEU FATORIAL DE FORMA ITERATIVA E RECURSIVA ---
        public static void Main(string[] args)
        {
            Console.Write("Digite um número inteiro não negativo: ");
            int numero = int.Parse(Console.ReadLine());

            if (numero < 0)
            {
                Console.WriteLine("Erro: O fatorial não está definido para números negativos.");
            }
            else
            {
                BigInteger numeroBig = new BigInteger(numero);
                BigInteger fatorialIterativo = CalcularFatorialIterativo(numeroBig);
                Console.WriteLine("Resultado iterativo: " + numero + "! = " + fatorialIterativo);

                BigInteger fatorialRecursivo = CalcularFatorialRecursivo(numeroBig);
                Console.WriteLine("Resultado recursivo: " + numero + "! = " + fatorialRecursivo);
            }
        }

        // --- CALCULA O FATORIAL UTILIZANDO UMA ABORDAGEM ITERATIVA ---
        public static BigInteger CalcularFatorialIterativo(BigInteger n)
        {
            BigInteger resultado = BigInteger.One;

            for (BigInteger i = new BigInteger(2); i.CompareTo(n) <= 0; i = i + BigInteger.One)
            {
                resultado = resultado * i;
            }
            return resultado;
        }

        // --- CALCULA O FATORIAL UTILIZANDO UMA ABORDAGEM RECURSIVA ---
        public static BigInteger CalcularFatorialRecursivo(BigInteger n)
        {
            if (n.CompareTo(BigInteger.One) <= 0)
            {
                return BigInteger.One;
            }
            return n * CalcularFatorialRecursivo(n - BigInteger.One);
        }
    }
}
