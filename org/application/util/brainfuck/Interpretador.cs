using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Util.Brainfuck
{
    public class Interpretador
    {
        private const int TamanhoFita = 30_000;

        private readonly byte[] fita = new byte[TamanhoFita];
        private int ponteiro = 0;
        private readonly Dictionary<int, int> pares = new Dictionary<int, int>();

        public string Executar(string programa)
        {
            MapearColchetes(programa);
            StringBuilder saida = new StringBuilder();
            int i = 0;
            while (i < programa.Length)
            {
                char c = programa[i];
                switch (c)
                {
                    case '>':
                        ponteiro = (ponteiro + 1) % TamanhoFita;
                        break;
                    case '<':
                        ponteiro = (ponteiro - 1 + TamanhoFita) % TamanhoFita;
                        break;
                    case '+':
                        fita[ponteiro]++;
                        break;
                    case '-':
                        fita[ponteiro]--;
                        break;
                    case '.':
                        saida.Append((char)(fita[ponteiro] & 0xFF));
                        break;
                    case ',':
                        int lido = Console.In.Read();
                        fita[ponteiro] = (byte)(lido == -1 ? 0 : lido);
                        break;
                    case '[':
                        if (fita[ponteiro] == 0)
                        {
                            i = pares[i];
                        }
                        break;
                    case ']':
                        if (fita[ponteiro] != 0)
                        {
                            i = pares[i];
                        }
                        break;
                    default:
                        break;
                }
                i++;
            }
            return saida.ToString();
        }

        private void MapearColchetes(string programa)
        {
            pares.Clear();
            Stack<int> pilha = new Stack<int>();
            for (int i = 0; i < programa.Length; i++)
            {
                char c = programa[i];
                if (c == '[')
                {
                    pilha.Push(i);
                }
                else if (c == ']')
                {
                    if (pilha.Count == 0)
                    {
                        throw new ArgumentException("] sem [ correspondente na posicao " + i);
                    }
                    int inicio = pilha.Pop();
                    pares[inicio] = i;
                    pares[i] = inicio;
                }
            }

            if (pilha.Count > 0)
            {
                throw new ArgumentException("[ sem ] correspondente na posicao " + pilha.Pop());
            }
        }

        public void Reset()
        {
            Array.Clear(fita, 0, fita.Length);
            ponteiro = 0;
            pares.Clear();
        }
    }
}
