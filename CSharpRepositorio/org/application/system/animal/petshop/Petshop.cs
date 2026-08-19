using System;
using System.Collections.Generic;

namespace Application.System.Animal.Petshop;

public class Petshop
{
    private string 					Nome 			{ get; set; }
    private string 					Cnpj 			{ get; set; }
    private string 					Endereco 		{ get; set; }
    private List<ClientePetshop> 	Clientes 		{ get; set; } = new();
    private List<Animal> 			Animais 		{ get; set; } = new();
    private List<Veterinario> 		Veterinarios 	{ get; set; } = new();
    private List<Servico> 			Servicos 		{ get; set; } = new();
    private List<Produto> 			Produtos 		{ get; set; } = new();
    private List<Agendamento> 		Agendamentos 	{ get; set; } = new();
    private List<Venda> 			Vendas 			{ get; set; } = new();
    private double 					Caixa 			{ get; set; }

    public Petshop(string nome, string cnpj, string endereco)
    {
        Nome = nome;
        Cnpj = cnpj;
        Endereco = endereco;
    }

    public void CadastrarCliente(ClientePetshop cliente)
    {
        Clientes.Add(cliente);
        Console.WriteLine($"Cliente {cliente.GetNome()} cadastrado.");
    }

    public void CadastrarAnimal(Animal animal)
    {
        Animais.Add(animal);
        Console.WriteLine($"Animal {animal.GetNome()} cadastrado no petshop.");
    }

    public void ContratarVeterinario(Veterinario vet)
    {
        Veterinarios.Add(vet);
        Console.WriteLine($"Veterinário {vet.GetNome()} contratado.");
    }

    public void AdicionarServico(Servico servico)
    {
        Servicos.Add(servico);
        Console.WriteLine($"Serviço {servico.GetTipo()} adicionado.");
    }

    public void AdicionarProduto(Produto produto)
    {
        Produtos.Add(produto);
        Console.WriteLine($"Produto {produto.GetNome()} adicionado ao estoque.");
    }

    public void CriarAgendamento(ClientePetshop cliente, Animal animal, Servico servico, DateTime dataHora, Veterinario vet)
    {
        if (servico.GetTipo() == TipoServico.CONSULTA_VETERINARIA && vet == null)
        {
            Console.WriteLine("Consulta veterinária requer um veterinário.");
            return;
        }
        
        var ag = new Agendamento(cliente, animal, servico, dataHora);
        if (vet != null) 
        {
            ag.SetVeterinarioResponsavel(vet);
        }
        Agendamentos.Add(ag);
        Console.WriteLine($"Agendamento criado para {cliente.GetNome()} - {animal.GetNome()}");
    }

    public Venda IniciarVenda(ClientePetshop cliente, FormaPagamentoPetshop forma)
    {
        var venda = new Venda(cliente, forma);
        Vendas.Add(venda);
        return venda;
    }

    public void ExibirClientes()
    {
        Console.WriteLine("\n=== CLIENTES ===");
        foreach (var c in Clientes)
        {
            c.ExibirInformacoes();
            Console.WriteLine("------------------");
        }
    }

    public void ExibirAnimais()
    {
        Console.WriteLine("\n=== ANIMAIS ===");
        foreach (var a in Animais)
        {
            a.ExibirInformacoes();
            Console.WriteLine("------------------");
        }
    }

    public void ExibirAgendamentosDoDia()
    {
        Console.WriteLine("\n=== AGENDAMENTOS DE HOJE ===");
        var hoje = DateTime.Now.Date;
        foreach (var a in Agendamentos)
        {
            if (a.GetDataHora().Date == hoje)
            {
                a.ExibirDetalhes();
                Console.WriteLine("------------------");
            }
        }
    }

    public void ExibirEstoqueBaixo(int limite)
    {
        Console.WriteLine($"\n=== PRODUTOS COM ESTOQUE BAIXO (<= {limite}) ===");
        foreach (var p in Produtos)
        {
            if (p.GetEstoque() <= limite)
            {
                p.ExibirInformacoes();
            }
        }
    }

    public string GetNome() => Nome;
    public List<ClientePetshop> GetClientes() => Clientes;
    public List<Produto> GetProdutos() => Produtos;
}
