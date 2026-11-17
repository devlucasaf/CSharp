using System;

namespace Application.System.Dinheiro.CaixaEletronico;

public class CaixaEletronico
{
    public static void Main(string[] args)
    {
        int[] notas = { 200, 100, 50, 20, 10, 5, 2 };
        int[] qntdNotaEstoque = { 50, 50, 50, 50, 50, 50, 50 };
        decimal valorTotal = 0m;

        for (int i = 0; i < notas.Length; i++)
        {
            valorTotal += notas[i] * qntdNotaEstoque[i];
        }

        Console.WriteLine($"Nesse caixa, tem disponível: R$ {valorTotal:F2}");

        while (true)
        {
            Console.Write("Digite quanto você quer sacar: ");
            decimal saque = decimal.Parse(Console.ReadLine());

            if (saque > valorTotal)
            {
                Console.WriteLine("Saque indisponível! Valor maior do que disponível!");
                Console.WriteLine($"O valor máximo permitido é: R$ {valorTotal:F2}");
                continue;
            }

            decimal resto = saque;

            foreach (int nota in notas)
            {
                if (resto / nota >= 1)
                {
                    int qntdNecessaria = (int)(resto / nota);
                    resto -= qntdNecessaria * nota;
                    Console.WriteLine($"A quantidade de notas de R$ {nota} é de: {qntdNecessaria} notas");
                }
            }

            valorTotal -= saque;
            Console.WriteLine($"Neste caixa tem R$ {valorTotal:F2} em notas de [{string.Join(", ", notas)}]");

            if (valorTotal <= 0)
            {
                Console.WriteLine("Você conseguiu estourar o caixa");
                break;
            }

            Console.Write("Deseja realizar um novo saque? Sim/Não: ");
            string newSaque = Console.ReadLine().Trim().ToUpper();

            if (newSaque == "NAO" || newSaque == "NÃO")
            {
                break;
            }
        }
    }
}
