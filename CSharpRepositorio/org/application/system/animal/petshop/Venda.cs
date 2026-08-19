using System;
using System.Collections.Generic;

namespace Application.System.Animal.Petshop;

public class Venda
{
    private static int _contadorId = 1;
    private int                     Id                      { get; set; }
    private ClientePetshop          Cliente                 { get; set; }
    private List<ItemVenda>         Itens                   { get; set; } = new();
    private DateTime                DataVenda               { get; set; }
    private double                  ValorTotal              { get; set; }
    private bool                    UsadoSaldoFidelidade    { get; set; }
    private FormaPagamentoPetshop   FormaPagamento          { get; set; }

    public Venda(ClientePetshop cliente, FormaPagamentoPetshop formaPagamento)
    {
        Id = _contadorId++;
        Cliente = cliente;
        DataVenda = DateTime.Now;
        ValorTotal = 0.0;
        FormaPagamento = formaPagamento;
        UsadoSaldoFidelidade = false;
    }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        if (produto.ReduzirEstoque(quantidade))
        {
            var item = new ItemVenda(produto, quantidade);
            Itens.Add(item);
            ValorTotal += item.GetSubtotal();
            Console.WriteLine($"{quantidade}x {produto.GetNome()} adicionado à venda.");
        }
        else
        {
            Console.WriteLine($"Estoque insuficiente para {produto.GetNome()}");
        }
    }

    public void AdicionarServico(Servico servico, Animal animal)
    {
        double precoServico = servico.CalcularPreco(animal);
        ValorTotal += precoServico;
        Console.WriteLine($"Serviço {servico.GetTipo()} para {animal.GetNome()} adicionado. Valor: R${precoServico:F2}");
    }

    public void AplicarSaldoFidelidade()
    {
        if (Cliente.GetSaldoFidelidade() > 0 && !UsadoSaldoFidelidade)
        {
            double desconto = Math.Min(ValorTotal, Cliente.GetSaldoFidelidade());
            ValorTotal -= desconto;
            Cliente.UsarSaldoFidelidade(desconto);
            UsadoSaldoFidelidade = true;
            Console.WriteLine($"Desconto de R${desconto:F2} aplicado via saldo fidelidade.");
        }
    }

    public void FinalizarVenda()
    {
        Console.WriteLine($"\n--- VENDA #{Id} FINALIZADA ---");
        Console.WriteLine($"Cliente: {Cliente.GetNome()}");
        Console.WriteLine($"Data: {DataVenda:dd/MM/yyyy}");
        Console.WriteLine("Itens:");
        foreach (var item in Itens)
        {
            item.ExibirItem();
        }

        Console.WriteLine($"Valor total: R${ValorTotal:F2}");
        Console.WriteLine($"Forma de pagamento: {FormaPagamento}");

        if (UsadoSaldoFidelidade)
        {
            Console.WriteLine("Saldo fidelidade utilizado.");
        }

        Cliente.AdicionarPontosFidelidade(ValorTotal);
    }

    public int GetId() => Id;
    public double GetValorTotal() => ValorTotal;
}
