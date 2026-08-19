namespace Application.System.Animal.Petshop;

public class ItemVenda
{
    private Produto Produto         { get; set; }
    private int     Quantidade      { get; set; }
    private double  PrecoUnitario   { get; set; }

    public ItemVenda(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnitario = produto.GetPreco();
    }

    public double GetSubtotal() => PrecoUnitario * Quantidade;

    public void ExibirItem()
    {
        Console.WriteLine($"  {Produto.GetNome()} x {Quantidade} = R${GetSubtotal():F2}");
    }

    public Produto GetProduto() => Produto;
    public int GetQuantidade() => Quantidade;
}
