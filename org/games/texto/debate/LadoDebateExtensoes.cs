namespace Games.Texto.Debate;

public static class LadoDebateExtensoes
{
    // --- RETORNA O NOME FORMATADO DO LADO ---
    public static string GetNomeFormatado(this LadoDebate lado)
    {
        return lado switch
        {
            LadoDebate.Favoravel => "Favorável",
            LadoDebate.Contrario => "Contrário",
            _ => throw new ArgumentOutOfRangeException(nameof(lado))
        };
    }

    // --- RETORNA O LADO OPOSTO ---
    public static LadoDebate GetOposto(this LadoDebate lado)
    {
        return lado == LadoDebate.Favoravel ? LadoDebate.Contrario : LadoDebate.Favoravel;
    }
}