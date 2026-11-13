using Application.Sistema.Automovel.LocadoraDeVeiculos.Enums;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Service;

public class CalculadoraPadraoService : ICalculadoraDiariaService
{
    public double Calcular(Locacao locacao)
    {
        long dias = locacao.CalcularDiasPrevistos();
        if (dias <= 0)
        {
            dias = 1;
        }

        double valorDiaria = locacao.Veiculo.ValorDiaria;
        double valorBase = valorDiaria * dias;

        double adicionalSeguro = 0.0;
        if (locacao.Seguro == TipoSeguro.COMPLETO)
        {
            adicionalSeguro = 50.0 * dias;
        }
        else if (locacao.Seguro == TipoSeguro.APENAS_TERCEIROS)
        {
            adicionalSeguro = 20.0 * dias;
        }

        double desconto = 0.0;
        if (locacao.Cliente.PontosFidelidade >= 100)
        {
            desconto = 0.05;
        }

        double total = (valorBase + adicionalSeguro) * (1 - desconto);

        // --- ARREDONDAMENTO "MEIO PARA CIMA", COMO O Math.round DO JAVA ---
        return Math.Floor(total * 100.0 + 0.5) / 100.0;
    }
}
