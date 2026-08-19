using System;

namespace Application.System.Animal.Petshop;

public class Gato : Animal
{
    private bool GostaDeArranhador  { get; set; }
    private bool Independente       { get; set; }

    public Gato(string nome, string raca, DateTime dataNascimento, PorteAnimal porte,
                double peso, string cor, bool gostaDeArranhador, bool independente)
        : base(nome, raca, dataNascimento, TipoAnimal.GATO, porte, peso, cor)
    {
        GostaDeArranhador = gostaDeArranhador;
        Independente = independente;
    }

    public override void EmitirSom()
    {
        Console.WriteLine($"{Nome} mia: Miau!");
    }

    public void Arranhar()
    {
        Console.WriteLine($"{Nome} está arranhando o sofá...");
    }

    public override void ExibirInformacoes()
    {
        base.ExibirInformacoes();
        Console.WriteLine($"Gosta de arranhador: {(GostaDeArranhador ? "Sim" : "Não")}");
        Console.WriteLine($"Independente: {(Independente ? "Sim" : "Não")}");
    }
}
