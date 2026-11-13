using Application.Sistema.Automovel.LocadoraDeVeiculos.Enums;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Service;

public class RelatorioService
{
    private readonly Locadora _locadora;

    public RelatorioService(Locadora locadora)
    {
        _locadora = locadora;
    }

    public void ExibirVeiculosDisponiveis()
    {
        Console.WriteLine("\n=== VEÍCULOS DISPONÍVEIS ===");
        foreach (Veiculo v in _locadora.Veiculos)
        {
            if (v.Disponivel)
            {
                v.ExibirInformacoes();
                Console.WriteLine("------------------");
            }
        }
    }

    public void ExibirLocacoesAtivas()
    {
        Console.WriteLine("\n=== LOCAÇÕES ATIVAS ===");
        foreach (Locacao l in _locadora.Locacoes)
        {
            if (l.Status == StatusLocacao.ATIVA)
            {
                l.ExibirInformacoes();
                Console.WriteLine("------------------");
            }
        }
    }

    public void ExibirHistoricoLocacoes()
    {
        Console.WriteLine("\n=== HISTÓRICO DE LOCAÇÕES ===");
        foreach (Locacao l in _locadora.Locacoes)
        {
            l.ExibirInformacoes();
            Console.WriteLine("------------------");
        }
    }

    public void ExibirRelatorioFinanceiro()
    {
        double totalFaturado = _locadora.Locacoes.Sum(l => l.ValorTotal);
        Console.WriteLine("\n=== RELATÓRIO FINANCEIRO ===");
        Console.WriteLine($"Caixa atual: R${_locadora.Caixa:F2}");
        Console.WriteLine($"Total faturado (histórico): R${totalFaturado:F2}");
        Console.WriteLine("Total de locações realizadas: " + _locadora.Locacoes.Count);
    }

    public void ExibirClientesFieis()
    {
        Console.WriteLine("\n=== CLIENTES COM MAIS PONTOS ===");
        foreach (Cliente c in _locadora.Clientes.OrderByDescending(c => c.PontosFidelidade).Take(5))
        {
            Console.WriteLine(c.Nome + " - Pontos: " + c.PontosFidelidade);
        }
    }
}
