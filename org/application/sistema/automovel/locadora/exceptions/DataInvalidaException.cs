namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Exceptions;

public class DataInvalidaException : Exception
{
    public DataInvalidaException(string mensagem) : base(mensagem)
    {
    }
}
