namespace CSharpRepositorio.org
{
    public class TipoPrimitivo
    {
        static void Main()
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
}