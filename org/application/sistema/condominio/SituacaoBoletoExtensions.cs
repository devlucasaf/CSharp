namespace Application.Sistema.Condominio;

public class SituacaoBoletoExtensions
{
    // --- RETORNA O NOME FORMATADO DA SITUAÇÃO ---
    public static string GetNomeFormatado(this SituacaoBoleto situacao)
    {
        return situacao switch
        {
            SituacaoBoleto.PENDENTE => "Pendente",
            SituacaoBoleto.PAGO => "Pago",
            SituacaoBoleto.VENCIDO => "Vencido",
            SituacaoBoleto.CANCELADO => "Cancelado",
            _ => throw new ArgumentOutOfRangeException(nameof(situacao))
        };
    }
}
