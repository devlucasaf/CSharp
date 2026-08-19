using System;
using System.Collections.Generic;
using System.Globalization;

namespace Application.System.Animal.Petshop;

public class Program
{
    public static void Main(string[] args)
    {
        // Criar petshop
        var petshop = new Petshop("Pet & Cia", "12.345.678/0001-90", "Rua dos Animais, 123");

        // Criar serviços
        var banho = new Servico(TipoServico.BANHO, "Banho completo", 50.0, 45);
        var tosa = new Servico(TipoServico.TOSA, "Tosa higiênica", 40.0, 30);
        var consulta = new Servico(TipoServico.CONSULTA_VETERINARIA, "Consulta clínica", 120.0, 60);
        petshop.AdicionarServico(banho);
        petshop.AdicionarServico(tosa);
        petshop.AdicionarServico(consulta);

        // Criar produtos
        var racao = new Produto("Ração Premium", TipoProduto.RACAO, 120.0, 20, "Royal Canin");
        var brinquedo = new Produto("Bolinha de borracha", TipoProduto.BRINQUEDO, 15.0, 50, "PetPlay");
        var vermifugo = new Produto("Vermífugo", TipoProduto.MEDICAMENTO, 25.0, 30, "VetFarma");
        petshop.AdicionarProduto(racao);
        petshop.AdicionarProduto(brinquedo);
        petshop.AdicionarProduto(vermifugo);

        // Criar clientes
        var cliente1 = new ClientePetshop("João Silva", "123.456.789-00",
                "(11) 98765-4321", "joao@email.com", "Rua A, 123", "C001");
        var cliente2 = new ClientePetshop("Maria Santos", "987.654.321-11",
                "(11) 91234-5678", "maria@email.com", "Rua B, 456", "C002");
        petshop.CadastrarCliente(cliente1);
        petshop.CadastrarCliente(cliente2);

        // Criar animais
        var formato = CultureInfo.InvariantCulture.DateTimeFormat; // ou "dd/MM/yyyy"
        var rex = new Cachorro("Rex", "Pastor Alemão", DateTime.ParseExact("10/05/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                PorteAnimal.GRANDE, 35.5, "Preto e castanho", true, "Alta");
        var mimi = new Gato("Mimi", "Siamês", DateTime.ParseExact("15/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                PorteAnimal.PEQUENO, 4.2, "Branco", true, true);
        var pipoca = new Passaro("Pipoca", "Calopsita", DateTime.ParseExact("20/07/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                PorteAnimal.PEQUENO, 0.09, "Amarelo", true, "Cinza e amarela");

        cliente1.AdicionarAnimal(rex);
        cliente1.AdicionarAnimal(mimi);
        cliente2.AdicionarAnimal(pipoca);
        petshop.CadastrarAnimal(rex);
        petshop.CadastrarAnimal(mimi);
        petshop.CadastrarAnimal(pipoca);

        // Criar veterinário
        var vet = new Veterinario("Dra. Carla Lima", "222.333.444-55", "(11) 95555-8888",
                "carla@vet.com", "Rua C, 789", "CRMV-12345", "Clínica geral", 6000.0);
        petshop.ContratarVeterinario(vet);

        // Agendar consulta
        DateTime dataConsulta = DateTime.Now.AddDays(2).Date.AddHours(14).AddMinutes(0);
        petshop.CriarAgendamento(cliente1, rex, consulta, dataConsulta, vet);

        // Agendar banho
        DateTime dataBanho = DateTime.Now.AddDays(1).Date.AddHours(10).AddMinutes(0);
        petshop.CriarAgendamento(cliente2, pipoca, banho, dataBanho, null);

        // Realizar uma venda
        var venda = petshop.IniciarVenda(cliente1, FormaPagamentoPetshop.PIX);
        venda.AdicionarItem(racao, 2);
        venda.AdicionarItem(brinquedo, 3);
        venda.AdicionarServico(tosa, rex);
        venda.AplicarSaldoFidelidade(); 
        venda.FinalizarVenda();

        // Exibir relatórios
        petshop.ExibirClientes();
        petshop.ExibirAnimais();
        petshop.ExibirAgendamentosDoDia();
        petshop.ExibirEstoqueBaixo(10);

        // Polimorfismo: animais emitindo som
        Console.WriteLine("\n=== SONS DOS ANIMAIS ===");
        var animais = new List<Animal> { rex, mimi, pipoca };
        foreach (var a in animais)
        {
            a.EmitirSom();
        }

        // Informações do veterinário
        vet.ExibirInformacoes();
    }
}
