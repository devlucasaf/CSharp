using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Games.Texto.Debate;

public class AvaliadorArgumentos
{
    private static readonly HashSet<string> Conectivos = new() { "porque", "portanto", "pois", "assim", "contudo", "entretanto", "alem", "consequentemente", "embora", "logo", "porem", "desse modo", "por isso" };
    private static readonly HashSet<string> MarcadoresEvidencia = new() { "dados", "pesquisa", "estudo", "exemplo", "estatistica", "resultado", "evidencia", "levantamento", "analise" };
    private static readonly HashSet<string> MarcadoresRefutacao = new() { "discordo", "contrario", "entretanto", "contudo", "refuto", "insuficiente", "equivocado" };
    private static readonly HashSet<string> MarcadoresSolucao = new() { "solucao", "proposta", "implementar", "criar", "melhorar", "reduzir", "resolver" };

    // --- AVALIA UM ARGUMENTO DE ACORDO COM O TEMA ---
    public AvaliacaoArgumento Avaliar(Argumento argumento, TemaDebate tema, string? argumentoAnterior)
    {
        if (argumento == null || tema == null)
        {
            throw new ArgumentException("O argumento e o tema devem ser informados.");
        }

        string textoNormalizado = NormalizarTexto(argumento.Texto);
        List<string> palavras = SepararPalavras(textoNormalizado);
        List<string> frases = SepararFrases(argumento.Texto);

        var notas = new Dictionary<CriterioAvaliacao, double>
        {
            [CriterioAvaliacao.Coerencia] = AvaliarCoerencia(palavras, frases),
            [CriterioAvaliacao.Relevancia] = AvaliarRelevancia(textoNormalizado, tema),
            [CriterioAvaliacao.Clareza] = AvaliarClareza(palavras, frases),
            [CriterioAvaliacao.Evidencias] = AvaliarEvidencias(textoNormalizado),
            [CriterioAvaliacao.Estrutura] = AvaliarEstrutura(textoNormalizado, palavras, argumento.Estrategia, argumentoAnterior),
            [CriterioAvaliacao.Vocabulario] = AvaliarVocabulario(palavras)
        };

        string observacao = CriarObservacao(notas);
        return new AvaliacaoArgumento(argumento, notas, observacao);
    }

    // --- AVALIA A COERÊNCIA DO TEXTO ---
    private double AvaliarCoerencia(List<string> palavras, List<string> frases)
    {
        if (palavras.Count == 0)
        {
            return 0;
        }

        double nota = 3.0;
        int conectivosEncontrados = ContarOcorrencias(palavras, Conectivos);

        nota += Math.Min(3.0, conectivosEncontrados * 0.75);

        if (palavras.Count >= 15)
        {
            nota += 1.5;
        }

        if (palavras.Count >= 30)
        {
            nota += 1.0;
        }

        if (frases.Count >= 2)
        {
            nota += 1.0;
        }

        if (PossuiRepeticaoExcessiva(palavras))
        {
            nota -= 1.5;
        }

        return LimitarNota(nota);
    }

    // --- AVALIA A RELAÇÃO DO ARGUMENTO COM O TEMA ---
    private double AvaliarRelevancia(string texto, TemaDebate tema)
    {
        if (tema.PalavrasChave.Count == 0)
        {
            return 6.0;
        }

        int palavrasEncontradas = 0;

        foreach (string palavraChave in tema.PalavrasChave)
        {
            if (texto.Contains(NormalizarTexto(palavraChave)))
            {
                palavrasEncontradas++;
            }
        }

        double proporcao = palavrasEncontradas / (double)tema.PalavrasChave.Count;
        return LimitarNota(3.0 + proporcao * 7.0);
    }

    // --- AVALIA A CLAREZA DO TEXTO ---
    private double AvaliarClareza(List<string> palavras, List<string> frases)
    {
        if (palavras.Count == 0 || frases.Count == 0)
        {
            return 0;
        }

        double mediaPalavrasPorFrase = palavras.Count / (double)frases.Count;
        double nota = 8.0;

        if (mediaPalavrasPorFrase > 35)
        {
            nota -= 3.0;
        }
        else if (mediaPalavrasPorFrase > 25)
        {
            nota -= 1.5;
        }

        if (palavras.Count < 8)
        {
            nota -= 3.0;
        }

        if (palavras.Count >= 15 && palavras.Count <= 120)
        {
            nota += 1.0;
        }

        return LimitarNota(nota);
    }

    // --- AVALIA O USO DE EXEMPLOS E MARCADORES DE EVIDÊNCIA ---
    private double AvaliarEvidencias(string texto)
    {
        List<string> palavras = SepararPalavras(texto);
        int marcadores = ContarOcorrencias(palavras, MarcadoresEvidencia);
        int numeros = ContarNumeros(texto);
        double nota = 2.0 + Math.Min(5.0, marcadores * 1.5) + Math.Min(3.0, numeros);

        return LimitarNota(nota);
    }

