using Application.Sistema.Automovel.LocadoraDeVeiculos.Enums;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public class Veiculo
{
    private static int _contadorId = 1;

    public int              Id              { get; }
    public string           Placa           { get; }
    public string           Modelo          { get; }
    public string           Marca           { get; }
    public int              Ano             { get; }
    public CategoriaVeiculo Categoria       { get; }
    public double           ValorDiaria     { get; }
    public bool             Disponivel      { get; private set; }
    public double           Quilometragem   { get; set; }

    public Veiculo(string placa, string modelo, string marca, int ano,
                    CategoriaVeiculo categoria, double valorDiaria, double quilometragem)
    {
        Id = _contadorId++;
        Placa = placa;
        Modelo = modelo;
        Marca = marca;
        Ano = ano;
        Categoria = categoria;
        ValorDiaria = valorDiaria;
        Disponivel = true;
        Quilometragem = quilometragem;
    }

    public void Alugar()
    {
        Disponivel = false;
    }

    public void Devolver(double quilometragemAtual)
    {
        Disponivel = true;
        Quilometragem = quilometragemAtual;
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine("--- VEÍCULO ---");
        Console.WriteLine("ID: " + Id);
        Console.WriteLine("Placa: " + Placa);
        Console.WriteLine("Modelo: " + Modelo + " (" + Marca + ")");
        Console.WriteLine("Ano: " + Ano);
        Console.WriteLine("Categoria: " + Categoria);
        Console.WriteLine($"Diária: R${ValorDiaria:F2}");
        Console.WriteLine("Disponível: " + (Disponivel ? "Sim" : "Não"));
        Console.WriteLine($"Quilometragem: {Quilometragem:F2} km");
    }
}
