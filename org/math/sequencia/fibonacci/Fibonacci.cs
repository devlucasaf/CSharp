using System;

namespace Math.Sequencia.Fibonacci
{
    public class Fibonacci
    {
        // --- LE A QUANTIDADE DE TERMOS E EXIBE A SEQUÊNCIA DE FIBONACCI ---
        public static void Main(string[] args)
        {
            Console.Write("Digite a quantidade de termos da sequência de Fibonacci (N): ");
            int numeroTermos = int.Parse(Console.ReadLine());

            if (numeroTermos <= 0)
            {
                Console.WriteLine("Por favor, digite um número inteiro positivo.");
            }
            else
            {
                Console.WriteLine("\n--- Sequência de Fibonacci (Abordagem Iterativa) ---");
                ExibirFibonacciIterativo(numeroTermos);

                Console.WriteLine("\n--- Sequência de Fibonacci (Abordagem Recursiva) ---");
                ExibirFibonacciRecursivo(numeroTermos);
            }
        }

        // --- EXIBE A SEQUÊNCIA DE FIBONACCI UTILIZANDO UMA ABORDAGEM ITERATIVA ---
        public static void ExibirFibonacciIterativo(int quantidade)
        {
            long termoAtual = 0;
            long proximoTermo = 1;

            for (int contador = 1; contador <= quantidade; contador++)
            {
                Console.Write(termoAtual + " ");
                long soma = termoAtual + proximoTermo;
                termoAtual = proximoTermo;
                proximoTermo = soma;
            }
            Console.WriteLine();
        }

        // --- EXIBE A SEQUÊNCIA DE FIBONACCI UTILIZANDO UMA ABORDAGEM RECURSIVA ---
        public static void ExibirFibonacciRecursivo(int quantidade)
        {
            for (int posicao = 1; posicao <= quantidade; posicao++)
            {
                long valor = CalcularFibonacciRecursivo(posicao - 1);
                Console.Write(valor + " ");
            }
            Console.WriteLine();
        }

        // --- CALCULA RECURSIVAMENTE O VALOR DE UM TERMO DA SEQUÊNCIA ---
        public static long CalcularFibonacciRecursivo(int indice)
        {
            if (indice == 0)
            {
                return 0;
            }

            if (indice == 1)
            {
                return 1;
            }
            return CalcularFibonacciRecursivo(indice - 1) + CalcularFibonacciRecursivo(indice - 2);
        }
    }
}
