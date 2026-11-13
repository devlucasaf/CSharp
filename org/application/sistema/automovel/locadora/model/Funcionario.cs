namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public class Funcionario : Pessoa
{
    public string   Matricula       { get; }
    public string   Cargo           { get; }
    public double   Salario         { get; }
    public DateOnly DataAdmissao    { get; }
    public bool     Ativo           { get; private set; }

    public Funcionario(string nome, string cpf, string telefone, string email,
                        string matricula, string cargo, double salario, DateOnly dataAdmissao)
        : base(nome, cpf, telefone, email)
    {
        Matricula = matricula;
        Cargo = cargo;
        Salario = salario;
        DataAdmissao = dataAdmissao;
        Ativo = true;
    }

    public void Demitir()
    {
        Ativo = false;
        Console.WriteLine("Funcionário " + Nome + " foi demitido.");
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine("--- FUNCIONÁRIO ---");
        Console.WriteLine("Nome: " + Nome);
        Console.WriteLine("Matrícula: " + Matricula);
        Console.WriteLine("Cargo: " + Cargo);
        Console.WriteLine($"Salário: R${Salario:F2}");
        Console.WriteLine("Ativo: " + (Ativo ? "Sim" : "Não"));
    }
}
