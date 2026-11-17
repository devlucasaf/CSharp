using System;

namespace Games.Narrativo.Rpg
{
    public static class Loja
    {
        private static readonly ItemRPG[] ITENS_DISPONIVEIS =
        {
            new ItemRPG("Poção de Vida", "cura", 30, 15),
            new ItemRPG("Poção de Vida Grande", "cura", 60, 30),
            new ItemRPG("Elixir de Vida", "cura", 100, 55),
            new ItemRPG("Amuleto de Força", "ataque", 5, 40),
            new ItemRPG("Escudo de Ferro", "defesa", 4, 35),
        };

        public static void Abrir(Personagem jogador)
        {
            Console.WriteLine("\n === LOJA DO AVENTUREIRO ===");
            Console.WriteLine("Seu ouro: " + jogador.Ouro);
            Console.WriteLine("0. Sair da loja");
            for (int i = 0; i < ITENS_DISPONIVEIS.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + ITENS_DISPONIVEIS[i]);
            }

            Console.Write("Escolha um item para comprar: ");
            int escolha;
            try
            {
                escolha = int.Parse(Console.ReadLine()!);
            }
            catch (FormatException)
            {
                Console.WriteLine("Opção inválida!");
                return;
            }

            if (escolha == 0) return;

            if (escolha < 1 || escolha > ITENS_DISPONIVEIS.Length)
            {
                Console.WriteLine("Item inválido!");
                return;
            }

            ItemRPG item = ITENS_DISPONIVEIS[escolha - 1];
            if (jogador.Ouro < item.Preco)
            {
                Console.WriteLine("Ouro insuficiente! Você precisa de " + item.Preco + " de ouro.");
                return;
            }

            if (item.Tipo == "ataque" || item.Tipo == "defesa")
            {
                jogador.SetOuro(jogador.Ouro - item.Preco);
                Console.WriteLine(" " + item.Nome + " equipado! (efeito permanente nesta sessão)");
                jogador.Inventario.AdicionarItem(
                    new ItemRPG(item.Nome, item.Tipo, item.Valor, item.Preco));
            }
            else
            {
                jogador.SetOuro(jogador.Ouro - item.Preco);
                jogador.Inventario.AdicionarItem(
                    new ItemRPG(item.Nome, item.Tipo, item.Valor, item.Preco));
                Console.WriteLine(" " + item.Nome + " adicionado ao inventário!");
            }
        }
    }
}
