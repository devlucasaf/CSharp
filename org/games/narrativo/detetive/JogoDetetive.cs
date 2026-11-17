using System;
using System.Collections.Generic;

namespace Games.Narrativo.Detetive
{
    public class JogoDetetive
    {
        private static readonly List<Suspeito> suspeitos = new List<Suspeito>();
        private static readonly List<Pista> pistas = new List<Pista>();
        private static int turnosRestantes = 10;
        private static int nivelInvestigacao = 0;
        private static readonly string culpado = "Marcos Silva";

        public static void Main(string[] args)
        {
            IniciarJogo();
        }

        public static void IniciarJogo()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("      SOMBRAS NO SOLAR MENDES     ");
            Console.WriteLine("========================================");
            Console.WriteLine("O empresário Artur Mendes foi encontrado morto.");
            Console.WriteLine("Você foi chamado para investigar o caso.");
            Console.WriteLine("Descubra a verdade antes que o tempo acabe.\n");

            CriarSuspeitos();
            CriarPistas();

            while (turnosRestantes > 0)
            {
                Console.WriteLine("\nTurnos restantes: " + turnosRestantes);
                Console.WriteLine("Escolha uma ação:");
                Console.WriteLine("1 - Interrogar suspeito");
                Console.WriteLine("2 - Analisar pistas");
                Console.WriteLine("3 - Revisar informações");
                Console.WriteLine("4 - Fazer acusação");

                int escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        Interrogar();
                        break;
                    case 2:
                        AnalisarPistas();
                        break;
                    case 3:
                        RevisarInfos();
                        break;
                    case 4:
                        Acusar();
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                turnosRestantes--;
                nivelInvestigacao++;
            }

            Console.WriteLine("\nO tempo acabou! Você deve fazer uma acusação.");
            Acusar();
        }

        public static void CriarSuspeitos()
        {
            suspeitos.Add(new Suspeito(
                "Marcos Silva",
                "Sócio da vítima, nervoso e endividado.",
                "Desvio de dinheiro descoberto por Artur.",
                "Alega estar no escritório, mas sem testemunhas."
            ));

            suspeitos.Add(new Suspeito(
                "Helena Mendes",
                "Esposa fria e distante.",
                "Herança milionária.",
                "Diz que dormia no quarto no horário do crime."
            ));

            suspeitos.Add(new Suspeito(
                "Rafael Costa",
                "Funcionário demitido recentemente.",
                "Vingança após demissão.",
                "Afirma estar em um bar, mas ninguém confirma."
            ));
        }

        public static void CriarPistas()
        {
            pistas.Add(new Pista("Pegadas masculinas sujas de lama no escritório.", true));
            pistas.Add(new Pista("Câmeras de segurança desligadas manualmente.", true));
            pistas.Add(new Pista("Uma luva feminina encontrada no jardim.", false));
            pistas.Add(new Pista("Documento rasgado sobre fraude financeira.", true));
        }

        public static void Interrogar()
        {
            Console.WriteLine("\nEscolha um suspeito:");
            for (int i = 0; i < suspeitos.Count; i++)
            {
                Console.WriteLine((i + 1) + " - " + suspeitos[i].Nome);
            }

            int escolha = int.Parse(Console.ReadLine()) - 1;

            if (escolha >= 0 && escolha < suspeitos.Count)
            {
                suspeitos[escolha].Interrogar(nivelInvestigacao);
            }
            else
            {
                Console.WriteLine("Suspeito inválido.");
            }
        }

        public static void AnalisarPistas()
        {
            Console.WriteLine("\nAnalisando pistas encontradas:");
            foreach (Pista pista in pistas)
            {
                pista.Mostrar();
            }
            Console.WriteLine("Algumas pistas podem ser enganosas...");
        }

        public static void RevisarInfos()
        {
            Console.WriteLine("\n===== Revisão Geral =====");
            foreach (Suspeito s in suspeitos)
            {
                Console.WriteLine("\nNome: " + s.Nome);
                Console.WriteLine("Motivação: " + s.Motivacao);
                Console.WriteLine("Álibi: " + s.Alibi);
                if (s.ComportamentoSuspeito)
                {
                    Console.WriteLine("Comportamento suspeito detectado.");
                }
            }
        }

        public static void Acusar()
        {
            Console.WriteLine("\nEscolha quem você acusa:");
            for (int i = 0; i < suspeitos.Count; i++)
            {
                Console.WriteLine((i + 1) + " - " + suspeitos[i].Nome);
            }

            int escolha = int.Parse(Console.ReadLine()) - 1;

            if (escolha < 0 || escolha >= suspeitos.Count)
            {
                Console.WriteLine("Acusação inválida.");
                return;
            }

            string acusado = suspeitos[escolha].Nome;

            Console.WriteLine("\n========================================");
            if (acusado == culpado)
            {
                Console.WriteLine("Você acertou!");
                Console.WriteLine("Marcos Silva matou Artur após ser confrontado sobre a fraude.");
                Console.WriteLine("As pistas financeiras e o falso álibi confirmam o crime.");
            }
            else
            {
                Console.WriteLine("Acusação incorreta.");
                Console.WriteLine(acusado + " era inocente.");
                Console.WriteLine("Marcos Silva era o verdadeiro culpado, motivado por ganância.");
            }
            Console.WriteLine("========================================");
            Console.WriteLine("FIM DE JOGO");
        }
    }
}
