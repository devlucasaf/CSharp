namespace Games.Texto.Debate;

public class JogoDebate
{
    private readonly AvaliadorArgumentos _avaliador;
    private readonly InteligenciaArtificialDebate _inteligenciaArtificialDebate;
    private readonly List<RodadaDebate> _rodadas;
    private long _proximoIdentificadorArgumento;

    public string       NomeJogador             { get; }
    public TemaDebate   Tema                    { get; }
    public LadoDebate   LadoJogador             { get; }
    public LadoDebate   LadoIa                  { get; }
    public int          QuantidadeMaximaRodadas { get; }

    public JogoDebate(string nomeJogador, TemaDebate tema, LadoDebate ladoJogador, int quantidadeMaximaRodadas)
    {
        if (string.IsNullOrWhiteSpace(nomeJogador))
        {
            throw new ArgumentException("O nome do jogador não pode estar vazio.");
        }

        if (tema == null)
        {
            throw new ArgumentException("O tema e o lado do jogador devem ser informados.");
        }

        if (quantidadeMaximaRodadas <= 0 || quantidadeMaximaRodadas > 20)
        {
            throw new ArgumentException("A quantidade de rodadas deve estar entre 1 e 20.");
        }

        NomeJogador = nomeJogador.Trim();
        Tema = tema;
        LadoJogador = ladoJogador;
        LadoIa = ladoJogador.GetOposto();
        QuantidadeMaximaRodadas = quantidadeMaximaRodadas;
        _avaliador = new AvaliadorArgumentos();
        _inteligenciaArtificialDebate = new InteligenciaArtificialDebate();
        _rodadas = new List<RodadaDebate>();
        _proximoIdentificadorArgumento = 1;
    }

    public int RodadaAtual => _rodadas.Count;
    public IReadOnlyList<RodadaDebate> Rodadas => _rodadas.AsReadOnly();

    // --- EXECUTA UMA RODADA COMPLETA DO DEBATE ---
    public RodadaDebate ExecutarRodada(string textoJogador, EstrategiaArgumentacao estrategiaJogador)
    {
        if (IsFinalizado())
        {
            throw new InvalidOperationException("O debate já foi finalizado.");
        }

        if (string.IsNullOrWhiteSpace(textoJogador))
        {
            throw new ArgumentException("O argumento do jogador não pode estar vazio.");
        }

        if (textoJogador.Trim().Length < 20)
        {
            throw new ArgumentException("O argumento deve possuir pelo menos vinte caracteres.");
        }

        string? argumentoAnteriorIa = _rodadas.Count == 0 ? null : _rodadas[^1].ArgumentoInteligenciaArtificial.Texto;
        var argumentoJogador = new Argumento(_proximoIdentificadorArgumento++, NomeJogador, LadoJogador, estrategiaJogador, textoJogador);
        AvaliacaoArgumento avaliacaoJogador = _avaliador.Avaliar(argumentoJogador, Tema, argumentoAnteriorIa);

        EstrategiaArgumentacao estrategiaIa = EscolherEstrategiaIa();
        string textoIa = _inteligenciaArtificialDebate.CriarArgumento(Tema, LadoIa, estrategiaIa, textoJogador);
        var argumentoIa = new Argumento(_proximoIdentificadorArgumento++, "Argumentador virtual", LadoIa, estrategiaIa, textoIa);
        AvaliacaoArgumento avaliacaoIa = _avaliador.Avaliar(argumentoIa, Tema, textoJogador);

        var rodada = new RodadaDebate(_rodadas.Count + 1, argumentoJogador, avaliacaoJogador, argumentoIa, avaliacaoIa);
        _rodadas.Add(rodada);
        return rodada;
    }

    // --- ESCOLHE UMA ESTRATÉGIA PARA O OPONENTE ---
    private EstrategiaArgumentacao EscolherEstrategiaIa()
    {
        EstrategiaArgumentacao[] estrategias = Enum.GetValues<EstrategiaArgumentacao>();

        if (_rodadas.Count > 0 && Random.Shared.Next(2) == 0)
        {
            return EstrategiaArgumentacao.Refutacao;
        }

        return estrategias[Random.Shared.Next(estrategias.Length)];
    }

    // --- CALCULA A PONTUAÇÃO TOTAL DO JOGADOR ---
    public double CalcularPontuacaoJogador()
    {
        double total = 0;

        foreach (RodadaDebate rodada in _rodadas)
        {
            total += rodada.AvaliacaoJogador.PontuacaoTotal;
        }

        return total;
    }

    // --- CALCULA A PONTUAÇÃO TOTAL DA INTELIGÊNCIA ARTIFICIAL ---
    public double CalcularPontuacaoIa()
    {
        double total = 0;

        foreach (RodadaDebate rodada in _rodadas)
        {
            total += rodada.AvaliacaoInteligenciaArtificial.PontuacaoTotal;
        }

        return total;
    }

    // --- RETORNA A QUANTIDADE DE RODADAS VENCIDAS PELO JOGADOR ---
    public int ContarVitoriasJogador()
    {
        int quantidade = 0;

        foreach (RodadaDebate rodada in _rodadas)
        {
            if (rodada.IsVitoriaJogador())
            {
                quantidade++;
            }
        }

        return quantidade;
    }

    // --- RETORNA A QUANTIDADE DE RODADAS VENCIDAS PELA IA ---
    public int ContarVitoriasIa()
    {
        int quantidade = 0;

        foreach (RodadaDebate rodada in _rodadas)
        {
            if (!rodada.IsVitoriaJogador() && !rodada.IsEmpate())
            {
                quantidade++;
            }
        }

        return quantidade;
    }

    // --- RETORNA O RESULTADO FINAL DO DEBATE ---
    public string GetResultadoFinal()
    {
        if (!IsFinalizado())
        {
            return "O debate ainda está em andamento.";
        }

        double pontuacaoJogador = CalcularPontuacaoJogador();
        double pontuacaoIa = CalcularPontuacaoIa();

        if (Math.Abs(pontuacaoJogador - pontuacaoIa) < 0.0001)
        {
            return "O debate terminou empatado.";
        }

        return pontuacaoJogador > pontuacaoIa ? NomeJogador + " venceu o debate." : "O argumentador virtual venceu o debate.";
    }

    // --- VERIFICA SE TODAS AS RODADAS FORAM EXECUTADAS ---
    public bool IsFinalizado()
    {
        return _rodadas.Count >= QuantidadeMaximaRodadas;
    }
}
