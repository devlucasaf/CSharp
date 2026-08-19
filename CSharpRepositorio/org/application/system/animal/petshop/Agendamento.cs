using System;

namespace Application.System.Animal.Petshop;

public class Agendamento
{
    private static int _contadorId = 1;
    private int                 Id                      { get; set; }
    private ClientePetshop      Cliente                 { get; set; }
    private Animal              Animal                  { get; set; }
    private Servico             Servico                 { get; set; }
    private DateTime            DataHora                { get; set; }
    private StatusAgendamento   Status                  { get; set; }
    private Veterinario         VeterinarioResponsavel  { get; set; }

    public Agendamento(ClientePetshop cliente, Animal animal, Servico servico, DateTime dataHora)
    {
        Id = _contadorId++;
        Cliente = cliente;
        Animal = animal;
        Servico = servico;
        DataHora = dataHora;
        Status = StatusAgendamento.PENDENTE;
    }

    public void Confirmar()
    {
        Status = StatusAgendamento.CONFIRMADO;
        Console.WriteLine($"Agendamento #{Id} confirmado.");
    }

    public void Cancelar()
    {
        Status = StatusAgendamento.CANCELADO;
        Console.WriteLine($"Agendamento #{Id} cancelado.");
    }

    public void Realizar()
    {
        Status = StatusAgendamento.REALIZADO;
        Console.WriteLine($"Serviço {Servico.GetTipo()} realizado para {Animal.GetNome()}");
    }

    public void ExibirDetalhes()
    {
        Console.WriteLine($"Agendamento #{Id} - Cliente: {Cliente.GetNome()} | Animal: {Animal.GetNome()}");
        Console.WriteLine($"Serviço: {Servico.GetTipo()} | Data: {DataHora:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Status: {Status}");

        if (VeterinarioResponsavel != null && Servico.GetTipo() == TipoServico.CONSULTA_VETERINARIA)
        {
            Console.WriteLine($"Veterinário: {VeterinarioResponsavel.GetNome()}");
        }
    }

    public int GetId() => Id;
    public ClientePetshop GetCliente() => Cliente;
    public Animal GetAnimal() => Animal;
    public Servico GetServico() => Servico;
    public DateTime GetDataHora() => DataHora;
    public StatusAgendamento GetStatus() => Status;
    public void SetVeterinarioResponsavel(Veterinario veterinario) => VeterinarioResponsavel = veterinario;
}
