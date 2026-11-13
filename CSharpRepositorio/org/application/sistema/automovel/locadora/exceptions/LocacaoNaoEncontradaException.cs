namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Exceptions;

public class LocacaoNaoEncontradaException : Exception
{
    public LocacaoNaoEncontradaException(string mensagem) : base(mensagem)
    {
    }
}
