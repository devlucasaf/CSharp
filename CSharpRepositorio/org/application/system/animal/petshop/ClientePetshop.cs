using System;
using System.Collections.Generic;

namespace Application.System.Animal.Petshop;

public class ClientePetshop : PessoaPetshop
{
    private string 			CodigoCliente 	{ get; set; }
    private List<Animal> 	Animais 		{ get; set; } = new();
    private double 			SaldoFidelidade { get; set; }
    private bool 			Ativo 			{ get; set; }

    public ClientePetshop(string nome, string cpf, string telefone, string email, string endereco, string codigoCliente)
        : base(nome, cpf, telefone, email, endereco)
    {
        CodigoCliente = codigoCliente;
        SaldoFidelidade = 0.0;
        Ativo = true;
    }

    public void AdicionarAnimal(Animal animal)
    {
        Animais.Add(animal);
        Console.WriteLine($"Animal {animal.GetNome()} adicionado ao cliente {Nome}");
    }

    public void RemoverAnimal(Animal animal)
    {
        Animais.Remove(animal);
        Console.WriteLine($"Animal {animal.GetNome()} removido.");
    }

    public void AdicionarPontosFidelidade(double valorGasto)
    {
        double pontos = valorGasto * 0.05; 
        SaldoFidelidade += pontos;
        Console.WriteLine($"Cliente {Nome} ganhou R${pontos:F2} de saldo fidelidade.");
    }

    public void UsarSaldoFidelidade(double valor)
    {
        if (valor <= SaldoFidelidade)
        {
            SaldoFidelidade -= valor;
            Console.WriteLine($"Cliente {Nome} utilizou R${valor:F2} do saldo fidelidade.");
        }
        else
        {
            Console.WriteLine($"Saldo insuficiente. Saldo atual: R${SaldoFidelidade:F2}");
        }
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine("--- CLIENTE ---");
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Código: {CodigoCliente}");
        Console.WriteLine($"Telefone: {Telefone}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Animais: {Animais.Count}");
        Console.WriteLine($"Saldo fidelidade: R${SaldoFidelidade:F2}");
        Console.WriteLine($"Ativo: {(Ativo ? "Sim" : "Não")}");
    }

    public string GetCodigoCliente() => CodigoCliente;
    public List<Animal> GetAnimais() => Animais;
    public double GetSaldoFidelidade() => SaldoFidelidade;
    public bool IsAtivo() => Ativo;
    public void SetAtivo(bool ativo) => Ativo = ativo;
}
