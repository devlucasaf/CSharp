namespace Games.Texto.Debate;

public class RodadaDebate
{
    public int                  Numero                          { get; }
    public Argumento            ArgumentoJogador                { get; }
    public AvaliacaoArgumento   AvaliacaoJogador                { get; }
    public Argumento            ArgumentoInteligenciaArtificial { get; }
    public AvaliacaoArgumento   AvaliacaoInteligenciaArtificial { get; }

    public RodadaDebate(int numero, Argumento argumentoJogador, AvaliacaoArgumento avaliacaoJogador,
                        Argumento argumentoInteligenciaArtificial, AvaliacaoArgumento avaliacaoInteligenciaArtificial)
    {
        if (numero <= 0)
        {
            throw new ArgumentException("O número da rodada deve ser maior que zero.");
        }

        if (argumentoJogador == null || avaliacaoJogador == null || argumentoInteligenciaArtificial == null || avaliacaoInteligenciaArtificial == null)
        {
            throw new ArgumentException("Todos os dados da rodada devem ser informados.");
        }

        Numero = numero;
        ArgumentoJogador = argumentoJogador;
        AvaliacaoJogador = avaliacaoJogador;
        ArgumentoInteligenciaArtificial = argumentoInteligenciaArtificial;
        AvaliacaoInteligenciaArtificial = avaliacaoInteligenciaArtificial;
    }

    // --- RETORNA A DIFERENÇA DE PONTOS DA RODADA ---
    public double CalcularDiferencaPontos()
    {
        return AvaliacaoJogador.PontuacaoTotal - AvaliacaoInteligenciaArtificial.PontuacaoTotal;
    }

    // --- VERIFICA SE O JOGADOR VENCEU A RODADA ---
    public bool IsVitoriaJogador()
    {
        return CalcularDiferencaPontos() > 0;
    }

    // --- VERIFICA SE A RODADA TERMINOU EMPATADA ---
    public bool IsEmpate()
    {
        return Math.Abs(CalcularDiferencaPontos()) < 0.0001;
    }
}
