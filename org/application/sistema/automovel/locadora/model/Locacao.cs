using Application.Sistema.Automovel.LocadoraDeVeiculos.Enums;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public class Locacao
{
    private static int _contadorId = 1;

    public int              Id                      { get; }
    public Veiculo          Veiculo                 { get; }
    public Cliente          Cliente                 { get; }
    public DateOnly         DataRetirada            { get; }
    public DateOnly         DataDevolucaoPrevista   { get; }
    public DateOnly?        DataDevolucaoReal       { get; private set; }
    public double           ValorTotal              { get; set; }
    public StatusLocacao    Status                  { get; private set; }
    public TipoSeguro       Seguro                  { get; }
    public FormaPagamento   FormaPagamento          { get; }

    public Locacao(Veiculo veiculo, Cliente cliente, DateOnly dataRetirada,
                    DateOnly dataDevolucaoPrevista, TipoSeguro seguro, FormaPagamento formaPagamento)
    {
        Id = _contadorId++;
        Veiculo = veiculo;
        Cliente = cliente;
        DataRetirada = dataRetirada;
        DataDevolucaoPrevista = dataDevolucaoPrevista;
        DataDevolucaoReal = null;
        ValorTotal = 0.0;
        Status = StatusLocacao.ATIVA;
        Seguro = seguro;
        FormaPagamento = formaPagamento;
    }

    // --- CALCULA OS DIAS ENTRE A RETIRADA E A DEVOLUÇÃO PREVISTA ---
    public long CalcularDiasPrevistos()
    {
        return DataDevolucaoPrevista.DayNumber - DataRetirada.DayNumber;
    }

    // --- CALCULA OS DIAS ENTRE A RETIRADA E A DEVOLUÇÃO REAL ---
    public long CalcularDiasEfetivos()
    {
        if (DataDevolucaoReal == null)
        {
            return 0;
        }

        return DataDevolucaoReal.Value.DayNumber - DataRetirada.DayNumber;
    }

    public void Finalizar(DateOnly dataDevolucaoReal, double valorTotal)
    {
        DataDevolucaoReal = dataDevolucaoReal;
        ValorTotal = valorTotal;
        Status = StatusLocacao.CONCLUIDA;
        Veiculo.Devolver(0);
    }

    public void Cancelar()
    {
        Status = StatusLocacao.CANCELADA;
        Veiculo.Devolver(0);
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine("--- LOCAÇÃO #" + Id + " ---");
        Console.WriteLine("Cliente: " + Cliente.Nome);
        Console.WriteLine("Veículo: " + Veiculo.Modelo + " (" + Veiculo.Placa + ")");
        Console.WriteLine("Retirada: " + Formatacao.Data(DataRetirada));
        Console.WriteLine("Devolução prevista: " + Formatacao.Data(DataDevolucaoPrevista));
        Console.WriteLine("Devolução real: " + (DataDevolucaoReal != null ? Formatacao.Data(DataDevolucaoReal) : "Pendente"));
        Console.WriteLine("Seguro: " + Seguro);
        Console.WriteLine("Forma de pagamento: " + FormaPagamento);
        Console.WriteLine($"Valor total: R${ValorTotal:F2}");
        Console.WriteLine("Status: " + Status);
    }
}
