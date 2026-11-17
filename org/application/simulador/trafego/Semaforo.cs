namespace Application.Simulador.Trafego;

public class Semaforo
{
    private Estado          estado;
    private readonly int    duracaoVerde;
    private readonly int    duracaoAmarelo;
    private readonly int    duracaoVermelho;
    private int             contador;

    // --- DEFINE AS DURAÇÕES E O ESTADO INICIAL DO SEMÁFORO ---
    public Semaforo(int duracaoVerde, int duracaoAmarelo, int duracaoVermelho, Estado estadoInicial)
    {
        this.duracaoVerde = duracaoVerde;
        this.duracaoAmarelo = duracaoAmarelo;
        this.duracaoVermelho = duracaoVermelho;
        this.estado = estadoInicial;
        this.contador = 0;
    }

    // --- AVANÇA UM TICK E TROCA DE ESTADO QUANDO A DURAÇÃO ACABA ---
    public void Atualizar()
    {
        contador++;
        switch (estado)
        {
            case Estado.VERDE:
                if (contador >= duracaoVerde)
                {
                    estado = Estado.Amarelo;
                    contador = 0;
                }
                break;
            case Estado.AMARELO:
                if (contador >= duracaoAmarelo)
                {
                    estado = Estado.Vermelho;
                    contador = 0;
                }
                break;
            case Estado.VERMELHO:
                if (contador >= duracaoVermelho)
                {
                    estado = Estado.Verde;
                    contador = 0;
                }
                break;
        }
    }

    // --- INDICA SE OS CARROS PODEM PASSAR ---
    public bool PodePassar()
    {
        return estado == Estado.Verde;
    }

    // --- RETORNA O ESTADO ATUAL DO SEMAFORO ---
    public Estado GetEstado()
    {
        return estado;
    }
}
