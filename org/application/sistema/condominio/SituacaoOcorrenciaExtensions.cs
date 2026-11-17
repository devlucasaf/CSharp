namespace Application.Sistema.Condominio;

public class SituacaoOcorrenciaExtensions
{
    // --- RETORNA O NOME FORMATADO DA SITUAÇÃO ---
    public static string GetNomeFormatado(this SituacaoOcorrencia situacao)
    {
        return situacao switch
        {
            SituacaoOcorrencia.ABERTA => "Aberta",
            SituacaoOcorrencia.EMANALISE => "Em análise",
            SituacaoOcorrencia.RESOLVIDA => "Resolvida",
            SituacaoOcorrencia.ARQUIVADA => "Arquivada",
            _ => throw new ArgumentOutOfRangeException(nameof(situacao))
        };
    }
}
