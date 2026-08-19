namespace Application.System.Animal.Petshop;

public class Produto
{
    private static int _contadorId = 1;
    private int         Id      { get; set; }
    private int         Estoque { get; set; }
    private string      Nome    { get; set; }
    private string      Marca   { get; set; }
    private TipoProduto Tipo    { get; set; }
    private double      Preco   { get; set; }

    public Produto(string nome, TipoProduto tipo, double preco, int estoque, string marca)
    {
        Id = _contadorId++;
        Nome = nome;
        Tipo = tipo;
        Preco = preco;
        Estoque = estoque;
        Marca = marca;
    }

    public bool ReduzirEstoque(int quantidade)
    {
        if (quantidade <= Estoque)
        {
            Estoque -= quantidade;
            return true;
        }
        return false;
    }

    public void ReporEstoque(int quantidade)
    {
        Estoque += quantidade;
        Console.WriteLine($"Estoque de {Nome} reposto. Agora: {Estoque}");
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine($"Produto: {Nome} ({Tipo}) | Marca: {Marca} | Preço: R${Preco:F2} | Estoque: {Estoque}");
    }

    public int GetId() => Id;
    public string GetNome() => Nome;
    public double GetPreco() => Preco;
    public int GetEstoque() => Estoque;
}
