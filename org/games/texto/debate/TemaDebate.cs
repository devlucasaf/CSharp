namespace Games.Texto.Debate;

public class TemaDebate
{
    private readonly List<string> _palavrasChave;
    private readonly List<string> _argumentosFavoraveis;
    private readonly List<string> _argumentosContrarios;

    public long     Identificador   { get; }
    public string   Titulo          { get; }
    public string   Descricao       { get; }

    public TemaDebate(long identificador, string titulo, string descricao, IEnumerable<string>? palavrasChave,
                        IEnumerable<string>? argumentosFavoraveis, IEnumerable<string>? argumentosContrarios)
    {
        ValidarIdentificador(identificador);
        ValidarTexto(titulo, "O título");
        ValidarTexto(descricao, "A descrição");

        Identificador = identificador;
        Titulo = titulo.Trim();
        Descricao = descricao.Trim();
        _palavrasChave = NormalizarLista(palavrasChave);
        _argumentosFavoraveis = NormalizarLista(argumentosFavoraveis);
        _argumentosContrarios = NormalizarLista(argumentosContrarios);
    }

    public IReadOnlyList<string> PalavrasChave => _palavrasChave.AsReadOnly();
    public IReadOnlyList<string> ArgumentosFavoraveis => _argumentosFavoraveis.AsReadOnly();
    public IReadOnlyList<string> ArgumentosContrarios => _argumentosContrarios.AsReadOnly();

    // --- RETORNA OS ARGUMENTOS COMPATÍVEIS COM O LADO ---
    public IReadOnlyList<string> GetArgumentosPorLado(LadoDebate lado)
    {
        return lado == LadoDebate.Favoravel ? ArgumentosFavoraveis : ArgumentosContrarios;
    }

    // --- NORMALIZA UMA LISTA DE TEXTOS ---
    private static List<string> NormalizarLista(IEnumerable<string>? valores)
    {
        var resultado = new List<string>();

        if (valores == null)
        {
            return resultado;
        }

        foreach (string valor in valores)
        {
            if (!string.IsNullOrWhiteSpace(valor))
            {
                resultado.Add(valor.Trim());
            }
        }

        return resultado;
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

        if (objeto is not TemaDebate outroTema)
        {
            return false;
        }

        return Identificador == outroTema.Identificador;
    }

    public override int GetHashCode()
    {
        return Identificador.GetHashCode();
    }

    public override string ToString()
    {
        return Identificador + " - " + Titulo;
    }
}
