namespace Application.System.Condominio;

public class TipoOcorrenciaExtensions
{
    // --- RETORNA O NOME FORMATADO DO TIPO DE OCORRÊNCIA ---
    public static string GetNomeFormatado(this TipoOcorrencia tipo)
    {
        return tipo switch
        {
            TipoOcorrencia.BARULHO => "Barulho",
            TipoOcorrencia.DANOS => "Danos",
            TipoOcorrencia.SEGURANCA => "Segurança",
            TipoOcorrencia.LIMPEZA => "Limpeza",
            TipoOcorrencia.ESTACIONAMENTO => "Estacionamento",
            TipoOcorrencia.DESCUMPRIMENTOREGRA => "Descumprimento de regra",
            TipoOcorrencia.OUTRO => "Outro",
            _ => throw new ArgumentOutOfRangeException(nameof(tipo))
        };
    }
}
