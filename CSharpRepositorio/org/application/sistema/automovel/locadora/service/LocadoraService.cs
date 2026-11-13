using Application.Sistema.Automovel.LocadoraDeVeiculos.Enums;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Exceptions;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Service;

public class LocadoraService
{
    private readonly Locadora _locadora;
    private readonly ICalculadoraDiariaService _calculadora;

    public LocadoraService(Locadora locadora, ICalculadoraDiariaService calculadora)
    {
        _locadora = locadora;
        _calculadora = calculadora;
    }

    // --- CADASTROS ---
    public void CadastrarVeiculo(Veiculo veiculo)
    {
        _locadora.AdicionarVeiculo(veiculo);
    }

    public void CadastrarCliente(Cliente cliente)
    {
        _locadora.AdicionarCliente(cliente);
    }

    // --- LÓGICA DE LOCAÇÃO ---
    public Locacao RealizarLocacao(string placa, string cpfCliente, DateOnly dataRetirada,
            DateOnly dataDevolucaoPrevista, TipoSeguro seguro, FormaPagamento formaPagamento)
    {
        // --- VALIDA AS DATAS ---
        if (dataDevolucaoPrevista <= dataRetirada)
        {
            throw new DataInvalidaException("A data de devolução deve ser posterior à data de retirada.");
        }

        Veiculo? veiculo = _locadora.BuscarVeiculoPorPlaca(placa);
        if (veiculo == null)
        {
            throw new VeiculoIndisponivelException("Veículo com placa " + placa + " não encontrado.");
        }

        if (!veiculo.Disponivel)
        {
            throw new VeiculoIndisponivelException("Veículo com placa " + placa + " não está disponível.");
        }

        Cliente? cliente = _locadora.BuscarClientePorCpf(cpfCliente);
        if (cliente == null)
        {
            throw new ClienteNaoHabilitadoException("Cliente com CPF " + cpfCliente + " não cadastrado.");
        }

        if (!cliente.IsHabilitado())
        {
            throw new ClienteNaoHabilitadoException("Cliente " + cliente.Nome + " não está habilitado (CNH vencida ou inativa).");
        }

        var locacao = new Locacao(veiculo, cliente, dataRetirada, dataDevolucaoPrevista, seguro, formaPagamento);
        double valor = _calculadora.Calcular(locacao);
        locacao.ValorTotal = valor;

        veiculo.Alugar();
        cliente.AdicionarLocacao(locacao);
        cliente.AdicionarPontos((int)(valor / 50));

        _locadora.AdicionarLocacao(locacao);
        return locacao;
    }

    public void DevolverVeiculo(int idLocacao, DateOnly dataDevolucaoReal, double quilometragemAtual)
    {
        Locacao? locacao = _locadora.BuscarLocacaoPorId(idLocacao);
        if (locacao == null)
        {
            throw new LocacaoNaoEncontradaException("Locação #" + idLocacao + " não encontrada.");
        }

        if (locacao.Status != StatusLocacao.ATIVA)
        {
            throw new LocacaoNaoEncontradaException("Locação #" + idLocacao + " não está ativa.");
        }

        if (dataDevolucaoReal < locacao.DataRetirada)
        {
            throw new DataInvalidaException("Data de devolução não pode ser anterior à retirada.");
        }

        long diasPrevistos = locacao.CalcularDiasPrevistos();
        long diasEfetivos = Math.Max(1L, dataDevolucaoReal.DayNumber - locacao.DataRetirada.DayNumber);
        double multa = 0.0;

        if (diasEfetivos > diasPrevistos)
        {
            multa = (diasEfetivos - diasPrevistos) * locacao.Veiculo.ValorDiaria * 0.5;
        }

        double valorBase = locacao.Veiculo.ValorDiaria * diasEfetivos;
        double adicionalSeguro = locacao.Seguro == TipoSeguro.COMPLETO ? 50.0 : 0.0;
        double valorTotal = valorBase + adicionalSeguro + multa;

        locacao.Finalizar(dataDevolucaoReal, valorTotal);
        locacao.Veiculo.Devolver(quilometragemAtual);
        _locadora.AdicionarCaixa(valorTotal);
        Console.WriteLine($"Devolução concluída. Valor total: R${valorTotal:F2}");
    }

    public void CancelarLocacao(int idLocacao)
    {
        Locacao? locacao = _locadora.BuscarLocacaoPorId(idLocacao);
        if (locacao == null || locacao.Status != StatusLocacao.ATIVA)
        {
            throw new LocacaoNaoEncontradaException("Locação ativa #" + idLocacao + " não encontrada.");
        }

        locacao.Cancelar();
        Console.WriteLine("Locação #" + idLocacao + " cancelada.");
    }

    // --- CONSULTAS ---
    public List<Veiculo> ListarVeiculosDisponiveis()
    {
        var disponiveis = new List<Veiculo>();
        foreach (Veiculo v in _locadora.Veiculos)
        {
            if (v.Disponivel)
            {
                disponiveis.Add(v);
            }
        }

        return disponiveis;
    }

    public List<Locacao> ListarLocacoesAtivas()
    {
        var ativas = new List<Locacao>();
        foreach (Locacao l in _locadora.Locacoes)
        {
            if (l.Status == StatusLocacao.ATIVA)
            {
                ativas.Add(l);
            }
        }

        return ativas;
    }

    public Cliente? BuscarClientePorCpf(string cpf)
    {
        return _locadora.BuscarClientePorCpf(cpf);
    }
}
