// Elevador.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Org.Application.Elevador;

public class Elevador
{
    private readonly int                _totalAndares;
    private int                         _andarAtual;
    private readonly List<Passageiro>   _passageiros;
    private readonly List<Passageiro>   _chamadas;
    private int                         _movimentos;

    public Elevador() : this(100) { }

    public Elevador(int totalAndares)
    {
        _totalAndares = totalAndares;
        _andarAtual = 0;
        _passageiros = new List<Passageiro>();
        _chamadas = new List<Passageiro>();
        _movimentos = 0;
    }

    public void AdicionarChamada(Passageiro passageiro)
    {
        if (passageiro.GetAndarOrigem() < 0 || passageiro.GetAndarOrigem() >= _totalAndares ||
            passageiro.GetAndarDestino() < 0 || passageiro.GetAndarDestino() >= _totalAndares)
        {
            return;
        }

        _chamadas.Add(passageiro);
        Console.WriteLine($"📞 Chamada: {passageiro.GetAndarOrigem()} - {passageiro.GetAndarDestino()}");
    }

    private int? EscolherDestino()
    {
        var destinos = new List<int>();

        foreach (var p in _passageiros)
        {
            destinos.Add(p.GetAndarDestino());
        }

        foreach (var p in _chamadas)
        {
            destinos.Add(p.GetAndarOrigem());
        }

        if (destinos.Count == 0)
        {
            return null;
        } 

        int destinoMaisProximo = destinos[0];
        int menorDistancia = Math.Abs(destinoMaisProximo - _andarAtual);

        for (int i = 1; i < destinos.Count; i++)
        {
            int distancia = Math.Abs(destinos[i] - _andarAtual);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                destinoMaisProximo = destinos[i];
            }
        }

        return destinoMaisProximo;
    }

    public void Mover()
    {
        int? destino = EscolherDestino();

        if (destino == null)
        {
            Console.WriteLine("⏸ Nenhuma chamada ou passageiro no momento.");
            return;
        }

        if (_andarAtual < destino)
        {
            _andarAtual++;
            _movimentos++;
            Console.WriteLine($"🔼 Subiu para {_andarAtual}");
        }
        else if (_andarAtual > destino)
        {
            _andarAtual--;
            _movimentos++;
            Console.WriteLine($"🔽 Desceu para {_andarAtual}");
        }
        else 
        {
            Console.WriteLine($"🚪 Chegou no andar {_andarAtual}");

            bool entrouPessoa = false;
            var chamadasParaRemover = new List<Passageiro>();

            foreach (var p in _chamadas)
            {
                if (p.GetAndarOrigem() == _andarAtual)
                {
                    p.SetNoElevador(true);
                    _passageiros.Add(p);
                    chamadasParaRemover.Add(p);
                    entrouPessoa = true;
                    Console.WriteLine($"⬆️ Entrou: {p}");
                }
            }

            foreach (var p in chamadasParaRemover)
            {
                _chamadas.Remove(p);
            }

            bool saiuPessoa = false;
            var passageirosParaRemover = new List<Passageiro>();

            foreach (var p in _passageiros)
            {
                if (p.GetAndarDestino() == _andarAtual)
                {
                    p.SetNoElevador(false);
                    passageirosParaRemover.Add(p);
                    saiuPessoa = true;
                    Console.WriteLine($"⬇️ Saiu: {p}");
                }
            }
            foreach (var p in passageirosParaRemover)
                _passageiros.Remove(p);

            if (!entrouPessoa && !saiuPessoa)
            {
                Console.WriteLine("🕓 Sem embarque/desembarque neste andar.");
            }
        }
    }

    public void Status()
    {
        Console.WriteLine($"🏢 Andar atual: {_andarAtual}");
        Console.WriteLine($"📞 Chamadas: {(_chamadas.Count == 0 ? "—" : string.Join(", ", _chamadas))}");
        Console.WriteLine($"🛗 Passageiros: {(_passageiros.Count == 0 ? "—" : string.Join(", ", _passageiros))}");
        Console.WriteLine(new string('-', 40));
    }

    public int GetTotalAndares() => _totalAndares;
    public int GetAndarAtual() => _andarAtual;
    public int GetMovimentos() => _movimentos;
}