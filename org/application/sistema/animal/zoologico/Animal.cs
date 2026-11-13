namespace Application.Sistema.Animais.Zoologico;

public abstract class Animal
{
    public string   Nome        { get; }
    public int      Idade       { get; }
    public Sexo     Sexo        { get; }
    public bool     Acordado    { get; private set; }

    protected Animal(string nome, int idade) : this(nome, idade, Sexo.MACHO)
    {
    }

    protected Animal(string nome, int idade, Sexo? sexo)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome do animal nao pode ser vazio.");
        }

        if (idade < 0)
        {
            throw new ArgumentException("Idade nao pode ser negativa.");
        }

        Nome = nome;
        Idade = idade;
        Sexo = sexo ?? Sexo.MACHO;
        Acordado = true;
    }

    public abstract void EmitirSom();

    public void Dormir()
    {
        if (!Acordado)
        {
            Console.WriteLine(Nome + " ja esta dormindo.");
            return;
        }

        Acordado = false;
        Console.WriteLine(Nome + " esta dormindo.");
    }

    public void Acordar()
    {
        if (Acordado)
        {
            Console.WriteLine(Nome + " ja esta acordado.");
            return;
        }

        Acordado = true;
        Console.WriteLine(Nome + " acordou.");
    }

    public void Alimentar()
    {
        Console.WriteLine(Nome + " esta se alimentando.");
    }

    public void Apresentar()
    {
        Console.WriteLine("Eu sou " + Nome + ", tenho " + Idade
                + " ano(s), sexo " + Sexo.GetDescricao() + ".");
    }

    public string Tipo => GetType().Name;

    public override string ToString()
    {
        return Tipo + " [" + Nome + ", " + Idade + " ano(s), " + Sexo.GetDescricao() + "]";
    }
}
