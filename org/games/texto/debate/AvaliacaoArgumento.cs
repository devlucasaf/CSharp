using System.Collections.ObjectModel;

namespace Games.Texto.Debate;

public class AvaliacaoArgumento
{
    private readonly Dictionary<CriterioAvaliacao, double> _notas;

    public Argumento    Argumento       { get; }
    public string       Observacao      { get; }
    public double       PontuacaoTotal  { get; }

    public AvaliacaoArgumento(Argumento argumento, IDictionary<CriterioAvaliacao, double> notas, string? observacao)
    {
        if (argumento == null)
        {
            throw new ArgumentException("O argumento não pode ser nulo.");
        }

        if (notas == null || notas.Count == 0)
        {
            throw new ArgumentException("As notas da avaliação devem ser informadas.");
        }

        Argumento = argumento;
        _notas = ValidarECopiarNotas(notas);
        Observacao = observacao == null ? "" : observacao.Trim();
        PontuacaoTotal = CalcularPontuacaoTotal();
    }

    public IReadOnlyDictionary<CriterioAvaliacao, double> Notas => new ReadOnlyDictionary<CriterioAvaliacao, double>(_notas);

    // --- VALIDA E COPIA AS NOTAS RECEBIDAS ---
    private static Dictionary<CriterioAvaliacao, double> ValidarECopiarNotas(IDictionary<CriterioAvaliacao, double> notasRecebidas)
    {
        var copia = new Dictionary<CriterioAvaliacao, double>();

        foreach (CriterioAvaliacao criterio in Enum.GetValues<CriterioAvaliacao>())
        {
            double nota = notasRecebidas.TryGetValue(criterio, out double valor) ? valor : 0.0;

            if (!double.IsFinite(nota) || nota < 0 || nota > 10)
            {
                throw new ArgumentException("As notas devem estar entre 0 e 10.");
            }

            copia[criterio] = nota;
        }

        return copia;
    }

    // --- CALCULA A PONTUAÇÃO PONDERADA DO ARGUMENTO ---
    private double CalcularPontuacaoTotal()
    {
        double coerencia = _notas[CriterioAvaliacao.Coerencia] * 0.25;
        double relevancia = _notas[CriterioAvaliacao.Relevancia] * 0.20;
        double clareza = _notas[CriterioAvaliacao.Clareza] * 0.15;
        double evidencias = _notas[CriterioAvaliacao.Evidencias] * 0.15;
        double estrutura = _notas[CriterioAvaliacao.Estrutura] * 0.15;
        double vocabulario = _notas[CriterioAvaliacao.Vocabulario] * 0.10;
        return coerencia + relevancia + clareza + evidencias + estrutura + vocabulario;
    }

    public double GetNota(CriterioAvaliacao criterio)
    {
        return _notas.TryGetValue(criterio, out double nota) ? nota : 0.0;
    }
}
