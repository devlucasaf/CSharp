using System;
using System.Collections.Generic;
using Org.Application.SistemaElevador;

namespace Org.Application.Elevador
{
    public class Main
    {
        public static void Run()
        {
            const int TOTAL_ANDARES = 100;
            Elevador elevador = new Elevador(TOTAL_ANDARES);

            List<Passageiro> passageiros = new List<Passageiro>();
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                int origem = random.Next(TOTAL_ANDARES);
                int destino = random.Next(TOTAL_ANDARES);

                while (destino == origem)
                {
                    destino = random.Next(TOTAL_ANDARES);
                }

                passageiros.Add(new Passageiro(origem, destino));
            }

            foreach (Passageiro p in passageiros)
            {
                elevador.AdicionarChamada(p);
            }

            for (int i = 0; i < 20; i++)
            {
                elevador.Status();
                elevador.Mover();
            }

            elevador.Status();
            Console.WriteLine($"Total de movimentos: {elevador.GetMovimentos()}");
        }
    }
}