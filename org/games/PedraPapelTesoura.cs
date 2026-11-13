namespace CSharpRepositorio.org.games;

class PedraPapelTesoura
{
    public static void Run()
    {
        string[] opcao = { "Pedra", "Papel", "Tesoura" };

        Random random = new Random();

        int indexNumero = random.Next(opcao.Length);

        string randomIA = opcao[indexNumero];

        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("Digite [0] para sair");
        Console.WriteLine("Pedra [1]");
        Console.WriteLine("Papel [2]");
        Console.WriteLine("Tesoura [3]");

        while (true)
        {
            Console.Write("Digite a sua escolha: ");
            int numero = int.Parse(Console.ReadLine());

            if (numero == 0)
            {
                break;
            }

            if (numero < 1 || numero > 3)
            {
                Console.WriteLine("Número inválido");
                continue;
            }

            string escolhaUsuario = opcao[numero - 1];

            if (escolhaUsuario.Equals(randomIA))
            {
                Console.WriteLine($"O pc escolheu {randomIA}! Vocês empataram.");
                break;
            }
            else
            {
                if (escolhaUsuario.Equals("Papel") && randomIA.Equals("Pedra"))
                {
                    Console.WriteLine($"Você ganhou! O PC escolheu: {randomIA}");
                    break;
                }
                else if (escolhaUsuario.Equals("Tesoura") && randomIA.Equals("Papel"))
                {
                    Console.WriteLine($"Você ganhou! O PC escolheu: {randomIA}");
                    break;
                }
                else if (escolhaUsuario.Equals("Pedra") && randomIA.Equals("Tesoura"))
                {
                    Console.WriteLine($"Você ganhou! O PC escolheu: {randomIA}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Você perdeu! O PC escolheu {randomIA}");
                    break;
                }
            }
        }
    }
}
