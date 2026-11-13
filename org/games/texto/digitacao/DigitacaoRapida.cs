using System;
using System.Collections.Generic;

namespace Games.Texto.Digitacao
{
    public class DigitacaoRapida
    {
        private readonly List<string> frases = new List<string>
        {
            "O rato roeu a roupa do rei de Roma",
            "Programar em Java pode ser muito divertido",
            "A persistencia e o caminho do exito",
            "Quem nao arrisca nao petisca",
            "O sol brilha forte no meio do verao",
            "A pratica leva a perfeicao com o tempo",
            "Cada dia e uma nova oportunidade de aprender",
            "Java e uma linguagem orientada a objetos poderosa",
            "Computadores nao cometem erros eles executam ordens",
            "Aprender algoritmos melhora o raciocinio logico"
        };

        public static void Main(string[] args)
        {
            new DigitacaoRapida().Iniciar();
        }

        public void Iniciar()
        {
            Console.WriteLine("=== DIGITACAO RAPIDA ===");
            Console.WriteLine("Voce vera uma frase e devera digita-la o mais rapido possivel.");
            Console.WriteLine("Sera medido seu WPM (palavras por minuto) e a precisao.");
            Console.Write("Quantas frases? ");

            int quantidade;
            if (!int.TryParse(Console.ReadLine()?.Trim(), out quantidade))
            {
                quantidade = 3;
            }

            if (quantidade < 1)
            {
                quantidade = 1;
            }
            else if (quantidade > frases.Count)
            {
                quantidade = frases.Count;
            }

            List<string> selecionadas = new List<string>(frases);
            Embaralhar(selecionadas);

            double somaWpm = 0;
            double somaPrecisao = 0;

            for (int i = 0; i < quantidade; i++)
            {
                string frase = selecionadas[i];

                Console.Write($"\n[{i + 1}/{quantidade}] Pressione ENTER para comecar...");
                Console.ReadLine();

                Console.WriteLine("Digite: " + frase);

                DateTime inicio = DateTime.Now;
                string digitado = Console.ReadLine() ?? string.Empty;
                DateTime fim = DateTime.Now;

                double segundos = (fim - inicio).TotalSeconds;
                double minutos = segundos / 60.0;
                int palavras = frase.Split(
                    new[] { ' ', '\t', '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries
                ).Length;

                double wpm = minutos > 0 ? palavras / minutos : 0;
                double precisao = CalcularPrecisao(frase, digitado);

                Console.WriteLine(
                    $"Tempo: {segundos:F2}s | WPM: {wpm:F1} | Precisao: {precisao:F1}%"
                );

                somaWpm += wpm;
                somaPrecisao += precisao;
            }

            double wpmMedio = somaWpm / quantidade;

            Console.WriteLine("\n===== RESULTADO FINAL =====");
            Console.WriteLine($"WPM medio: {wpmMedio:F1}");
            Console.WriteLine($"Precisao media: {somaPrecisao / quantidade:F1}%");

            Classificar(wpmMedio);
        }

        private static double CalcularPrecisao(string esperado, string digitado)
        {
            int acertos = 0;
            int tamanho = Math.Max(esperado.Length, digitado.Length);
            int minimo = Math.Min(esperado.Length, digitado.Length);

            if (tamanho == 0)
            {
                return 100.0;
            }

            for (int i = 0; i < minimo; i++)
            {
                if (esperado[i] == digitado[i])
                {
                    acertos++;
                }
            }

            return acertos * 100.0 / tamanho;
        }

        private static void Classificar(double wpm)
        {
            string nivel;

            if (wpm < 20)
            {
                nivel = "Iniciante";
            }
            else if (wpm < 40)
            {
                nivel = "Mediano";
            }
            else if (wpm < 60)
            {
                nivel = "Avancado";
            }
            else if (wpm < 80)
            {
                nivel = "Profissional";
            }
            else
            {
                nivel = "Mestre";
            }

            Console.WriteLine("Nivel: " + nivel);
        }

        private static void Embaralhar<T>(IList<T> itens)
        {
            Random random = new Random();

            for (int i = itens.Count - 1; i > 0; i--)
            {
                int indice = random.Next(i + 1);
                T temporario = itens[i];
                itens[i] = itens[indice];
                itens[indice] = temporario;
            }
        }
    }
}
