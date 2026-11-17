using System;

namespace Application.System.Condominio;

public class AreaComum
{
    private readonly long   identificador;
    private string          nome;
    private int             capacidadeMaxima;
    private decimal         valorReserva;
    private TimeOnly        horarioAbertura;
    private TimeOnly        horarioFechamento;
    private bool            ativa;

    public AreaComum(long identificador, string nome, int capacidadeMaxima, decimal valorReserva,
                        TimeOnly horarioAbertura, TimeOnly horarioFechamento)
    {
        if (identificador <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }

        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome da área não pode estar vazio.");
        }

        if (capacidadeMaxima <= 0)
        {
            throw new ArgumentException("A capacidade deve ser maior que zero.");
        }

        if (valorReserva < 0)
        {
            throw new ArgumentException("O valor da reserva não pode ser negativo.");
        }

        if (!horarioAbertura.IsBefore(horarioFechamento))
        {
            throw new ArgumentException("O horário de funcionamento é inválido.");
        }

        this.identificador = identificador;
        this.nome = nome.Trim();
        this.capacidadeMaxima = capacidadeMaxima;
        this.valorReserva = valorReserva;
        this.horarioAbertura = horarioAbertura;
        this.horarioFechamento = horarioFechamento;
        this.ativa = true;
    }

    // --- VERIFICA SE O HORÁRIO ESTÁ DENTRO DO FUNCIONAMENTO ---
    public bool AceitaHorario(TimeOnly inicio, TimeOnly fim)
    {
        return inicio.IsBefore(fim)
            && !inicio.IsBefore(horarioAbertura)
            && !fim.IsAfter(horarioFechamento);
    }

    public long Identificador => identificador;
    public string Nome => nome;
    public int CapacidadeMaxima => capacidadeMaxima;
    public decimal ValorReserva => valorReserva;
    public TimeOnly HorarioAbertura => horarioAbertura;
    public TimeOnly HorarioFechamento => horarioFechamento;
    public bool IsAtiva => ativa;
    public void SetAtiva(bool ativa) => this.ativa = ativa;

    public override string ToString()
    {
        return identificador + " - " + nome;
    }
}
