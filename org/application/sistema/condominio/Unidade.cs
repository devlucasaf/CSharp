using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.System.Condominio;

public class Unidade
{
    private readonly long           identificador;
    private readonly string         bloco;
    private readonly string         numero;
    private readonly List<Morador>  moradores;
    private int                     quantidadeVagas;
    private bool                    ocupada;

    public Unidade(long identificador, string bloco, string numero, int quantidadeVagas)
    {
        ValidarIdentificador(identificador);
        ValidarTextoObrigatorio(bloco, "O bloco");
        ValidarTextoObrigatorio(numero, "O número da unidade");

        if (quantidadeVagas < 0)
        {
            throw new ArgumentException("A quantidade de vagas não pode ser negativa.");
        }

        this.identificador = identificador;
        this.bloco = bloco.Trim();
        this.numero = numero.Trim();
        this.quantidadeVagas = quantidadeVagas;
        this.moradores = new List<Morador>();
        this.ocupada = false;
    }

    // --- ADICIONA UM MORADOR À UNIDADE ---
    public bool AdicionarMorador(Morador morador)
    {
        if (morador == null)
        {
            throw new ArgumentException("O morador não pode ser nulo.");
        }

        if (moradores.Contains(morador) || BuscarMoradorPorCpf(morador.Cpf) != null)
        {
            return false;
        }

        moradores.Add(morador);
        AtualizarSituacaoOcupacao();
        return true;
    }

    // --- REMOVE UM MORADOR DA UNIDADE ---
    public bool RemoverMorador(Morador morador)
    {
        if (morador == null) 
        {
            return false;
        }
        bool removido = moradores.Remove(morador);
        AtualizarSituacaoOcupacao();
        return removido;
    }

    public Morador? RemoverMoradorPorCpf(string cpf)
    {
        Morador? morador = BuscarMoradorPorCpf(cpf);
        if (morador == null) 
        {
            return null;
        }
        moradores.Remove(morador);
        AtualizarSituacaoOcupacao();
        return morador;
    }

    public Morador? RemoverMoradorPorIdentificador(long identificadorMorador)
    {
        Morador? morador = BuscarMoradorPorIdentificador(identificadorMorador);
        if (morador == null) 
        {
            return null;
        }
        moradores.Remove(morador);
        AtualizarSituacaoOcupacao();
        return morador;
    }

    public Morador? BuscarMoradorPorCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) 
        {
            return null;
        }
        return moradores.FirstOrDefault(m => m.Cpf == cpf.Trim());
    }

    public Morador? BuscarMoradorPorIdentificador(long identificadorMorador)
    {
        if (identificadorMorador <= 0) 
        {
            return null;
        }
        return moradores.FirstOrDefault(m => m.Identificador == identificadorMorador);
    }

    public bool PossuiMorador(Morador morador) => morador != null && moradores.Contains(morador);

    public bool PossuiMoradorPorCpf(string cpf) => BuscarMoradorPorCpf(cpf) != null;

    // --- RETORNA O RESPONSÁVEL PRINCIPAL PELA UNIDADE ---
    public Morador? GetResponsavel()
    {
        return moradores.FirstOrDefault(m => m.IsAtivo && m.Tipo == TipoMorador.PROPRIETARIO)
            ?? moradores.FirstOrDefault(m => m.IsAtivo && m.Tipo == TipoMorador.INQUILINO)
            ?? moradores.FirstOrDefault(m => m.IsAtivo);
    }

    public List<Morador> GetMoradoresAtivos() => moradores.Where(m => m.IsAtivo).ToList().AsReadOnly();
    public List<Morador> GetProprietarios() => BuscarMoradoresPorTipo(TipoMorador.PROPRIETARIO);
    public List<Morador> GetInquilinos() => BuscarMoradoresPorTipo(TipoMorador.INQUILINO);
    public List<Morador> GetDependentes() => BuscarMoradoresPorTipo(TipoMorador.DEPENDENTE);

    public List<Morador> BuscarMoradoresPorTipo(TipoMorador tipoMorador) => moradores.Where(m => m.Tipo == tipoMorador).ToList().AsReadOnly();

    public bool PossuiProprietarioAtivo() => moradores.Any(m => m.IsAtivo && m.Tipo == TipoMorador.PROPRIETARIO);

    public bool PossuiInquilinoAtivo() => moradores.Any(m => m.IsAtivo && m.Tipo == TipoMorador.INQUILINO);

    private void AtualizarSituacaoOcupacao() => ocupada = moradores.Count > 0;

    private void ValidarIdentificador(long identificador)
    {
        if (identificador <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }
    }

    private void ValidarTextoObrigatorio(string texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException(campo + " não pode estar vazio.");
        }
    }

    public long Identificador => identificador;
    public string Bloco => bloco;
    public string Numero => numero;
    public int QuantidadeVagas => quantidadeVagas;

    public void SetQuantidadeVagas(int quantidadeVagas)
    {
        if (quantidadeVagas < 0)
        {
            throw new ArgumentException("A quantidade de vagas não pode ser negativa.");
        }
        this.quantidadeVagas = quantidadeVagas;
    }

    public void AdicionarVaga() => quantidadeVagas++;

    public void RemoverVaga()
    {
        if (quantidadeVagas == 0)
        {
            throw new InvalidOperationException("A unidade não possui vagas para remover.");
        }
        quantidadeVagas--;
    }

    public bool IsOcupada => ocupada;

    public IReadOnlyList<Morador> Moradores => moradores.AsReadOnly();

    public int QuantidadeMoradores => moradores.Count;
    public int QuantidadeMoradoresAtivos => moradores.Count(m => m.IsAtivo);

    public string GetIdentificacaoCompleta() => "Bloco " + bloco + ", unidade " + numero;

    public string GetSituacaoFormatada() => ocupada ? "Ocupada" : "Desocupada";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) 
        {
            return true;
        }

        if (obj is not Unidade outra) 
        {
            return false;
        }

        return string.Equals(bloco, outra.bloco, StringComparison.OrdinalIgnoreCase)
            && string.Equals(numero, outra.numero, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() => HashCode.Combine(bloco.ToLowerInvariant(), numero.ToLowerInvariant());

    public override string ToString()
        => identificador + " - " + GetIdentificacaoCompleta() + " - " + GetSituacaoFormatada()
            + " - moradores: " + moradores.Count + " - vagas: " + quantidadeVagas;
}
