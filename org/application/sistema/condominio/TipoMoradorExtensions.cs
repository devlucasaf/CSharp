namespace Application.Sistema.Condominio;

public class TipoMoradorExtensions
{
    // --- RETORNA O NOME FORMATADO DO TIPO DE MORADOR ---
    public static string GetNomeFormatado(this TipoMorador tipo)
    {
        return tipo switch
        {
            TipoMorador.PROPRIETARIO => "Proprietário",
            TipoMorador.INQUILINO => "Inquilino",
            TipoMorador.DEPENDENTE => "Dependente",
            _ => throw new ArgumentOutOfRangeException(nameof(tipo))
        };
    }
}