    // --- AVALIA A ESTRUTURA E O USO DA ESTRATÉGIA ESCOLHIDA ---
    private double AvaliarEstrutura(string texto, List<string> palavras, EstrategiaArgumentacao estrategia, string? argumentoAnterior)
    {
        double nota = 4.0;

        if (palavras.Count >= 20)
        {
            nota += 2.0;
        }

        if (ContarOcorrencias(palavras, Conectivos) > 0)
        {
            nota += 1.5;
        }

        if (estrategia == EstrategiaArgumentacao.Refutacao && ContarOcorrencias(palavras, MarcadoresRefutacao) > 0)
        {
            nota += 2.0;
        }

        if (estrategia == EstrategiaArgumentacao.PropostaSolucao && ContarOcorrencias(palavras, MarcadoresSolucao) > 0)
        {
            nota += 2.0;
        }

        if (estrategia == EstrategiaArgumentacao.ExemploPratico && texto.Contains("exemplo"))
        {
            nota += 2.0;
        }

        if (estrategia == EstrategiaArgumentacao.CausaEEfeito && (texto.Contains("porque") || texto.Contains("consequentemente") || texto.Contains("por isso")))
        {
            nota += 2.0;
        }

        if (estrategia == EstrategiaArgumentacao.Comparacao && (texto.Contains("compar") || texto.Contains("enquanto") || texto.Contains("diferente")))
        {
            nota += 2.0;
        }

        if (estrategia == EstrategiaArgumentacao.Refutacao && !string.IsNullOrWhiteSpace(argumentoAnterior))
        {
            nota += CalcularSobreposicaoTextual(texto, NormalizarTexto(argumentoAnterior)) * 2.0;
        }

        return LimitarNota(nota);
    }

    // --- AVALIA A DIVERSIDADE DO VOCABULÁRIO ---
    private double AvaliarVocabulario(List<string> palavras)
    {
        if (palavras.Count == 0)
        {
            return 0;
        }

        var palavrasUnicas = new HashSet<string>(palavras);
        double diversidade = palavrasUnicas.Count / (double)palavras.Count;
        double nota = diversidade * 10.0;

        if (palavras.Count < 10)
        {
            nota -= 2.0;
        }

        return LimitarNota(nota);
    }

    // --- CALCULA A SOBREPOSIÇÃO ENTRE DOIS TEXTOS ---
    private double CalcularSobreposicaoTextual(string primeiroTexto, string segundoTexto)
    {
        var primeirasPalavras = new HashSet<string>(SepararPalavras(primeiroTexto));
        var segundasPalavras = new HashSet<string>(SepararPalavras(segundoTexto));

        if (primeirasPalavras.Count == 0 || segundasPalavras.Count == 0)
        {
            return 0;
        }

        primeirasPalavras.IntersectWith(segundasPalavras);
        return Math.Min(1.0, primeirasPalavras.Count / 8.0);
    }

    // --- VERIFICA SE EXISTE REPETIÇÃO EXCESSIVA ---
    private bool PossuiRepeticaoExcessiva(List<string> palavras)
    {
        if (palavras.Count < 8)
        {
            return false;
        }

        var frequencias = new Dictionary<string, int>();

        foreach (string palavra in palavras)
        {
            frequencias[palavra] = frequencias.GetValueOrDefault(palavra, 0) + 1;
        }

        foreach (int frequencia in frequencias.Values)
        {
            if (frequencia > palavras.Count * 0.25)
            {
                return true;
            }
        }

        return false;
    }

    // --- CONTA MARCADORES PRESENTES NO TEXTO ---
    private int ContarOcorrencias(List<string> palavras, HashSet<string> marcadores)
    {
        int quantidade = 0;

        foreach (string palavra in palavras)
        {
            if (marcadores.Contains(palavra))
            {
                quantidade++;
            }
        }

        return quantidade;
    }

    // --- CONTA VALORES NUMÉRICOS PRESENTES NO TEXTO ---
    private int ContarNumeros(string texto)
    {
        int quantidade = 0;

        foreach (string parte in Regex.Split(texto, @"\s+"))
        {
            if (parte.Any(char.IsAsciiDigit))
            {
                quantidade++;
            }
        }

        return quantidade;
    }

    // --- CRIA UM RETORNO TEXTUAL SOBRE A AVALIAÇÃO ---
    private string CriarObservacao(Dictionary<CriterioAvaliacao, double> notas)
    {
        CriterioAvaliacao melhorCriterio = default;
        CriterioAvaliacao criterioMelhoria = default;
        double maiorNota = double.NegativeInfinity;
        double menorNota = double.PositiveInfinity;

        foreach (var entrada in notas.OrderBy(e => e.Key))
        {
            if (entrada.Value > maiorNota)
            {
                maiorNota = entrada.Value;
                melhorCriterio = entrada.Key;
            }

            if (entrada.Value < menorNota)
            {
                menorNota = entrada.Value;
                criterioMelhoria = entrada.Key;
            }
        }

        return "Ponto forte: " + melhorCriterio.GetNomeFormatado() + ". Principal aspecto para melhoria: " + criterioMelhoria.GetNomeFormatado() + ".";
    }

    // --- SEPARA AS PALAVRAS DE UM TEXTO ---
    private List<string> SepararPalavras(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return new List<string>();
        }

        return Regex.Split(NormalizarTexto(texto), @"\s+").Where(palavra => !string.IsNullOrWhiteSpace(palavra)).ToList();
    }

    // --- SEPARA AS FRASES DE UM TEXTO ---
    private List<string> SepararFrases(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return new List<string>();
        }

        return Regex.Split(texto, "[.!?]+").Select(frase => frase.Trim()).Where(frase => frase.Length > 0).ToList();
    }

    // --- NORMALIZA O TEXTO PARA COMPARAÇÕES ---
    private string NormalizarTexto(string? texto)
    {
        if (texto == null)
        {
            return "";
        }

        string textoSemAcentos = Regex.Replace(texto.Normalize(NormalizationForm.FormD), @"\p{M}", "");
        string minusculo = textoSemAcentos.ToLowerInvariant();
        string limpo = Regex.Replace(minusculo, @"[^a-z0-9\s]", " ");
        return Regex.Replace(limpo, @"\s+", " ").Trim();
    }

    // --- LIMITA UMA NOTA AO INTERVALO DE ZERO A DEZ ---
    private double LimitarNota(double nota)
    {
        return Math.Max(0.0, Math.Min(10.0, nota));
    }
}
