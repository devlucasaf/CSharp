namespace Games.Texto.Debate;

public class Argumento
{
    public long                     Identificador       { get; }
    public string                   Autor               { get; }
    public LadoDebate               Lado                { get; }
    public EstrategiaArgumentacao   Estrategia          { get; }
    public string                   Texto               { get; }
    public DateTime                 DataApresentacao    { get; }

    public Argumento(long identificador, string autor, LadoDebate lado, EstrategiaArgumentacao estrategia, string texto)
    {
        ValidarIdentificador(identificador);
        ValidarTexto(autor, "O autor");
        ValidarTexto(texto, "O argumento");

        Identificador = identificador;
        Autor = autor.Trim();
        Lado = lado;
        Estrategia = estrategia;
        Texto = texto.Trim();
        DataApresentacao = DateTime.Now;
    }

    // --- RETORNA A QUANTIDADE APROXIMADA DE PALAVRAS ---
    public int ContarPalavras()
    {
        if (string.IsNullOrWhiteSpace(Texto))
        {
            return 0;
        }

        return Texto.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    // --- VALIDA O IDENTIFICADOR ---
    private static void ValidarIdentificador(long identificador)
    {
        if (identificador <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }
    }

    // --- VALIDA UM TEXTO OBRIGATÓRIO ---
    private static void ValidarTexto(string? texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException(campo + " não pode estar vazio.");
        }
    }

    public override bool Equals(object? objeto)
    {
        if (ReferenceEquals(this, objeto))
        {
            return true;
        }

        if (objeto is not Argumento outroArgumento)
        {
            return false;
        }

        return Identificador == outroArgumento.Identificador;
    }

    public override int GetHashCode()
    {
        return Identificador.GetHashCode();
    }

    public override string ToString()
    {
        return Autor + ": " + Texto;
    }
}
