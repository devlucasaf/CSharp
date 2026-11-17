using System;

namespace Games.Narrativo.Rpg
{
    public class Program
    {
        private static Personagem jogador = null!;
        private static int inimigosDerrotados = 0;

        public static void Main(string[] args)
        {
            CriarPersonagem();
            LoopPrincipal();

            Console.WriteLine("\n Fim de jogo! Você derrotou " + inimigosDerrotados + " inimigos!");
            jogador.MostrarStatus();
        }

        private static void CriarPersonagem()
        {
            Console.Write("\nDigite o nome do seu herói: ");
            string nome = Console.ReadLine()!;

            Console.WriteLine("\nEscolha sua classe:");
            Console.WriteLine("1. ️ Guerreiro (Vida alta, defesa boa)");
            Console.WriteLine("2.  Mago (Ataque alto, vida baixa)");
            Console.WriteLine("3.  Arqueiro (Equilibrado)");
            Console.Write("Escolha: ");

            string classe = Console.ReadLine() switch
            {
                "1" => "Guerreiro",
                "2" => "Mago",
                "3" => "Arqueiro",
                _ => "Guerreiro"
            };

            jogador = new Personagem(nome, classe);
            Console.WriteLine("\nBem-vindo, " + nome + " o " + classe + "!");
            jogador.MostrarStatus();
        }

        private static void LoopPrincipal()
        {
            while (jogador.EstaVivo())
            {
                Console.WriteLine("\n--- O que deseja fazer? ---");
                Console.WriteLine("1. ️ Explorar (encontrar inimigos)");
                Console.WriteLine("2.  Inventário");
                Console.WriteLine("3.  Loja");
                Console.WriteLine("4.  Ver Status");
                Console.WriteLine("5.  Sair do jogo");
                Console.Write("Escolha: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Explorar();
                        break;
                    case "2":
                        UsarInventario();
                        break;
                    case "3":
                        Loja.Abrir(jogador);
                        break;
                    case "4":
                        jogador.MostrarStatus();
                        break;
                    case "5":
                        Console.WriteLine("Até a próxima, aventureiro!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            Console.WriteLine("\n Você foi derrotado... Game Over!");
        }

        private static void Explorar()
        {
            Inimigo inimigo = Inimigo.GerarInimigo(jogador.Nivel);
            Console.WriteLine("\n Um " + inimigo.Nome + " apareceu! (Vida: " + inimigo.VidaMaxima + ")");

            while (jogador.EstaVivo() && inimigo.EstaVivo())
            {
                Console.WriteLine("\n[" + jogador.Nome + ": " + jogador.Vida + "/" + jogador.VidaMaxima + " HP]");
                Console.WriteLine("[" + inimigo.Nome + ": " + inimigo.BarraVida() + " " + inimigo.Vida + "/" + inimigo.VidaMaxima + " HP]");
                Console.WriteLine("1. Atacar  2. Usar item  3. Fugir");
                Console.Write("Ação: ");

                string acao = Console.ReadLine()!;

                if (acao == "3")
                {
                    Console.WriteLine("Você fugiu da batalha!");
                    return;
                }

                if (acao == "2")
                {
                    if (!jogador.Inventario.TemItens())
                    {
                        Console.WriteLine("Sem itens! Você perde o turno.");
                    }
                    else
                    {
                        jogador.Inventario.Mostrar();
                        Console.Write("Usar qual item? (número): ");
                        try
                        {
                            int idx = int.Parse(Console.ReadLine()!) - 1;
                            ItemRPG? item = jogador.Inventario.UsarItem(idx);
                            if (item != null && item.Tipo == "cura")
                            {
                                jogador.Curar(item.Valor);
                                Console.WriteLine("❤️ Você recuperou " + item.Valor + " HP! Vida: " + jogador.Vida);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Inválido, turno perdido!");
                        }
                    }
                }
                else
                {
                    int danoJogador = jogador.Atacar();
                    int danoReal = inimigo.ReceberDano(danoJogador);
                    Console.WriteLine("🗡️ Você causou " + danoReal + " de dano ao " + inimigo.Nome + "!");
                }

                if (inimigo.EstaVivo())
                {
                    int danoInimigo = inimigo.Atacar();
                    int danoRecebido = jogador.ReceberDano(danoInimigo);
                    Console.WriteLine("💥 " + inimigo.Nome + " causou " + danoRecebido + " de dano em você!");
                }
            }

            if (!inimigo.EstaVivo())
            {
                inimigosDerrotados++;
                Console.WriteLine("\n Você derrotou o " + inimigo.Nome + "!");
                jogador.GanharExperiencia(inimigo.XpRecompensa);
                jogador.GanharOuro(inimigo.OuroRecompensa);
                Console.WriteLine(" +" + inimigo.OuroRecompensa + " ouro! (Total: " + jogador.Ouro + ")");
            }
        }

        private static void UsarInventario()
        {
            jogador.Inventario.Mostrar();
            if (!jogador.Inventario.TemItens()) 
            {
                return;
            }

            Console.Write("Usar item? (número ou 0 para sair): ");
            try
            {
                int idx = int.Parse(Console.ReadLine()!);
                if (idx == 0) 
                {
                    return;
                }

                ItemRPG? item = jogador.Inventario.UsarItem(idx - 1);
                if (item != null && item.Tipo == "cura")
                {
                    jogador.Curar(item.Valor);
                    Console.WriteLine("❤️ Vida recuperada! Vida: " + jogador.Vida + "/" + jogador.VidaMaxima);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }
}
