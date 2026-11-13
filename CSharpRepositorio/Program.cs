using System.Text;

namespace CSharpRepositorio;

public static class Program
{
    private static readonly (string Categoria, string Nome, Action<string[]> Executar)[] Programas =
    {
        ("Aplicações", "Tipos primitivos",                          _ => global::CSharpRepositorio.org.application.TipoPrimitivo.Run()),
        ("Aplicações", "Calculadora de renda (IR/INSS)",            a => global::Org.Application.Renda.CalculadoraRenda.Main(a)),
        ("Aplicações", "Mega-Sena",                                 a => global::Application.Outros.MegaSena.Program.Main(a)),
        ("Aplicações", "Simulador de eleição",                      _ => global::Org.Application.Simulador.Eleicao.Votacao.Run()),
        ("Aplicações", "Simulador de elevador",                     _ => global::Org.Application.Elevador.Main.Run()),
        ("Aplicações", "Gerador de senhas",                         _ => global::Org.Application.Senha.GeradorSenha.Run()),
        ("Aplicações", "Veículos",                                  _ => global::CSharpRepositorio.org.application.Veiculo.Veiculo.Run()),
        ("Aplicações", "Sistema de veículos",                       a => global::Application.Sistema.Automovel.Veiculos.Veiculo.Main(a)),
        ("Aplicações", "Locadora de veículos",                      a => global::Application.Sistema.Automovel.LocadoraDeVeiculos.Programa.Main(a)),
        ("Aplicações", "Petshop",                                   a => global::Application.Sistema.Animal.Petshop.Program.Main(a)),
        ("Aplicações", "Zoológico",                                 a => global::Application.Sistema.Animais.Zoologico.Programa.Main(a)),
        ("Aplicações", "Bot Discord (demonstração)",                a => global::Application.Bots.Discord.BotDiscord.Main(a)),
        ("Aplicações", "Bot Telegram (requer TELEGRAM_BOT_TOKEN)",  a => global::Application.Bots.Telegram.BotTelegram.Main(a)),

        ("Jogos", "Bingo",                                          a => global::games.Bingo.Main(a)),
        ("Jogos", "Pedra, papel e tesoura",                         _ => global::CSharpRepositorio.org.games.PedraPapelTesoura.Run()),
        ("Jogos", "Ping pong (placar)",                             _ => global::Org.Games.PingPong.Run()),
        ("Jogos", "Jogo da forca",                                  a => global::games.forca.JogoDaForca.Main(a)),
        ("Jogos", "Termo",                                          a => global::games.termo.TermoGame.Main(a)),
        ("Jogos", "Capitais do mundo",                              a => global::Games.Texto.Capitais.CapitaisDoMundo.Main(a)),
        ("Jogos", "Criptografia (César/Vigenère)",                  a => global::Games.Texto.Criptografia.Program.Main(a)),
        ("Jogos", "Debate com IA",                                  a => global::Games.Texto.Debate.PrincipalDebateInteligenciaArtificial.Main(a)),
        ("Jogos", "Digitação rápida",                               a => global::Games.Texto.Digitacao.DigitacaoRapida.Main(a)),
        ("Jogos", "Jogo da velha",                                  a => global::games.velha.JogoDaVelha.Main(a)),

        ("Matemática", "Tabuada",                                   _ => global::CSharpRepositorio.org.math.MultiplicacaoTable.Run()),
        ("Matemática", "Calculadora de matrizes",                   _ => global::CSharpRepositorio.org.math.MatrizCalculadora.Run()),
        ("Matemática", "Logaritmo",                                 a => global::CSharpRepositorio.org.math.Logaritmo.Main(a)),
        ("Matemática", "Equação quadrática",                        a => global::CSharpRepositorio.org.math.algebra.EquacaoQuadratica.Main(a)),
        ("Matemática", "Equação cúbica",                            a => global::CSharpRepositorio.org.math.algebra.EquacaoCubica.Main(a)),
    };

    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // --- PERMITE EXECUTAR DIRETO: dotnet run -- <número> ---
        if (args.Length > 0 && int.TryParse(args[0], out int opcaoDireta))
        {
            Executar(opcaoDireta, args.Skip(1).ToArray());
            return;
        }

        while (true)
        {
            ExibirMenu();
            Console.Write("Escolha uma opção: ");
            string? entrada = Console.ReadLine();

            if (entrada == null || entrada.Trim() == "0")
            {
                Console.WriteLine("Até mais!");
                return;
            }

            if (!int.TryParse(entrada.Trim(), out int opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            Executar(opcao, Array.Empty<string>());

            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
        }
    }

    private static void ExibirMenu()
    {
        Console.WriteLine("\n========== REPOSITÓRIO C# ==========");
        string categoriaAtual = "";

        for (int i = 0; i < Programas.Length; i++)
        {
            if (Programas[i].Categoria != categoriaAtual)
            {
                categoriaAtual = Programas[i].Categoria;
                Console.WriteLine($"\n--- {categoriaAtual} ---");
            }

            Console.WriteLine($"{i + 1,2} - {Programas[i].Nome}");
        }

        Console.WriteLine("\n 0 - Sair");
    }

    private static void Executar(int opcao, string[] args)
    {
        if (opcao < 1 || opcao > Programas.Length)
        {
            Console.WriteLine("Opção inválida!");
            return;
        }

        Console.WriteLine($"\n>>> {Programas[opcao - 1].Nome}\n");

        try
        {
            Programas[opcao - 1].Executar(args);
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nO programa terminou com erro: {e.Message}");
        }
    }
}
