using System;

namespace Application.System.Animal.Petshop;

public class Cachorro : Animal
{
    private bool    Adestrado       { get; set; }
    private string  NivelEnergia    { get; set; }

    public Cachorro(string nome, string raca, DateTime dataNascimento, PorteAnimal porte,
                    double peso, string cor, bool adestrado, string nivelEnergia)
        : base(nome, raca, dataNascimento, TipoAnimal.CACHORRO, porte, peso, cor)
    {
        Adestrado = adestrado;
        NivelEnergia = nivelEnergia;
    }

    public override void EmitirSom()
    {
        Console.WriteLine($"{Nome} late: Au au!");
    }

    public void Brincar()
    {
        Console.WriteLine($"{Nome} está brincando com a bola.");
    }

    public override void ExibirInformacoes()
    {
        base.ExibirInformacoes();
        Console.WriteLine($"Adestrado: {(Adestrado ? "Sim" : "Não")}");
        Console.WriteLine($"Nível de energia: {NivelEnergia}");
    }
}
