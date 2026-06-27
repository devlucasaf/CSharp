using System;

namespace games.velha
{
    public static class JogoDaVelha
    {
        private static char[,] tabuleiro = new char[3, 3];
        private static readonly Random random = new Random();

        public static void Main(string[] args)
        {
            bool jogarNovamente;

            do
            {
                IniciarTabuleiro();
                Console.WriteLine("    JOGO DA VELHA    ");
                Console.WriteLine("1 - Jogador vs Jogador");
                Console.WriteLine("2 - Jogador vs Máquina");
                Console.Write("Escolha o modo de jogo: ");
                int modo = int.Parse(Console.ReadLine() ?? "1");

                Jogar(modo);

                Console.Write("\nDeseja jogar novamente? (s/n): ");
                string resposta = Console.ReadLine() ?? "";
                jogarNovamente = resposta.Equals("s", StringComparison.OrdinalIgnoreCase);

            } 
            while (jogarNovamente);

            Console.WriteLine("Obrigado por jogar!");
        }

        private static void Jogar(int modo)
        {
            char jogadorAtual = 'X';
            bool jogoAtivo = true;

            while (jogoAtivo)
            {
                ExibirTabuleiro();

                if (modo == 2 && jogadorAtual == 'O')
                {
                    JogadaMaquina();
                    Console.WriteLine("Máquina jogou.");
                }
                else
                {
                    JogadaJogador(jogadorAtual);
                }

                if (VerificarVitoria(jogadorAtual))
                {
                    ExibirTabuleiro();
                    Console.WriteLine($"Jogador {jogadorAtual} venceu!");
                    jogoAtivo = false;
                }
                else if (VerificarEmpate())
                {
                    ExibirTabuleiro();
                    Console.WriteLine("Empate!");
                    jogoAtivo = false;
                }
                else
                {
                    jogadorAtual = (jogadorAtual == 'X') ? 'O' : 'X';
                }
            }
        }

        private static void IniciarTabuleiro()
        {
            char posicao = '1';
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tabuleiro[i, j] = posicao++;
                }
            }
        }

        private static void ExibirTabuleiro()
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tabuleiro[i, j]);
                    if (j < 2)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
                if (i < 2) 
                {
                    Console.WriteLine("---+---+---");
                }
            }
            Console.WriteLine();
        }

        private static void JogadaJogador(char jogador)
        {
            int posicao;
            bool valido;

            do
            {
                Console.Write($"Jogador {jogador}, escolha uma posição (1-9): ");
                string input = Console.ReadLine() ?? "";
                if (!int.TryParse(input, out posicao))
                {
                    Console.WriteLine("Entrada inválida. Digite um número.");
                    valido = false;
                    continue;
                }

                valido = ValidarJogada(posicao);
                if (!valido)
                {
                    Console.WriteLine("Jogada inválida. Tente novamente.");
                }
            } while (!valido);

            MarcarPosicao(posicao, jogador);
        }

        private static void JogadaMaquina()
        {
            int posicao;
            do
            {
                posicao = random.Next(1, 10); 
            } 
            while (!ValidarJogada(posicao));

            MarcarPosicao(posicao, 'O');
        }

        private static bool ValidarJogada(int posicao)
        {
            if (posicao < 1 || posicao > 9)
            {
                return false;
            }

            int linha = (posicao - 1) / 3;
            int coluna = (posicao - 1) % 3;

            return tabuleiro[linha, coluna] != 'X' && tabuleiro[linha, coluna] != 'O';
        }

        private static void MarcarPosicao(int posicao, char jogador)
        {
            int linha = (posicao - 1) / 3;
            int coluna = (posicao - 1) % 3;
            tabuleiro[linha, coluna] = jogador;
        }

        private static bool VerificarVitoria(char jogador)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador) ||
                    (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador))
                {
                    return true;
                }
            }

            // Diagonais
            return (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador) ||
                    (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador);
        }

        private static bool VerificarEmpate()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] != 'X' && tabuleiro[i, j] != 'O')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}