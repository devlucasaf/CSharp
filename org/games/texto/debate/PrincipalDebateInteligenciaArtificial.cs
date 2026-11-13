using System.Globalization;
using System.Text;

namespace Games.Texto.Debate;

public class PrincipalDebateInteligenciaArtificial
{
    private static readonly CultureInfo CulturaPtBr = new("pt-BR");

    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        ExibirTitulo();

        string nomeJogador = LerTextoObrigatorio("Informe seu nome: ");
        TemaDebate tema = SelecionarTema();
        LadoDebate ladoJogador = SelecionarLado();
        int quantidadeRodadas = LerInteiroNoIntervalo("Quantidade de rodadas, entre 1 e 10: ", 1, 10);

        var jogo = new JogoDebate(nomeJogador, tema, ladoJogador, quantidadeRodadas);

        ExibirIntroducao(jogo);
        ExecutarDebate(jogo);
        ExibirResultadoFinal(jogo);
    }

    // --- EXECUTA TODAS AS RODADAS DO DEBATE ---
    private static void ExecutarDebate(JogoDebate jogo)
    {
        while (!jogo.IsFinalizado())
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine("RODADA " + (jogo.RodadaAtual + 1) + " DE " + jogo.QuantidadeMaximaRodadas);
            Console.WriteLine("============================================================");

            if (jogo.Rodadas.Count > 0)
            {
                string argumentoAnterior = jogo.Rodadas[^1].ArgumentoInteligenciaArtificial.Texto;
                Console.WriteLine("Último argumento do oponente:");
                Console.WriteLine(argumentoAnterior);
                Console.WriteLine();
            }

            EstrategiaArgumentacao estrategia = SelecionarEstrategia();
            Console.WriteLine("Orientação: " + estrategia.GetOrientacao());
            string argumento = LerArgumento();

            RodadaDebate rodada = jogo.ExecutarRodada(argumento, estrategia);
            ExibirRodada(rodada);
        }
    }

    // --- EXIBE OS ARGUMENTOS E AS NOTAS DA RODADA ---
    private static void ExibirRodada(RodadaDebate rodada)
    {
        Console.WriteLine("\nARGUMENTO DO OPONENTE");
        Console.WriteLine(rodada.ArgumentoInteligenciaArtificial.Texto);
        Console.WriteLine("Estratégia: " + rodada.ArgumentoInteligenciaArtificial.Estrategia.GetNomeFormatado());

        ExibirAvaliacao("Sua avaliação", rodada.AvaliacaoJogador);
        ExibirAvaliacao("Avaliação do oponente", rodada.AvaliacaoInteligenciaArtificial);

        if (rodada.IsEmpate())
        {
            Console.WriteLine("\nResultado da rodada: empate.");
        }
        else if (rodada.IsVitoriaJogador())
        {
            Console.WriteLine("\nResultado da rodada: você venceu.");
        }
        else
        {
            Console.WriteLine("\nResultado da rodada: o oponente venceu.");
        }
    }

    // --- EXIBE UMA AVALIAÇÃO COMPLETA ---
    private static void ExibirAvaliacao(string titulo, AvaliacaoArgumento avaliacao)
    {
        Console.WriteLine("\n" + titulo.ToUpper(CulturaPtBr));

        foreach (CriterioAvaliacao criterio in Enum.GetValues<CriterioAvaliacao>())
        {
            Console.WriteLine(criterio.GetNomeFormatado() + ": " + FormatarNota(avaliacao.GetNota(criterio)));
        }

        Console.WriteLine("Pontuação ponderada: " + FormatarNota(avaliacao.PontuacaoTotal));
        Console.WriteLine("Comentário: " + avaliacao.Observacao);
    }

    // --- EXIBE O RESULTADO FINAL DO JOGO ---
    private static void ExibirResultadoFinal(JogoDebate jogo)
    {
        Console.WriteLine("\n============================================================");
        Console.WriteLine("                    RESULTADO FINAL");
        Console.WriteLine("============================================================");
        Console.WriteLine(jogo.GetResultadoFinal());
        Console.WriteLine("Pontuação de " + jogo.NomeJogador + ": " + FormatarNota(jogo.CalcularPontuacaoJogador()));
        Console.WriteLine("Pontuação do argumentador virtual: " + FormatarNota(jogo.CalcularPontuacaoIa()));
        Console.WriteLine("Rodadas vencidas pelo jogador: " + jogo.ContarVitoriasJogador());
        Console.WriteLine("Rodadas vencidas pelo oponente: " + jogo.ContarVitoriasIa());
    }

    // --- PERMITE QUE O JOGADOR SELECIONE UM TEMA ---
    private static TemaDebate SelecionarTema()
    {
        List<TemaDebate> temas = CriarTemas();

        Console.WriteLine("\nTEMAS DISPONÍVEIS");

        for (int indice = 0; indice < temas.Count; indice++)
        {
            Console.WriteLine((indice + 1) + ". " + temas[indice].Titulo);
            Console.WriteLine("   " + temas[indice].Descricao);
        }

        int opcao = LerInteiroNoIntervalo("Escolha um tema: ", 1, temas.Count);
        return temas[opcao - 1];
    }

    // --- PERMITE QUE O JOGADOR SELECIONE UM LADO ---
    private static LadoDebate SelecionarLado()
    {
        Console.WriteLine("\n1. Favorável");
        Console.WriteLine("2. Contrário");

        int opcao = LerInteiroNoIntervalo("Escolha seu lado: ", 1, 2);
        return opcao == 1 ? LadoDebate.Favoravel : LadoDebate.Contrario;
    }

    // --- PERMITE QUE O JOGADOR SELECIONE UMA ESTRATÉGIA ---
    private static EstrategiaArgumentacao SelecionarEstrategia()
    {
        EstrategiaArgumentacao[] estrategias = Enum.GetValues<EstrategiaArgumentacao>();

        Console.WriteLine("\nESTRATÉGIAS");

        for (int indice = 0; indice < estrategias.Length; indice++)
        {
            Console.WriteLine((indice + 1) + ". " + estrategias[indice].GetNomeFormatado());
        }

        int opcao = LerInteiroNoIntervalo("Escolha sua estratégia: ", 1, estrategias.Length);
        return estrategias[opcao - 1];
    }

    // --- CRIA OS TEMAS DISPONÍVEIS NO JOGO ---
    private static List<TemaDebate> CriarTemas()
    {
        var tecnologiaEducacao = new TemaDebate(
            1,
            "Uso de tecnologia na educação",
            "Debata se ferramentas digitais devem ter maior presença no processo educacional.",
            new List<string> { "tecnologia", "educação", "aprendizado", "alunos", "professores", "digital" },
            new List<string>
            {
                "A tecnologia pode ampliar o acesso a materiais educacionais e permitir novas formas de aprendizado.",
                "Ferramentas digitais podem apoiar professores na personalização de atividades e no acompanhamento dos alunos.",
                "Recursos interativos podem aumentar a participação dos estudantes quando são utilizados com planejamento."
            },
            new List<string>
            {
                "O uso excessivo de ferramentas digitais pode aumentar distrações e prejudicar a concentração dos alunos.",
                "A desigualdade de acesso a equipamentos pode ampliar diferenças entre estudantes.",
                "A tecnologia não substitui o acompanhamento pedagógico e pode ser ineficiente quando aplicada sem preparo."
            }
        );

        var trabalhoRemoto = new TemaDebate(
            2,
            "Trabalho remoto",
            "Debata se o trabalho remoto deve ser adotado como modelo principal pelas empresas.",
            new List<string> { "trabalho", "remoto", "empresas", "produtividade", "funcionários", "comunicação" },
            new List<string>
            {
                "O trabalho remoto pode reduzir deslocamentos e oferecer maior flexibilidade aos funcionários.",
                "Empresas podem reduzir custos físicos e contratar profissionais de diferentes localidades.",
                "Com metas claras e ferramentas adequadas, equipes remotas podem manter bons níveis de produtividade."
            },
            new List<string>
            {
                "A distância pode dificultar a comunicação e reduzir a integração entre os membros da equipe.",
                "Nem todos os profissionais possuem um ambiente doméstico adequado para trabalhar.",
                "Algumas atividades dependem de colaboração presencial e acesso a estruturas específicas."
            }
        );

        var transportePublico = new TemaDebate(
            3,
            "Investimento em transporte coletivo",
            "Debata se as cidades devem priorizar investimentos em transporte coletivo.",
            new List<string> { "transporte", "coletivo", "cidades", "mobilidade", "trânsito", "investimento" },
            new List<string>
            {
                "O transporte coletivo eficiente pode reduzir congestionamentos e melhorar a mobilidade nas cidades.",
                "Investimentos em redes integradas podem ampliar o acesso da população a serviços e oportunidades.",
                "A redução do número de automóveis nas vias pode diminuir impactos ambientais e melhorar o espaço urbano."
            },
            new List<string>
            {
                "Grandes projetos de transporte exigem recursos elevados e podem demorar para produzir resultados.",
                "Regiões com baixa densidade populacional podem não apresentar demanda suficiente para determinadas linhas.",
                "Investimentos exclusivos no transporte coletivo podem deixar de atender outras necessidades importantes de mobilidade."
            }
        );

        return new List<TemaDebate> { tecnologiaEducacao, trabalhoRemoto, transportePublico };
    }

    // --- LÊ UM ARGUMENTO COM VÁRIAS LINHAS ---
    private static string LerArgumento()
    {
        Console.WriteLine("Digite seu argumento. Digite FIM em uma linha separada para concluir:");

        var argumento = new StringBuilder();

        while (true)
        {
            string? linha = Console.ReadLine();

            if (linha == null || linha.Equals("FIM", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (argumento.Length > 0)
            {
                argumento.Append(Environment.NewLine);
            }

            argumento.Append(linha);
        }

        if (argumento.ToString().Trim().Length == 0)
        {
            throw new ArgumentException("O argumento não pode estar vazio.");
        }

        return argumento.ToString();
    }

    // --- EXIBE A INTRODUÇÃO DO DEBATE ---
    private static void ExibirIntroducao(JogoDebate jogo)
    {
        Console.WriteLine("\n============================================================");
        Console.WriteLine("Tema: " + jogo.Tema.Titulo);
        Console.WriteLine("Posição do jogador: " + jogo.LadoJogador.GetNomeFormatado());
        Console.WriteLine("Posição do oponente: " + jogo.LadoIa.GetNomeFormatado());
        Console.WriteLine("Rodadas: " + jogo.QuantidadeMaximaRodadas);
        Console.WriteLine("============================================================");
        Console.WriteLine("A avaliação considera a construção textual, não a correção da opinião apresentada.");
    }

    // --- LÊ UM TEXTO OBRIGATÓRIO ---
    private static string LerTextoObrigatorio(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string texto = (Console.ReadLine() ?? "").Trim();

            if (texto.Length > 0)
            {
                return texto;
            }

            Console.WriteLine("O valor não pode estar vazio.");
        }
    }

    // --- LÊ UM NÚMERO DENTRO DE UM INTERVALO ---
    private static int LerInteiroNoIntervalo(string mensagem, int minimo, int maximo)
    {
        while (true)
        {
            Console.Write(mensagem);
            string entrada = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(entrada, NumberStyles.Integer, CultureInfo.InvariantCulture, out int valor))
            {
                if (valor >= minimo && valor <= maximo)
                {
                    return valor;
                }

                Console.WriteLine("Digite um número entre " + minimo + " e " + maximo + ".");
            }
            else
            {
                Console.WriteLine("Digite um número inteiro válido.");
            }
        }
    }

    // --- FORMATA UMA NOTA ---
    private static string FormatarNota(double nota)
    {
        return nota.ToString("F2", CulturaPtBr);
    }

    // --- EXIBE O TÍTULO DO JOGO ---
    private static void ExibirTitulo()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("                   DEBATE AI");
        Console.WriteLine("============================================================");
        Console.WriteLine("Apresente argumentos claros, coerentes e relacionados ao tema.");
    }
}
