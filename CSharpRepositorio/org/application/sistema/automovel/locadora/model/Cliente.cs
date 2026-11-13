namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public class Cliente : Pessoa
{
    public string           NumeroCnh           { get; }
    public DateOnly?        DataValidadeCnh     { get; }
    public int              PontosFidelidade    { get; private set; }
    public List<Locacao>    Locacoes            { get; }
    public bool             Ativo               { get; set; }

    public Cliente(string nome, string cpf, string telefone, string email,
                    string numeroCnh, DateOnly? dataValidadeCnh)
        : base(nome, cpf, telefone, email)
    {
        NumeroCnh = numeroCnh;
        DataValidadeCnh = dataValidadeCnh;
        PontosFidelidade = 0;
        Locacoes = new List<Locacao>();
        Ativo = true;
    }

    public void AdicionarLocacao(Locacao locacao)
    {
        Locacoes.Add(locacao);
    }

    public void AdicionarPontos(int pontos)
    {
        PontosFidelidade += pontos;
        Console.WriteLine("Cliente " + Nome + " ganhou " + pontos + " pontos. Total: " + PontosFidelidade);
    }

    public bool IsHabilitado()
    {
        return Ativo && DataValidadeCnh != null && DataValidadeCnh.Value > DateOnly.FromDateTime(DateTime.Now);
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine("--- CLIENTE ---");
        Console.WriteLine("Nome: " + Nome);
        Console.WriteLine("CPF: " + Cpf);
        Console.WriteLine("Telefone: " + Telefone);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("CNH: " + NumeroCnh + " (válida até " + Formatacao.Data(DataValidadeCnh) + ")");
        Console.WriteLine("Pontos fidelidade: " + PontosFidelidade);
        Console.WriteLine("Locações realizadas: " + Locacoes.Count);
        Console.WriteLine("Ativo: " + (Ativo ? "Sim" : "Não"));
    }
}
