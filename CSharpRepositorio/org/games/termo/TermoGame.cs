using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace games.termo
{
    public class TermoGame
    {
        private const string RESET = "\u001B[0m";
        private const string VERDE_BG = "\u001B[42m\u001B[30m";   
        private const string AMARELO_BG = "\u001B[43m\u001B[30m"; 
        private const string CINZA_BG = "\u001B[40m\u001B[37m";   

        private List<string> dicionarioPalavras;
        private string palavraSecreta;
        private string secretaLogica;
        private int tentativasMaximas;
        private Scanner scanner; 

        public TermoGame()
        {
            dicionarioPalavras = new List<string>
            {
                "IDEIA", "CHAVE", "CENSO", "FURIA", "TEMPO", "FILHO", "FILHA",
                "QUASE", "FATOR", "LAMBE", "BALDE", "VIRAR", "JOGOS", "LAÇOS",
                "ILHAS", "PAPAI", "MAMAE", "MUNDO", "VULGO", "FORTE", "CULTO",
                "JUSTO", "HONRA", "VIGOR", "VASCO", "SAGAZ", "NOBRE", "ANEXO",
                "NEGRO", "MEXER", "PLENA", "FAZER", "MORAL", "DESDE", "JUSTO"
            };

            Random rand = new Random();
            palavraSecreta = dicionarioPalavras[rand.Next(dicionarioPalavras.Count)];

            secretaLogica = RemoverAcentos(palavraSecreta);

            tentativasMaximas = 6;
        }

        private string RemoverAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto)) 
            {
                return texto;
            }
            string normalized = texto.Normalize(NormalizationForm.FormD);
            
            return Regex.Replace(normalized, @"\p{M}", "");
        }

        private EstadoCor[] CalcularCores(string chute)
        {
            char[] secretaChars = secretaLogica.ToCharArray();
            char[] chuteChars = chute.ToCharArray();

            EstadoCor[] resultadoCores = new EstadoCor[5];
            Array.Fill(resultadoCores, EstadoCor.CINZA);

            Dictionary<char, int> contagemLetras = new Dictionary<char, int>();

            foreach (char c in secretaChars)
            {
                if (contagemLetras.ContainsKey(c))
                {
                    contagemLetras[c]++;
                }
                else
                {
                    contagemLetras[c] = 1;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (chuteChars[i] == secretaChars[i])
                {
                    resultadoCores[i] = EstadoCor.VERDE;
                    contagemLetras[chuteChars[i]]--;
                }
            }

            // Second pass: mark yellows
            for (int i = 0; i < 5; i++)
            {
                if (resultadoCores[i] == EstadoCor.VERDE)
                {
                    continue;
                }

                char letra = chuteChars[i];
                if (contagemLetras.ContainsKey(letra) && contagemLetras[letra] > 0)
                {
                    resultadoCores[i] = EstadoCor.AMARELO;
                    contagemLetras[letra]--;
                }
            }

            return resultadoCores;
        }

        private void AnimacaoTerminal(string palavra, EstadoCor[] cores)
        {
            Console.Write("\r" + new string(' ', 30) + "\r");

            char[] letras = palavra.ToCharArray();

            for (int i = 0; i < letras.Length; i++)
            {
                string corCode;
                switch (cores[i])
                {
                    case EstadoCor.VERDE:
                        corCode = VERDE_BG;
                        break;
                    case EstadoCor.AMARELO:
                        corCode = AMARELO_BG;
                        break;
                    default:
                        corCode = CINZA_BG;
                        break;
                }

                Console.Write($"{corCode} {letras[i]} {RESET} ");
                Console.Out.Flush();
                Sleep(500);
            }
            Console.WriteLine();
        }

        private void AnimarTextoMatrix(string fraseFinal)
        {
            string charsPossiveis = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%";
            Random rand = new Random();
            char[] palavraAtual = new char[fraseFinal.Length];
            Array.Fill(palavraAtual, ' ');

            for (int i = 0; i < fraseFinal.Length; i++)
            {
                char letraAlvo = fraseFinal[i];

                for (int j = 0; j < 10; j++)
                {
                    palavraAtual[i] = charsPossiveis[rand.Next(charsPossiveis.Length)];
                    Console.Write("\r" + new string(palavraAtual));
                    Console.Out.Flush();
                    Sleep(20);
                }

                palavraAtual[i] = letraAlvo;
                Console.Write("\r" + new string(palavraAtual));
            }
            Console.WriteLine("\n");
        }

        private void LimparTela()
        {
            Console.Write("\033[H\033[2J");
            Console.Out.Flush();
        }

        private void Sleep(int millis)
        {
            Thread.Sleep(millis);
        }

        public void Jogar()
        {
            LimparTela();
            Console.WriteLine(">>>>> JOGO DO TERMO <<<<<");
            Console.WriteLine("----------------------------------------");

            for (int i = 0; i < tentativasMaximas; i++)
            {
                int tentativaAtual = i + 1;
                Console.Write($"\nTentativa {tentativaAtual}/{tentativasMaximas}: ");

                string chute = Console.ReadLine()?.ToUpper().Trim() ?? string.Empty;

                if (chute.Length != 5)
                {
                    Console.WriteLine("A palavra precisa ter 5 letras!");
                    i--;
                    continue;
                }

                string chuteLogico = RemoverAcentos(chute);
                EstadoCor[] cores = CalcularCores(chuteLogico);

                AnimacaoTerminal(chute, cores);

                if (chuteLogico.Equals(secretaLogica, StringComparison.Ordinal))
                {
                    Console.WriteLine();
                    AnimarTextoMatrix("PARABENS! VOCE VENCEU!");
                    Console.WriteLine($"A palavra era: {palavraSecreta}");
                    return;
                }
            }

            Console.WriteLine("\nFim de jogo!");
            Console.WriteLine($"A palavra era: {palavraSecreta}");
        }

        public static void Main(string[] args)
        {
            new TermoGame().Jogar();
        }
    }
}