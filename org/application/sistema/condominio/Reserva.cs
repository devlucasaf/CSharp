using System;

namespace Application.System.Condominio;

public class Reserva
{
    private readonly long       identificador;
    private readonly AreaComum  areaComum;
    private readonly Unidade    unidade;
    private readonly Morador    responsavel;
    private readonly DateOnly   data;
    private readonly TimeOnly   horarioInicio;
    private readonly TimeOnly   horarioFim;
    private readonly DateTime   dataSolicitacao;
    private bool                cancelada;
    private DateTime?           dataCancelamento;
    private string              motivoCancelamento;

    public Reserva(long identificador, AreaComum areaComum, Unidade unidade, Morador responsavel,
                    DateOnly data, TimeOnly horarioInicio, TimeOnly horarioFim)
    {
        ValidarIdentificador(identificador);
        ValidarDadosObrigatorios(areaComum, unidade, responsavel);
        ValidarResponsavel(unidade, responsavel);
        ValidarData(data);
        ValidarHorario(areaComum, horarioInicio, horarioFim);

        this.identificador = identificador;
        this.areaComum = areaComum;
        this.unidade = unidade;
        this.responsavel = responsavel;
        this.data = data;
        this.horarioInicio = horarioInicio;
        this.horarioFim = horarioFim;
        this.dataSolicitacao = DateTime.Now;
        this.cancelada = false;
        this.motivoCancelamento = "";
    }

    // --- VERIFICA SE EXISTE CONFLITO COM OUTRA RESERVA ---
    public bool ConflitaCom(Reserva? outraReserva)
    {
        if (outraReserva == null || cancelada || outraReserva.cancelada)
        {
            return false;
        }

        if (!areaComum.Equals(outraReserva.areaComum) || data != outraReserva.data)
        {
            return false;
        }

        return horarioInicio < outraReserva.horarioFim && horarioFim > outraReserva.horarioInicio;
    }

    public bool PertenceAoPeriodo(DateOnly dataInicial, DateOnly dataFinal)
    {
        if (dataInicial > dataFinal)
        {
            throw new ArgumentException("O período informado é inválido.");
        }

        return data >= dataInicial && data <= dataFinal;
    }

    public bool IsAtiva => !cancelada;

    public bool IsFinalizada()
    {
        DateTime termino = data.ToDateTime(horarioFim);
        return !cancelada && termino < DateTime.Now;
    }

    public bool IsEmAndamento()
    {
        if (cancelada) 
        {
            return false;
        }

        DateTime agora = DateTime.Now;
        DateTime inicio = data.ToDateTime(horarioInicio);
        DateTime fim = data.ToDateTime(horarioFim);

        return agora >= inicio && agora < fim;
    }

    public bool IsFutura()
    {
        DateTime inicio = data.ToDateTime(horarioInicio);
        return !cancelada && inicio > DateTime.Now;
    }

    public void Cancelar() => Cancelar("Motivo não informado.");

    public void Cancelar(string motivo)
    {
        if (cancelada)
        {
            throw new InvalidOperationException("A reserva já está cancelada.");
        }

        if (IsFinalizada())
        {
            throw new InvalidOperationException("Uma reserva finalizada não pode ser cancelada.");
        }

        this.cancelada = true;
        this.dataCancelamento = DateTime.Now;
        this.motivoCancelamento = string.IsNullOrWhiteSpace(motivo)
            ? "Motivo não informado." : motivo.Trim();
    }

    public long CalcularDuracaoMinutos() => (long)(horarioFim - horarioInicio).TotalMinutes;

    public string GetSituacaoFormatada()
    {
        if (cancelada)
        {
            return "Cancelada";
        }

        if (IsEmAndamento())
        {
            return "Em andamento";
        }

        if (IsFinalizada())
        {
            return "Finalizada";
        }

        return "Agendada";
    }

    public string GetPeriodoFormatado()
    {
        return data.ToString("dd/MM/yyyy") + " das "
            + horarioInicio.ToString("HH:mm") + " às "
            + horarioFim.ToString("HH:mm");
    }

    private void ValidarIdentificador(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }
    }

    private void ValidarDadosObrigatorios(AreaComum a, Unidade u, Morador r)
    {
        if (a == null)
        {
            throw new ArgumentException("A área comum deve ser informada.");
        }

        if (u == null)
        {
            throw new ArgumentException("A unidade deve ser informada.");
        }

        if (r == null)
        {
            throw new ArgumentException("O responsável deve ser informado.");
        }
    }

    private void ValidarResponsavel(Unidade u, Morador r)
    {
        if (!r.IsAtivo)
        {
            throw new ArgumentException("O responsável pela reserva está inativo.");
        }

        if (!u.PossuiMorador(r))
        {
            throw new ArgumentException("O responsável não pertence à unidade informada.");
        }
    }

    private void ValidarData(DateOnly d)
    {
        if (d < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("A data da reserva não pode estar no passado.");
        }
    }

    private void ValidarHorario(AreaComum a, TimeOnly inicio, TimeOnly fim)
    {
        if (!inicio.IsBefore(fim))
        {
            throw new ArgumentException("O horário inicial deve ser anterior ao horário final.");
        }

        if (!a.AceitaHorario(inicio, fim))
        {
            throw new ArgumentException("O horário está fora do funcionamento da área.");
        }
    }

    public long Identificador => identificador;
    public AreaComum AreaComum => areaComum;
    public Unidade Unidade => unidade;
    public Morador Responsavel => responsavel;
    public DateOnly Data => data;
    public TimeOnly HorarioInicio => horarioInicio;
    public TimeOnly HorarioFim => horarioFim;
    public DateTime DataSolicitacao => dataSolicitacao;
    public bool IsCancelada => cancelada;
    public DateTime? DataCancelamento => dataCancelamento;
    public string MotivoCancelamento => motivoCancelamento;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) 
        {
            return true;
        }

        if (obj is not Reserva outra) 
        {
            return false;
        }
        return identificador == outra.identificador;
    }

    public override int GetHashCode() => HashCode.Combine(identificador);

    public override string ToString()
        => identificador + " - " + areaComum.Nome + " - " + unidade.GetIdentificacaoCompleta()
            + " - " + GetPeriodoFormatado() + " - " + GetSituacaoFormatada();
}
