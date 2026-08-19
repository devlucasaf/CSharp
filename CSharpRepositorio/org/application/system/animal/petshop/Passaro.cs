using System;

namespace Application.System.Animal.Petshop;

public class Passaro : Animal
{
    private bool    SabeCantar  { get; set; }
    private string  CorPenas    { get; set; }

    public Passaro(string nome, string raca, DateTime dataNascimento, PorteAnimal porte,
                    double peso, string cor, bool sabeCantar, string corPenas)
        : base(nome, raca, dataNascimento, TipoAnimal.PASSARO, porte, peso, cor)
    {
        SabeCantar = sabeCantar;
        CorPenas = corPenas;
    }

    public override void EmitirSom()
    {
        if (SabeCantar)
        {
            Console.WriteLine($"{Nome} canta: Fiu fiu!");
        }
        else
        {
            Console.WriteLine($"{Nome} piou: Piu piu.");
        }
    }

    public void Voar()
    {
        Console.WriteLine($"{Nome} está voando pela loja.");
    }

    public override void ExibirInformacoes()
    {
        base.ExibirInformacoes();
        Console.WriteLine($"Sabe cantar: {(SabeCantar ? "Sim" : "Não")}");
        Console.WriteLine($"Cor das penas: {CorPenas}");
    }
}
