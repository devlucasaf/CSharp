using Application.System.Restaurante;

namespace Application.Outros.Cardapio
{
    public class ItemPedido
    {
        public ItemCardapio Item        { get; }
        public int          Quantidade  { get; set; }

        public ItemPedido(ItemCardapio item, int quantidade)
        {
            Item = item;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"{Quantidade}x {Item.Nome} - R$ {Item.CalcularPrecoFinal() * Quantidade:F2}";
        }
    }
}
