using System;
using System.Collections.Generic;

namespace Application.System.Dinheiro.Banco;

public class SimuladorBanco
{
    private const int SALDO_INICIAL = 1000;
    private const int GANHO_TRABALHO = 200;
    private const int MAX_TURNOS = 10;

    private static int saldo = SALDO_INICIAL;
    private static int turnos = 0;

    private static readonly Random random = new Random();
    private static readonly List<string> historico = new List<string>();

    public static void Main(string[] args)
    {
        Console.WriteLine("SIMULADOR DE BANCO");
        Console.WriteLine("Saldo inicial: R$ " + saldo);

        while (true)
        {
            if (saldo <= 0)
            {
                Console.WriteLine("\nVocê ficou sem dinheiro!");
                break;
            }

            if (turnos >= MAX_TURNOS)
            {
                Console.WriteLine("\n⏱Número máximo de turnos atingido!");
                break;
            }

            MostrarNivel();
            MostrarMenu();

            if (!int.TryParse(Console.ReadLine(), out int opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            turnos++;

            switch (opcao)
            {
                case 1:
                    Trabalhar();
                    break;
                case 2:
                    Investir();
                    break;
                case 3:
                    Apostar();
                    break;
                case 4:
                    MostrarSaldo();
                    turnos--; 
                    break;
                case 5:
                    Console.WriteLine("\nVocê saiu do jogo.");
                    EncerrarJogo();
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    turnos--;
                    break;
            }

            EventoAleatorio();
        }

        EncerrarJogo();
    }

    public static void Trabalhar()
    {
        saldo += GANHO_TRABALHO;
        string msg = "Trabalhou e ganhou R$ " + GANHO_TRABALHO;
        Console.WriteLine(msg);
        historico.Add(msg);
    }

    public static void Investir()
    {
        int valor = random.Next(401) - 200; 
        saldo += valor;

        string mensagem;
        if (valor >= 0)
        {
            mensagem = "Investimento deu lucro de R$ " + valor;
        }
        else
        {
            mensagem = "Investimento deu prejuízo de R$ " + Math.Abs(valor);
        }

        Console.WriteLine(mensagem);
        historico.Add(mensagem);
    }

    public static void Apostar()
    {
        int valor = random.Next(801) - 400; 
        saldo += valor;

        string mensagem;
        if (valor >= 0)
        {
            mensagem = "Apostou e ganhou R$ " + valor;
        }
        else
        {
            mensagem = "Apostou e perdeu R$ " + Math.Abs(valor);
        }

        Console.WriteLine(mensagem);
        historico.Add(mensagem);
    }

    public static void MostrarSaldo()
    {
        Console.WriteLine("Saldo atual: R$ " + saldo);
    }

    public static void MostrarMenu()
    {
        Console.WriteLine("\n------ TURNO " + (turnos + 1) + " ------");
        Console.WriteLine("1 - Trabalhar");
        Console.WriteLine("2 - Investir");
        Console.WriteLine("3 - Apostar");
        Console.WriteLine("4 - Ver saldo");
        Console.WriteLine("5 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    public static void MostrarNivel()
    {
        string nivel;

        if (saldo < 1500)
        {
            nivel = "Iniciante";
        }
        else if (saldo < 3000)
        {
            nivel = "Intermediário";
        }
        else
        {
            nivel = "Avançado";
        }

        Console.WriteLine("\n🎖Nível atual: " + nivel);
    }

    public static void EventoAleatorio()
    {
        int chance = random.Next(100);

        if (chance < 15)
        {
            int bonus = random.Next(201) + 100;
            saldo += bonus;
            string msg = "Bônus inesperado! Recebeu R$ " + bonus;
            Console.WriteLine(msg);
            historico.Add(msg);
        }
        else if (chance > 85)
        {
            int perda = random.Next(201) + 100;
            saldo -= perda;
            string msg = "Crise econômica! Perdeu R$ " + perda;
            Console.WriteLine(msg);
            historico.Add(msg);
        }
    }

    public static void EncerrarJogo()
    {
        Console.WriteLine("\nFIM DO JOGO");
        Console.WriteLine("Saldo final: R$ " + saldo);
        Console.WriteLine("Turnos jogados: " + turnos);

        Console.WriteLine("\nHistórico de ações:");
        foreach (string h in historico)
        {
            Console.WriteLine("- " + h);
        }

        Console.WriteLine("\nObrigado por jogar!");
    }
}

