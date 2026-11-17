using System;

namespace Application.System.Condominio;

public class Boleto
{
    private readonly long       identificador;
    private readonly Unidade    unidade;
    private readonly string     descricao;
    private readonly decimal    valorOriginal;
    private readonly DateOnly   dataEmissao;
    private readonly DateOnly   dataVencimento;
    private SituacaoBoleto      situacao;
    private DateTime?           dataPagamento;
    private decimal?            valorPago;

    public Boleto(long identificador, Unidade unidade, string descricao, decimal valorOriginal, DateOnly dataVencimento)
    {
        if (identificador <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }

        if (unidade == null)
        {
            throw new ArgumentException("A unidade não pode ser nula.");
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição não pode estar vazia.");
        }

        if (valorOriginal <= 0)
        {
            throw new ArgumentException("O valor deve ser maior que zero.");
        }

        this.identificador = identificador;
        this.unidade = unidade;
        this.descricao = descricao.Trim();
        this.valorOriginal = Math.Round(valorOriginal, 2, MidpointRounding.AwayFromZero);
        this.dataEmissao = DateOnly.FromDateTime(DateTime.Now);
        this.dataVencimento = dataVencimento;
        this.situacao = SituacaoBoleto.Pendente;
    }

    // --- CALCULA O VALOR ATUALIZADO DO BOLETO ---
    public decimal CalcularValorAtualizado(DateOnly dataReferencia)
    {
        AtualizarSituacao(dataReferencia);

        if (situacao != SituacaoBoleto.Vencido)
        {
            return valorOriginal;
        }

        long diasAtraso = dataReferencia.DayNumber - dataVencimento.DayNumber;
        decimal multa = valorOriginal * 0.02m;
        decimal jurosDiarios = valorOriginal * 0.00033m * diasAtraso;
        return Math.Round(valorOriginal + multa + jurosDiarios, 2, MidpointRounding.AwayFromZero);
    }

    // --- REGISTRA O PAGAMENTO DO BOLETO ---
    public void Pagar(decimal valorPago)
    {
        if (situacao == SituacaoBoleto.Pago || situacao == SituacaoBoleto.Cancelado)
        {
            throw new InvalidOperationException("Este boleto não pode ser pago.");
        }

        decimal valorDevido = CalcularValorAtualizado(DateOnly.FromDateTime(DateTime.Now));

        if (valorPago < valorDevido)
        {
            throw new ArgumentException($"O valor pago é menor que o valor devido de R$ {valorDevido}.");
        }

        this.valorPago = Math.Round(valorPago, 2, MidpointRounding.AwayFromZero);
        this.dataPagamento = DateTime.Now;
        this.situacao = SituacaoBoleto.Pago;
    }

    // --- ATUALIZA A SITUAÇÃO DO BOLETO ---
    public void AtualizarSituacao(DateOnly dataReferencia)
    {
        if (situacao == SituacaoBoleto.Pendente && dataReferencia > dataVencimento)
        {
            situacao = SituacaoBoleto.Vencido;
        }
    }

    // --- CANCELA O BOLETO ---
    public void Cancelar()
    {
        if (situacao == SituacaoBoleto.Pago)
        {
            throw new InvalidOperationException("Um boleto pago não pode ser cancelado.");
        }
        situacao = SituacaoBoleto.Cancelado;
    }

    public long Identificador => identificador;
    public Unidade Unidade => unidade;
    public string Descricao => descricao;
    public decimal ValorOriginal => valorOriginal;
    public DateOnly DataEmissao => dataEmissao;
    public DateOnly DataVencimento => dataVencimento;

    public SituacaoBoleto GetSituacao()
    {
        AtualizarSituacao(DateOnly.FromDateTime(DateTime.Now));
        return situacao;
    }

    public DateTime? DataPagamento => dataPagamento;
    public decimal? ValorPago => valorPago;
}
