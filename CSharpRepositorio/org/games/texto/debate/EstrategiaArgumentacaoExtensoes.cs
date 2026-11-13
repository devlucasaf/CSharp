namespace Games.Texto.Debate;

public static class EstrategiaArgumentacaoExtensoes
{
    // --- RETORNA O NOME FORMATADO DA ESTRATÉGIA ---
    public static string GetNomeFormatado(this EstrategiaArgumentacao estrategia)
    {
        return estrategia switch
        {
            EstrategiaArgumentacao.CausaEEfeito => "Causa e efeito",
            EstrategiaArgumentacao.ExemploPratico => "Exemplo prático",
            EstrategiaArgumentacao.Comparacao => "Comparação",
            EstrategiaArgumentacao.Refutacao => "Refutação",
            EstrategiaArgumentacao.PropostaSolucao => "Proposta de solução",
            EstrategiaArgumentacao.PrincipioGeral => "Princípio geral",
            _ => throw new ArgumentOutOfRangeException(nameof(estrategia))
        };
    }

    // --- RETORNA UMA ORIENTAÇÃO PARA O JOGADOR ---
    public static string GetOrientacao(this EstrategiaArgumentacao estrategia)
    {
        return estrategia switch
        {
            EstrategiaArgumentacao.CausaEEfeito => "Explique como uma ação produz determinada consequência.",
            EstrategiaArgumentacao.ExemploPratico => "Apresente uma situação concreta que sustente sua posição.",
            EstrategiaArgumentacao.Comparacao => "Compare duas situações e destaque diferenças relevantes.",
            EstrategiaArgumentacao.Refutacao => "Responda a uma ideia contrária e explique por que ela é insuficiente.",
            EstrategiaArgumentacao.PropostaSolucao => "Apresente uma solução e explique como ela poderia funcionar.",
            EstrategiaArgumentacao.PrincipioGeral => "Defenda sua posição com base em um princípio ou valor geral.",
            _ => throw new ArgumentOutOfRangeException(nameof(estrategia))
        };
    }
}