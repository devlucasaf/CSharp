using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Simulador.Trafego;

public class SimuladorTrafego
{
    private readonly int        tamanhoVia;
    private readonly int        posicaoSemaforo;
    private readonly Semaforo   semaforo;

    private readonly List<Carro> carros = new List<Carro>();
    private readonly Random random = new Random();

    private int proximoId = 1;
    private int ticksAtuais = 0;
    private int totalCarrosGerados = 0;
    private int totalCarrosQuePassaram = 0;
    private readonly HashSet<int> idsJaContabilizados = new HashSet<int>();

    // --- DEFINE O TAMANHO DA VIA, A POSIÇÃO E O SEMÁFORO ---
    public SimuladorTrafego(int tamanhoVia, int posicaoSemaforo, Semaforo semaforo)
    {
        this.tamanhoVia = tamanhoVia;
        this.posicaoSemaforo = posicaoSemaforo;
        this.semaforo = semaforo;
    }

    // --- EXECUTA A SIMULACAO POR UM NÚMERO DE TICKS ---
    public void Simular(int totalTicks, double probabilidadeNovoCarro)
    {
        for (int t = 0; t < totalTicks; t++)
        {
            ticksAtuais = t;
            semaforo.Atualizar();

            if (random.NextDouble() < probabilidadeNovoCarro)
            {
                GerarCarro();
            }

            AtualizarCarros();
            RemoverCarrosQueSairam();

            if (t % 10 == 0)
            {
                ImprimirEstadoVia();
            }
        }

        ImprimirResumo();
    }

    // --- CRIA UM CARRO NO INÍCIO DA VIA COM VELOCIDADE ALEATÓRIA ---
    private void GerarCarro()
    {
        int velocidadeMaxima = 2 + random.Next(3);
        carros.Add(new Carro(proximoId++, 0, velocidadeMaxima));
        totalCarrosGerados++;
    }

    // --- MOVE CADA CARRO RESPEITANDO O CARRO DA FRENTE E O SEMÁFORO ---
    private void AtualizarCarros()
    {
        carros.Sort((a, b) => b.Posicao.CompareTo(a.Posicao));

        for (int i = 0; i < carros.Count; i++)
        {
            Carro carro = carros[i];
            int distanciaLivre;

            if (i == 0)
            {
                if (carro.Posicao < posicaoSemaforo && !semaforo.PodePassar())
                {
                    distanciaLivre = posicaoSemaforo - carro.Posicao - 1;
                }
                else
                {
                    distanciaLivre = tamanhoVia - carro.Posicao;
                }
            }
            else
            {
                Carro carroDaFrente = carros[i - 1];
                distanciaLivre = carroDaFrente.Posicao - carro.Posicao - 1;

                if (carro.Posicao < posicaoSemaforo && posicaoSemaforo <= carroDaFrente.Posicao && !semaforo.PodePassar())
                {
                    distanciaLivre = Math.Min(distanciaLivre, posicaoSemaforo - carro.Posicao - 1);
                }
            }

            carro.Mover(Math.Max(0, distanciaLivre), semaforo.PodePassar());

            if (carro.Posicao >= posicaoSemaforo && idsJaContabilizados.Add(carro.Id))
            {
                totalCarrosQuePassaram++;
            }
        }
    }

    // --- REMOVE CARROS QUE JA SAIRAM DO FIM DA VIA ---
    private void RemoverCarrosQueSairam()
    {
        carros.RemoveAll(c => c.Posicao >= tamanhoVia);
    }

    // --- DESENHA A PISTA COM CARROS E O SEMAFORO ---
    private void ImprimirEstadoVia()
    {
        char[] pista = new char[tamanhoVia];
        for (int i = 0; i < pista.Length; i++) pista[i] = '.';

        if (posicaoSemaforo < tamanhoVia)
        {
            pista[posicaoSemaforo] = semaforo.PodePassar() ? 'V' : '|';
        }

        foreach (Carro carro in carros)
        {
            if (carro.Posicao >= 0 && carro.Posicao < tamanhoVia)
            {
                pista[carro.Posicao] = 'C';
            }
        }

        Console.WriteLine($"Tick {ticksAtuais} [{new string(pista)}] semaforo={semaforo.GetEstado()}");
    }

    // --- MOSTRA ESTATISTICAS FINAIS DA SIMULACAO ---
    private void ImprimirResumo()
    {
        Console.WriteLine();
        Console.WriteLine("=== Resumo da simulacao ===");
        Console.WriteLine($"Carros gerados: {totalCarrosGerados}");
        Console.WriteLine($"Carros que passaram pelo semaforo: {totalCarrosQuePassaram}");
        Console.WriteLine($"Carros ainda na via: {carros.Count}");
        if (ticksAtuais > 0)
        {
            double fluxoMedio = (double)totalCarrosQuePassaram / ticksAtuais;
            Console.WriteLine($"Fluxo medio: {fluxoMedio:F3} carros/tick");
        }
    }
}

