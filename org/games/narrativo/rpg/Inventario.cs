using System;
using System.Collections.Generic;

namespace Games.Narrativo.Rpg
{
    public class Inventario
    {
        private readonly List<ItemRPG> itens = new List<ItemRPG>();
        private readonly int capacidadeMaxima = 10;

        public Inventario()
        {
            itens.Add(new ItemRPG("Poção de Vida", "cura", 30, 15));
            itens.Add(new ItemRPG("Poção de Vida", "cura", 30, 15));
        }

        public bool AdicionarItem(ItemRPG item)
        {
            if (itens.Count >= capacidadeMaxima)
            {
                Console.WriteLine("Inventário cheio! Não é possível adicionar " + item.Nome);
                return false;
            }
            itens.Add(item);
            return true;
        }

        public ItemRPG? UsarItem(int indice)
        {
            if (indice < 0 || indice >= itens.Count)
            {
                Console.WriteLine("Item inválido!");
                return null;
            }
            ItemRPG item = itens[indice];
            itens.RemoveAt(indice);
            return item;
        }

        public void Mostrar()
        {
            if (itens.Count == 0)
            {
                Console.WriteLine("Inventário vazio!");
                return;
            }
            Console.WriteLine("\n=== INVENTÁRIO ===");
            for (int i = 0; i < itens.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + itens[i]);
            }
            Console.WriteLine("==================");
        }

        public IReadOnlyList<ItemRPG> Itens => itens.AsReadOnly();
        public bool TemItens() => itens.Count > 0;
    }
}
