using System;

namespace Application.System.Condominio;

public class Morador
{
    private readonly long   identificador;
    private string          nome;
    private readonly string cpf;
    private DateOnly?       dataNascimento;
    private string          telefone;
    private string          email;
    private TipoMorador     tipo;
    private bool            ativo;

    public Morador(long identificador, string nome, string cpf, DateOnly? dataNascimento,
                    string telefone, string email, TipoMorador tipo)
    {
        ValidarIdentificador(identificador);
        ValidarTexto(nome, "O nome");
        ValidarTexto(cpf, "O CPF");

        this.identificador = identificador;
        this.nome = nome.Trim();
        this.cpf = cpf.Trim();
        this.dataNascimento = dataNascimento;
        this.telefone = telefone?.Trim() ?? "";
        this.email = email?.Trim() ?? "";
        this.tipo = tipo;
        this.ativo = true;
    }

    // --- ATUALIZA OS DADOS DO MORADOR ---
    public void Atualizar(string nome, DateOnly? dataNascimento, string telefone, string email, TipoMorador tipo)
    {
        ValidarTexto(nome, "O nome");

        this.nome = nome.Trim();
        this.dataNascimento = dataNascimento;
        this.telefone = telefone?.Trim() ?? "";
        this.email = email?.Trim() ?? "";
        this.tipo = tipo;
    }

    // --- CALCULA A IDADE DO MORADOR ---
    public int CalcularIdade()
    {
        if (dataNascimento == null)
        {
            return 0;
        }

        DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
        int idade = hoje.Year - dataNascimento.Value.Year;
        if (hoje < dataNascimento.Value.AddYears(idade))
        {
            idade--;
        }
        return idade;
    }

    public void Desativar() => ativo = false;
    public void Reativar() => ativo = true;

    private void ValidarIdentificador(long identificador)
    {
        if (identificador <= 0)
        {
            throw new ArgumentException("O identificador deve ser maior que zero.");
        }
    }

    private void ValidarTexto(string texto, string campo)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException(campo + " não pode estar vazio.");
        }
    }

    public long Identificador => identificador;
    public string Nome => nome;
    public string Cpf => cpf;
    public DateOnly? DataNascimento => dataNascimento;
    public string Telefone => telefone;
    public string Email => email;
    public TipoMorador Tipo => tipo;
    public bool IsAtivo => ativo;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) 
        {
            return true;
        }

        if (obj is not Morador outro) 
        {
            return false;
        }
        return cpf == outro.cpf;
    }

    public override int GetHashCode() => HashCode.Combine(cpf);

    public override string ToString()
    {
        return identificador + " - " + nome + " - " + tipo.GetNomeFormatado();
    }
}
