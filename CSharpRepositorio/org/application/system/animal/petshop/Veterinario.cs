using System.Collections.Generic;

namespace Application.System.Animal.Petshop;

public class Veterinario : PessoaPetshop
{
    private string          RegistroCRMV        { get; set; }
    private string          Especialidade       { get; set; }
    private List<Servico>   ConsultasRealizadas { get; set; } = new();
    private double          Salario             { get; set; }

    public Veterinario(string nome, string cpf, string telefone, string email, string endereco,
                        string registroCRMV, string especialidade, double salario)
        : base(nome, cpf, telefone, email, endereco)
    {
        RegistroCRMV = registroCRMV;
        Especialidade = especialidade;
        Salario = salario;
    }

    public void RealizarConsulta(Animal animal, string diagnostico)
    {
        Console.WriteLine($"Veterinário {Nome} realizou consulta em {animal.GetNome()}");
        Console.WriteLine($"Diagnóstico: {diagnostico}");
    }

    public void AplicarVacina(Animal animal, string vacina)
    {
        Console.WriteLine($"Veterinário {Nome} aplicou vacina {vacina} em {animal.GetNome()}");
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine("--- VETERINÁRIO ---");
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"CRMV: {RegistroCRMV}");
        Console.WriteLine($"Especialidade: {Especialidade}");
        Console.WriteLine($"Salário: R${Salario:F2}");
        Console.WriteLine($"Consultas realizadas: {ConsultasRealizadas.Count}");
    }
}
