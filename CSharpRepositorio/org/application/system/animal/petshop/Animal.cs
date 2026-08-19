using System;

namespace Application.System.Animal.Petshop;

public abstract class Animal
{
    protected string        Nome            { get; set; }
    protected string        Raca            { get; set; }
    protected DateTime      DataNascimento  { get; set; }
    protected TipoAnimal    Tipo            { get; set; }
    protected PorteAnimal   Porte           { get; set; }
    protected double        Peso            { get; set; }
    protected string        Cor             { get; set; }
    protected bool          Ativo           { get; set; }

    protected Animal(string nome, string raca, DateTime dataNascimento, TipoAnimal tipo,
                    PorteAnimal porte, double peso, string cor)
    {
        Nome = nome;
        Raca = raca;
        DataNascimento = dataNascimento;
        Tipo = tipo;
        Porte = porte;
        Peso = peso;
        Cor = cor;
        Ativo = true;
    }

    public int CalcularIdade()
    {
        var hoje = DateTime.Now;
        var idade = hoje.Year - DataNascimento.Year;
        if (DataNascimento > hoje.AddYears(-idade)) 
        {
            idade--;
        }
        return idade;
    }

    public abstract void EmitirSom();

    public virtual void ExibirInformacoes()
    {
        Console.WriteLine("--- ANIMAL ---");
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Raça: {Raca}");
        Console.WriteLine($"Tipo: {Tipo}");
        Console.WriteLine($"Porte: {Porte}");
        Console.WriteLine($"Peso: {Peso} kg");
        Console.WriteLine($"Cor: {Cor}");
        Console.WriteLine($"Idade: {CalcularIdade()} anos");
        Console.WriteLine($"Ativo: {(Ativo ? "Sim" : "Não")}");
    }

    public string GetNome() => Nome;
    public string GetRaca() => Raca;
    public TipoAnimal GetTipo() => Tipo;
    public PorteAnimal GetPorte() => Porte;
    public double GetPeso() => Peso;
    public void SetPeso(double peso) => Peso = peso;
    public bool IsAtivo() => Ativo;
    public void SetAtivo(bool ativo) => Ativo = ativo;
}
