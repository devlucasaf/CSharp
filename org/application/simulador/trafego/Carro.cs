namespace Application.Simulador.Trafego;

public class Carro
{
    private readonly int    id;
    private int             posicao;
    private int             velocidade;
    private readonly int    velocidadeMaxima;
    private int             ticksParado;

    public Carro(int id, int posicaoInicial, int velocidadeMaxima)
    {
        this.id = id;
        this.posicao = posicaoInicial;
        this.velocidadeMaxima = velocidadeMaxima;
        this.velocidade = velocidadeMaxima;
        this.ticksParado = 0;
    }

    // --- RETORNA O IDENTIFICADOR DO CARRO ---
    public int Id => id;

    // --- RETORNA A POSIÇÃO ATUAL DO CARRO ---
    public int Posicao => posicao;

    // --- RETORNA A VELOCIDADE ATUAL ---
    public int Velocidade => velocidade;

    // --- RETORNA QUANTOS TICKS O CARRO FICOU PARADO ---
    public int TicksParado => ticksParado;

    // --- ATUALIZA A VELOCIDADE E AVANÇA CONFORME O ESPAÇO LIVRE E O SEMÁFORO ---
    public void Mover(int distanciaLivre, bool semaforoLiberado)
    {
        if (velocidade < velocidadeMaxima)
        {
            velocidade++;
        }

        int distanciaMaximaPermitida = semaforoLiberado ? distanciaLivre : Math.Max(0, distanciaLivre);
        int deslocamento = Math.Min(velocidade, distanciaMaximaPermitida);

        if (deslocamento == 0)
        {
            ticksParado++;
            velocidade = 0;
        }

        posicao += deslocamento;
    }
}
