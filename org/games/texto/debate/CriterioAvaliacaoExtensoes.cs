namespace Games.Texto.Debate;

public static class CriterioAvaliacaoExtensoes
{
    public static string GetNomeFormatado(this CriterioAvaliacao criterio)
    {
        return criterio switch
        {
            CriterioAvaliacao.Coerencia => "Coerência",
            CriterioAvaliacao.Relevancia => "Relevância",
            CriterioAvaliacao.Clareza => "Clareza",
            CriterioAvaliacao.Evidencias => "Evidências",
            CriterioAvaliacao.Estrutura => "Estrutura",
            CriterioAvaliacao.Vocabulario => "Vocabulário",
            _ => throw new ArgumentOutOfRangeException(nameof(criterio))
        };
    }
}