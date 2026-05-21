namespace CSharpRepositorio.org.math;

public class MatrizCalculadora
{
    public static double[,] Soma(double[,] a, double[,] b)
    {
        int linhas = a.GetLength(0), colunas = a.GetLength(1);
        double[,] resultado = new double[linhas, colunas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                resultado[i, j] = a[i, j] + b[i, j];
            }
        }
        return resultado;
    }

    public static double[,] Subtracao(double[,] a, double[,] b)
    {
        int linhas = a.GetLength(0), colunas = a.GetLength(1);
        double[,] resultado = new double[linhas, colunas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                resultado[i, j] = a[i, j] - b[i, j];
            }
        }
        return resultado;
    }

    public static double[,] Multiplicacao(double[,] a, double[,] b)
    {
        int linhasA = a.GetLength(0), colunasA = a.GetLength(1), colunasB = b.GetLength(1);
        double[,] resultado = new double[linhasA, colunasB];
        for (int i = 0; i < linhasA; i++)
        {
            for (int j = 0; j < colunasB; j++)
            {
                for (int k = 0; k < colunasA; k++)
                {
                    resultado[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return resultado;
    }

    public static double[,] Transposta(double[,] matriz)
    {
        int linhas = matriz.GetLength(0), colunas = matriz.GetLength(1);
        double[,] resultado = new double[colunas, linhas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                resultado[j, i] = matriz[i, j];
            }
        }
        return resultado;
    }

    public static double Determinante(double[,] matriz)
    {
        int n = matriz.GetLength(0);
        if (n == 1)
        {
            return matriz[0, 0];
        }

        if (n == 2)
        {
            return matriz[0, 0] * matriz[1, 1] - matriz[0, 1] * matriz[1, 0];
        }

        if (n == 3)
        {
            return matriz[0, 0] * (matriz[1, 1] * matriz[2, 2] - matriz[1, 2] * matriz[2, 1])
                 - matriz[0, 1] * (matriz[1, 0] * matriz[2, 2] - matriz[1, 2] * matriz[2, 0])
                 + matriz[0, 2] * (matriz[1, 0] * matriz[2, 1] - matriz[1, 1] * matriz[2, 0]);
        }
        throw new ArgumentException("Determinante suportado apenas para matrizes até 3x3");
    }

    public static double[,] MultiplicacaoEscalar(double[,] matriz, double escalar)
    {
        int linhas = matriz.GetLength(0), colunas = matriz.GetLength(1);
        double[,] resultado = new double[linhas, colunas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                resultado[i, j] = matriz[i, j] * escalar;
            }
        }
        return resultado;
    }

    public static void Imprimir(double[,] matriz)
    {
        int linhas = matriz.GetLength(0), colunas = matriz.GetLength(1);
        for (int i = 0; i < linhas; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < colunas; j++)
            {
                Console.Write($"{matriz[i, j],8:0.00} ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine();
    }

    public static double[,] LerMatriz(int linhas, int colunas)
    {
        double[,] matriz = new double[linhas, colunas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                Console.Write($"  [{i}][{j}]: ");
                matriz[i, j] = double.Parse(Console.ReadLine() ?? "0",
                    System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        return matriz;
    }

    public static void Run()
    {
        int opcao;

        do
        {
            Console.WriteLine("      CALCULADORA DE MATRIZES         ");
            Console.WriteLine(" 1. Soma                              ");
            Console.WriteLine(" 2. Subtração                         ");
            Console.WriteLine(" 3. Multiplicação                     ");
            Console.WriteLine(" 4. Transposta                        ");
            Console.WriteLine(" 5. Determinante (2x2 ou 3x3)         ");
            Console.WriteLine(" 6. Multiplicação por escalar         ");
            Console.WriteLine(" 0. Sair                              ");
            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine() ?? "0");

            switch (opcao)
            {
                case 1:
                case 2:
                case 3:
                {
                    Console.Write("Linhas da matriz A: ");
                    int linhasA = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Colunas da matriz A: ");
                    int colunasA = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Matriz A:");
                    double[,] a = LerMatriz(linhasA, colunasA);

                    int linhasB, colunasB;
                    if (opcao == 3)
                    {
                        linhasB = colunasA;
                        Console.Write("Colunas da matriz B: ");
                        colunasB = int.Parse(Console.ReadLine() ?? "0");
                    }
                    else
                    {
                        linhasB = linhasA;
                        colunasB = colunasA;
                    }

                    Console.WriteLine("Matriz B:");
                    double[,] b = LerMatriz(linhasB, colunasB);

                    double[,]? resultado = opcao switch
                    {
                        1 => Soma(a, b),
                        2 => Subtracao(a, b),
                        3 => Multiplicacao(a, b),
                        _ => null
                    };

                    Console.WriteLine("Resultado:");
                    if (resultado != null) Imprimir(resultado);
                    break;
                }
                case 4:
                {
                    Console.Write("Linhas: ");
                    int l = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Colunas: ");
                    int c = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Matriz:");
                    double[,] m = LerMatriz(l, c);
                    Console.WriteLine("Transposta:");
                    Imprimir(Transposta(m));
                    break;
                }
                case 5:
                {
                    Console.Write("Tamanho (2 ou 3): ");
                    int n = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Matriz:");
                    double[,] m = LerMatriz(n, n);
                    Console.WriteLine($"Determinante: {Determinante(m):0.00}\n");
                    break;
                }
                case 6:
                {
                    Console.Write("Linhas: ");
                    int l = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Colunas: ");
                    int c = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine("Matriz:");
                    double[,] m = LerMatriz(l, c);
                    Console.Write("Escalar: ");
                    double escalar = double.Parse(Console.ReadLine() ?? "0",
                        System.Globalization.CultureInfo.InvariantCulture);
                    Console.WriteLine("Resultado:");
                    Imprimir(MultiplicacaoEscalar(m, escalar));
                    break;
                }
                case 0:
                    Console.WriteLine("Encerrando...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (opcao != 0);
    }
}