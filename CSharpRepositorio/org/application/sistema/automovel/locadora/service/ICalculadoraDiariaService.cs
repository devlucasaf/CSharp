using Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Service;

public interface ICalculadoraDiariaService
{
    double Calcular(Locacao locacao);
}
