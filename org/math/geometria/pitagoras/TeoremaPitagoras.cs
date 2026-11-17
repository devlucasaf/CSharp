using System;

namespace Math.Geometria.Pitagoras;

public class TeoremaPitagoras
{
    // --- CALCULA A HIPOTENUSA A PARTIR DOS DOIS CATETOS ---
    public static double CalcularHipotenusa(double catetoA, double catetoB)
    {
        return Math.Sqrt(Math.Pow(catetoA, 2) + Math.Pow(catetoB, 2));
    }

    // --- CALCULA O CATETO DESCONHECIDO A PARTIR DA HIPOTENUSA ---
    public static double CalcularCateto(double hipotenusa, double catetoConhecido)
    {
        if (hipotenusa <= catetoConhecido)
        {
            throw new ArgumentException("A hipotenusa deve ser maior que o cateto conhecido.");
        }
        return Math.Sqrt(Math.Pow(hipotenusa, 2) - Math.Pow(catetoConhecido, 2));
    }

    // --- MENU INTERATIVO PARA CÁLCULOS DO TEOREMA DE PITÁGORAS ---
    public static void Main(string[] args)
    {
        int opcao;

        Console.WriteLine("=== TEOREMA DE PITÁGORAS ===");

        do
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Calcular a hipotenusa");
            Console.WriteLine("2 - Calcular um cateto (dado o outro cateto e a hipotenusa)");
            Console.WriteLine("3 - Sair");
            Console.Write("Opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Digite o valor do primeiro cateto: ");
                    double catetoOposto = double.Parse(Console.ReadLine());
                    Console.Write("Digite o valor do segundo cateto: ");
                    double catetoAdjacente = double.Parse(Console.ReadLine());

                    if (catetoOposto <= 0 || catetoAdjacente <= 0)
                    {
                        Console.WriteLine("Os catetos devem ser valores positivos.");
                    }
                    else
                    {
                        double hipotenusa = CalcularHipotenusa(catetoOposto, catetoAdjacente);
                        Console.WriteLine($"A hipotenusa é: {hipotenusa:F2}");
                    }
                    break;
                case 2:
                    Console.Write("Digite o valor da hipotenusa: ");
                    double hipotenusa = double.Parse(Console.ReadLine());
                    Console.Write("Digite o valor do cateto conhecido: ");
                    double catetoConhecido = double.Parse(Console.ReadLine());

                    if (hipotenusa <= 0 || catetoConhecido <= 0)
                    {
                        Console.WriteLine("Os valores devem ser positivos.");
                    }
                    else
                    {
                        try
                        {
                            double catetoDesconhecido = CalcularCateto(hipotenusa, catetoConhecido);
                            Console.WriteLine($"O outro cateto mede: {catetoDesconhecido:F2}");
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Erro: " + e.Message);
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Encerrando o programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida! Digite 1, 2 ou 3.");
                    break;
            }

        } while (opcao != 3);
    }
}
