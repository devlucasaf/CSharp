namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Exceptions;

public class ClienteNaoHabilitadoException : Exception
{
    public ClienteNaoHabilitadoException(string mensagem) : base(mensagem)
    {
    }
}
