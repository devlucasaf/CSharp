using System;

namespace CSharpRepositorio.org.math
{
    public class MultiplicacaoTable
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Digite um número: ");
            int numero;
            if (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.WriteLine("Valor inválido!");
                numero = 0;
            }

            for (int o = 1; o <= 10; o++)
            {
                int soma = numero + o;
                Console.WriteLine($"{numero} + {o} = {soma}");
            }

            Console.Write("\n+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=\n");

            for (int a = 1; a <= 10; a++)
            {
                int subtracao = numero - a;
                Console.WriteLine($"{numero} - {a} = {subtracao}");
            }

            Console.Write("+=+=" + 25);

            for (int i = 1; i <= 10; i++)
            {
                int multiplicacao = numero * i;
                Console.WriteLine($"{numero} x {i} = {multiplicacao}");
            }

            Console.Write("\n+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=\n");

            for (int u = 1; u <= 10; u++)
            {
                int divisao = numero / u;
                Console.WriteLine($"\n{divisao} / {numero} = {u}");
            }

            Console.Write("\n+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=\n");
        }
    }
}