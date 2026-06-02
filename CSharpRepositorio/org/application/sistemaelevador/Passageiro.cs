namespace Org.Application.SistemaElevador;

public class Passageiro
{
    private int     _andarOrigem;
    private int     _andarDestino;
    private bool    _noElevador;

    public Passageiro(int andarOrigem, int andarDestino)
    {
        _andarOrigem = andarOrigem;
        _andarDestino = andarDestino;
        _noElevador = false;
    }

    public int GetAndarOrigem() => _andarOrigem;
    public int GetAndarDestino() => _andarDestino;
    public bool IsNoElevador() => _noElevador;
    public void SetNoElevador(bool noElevador) => _noElevador = noElevador;

    public override string ToString()
    {
        string estado = _noElevador ? "🟢" : "⚪";
        return $"{estado}👤({_andarOrigem}->{_andarDestino})";
    }
}