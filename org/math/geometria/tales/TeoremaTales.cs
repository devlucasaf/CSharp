using System;

namespace Math.Geometria.Tales;

public class TeoremaTales
{
    public static double CalcularSegmentoDesconhecido(char segmentoDesconhecido, double a, double b, double c, double d)
    {
        switch (char.ToLower(segmentoDesconhecido))
        {
            case 'a':
                ValidarDivisor(b, "b");
                return (b * c) / d;
            case 'b':
                ValidarDivisor(c, "c");
                return (a * d) / c;
            case 'c':
                ValidarDivisor(b, "b");
                return (a * d) / b;
            case 'd':
                ValidarDivisor(a, "a");
                return (b * c) / a;
            default:
                throw new ArgumentException("Segmento desconhecido inválido. Use apenas: a, b, c ou d.");
        }
    }

    private static void ValidarDivisor(double valor, string nomeSegmento)
    {
        if (valor == 0)
        {
            throw new ArgumentException(
                $"O segmento '{nomeSegmento}' não pode ser zero para evitar divisão por zero.");
        }
    }

    private static double LerSegmentoPositivo(string nomeSegmento)
    {
        double valor;
        do
        {
            Console.Write("Digite o valor de " + nomeSegmento + ": ");
            valor = double.Parse(Console.ReadLine());
            if (valor <= 0)
            {
                Console.WriteLine("Valor inválido. Informe um número maior que zero.");
            }
        } while (valor <= 0);
        return valor;
    }

    private static void ExibirResultado(double a, double b, double c, double d, char desconhecido)
    {
        char segmento = char.ToLower(desconhecido);
        double valorDesconhecido = segmento == 'a' ? a
                                    : segmento == 'b' ? b
                                    : segmento == 'c' ? c
                                    : d;

        Console.WriteLine("\n=== RESULTADO ===");
        Console.WriteLine("Proporção aplicada: a/b = c/d");
        Console.WriteLine($"Substituindo os valores: {a:F2}/{b:F2} = {c:F2}/{d:F2}");
        Console.WriteLine($"Segmento desconhecido ({segmento}) = {valorDesconhecido:F2}");
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("=== TEOREMA DE TALES ===");
        Console.WriteLine("Relação usada: a/b = c/d");
        Console.WriteLine("Informe qual segmento é desconhecido (a, b, c ou d): ");

        char segmentoDesconhecido = char.ToLower(Console.ReadLine()[0]);
        double a = 0;
        double b = 0;
        double c = 0;
        double d = 0;

        switch (segmentoDesconhecido)
        {
            case 'a':
                b = LerSegmentoPositivo("b");
                c = LerSegmentoPositivo("c");
                d = LerSegmentoPositivo("d");
                a = CalcularSegmentoDesconhecido('a', a, b, c, d);
                break;
            case 'b':
                a = LerSegmentoPositivo("a");
                c = LerSegmentoPositivo("c");
                d = LerSegmentoPositivo("d");
                b = CalcularSegmentoDesconhecido('b', a, b, c, d);
                break;
            case 'c':
                a = LerSegmentoPositivo("a");
                b = LerSegmentoPositivo("b");
                d = LerSegmentoPositivo("d");
                c = CalcularSegmentoDesconhecido('c', a, b, c, d);
                break;
            case 'd':
                a = LerSegmentoPositivo("a");
                b = LerSegmentoPositivo("b");
                c = LerSegmentoPositivo("c");
                d = CalcularSegmentoDesconhecido('d', a, b, c, d);
                break;
            default:
                Console.WriteLine("Opção inválida. Encerrando o programa.");
                return;
        }

        ExibirResultado(a, b, c, d, segmentoDesconhecido);
    }
}

