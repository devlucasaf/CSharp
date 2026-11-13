namespace Games.Texto.Debate;

public class InteligenciaArtificialDebate
{
    private readonly Random _random;
    private readonly List<string> _argumentosUtilizados;

    public InteligenciaArtificialDebate()
    {
        _random = new Random();
        _argumentosUtilizados = new List<string>();
    }

    // --- CRIA UM ARGUMENTO PARA A INTELIGÊNCIA ARTIFICIAL ---
    public string CriarArgumento(TemaDebate tema, LadoDebate lado, EstrategiaArgumentacao estrategia, string? argumentoAnterior)
    {
        if (tema == null)
        {
            throw new ArgumentException("O tema, o lado e a estratégia devem ser informados.");
        }

        string argumentoBase = EscolherArgumentoBase(tema.GetArgumentosPorLado(lado));
        return AplicarEstrategia(argumentoBase, estrategia, argumentoAnterior);
    }

    // --- ESCOLHE UM ARGUMENTO AINDA NÃO UTILIZADO ---
    private string EscolherArgumentoBase(IReadOnlyList<string> argumentos)
    {
        if (argumentos.Count == 0)
        {
            return "A questão precisa ser observada por diferentes perspectivas antes que uma conclusão definitiva seja estabelecida.";
        }

        List<string> disponiveis = argumentos.Where(argumento => !_argumentosUtilizados.Contains(argumento)).ToList();

        if (disponiveis.Count == 0)
        {
            _argumentosUtilizados.Clear();
            disponiveis = argumentos.ToList();
        }

        string escolhido = disponiveis[_random.Next(disponiveis.Count)];
        _argumentosUtilizados.Add(escolhido);
        return escolhido;
    }

    // --- ADAPTA O ARGUMENTO À ESTRATÉGIA SELECIONADA ---
    private string AplicarEstrategia(string argumentoBase, EstrategiaArgumentacao estrategia, string? argumentoAnterior)
    {
        return estrategia switch
        {
            EstrategiaArgumentacao.CausaEEfeito => argumentoBase + " Isso ocorre porque decisões bem planejadas produzem consequências mais previsíveis e sustentáveis.",
            EstrategiaArgumentacao.ExemploPratico => "Como exemplo prático, podemos observar situações em que medidas semelhantes produziram resultados mensuráveis. " + argumentoBase,
            EstrategiaArgumentacao.Comparacao => "Ao comparar soluções tradicionais com abordagens alternativas, percebemos diferenças importantes. " + argumentoBase,
            EstrategiaArgumentacao.Refutacao => CriarRefutacao(argumentoBase, argumentoAnterior),
            EstrategiaArgumentacao.PropostaSolucao => argumentoBase + " Uma solução possível seria implementar a proposta gradualmente, avaliando seus resultados e corrigindo possíveis falhas.",
            EstrategiaArgumentacao.PrincipioGeral => "Com base nos princípios de responsabilidade, equilíbrio e benefício coletivo, " + IniciarComMinuscula(argumentoBase),
            _ => throw new ArgumentOutOfRangeException(nameof(estrategia))
        };
    }

    // --- CRIA UMA REFUTAÇÃO AO ARGUMENTO ANTERIOR ---
    private string CriarRefutacao(string argumentoBase, string? argumentoAnterior)
    {
        if (string.IsNullOrWhiteSpace(argumentoAnterior))
        {
            return "Embora existam opiniões contrárias, elas não eliminam os benefícios apresentados. " + argumentoBase;
        }

        return "O argumento anterior apresenta uma perspectiva relevante, contudo sua conclusão é insuficiente porque não considera todos os efeitos envolvidos. " + argumentoBase;
    }

    // --- CONVERTE O PRIMEIRO CARACTERE PARA MINÚSCULO ---
    private string IniciarComMinuscula(string? texto)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return "";
        }

        return char.ToLowerInvariant(texto[0]) + texto.Substring(1);
    }

    // --- LIMPA O HISTÓRICO DE ARGUMENTOS UTILIZADOS ---
    public void Reiniciar()
    {
        _argumentosUtilizados.Clear();
    }
}
