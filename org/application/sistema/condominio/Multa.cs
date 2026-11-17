using System;

namespace Application.System.Condominio;

public class Multa
{
    private readonly long           identificador;
    private readonly Unidade        unidade;
    private readonly Ocorrencia?    ocorrencia;
    private readonly string         motivo;
    private readonly decimal        valor;
    private readonly DateTime       dataAplicacao;
    private readonly DateOnly       dataVencimento;
    private bool                    paga;
    private bool                    cancelada;
    private DateTime?               dataPagamento;
    private DateTime?               dataCancelamento;
    private string                  motivoCancelamento;

    public Multa(long identificador, Unidade unidade, Ocorrencia? ocorrencia, string motivo, decimal valor, DateOnly dataVencimento)
    {
        ValidarIdentificador(identificador);
        ValidarUnidade(unidade);
        ValidarOcorrencia(unidade, ocorrencia);
        ValidarMotivo(motivo);
        ValidarValor(valor);
        ValidarDataVencimento(dataVencimento);

        this.identificador = identificador;
        this.unidade = unidade;
        this.ocorrencia = ocorrencia;
        this.motivo = motivo.Trim();
        this.valor = Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        this.dataAplicacao = DateTime.Now;
        this.dataVencimento = dataVencimento;
        this.paga = false;
        this.cancelada = false;
        this.motivoCancelamento = "";
    }

    public void Pagar()
    {
        if (cancelada)
        {
            throw new InvalidOperationException("Uma multa cancelada não pode ser paga.");
        }

        if (paga)
        {
            throw new InvalidOperationException("A multa já foi paga.");
        }

        paga = true;
        dataPagamento = DateTime.Now;
    }

    public void Cancelar() => Cancelar("Motivo não informado.");

    public void Cancelar(string motivoCancelamento)
    {
        if (paga)
        {
            throw new InvalidOperationException("Uma multa paga não pode ser cancelada.");
        }

        if (cancelada)
        {
            throw new InvalidOperationException("A multa já está cancelada.");
        }

        this.cancelada = true;
        this.dataCancelamento = DateTime.Now;
        this.motivoCancelamento = string.IsNullOrWhiteSpace(motivoCancelamento)
            ? "Motivo não informado." : motivoCancelamento.Trim();
    }

    public bool IsPendente => !paga && !cancelada;
    public bool IsVencida() => IsVencida(DateOnly.FromDateTime(DateTime.Now));

    public bool IsVencida(DateOnly dataReferencia) => IsPendente && dataReferencia > dataVencimento;

    public long CalcularDiasAtraso() => CalcularDiasAtraso(DateOnly.FromDateTime(DateTime.Now));

    public long CalcularDiasAtraso(DateOnly dataReferencia)
    {
        if (!IsVencida(dataReferencia)) 
        {
            return 0;
        }
        return dataReferencia.DayNumber - dataVencimento.DayNumber;
    }

    public decimal CalcularValorAtualizado() => CalcularValorAtualizado(DateOnly.FromDateTime(DateTime.Now));

    public decimal CalcularValorAtualizado(DateOnly dataReferencia)
    {
        if (paga || cancelada || !IsVencida(dataReferencia))
        {
            return valor;
        }

        long diasAtraso = CalcularDiasAtraso(dataReferencia);
        decimal multaAtraso = valor * 0.02m;
        decimal jurosDiarios = valor * 0.00033m * diasAtraso;

        return Math.Round(valor + multaAtraso + jurosDiarios, 2, MidpointRounding.AwayFromZero);
    }

    public string GetSituacaoFormatada()
    {
        if (cancelada)
        {
            return "Cancelada";
        }

        if (paga)
        {
            return "Paga";
        }

        if (IsVencida())
        {
            return "Vencida";
        }

        return "Pendente";
    }

    public string GetDataAplicacaoFormatada() => FormatarDataHora(dataAplicacao);
    public string GetDataVencimentoFormatada() => dataVencimento.ToString("dd/MM/yyyy");
    public string GetDataPagamentoFormatada() => FormatarDataHora(dataPagamento);
    public string GetDataCancelamentoFormatada() => FormatarDataHora(dataCancelamento);

    private static string FormatarDataHora(DateTime? data)
        => data.HasValue ? data.Value.ToString("dd/MM/yyyy HH:mm:ss") : "Não informada";

    private void ValidarIdentificador(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }
    }

    private void ValidarUnidade(Unidade u)
    {
        if (u == null)
        {
            throw new ArgumentException("A unidade não pode ser nula.");
        }
    }

    private void ValidarOcorrencia(Unidade u, Ocorrencia? o)
    {
        if (o != null && !u.Equals(o.Unidade))
        {
            throw new ArgumentException("A ocorrência não pertence à unidade informada.");
        }
    }

    private void ValidarMotivo(string m)
    {
        if (string.IsNullOrWhiteSpace(m))
        {
            throw new ArgumentException("O motivo não pode estar vazio.");
        }
    }

    private void ValidarValor(decimal v)
    {
        if (v <= 0)
        {
            throw new ArgumentException("O valor da multa deve ser maior que zero.");
        }
    }

    private void ValidarDataVencimento(DateOnly d)
    {
        if (d < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("A data de vencimento não pode estar no passado.");
        }
    }

    public long Identificador => identificador;
    public Unidade Unidade => unidade;
    public Ocorrencia? Ocorrencia => ocorrencia;
    public string Motivo => motivo;
    public decimal Valor => valor;
    public DateTime DataAplicacao => dataAplicacao;
    public DateOnly DataVencimento => dataVencimento;
    public bool IsPaga => paga;
    public bool IsCancelada => cancelada;
    public DateTime? DataPagamento => dataPagamento;
    public DateTime? DataCancelamento => dataCancelamento;
    public string MotivoCancelamento => motivoCancelamento;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is not Multa outra)
        {
            return false;
        }

        return identificador == outra.identificador;
    }

    public override int GetHashCode() => HashCode.Combine(identificador);

    public override string ToString()
        => identificador + " - " + unidade.GetIdentificacaoCompleta() + " - " + motivo
            + " - R$ " + valor + " - " + GetSituacaoFormatada();
}
