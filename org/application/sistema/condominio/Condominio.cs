using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.System.Condominio;

public class Condominio
{
    private readonly string             nome;
    private readonly string             endereco;
    private readonly List<Morador>      moradores = new();
    private readonly List<Unidade>      unidades = new();
    private readonly List<AreaComum>    areasComuns = new();
    private readonly List<Reserva>      reservas = new();
    private readonly List<Boleto>       boletos = new();
    private readonly List<Ocorrencia>   ocorrencias = new();
    private readonly List<Multa>        multas = new();

    private long proximoMorador = 1;
    private long proximaUnidade = 1;
    private long proximaArea = 1;
    private long proximaReserva = 1;
    private long proximoBoleto = 1;
    private long proximaOcorrencia = 1;
    private long proximaMulta = 1;

    public Condominio(string nome, string endereco)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(endereco))
        {
            throw new ArgumentException("O nome e o endereço devem ser informados.");
        }

        this.nome = nome.Trim();
        this.endereco = endereco.Trim();
    }

    public Unidade CadastrarUnidade(string bloco, string numero, int quantidadeVagas)
    {
        if (BuscarUnidade(bloco, numero) != null)
        {
            throw new ArgumentException("A unidade já está cadastrada.");
        }

        Unidade unidade = new(proximaUnidade++, bloco, numero, quantidadeVagas);
        unidades.Add(unidade);
        return unidade;
    }

    public Morador CadastrarMorador(Unidade unidade, string nome, string cpf, DateOnly? dataNascimento, 
        string telefone, string email, TipoMorador tipo)
    {
        ValidarUnidade(unidade);

        if (BuscarMoradorPorCpf(cpf) != null)
        {
            throw new ArgumentException("Já existe um morador com esse CPF.");
        }

        Morador morador = new(proximoMorador++, nome, cpf, dataNascimento, telefone, email, tipo);
        moradores.Add(morador);
        unidade.AdicionarMorador(morador);
        return morador;
    }

    public AreaComum CadastrarAreaComum(string nome, int capacidade, decimal valorReserva,
        TimeOnly abertura, TimeOnly fechamento)
    {
        AreaComum area = new(proximaArea++, nome, capacidade, valorReserva, abertura, fechamento);
        areasComuns.Add(area);
        return area;
    }

    public Reserva ReservarArea(AreaComum area, Unidade unidade, Morador responsavel, DateOnly data, TimeOnly inicio, TimeOnly fim)
    {
        ValidarArea(area);
        ValidarUnidade(unidade);

        if (!area.IsAtiva)
        {
            throw new InvalidOperationException("A área comum está inativa.");
        }

        if (PossuiDebitoVencido(unidade))
        {
            throw new InvalidOperationException("A unidade possui débitos vencidos e não pode realizar reservas.");
        }

        Reserva novaReserva = new(proximaReserva, area, unidade, responsavel, data, inicio, fim);

        foreach (Reserva reserva in reservas)
        {
            if (novaReserva.ConflitaCom(reserva))
            { 
                throw new InvalidOperationException("A área já está reservada no período informado.");
            }
        }

        proximaReserva++;
        reservas.Add(novaReserva);

        if (area.ValorReserva > 0)
        {
            EmitirBoleto(unidade, "Reserva da área " + area.Nome, area.ValorReserva, DateOnly.FromDateTime(DateTime.Now).AddDays(5));
        }

        return novaReserva;
    }

    public Boleto EmitirBoleto(Unidade unidade, string descricao, decimal valor, DateOnly vencimento)
    {
        ValidarUnidade(unidade);
        Boleto boleto = new(proximoBoleto++, unidade, descricao, valor, vencimento);
        boletos.Add(boleto);
        return boleto;
    }

    public Ocorrencia RegistrarOcorrencia(Unidade unidade, Morador registrador,
                                            TipoOcorrencia tipo, string descricao)
    {
        ValidarUnidade(unidade);
        Ocorrencia ocorrencia = new(proximaOcorrencia++, unidade, registrador, tipo, descricao);
        ocorrencias.Add(ocorrencia);
        return ocorrencia;
    }

    public Multa AplicarMulta(Unidade unidade, Ocorrencia? ocorrencia, string motivo,
                                decimal valor, DateOnly vencimento)
    {
        ValidarUnidade(unidade);

        if (ocorrencia != null && !ocorrencia.Unidade.Equals(unidade))
        {
            throw new ArgumentException("A ocorrência não pertence à unidade informada.");
        }

        Multa multa = new(proximaMulta++, unidade, ocorrencia, motivo, valor, vencimento);
        multas.Add(multa);
        EmitirBoleto(unidade, "Multa: " + motivo, valor, vencimento);
        return multa;
    }

    public bool PossuiDebitoVencido(Unidade unidade)
    {
        ValidarUnidade(unidade);
        return boletos.Any(b => b.Unidade.Equals(unidade) && b.GetSituacao() == SituacaoBoleto.Vencido);
    }

    public decimal CalcularTotalEmAberto(Unidade unidade)
    {
        ValidarUnidade(unidade);

        decimal total = 0m;
        DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);

        foreach (Boleto boleto in boletos)
        {
            SituacaoBoleto situacao = boleto.GetSituacao();
            if (boleto.Unidade.Equals(unidade) && situacao != SituacaoBoleto.Pago && situacao != SituacaoBoleto.Cancelado)
            {
                total += boleto.CalcularValorAtualizado(hoje);
            }
        }

        return total;
    }

    public Unidade? BuscarUnidade(string bloco, string numero)
    {
        if (bloco == null || numero == null) 
        {
            return null;
        }

        return unidades.FirstOrDefault(u =>
            string.Equals(u.Bloco, bloco.Trim(), StringComparison.OrdinalIgnoreCase) &&
            string.Equals(u.Numero, numero.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public Morador? BuscarMoradorPorCpf(string cpf)
    {
        if (cpf == null) 
        {
            return null;
        }
        return moradores.FirstOrDefault(m => m.Cpf == cpf.Trim());
    }

    public List<Reserva> ListarReservasAtivas()
        => reservas.Where(r => !r.IsCancelada)
                    .OrderBy(r => r.Data)
                    .ThenBy(r => r.HorarioInicio)
                    .ToList();

    public List<Boleto> ListarBoletos(Unidade unidade)
    {
        ValidarUnidade(unidade);
        return boletos.Where(b => b.Unidade.Equals(unidade)).ToList();
    }

    public List<Ocorrencia> ListarOcorrencias(Unidade unidade)
    {
        ValidarUnidade(unidade);
        return ocorrencias.Where(o => o.Unidade.Equals(unidade)).ToList();
    }

    private void ValidarUnidade(Unidade unidade)
    {
        if (unidade == null || !unidades.Contains(unidade))
        {
            throw new ArgumentException("A unidade não pertence ao condomínio.");
        }
    }

    private void ValidarArea(AreaComum area)
    {
        if (area == null || !areasComuns.Contains(area))
        {
            throw new ArgumentException("A área comum não pertence ao condomínio.");
        }
    }

    public string Nome => nome;
    public string Endereco => endereco;
    public IReadOnlyList<Morador> Moradores => moradores.AsReadOnly();
    public IReadOnlyList<Unidade> Unidades => unidades.AsReadOnly();
    public IReadOnlyList<AreaComum> AreasComuns => areasComuns.AsReadOnly();
    public IReadOnlyList<Reserva> Reservas => reservas.AsReadOnly();
    public IReadOnlyList<Boleto> Boletos => boletos.AsReadOnly();
    public IReadOnlyList<Ocorrencia> Ocorrencias => ocorrencias.AsReadOnly();
    public IReadOnlyList<Multa> Multas => multas.AsReadOnly();
}
