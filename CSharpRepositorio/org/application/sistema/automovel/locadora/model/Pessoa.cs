namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public abstract class Pessoa
{
    public string Nome      { get; protected set; }
    public string Cpf       { get; protected set; }
    public string Telefone  { get; protected set; }
    public string Email     { get; protected set; }

    protected Pessoa(string nome, string cpf, string telefone, string email)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
    }

    public abstract void ExibirInformacoes();
}
