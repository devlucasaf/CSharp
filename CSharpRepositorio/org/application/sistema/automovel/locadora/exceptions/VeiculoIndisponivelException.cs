namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Exceptions;

public class VeiculoIndisponivelException : Exception
{
    public VeiculoIndisponivelException(string mensagem) : base(mensagem)
    {
    }
}
