namespace Application.System.Animal.Petshop;

public class Servico
{
    private static int _contadorId = 1;
    private int         Id              { get; set; }
    private TipoServico Tipo            { get; set; }
    private string      Descricao       { get; set; }
    private double      PrecoBase       { get; set; }
    private int         DuracaoMinutos  { get; set; }

    public Servico(TipoServico tipo, string descricao, double precoBase, int duracaoMinutos)
    {
        Id = _contadorId++;
        Tipo = tipo;
        Descricao = descricao;
        PrecoBase = precoBase;
        DuracaoMinutos = duracaoMinutos;
    }

    public double CalcularPreco(Animal animal)
    {
        double preco = PrecoBase;

        if (animal.GetPorte() == PorteAnimal.GRANDE && Tipo == TipoServico.BANHO)
        {
            preco *= 1.5;
        }
        else if (animal.GetPorte() == PorteAnimal.PEQUENO && Tipo == TipoServico.BANHO)
        {
            preco *= 0.8;
        }

        return preco;
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine($"Serviço: {Tipo} - {Descricao} | Preço base: R${PrecoBase:F2}");
    }

    public int GetId() => Id;
    public TipoServico GetTipo() => Tipo;
    public string GetDescricao() => Descricao;
    public double GetPrecoBase() => PrecoBase;
}
