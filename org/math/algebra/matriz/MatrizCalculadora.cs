using System;

namespace Math.Algebra.Matriz;

public class MatrizCalculadora
{
    // --- SOMA ELEMENTO A ELEMENTO DAS MATRIZES A E B ---
    public static double[][] Soma(double[][] a, double[][] b)
    {
        int linhas = a.Length;
        int colunas = a[0].Length;
        double[][] resultado = new double[linhas][];
        for (int i = 0; i < linhas; i++)
        {
            resultado[i] = new double[colunas];
            for (int j = 0; j < colunas; j++)
            {
                resultado[i][j] = a[i][j] + b[i][j];
            }
        }
        return resultado;
    }

    // --- SUBTRAI ELEMENTO A ELEMENTO A MATRIZ B DA MATRIZ A ---
    public static double[][] Subtracao(double[][] a, double[][] b)
    {
        int linhas = a.Length;
        int colunas = a[0].Length;
        double[][] resultado = new double[linhas][];
        for (int i = 0; i < linhas; i++)
        {
            resultado[i] = new double[colunas];
            for (int j = 0; j < colunas; j++)
            {
                resultado[i][j] = a[i][j] - b[i][j];
            }
        }
        return resultado;
    }

    // --- MULTIPLICA AS MATRIZES A E B ---
    public static double[][] Multiplicacao(double[][] a, double[][] b)
    {
        int linhasA = a.Length;
        int colunasA = a[0].Length;
        int colunasB = b[0].Length;
        double[][] resultado = new double[linhasA][];
        for (int i = 0; i < linhasA; i++)
        {
            resultado[i] = new double[colunasB];
            for (int j = 0; j < colunasB; j++)
            {
                for (int k = 0; k < colunasA; k++)
                {
                    resultado[i][j] += a[i][k] * b[k][j];
                }
            }
        }
        return resultado;
    }

    // --- TROCA LINHAS POR COLUNAS DA MATRIZ ---
    public static double[][] Transposta(double[][] matriz)
    {
        int linhas = matriz.Length;
        int colunas = matriz[0].Length;
        double[][] resultado = new double[colunas][];
        for (int i = 0; i < colunas; i++)
        {
            resultado[i] = new double[linhas];
        }

        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                resultado[j][i] = matriz[i][j];
            }
        }
        return resultado;
    }

    // --- CALCULA O DETERMINANTE DE MATRIZES ATE 3X3 ---
    public static double Determinante(double[][] matriz)
    {
        int n = matriz.Length;
        if (n == 1)
        {
            return matriz[0][0];
        }

        if (n == 2)
        {
            return matriz[0][0] * matriz[1][1] - matriz[0][1] * matriz[1][0];
        }

        if (n == 3)
        {
            return matriz[0][0] * (matriz[1][1] * matriz[2][2] - matriz[1][2] * matriz[2][1])
                    - matriz[0][1] * (matriz[1][0] * matriz[2][2] - matriz[1][2] * matriz[2][0])
                    + matriz[0][2] * (matriz[1][0] * matriz[2][1] - matriz[1][1] * matriz[2][0]);
        }
        throw new ArgumentException("Determinante suportado apenas para matrizes até 3x3");
    }

    // --- MULTIPLICA TODOS OS ELEMENTOS DA MATRIZ POR UM ESCALAR ---
    public static double[][] MultiplicacaoEscalar(double[][] matriz, double escalar)
    {
        int linhas = matriz.Length;
        int colunas = matriz[0].Length;
        double[][] resultado = new double[linhas][];
        for (int i = 0; i < linhas; i++)
        {
            resultado[i] = new double[colunas];
            for (int j = 0; j < colunas; j++)
            {
                resultado[i][j] = matriz[i][j] * escalar;
            }
        }
        return resultado;
    }

    // --- IMPRIME A MATRIZ FORMATADA NO CONSOLE ---
    public static void Imprimir(double[][] matriz)
    {
        foreach (double[] linha in matriz)
        {
            Console.Write("| ");
            foreach (double val in linha)
            {
                Console.Write($"{val,8:F2} ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine();
    }

    // --- LE OS ELEMENTOS DE UMA MATRIZ A PARTIR DO TECLADO ---
    public static double[][] LerMatriz(int linhas, int colunas)
    {
        double[][] matriz = new double[linhas][];
        for (int i = 0; i < linhas; i++)
        {
            matriz[i] = new double[colunas];
            for (int j = 0; j < colunas; j++)
            {
                Console.Write($"  [{i}][{j}]: ");
                matriz[i][j] = double.Parse(Console.ReadLine());
            }
        }
        return matriz;
    }

    // --- MENU INTERATIVO DAS OPERACOES COM MATRIZES ---
    public static void Main(string[] args)
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
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                case 2:
                case 3:
                {
                    Console.Write("Linhas da matriz A: ");
                    int linhasMatrizA = int.Parse(Console.ReadLine());
                    Console.Write("Colunas da matriz A: ");
                    int colunasMatrizA = int.Parse(Console.ReadLine());

                    Console.WriteLine("Matriz A:");
                    double[][] matrizA = LerMatriz(linhasMatrizA, colunasMatrizA);

                    int linhasMatrizB;
                    int colunasMatrizB;
                    if (opcao == 3)
                    {
                        linhasMatrizB = colunasMatrizA;
                        Console.Write("Colunas da matriz B: ");
                        colunasMatrizB = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        linhasMatrizB = linhasMatrizA;
                        colunasMatrizB = colunasMatrizA;
                    }

                    Console.WriteLine("Matriz B:");
                    double[][] matrizB = LerMatriz(linhasMatrizB, colunasMatrizB);

                    double[][] resultado = opcao switch
                    {
                        1 => Soma(matrizA, matrizB),
                        2 => Subtracao(matrizA, matrizB),
                        3 => Multiplicacao(matrizA, matrizB),
                        _ => null!
                    };

                    Console.WriteLine("Resultado:");
                    Imprimir(resultado);
                    break;
                }
                case 4:
                {
                    Console.Write("Linhas: ");
                    int linha = int.Parse(Console.ReadLine());
                    Console.Write("Colunas: ");
                    int coluna = int.Parse(Console.ReadLine());
                    Console.WriteLine("Matriz:");
                    double[][] matriz = LerMatriz(linha, coluna);
                    Console.WriteLine("Transposta:");
                    Imprimir(Transposta(matriz));
                    break;
                }
                case 5:
                {
                    Console.Write("Tamanho (2 ou 3): ");
                    int n = int.Parse(Console.ReadLine());
                    Console.WriteLine("Matriz:");
                    double[][] m = LerMatriz(n, n);
                    Console.WriteLine($"Determinante: {Determinante(m):F2}\n");
                    break;
                }
                case 6:
                {
                    Console.Write("Linhas: ");
                    int linha = int.Parse(Console.ReadLine());
                    Console.Write("Colunas: ");
                    int coluna = int.Parse(Console.ReadLine());
                    Console.WriteLine("Matriz:");
                    double[][] matriz = LerMatriz(linha, coluna);
                    Console.Write("Escalar: ");
                    double escalar = double.Parse(Console.ReadLine());
                    Console.WriteLine("Resultado:");
                    Imprimir(MultiplicacaoEscalar(matriz, escalar));
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
