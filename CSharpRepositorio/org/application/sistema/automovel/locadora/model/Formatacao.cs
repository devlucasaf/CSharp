namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public static class Formatacao
{
    // --- FORMATA UMA DATA NO PADRÃO ISO ---
    public static string Data(DateOnly? data)
    {
        return data == null ? "null" : data.Value.ToString("yyyy-MM-dd");
    }
}
