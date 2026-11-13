namespace Application.Sistema.Animais.Zoologico;

public static class SexoExtensoes
{
    // --- RETORNA A DESCRIÇÃO DO SEXO ---
    public static string GetDescricao(this Sexo sexo)
    {
        return sexo switch
        {
            Sexo.MACHO => "Macho",
            Sexo.FEMEA => "Femea",
            _ => throw new ArgumentOutOfRangeException(nameof(sexo))
        };
    }
}
