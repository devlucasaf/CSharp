using Application.Simulador.Trafego;

namespace Application.Simuladores.Trafego
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Semaforo semaforo = new Semaforo(8, 2, 6, Estado.Verde);
            SimuladorTrafego simulador = new SimuladorTrafego(60, 40, semaforo);

            Console.WriteLine("Legenda: '.' via livre | 'C' carro | 'V' semaforo verde | '|' semaforo fechado");
            Console.WriteLine();

            simulador.Simular(100, 0.35);
        }
    }
}
