namespace CSharpRepositorio.org.application;

public class TipoPrimitivo
{
    public static void Run()
    {
        string produto = "Betoneira";
        int quantidade = 10;
        decimal precoUnitario = 1500.50m;
        bool temNoEstoque = false;

        decimal total = quantidade * precoUnitario;

        Console.WriteLine("Produto: " + produto);
        Console.WriteLine("Quantidade: " + quantidade);
        Console.WriteLine("Preço total: R$ " + total);
        Console.WriteLine("Disponível no estoque: " + (temNoEstoque ? "Sim" : "Não"));
    }
}
