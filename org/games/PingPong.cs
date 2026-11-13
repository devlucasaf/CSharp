using System;

namespace Org.Games
{
    public class PingPong
    {
        public static void Run()
        {
            int userPointSetOne = 0;
            int userPointSetTwo = 0;
            int userPointOne = 0;
            int userPointTwo = 0;

            Console.Write("Digite o nome do participante: ");
            string userNameOne = Console.ReadLine();
            Console.Write("Digite o nome do participante: ");
            string userNameTwo = Console.ReadLine();

            Console.Write("Digite o total de pontos da partida: ");
            string totalPointsInput = Console.ReadLine();
            int totalPoints = int.Parse(totalPointsInput);

            Console.Write("Digite o total de Sets da partida: ");
            string totalSetsInput = Console.ReadLine();
            int totalSets = int.Parse(totalSetsInput);

            while (true)
            {
                while (true)
                {
                    Console.Write("Digite o nome de quem fez o ponto: ");
                    string user = Console.ReadLine();

                    if (user == userNameOne)
                    {
                        userPointOne++;
                        Console.WriteLine($"Ponto do {userNameOne}");
                        Console.WriteLine($"{userNameOne}:{userPointOne}");
                        Console.WriteLine($"{userNameTwo}:{userPointTwo}");
                    }
                    else
                    {
                        userPointTwo++;
                        Console.WriteLine($"Ponto do {userNameTwo}");
                        Console.WriteLine($"{userNameOne}:{userPointOne}");
                        Console.WriteLine($"{userNameTwo}:{userPointTwo}");
                    }

                    if (userPointOne == totalPoints || userPointTwo == totalPoints)
                    {
                        break;
                    }
                }

                if (userPointOne == totalPoints)
                {
                    userPointSetOne++;
                    userPointOne = 0;
                    userPointTwo = 0;
                    Console.WriteLine($"setpoint do {userNameOne}");
                }
                else
                {
                    userPointSetTwo++;
                    userPointOne = 0;
                    userPointTwo = 0;
                    Console.WriteLine($"setpoint do {userNameTwo}");
                }

                Console.WriteLine("sets: \n");
                Console.WriteLine($"{userNameOne} {userPointSetOne}\n");
                Console.WriteLine($"{userNameTwo} {userPointSetTwo}\n");

                if (userPointSetOne == totalSets || userPointSetTwo == totalSets)
                {
                    break;
                }
            }

            Console.WriteLine("sets: \n");
            Console.WriteLine($"{userNameOne} {userPointSetOne}\n");
            Console.WriteLine($"{userNameTwo} {userPointSetTwo}\n");
        }
    }
}