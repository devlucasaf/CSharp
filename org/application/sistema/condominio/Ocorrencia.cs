using System;

namespace Application.System.Condominio;

public class Ocorrencia
{
    private readonly long           identificador;
    private readonly Unidade        unidade;
    private readonly Morador        registrador;
    private readonly TipoOcorrencia tipo;
    private readonly string         descricao;
    private readonly DateTime       dataRegistro;
    private SituacaoOcorrencia      situacao;
    private string                  respostaAdministracao;
    private DateTime?               dataInicioAnalise;
    private DateTime?               dataResolucao;
    private DateTime?               dataArquivamento;

    public Ocorrencia(long identificador, Unidade unidade, Morador registrador, TipoOcorrencia tipo, string descricao)
    {
        ValidarIdentificador(identificador);
        ValidarUnidade(unidade);
        ValidarRegistrador(registrador);
        ValidarDescricao(descricao);

        if (!unidade.PossuiMorador(registrador))
        {
            throw new ArgumentException("O registrador não pertence à unidade informada.");
        }

        if (!registrador.IsAtivo)
        {
            throw new ArgumentException("O registrador da ocorrência está inativo.");
        }

        this.identificador = identificador;
        this.unidade = unidade;
        this.registrador = registrador;
        this.tipo = tipo;
        this.descricao = descricao.Trim();
        this.dataRegistro = DateTime.Now;
        this.situacao = SituacaoOcorrencia.Aberta;
        this.respostaAdministracao = "";
    }

    public void IniciarAnalise()
    {
        if (situacao != SituacaoOcorrencia.Aberta)
        {
            throw new InvalidOperationException("Somente ocorrências abertas podem entrar em análise.");
        }

        situacao = SituacaoOcorrencia.EmAnalise;
        dataInicioAnalise = DateTime.Now;
    }

    public void Resolver(string respostaAdministracao)
    {
        if (situacao == SituacaoOcorrencia.Resolvida)
        {
            throw new InvalidOperationException("A ocorrência já foi resolvida.");
        }

        if (situacao == SituacaoOcorrencia.Arquivada)
        {
            throw new InvalidOperationException("Uma ocorrência arquivada não pode ser resolvida novamente.");
        }

        if (string.IsNullOrWhiteSpace(respostaAdministracao))
        {
            throw new ArgumentException("A resposta da administração não pode estar vazia.");
        }

        if (situacao == SituacaoOcorrencia.Aberta)
        {
            dataInicioAnalise = DateTime.Now;
        }

        this.respostaAdministracao = respostaAdministracao.Trim();
        this.dataResolucao = DateTime.Now;
        this.situacao = SituacaoOcorrencia.Resolvida;
    }

    public void Arquivar()
    {
        if (situacao != SituacaoOcorrencia.Resolvida)
        {
            throw new InvalidOperationException("Somente ocorrências resolvidas podem ser arquivadas.");
        }

        situacao = SituacaoOcorrencia.Arquivada;
        dataArquivamento = DateTime.Now;
    }

    public void Reabrir(string justificativa)
    {
        if (situacao != SituacaoOcorrencia.Resolvida && situacao != SituacaoOcorrencia.Arquivada)
        {
            throw new InvalidOperationException("Somente ocorrências resolvidas ou arquivadas podem ser reabertas.");
        }

        if (string.IsNullOrWhiteSpace(justificativa))
        {
            throw new ArgumentException("A justificativa da reabertura não pode estar vazia.");
        }

        respostaAdministracao = "Ocorrência reaberta. Justificativa: " + justificativa.Trim();
        situacao = SituacaoOcorrencia.EmAnalise;
        dataInicioAnalise = DateTime.Now;
        dataResolucao = null;
        dataArquivamento = null;
    }

    public bool IsAberta => situacao == SituacaoOcorrencia.Aberta;
    public bool IsEmAnalise => situacao == SituacaoOcorrencia.EmAnalise;
    public bool IsResolvida => situacao == SituacaoOcorrencia.Resolvida;
    public bool IsArquivada => situacao == SituacaoOcorrencia.Arquivada;
    public bool IsEncerrada => IsResolvida || IsArquivada;

    public string GetDataRegistroFormatada() => FormatarData(dataRegistro);
    public string GetDataInicioAnaliseFormatada() => FormatarData(dataInicioAnalise);
    public string GetDataResolucaoFormatada() => FormatarData(dataResolucao);
    public string GetDataArquivamentoFormatada() => FormatarData(dataArquivamento);

    private static string FormatarData(DateTime? data)
        => data.HasValue ? data.Value.ToString("dd/MM/yyyy HH:mm:ss") : "Não informada";

    private void ValidarIdentificador(long id)
    {
        if (id <= 0) 
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }
    }

    private void ValidarUnidade(Unidade u)
    {
        if (u == null) 
        {
            throw new ArgumentException("A unidade não pode ser nula.");
        }
    }

    private void ValidarRegistrador(Morador r)
    {
        if (r == null) 
        {
            throw new ArgumentException("O registrador não pode ser nulo.");
        }
    }

    private void ValidarDescricao(string d)
    {
        if (string.IsNullOrWhiteSpace(d))
        {
            throw new ArgumentException("A descrição não pode estar vazia.");
        }
    }

    public long Identificador => identificador;
    public Unidade Unidade => unidade;
    public Morador Registrador => registrador;
    public TipoOcorrencia Tipo => tipo;
    public string Descricao => descricao;
    public DateTime DataRegistro => dataRegistro;
    public SituacaoOcorrencia Situacao => situacao;
    public string RespostaAdministracao => respostaAdministracao;
    public DateTime? DataInicioAnalise => dataInicioAnalise;
    public DateTime? DataResolucao => dataResolucao;
    public DateTime? DataArquivamento => dataArquivamento;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) 
        {
            return true;
        }

        if (obj is not Ocorrencia outra) 
        {
            return false;
        }
        return identificador == outra.identificador;
    }

    public override int GetHashCode() => HashCode.Combine(identificador);

    public override string ToString() => identificador + " - " + tipo.GetNomeFormatado() + " - "
            + unidade.GetIdentificacaoCompleta() + " - " + situacao.GetNomeFormatado();
}
