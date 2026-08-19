namespace Application.System.Animal.Petshop;

public abstract class PessoaPetshop
{
    protected string Nome       { get; set; }
    protected string Cpf        { get; set; }
    protected string Telefone   { get; set; }
    protected string Email      { get; set; }
    protected string Endereco   { get; set; }

    protected PessoaPetshop(string nome, string cpf, string telefone, string email, string endereco)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
        Endereco = endereco;
    }

    public abstract void ExibirInformacoes();

    public string GetNome() => Nome;
    public string GetCpf() => Cpf;
    public string GetTelefone() => Telefone;
    public string GetEmail() => Email;
}
