namespace Application.Sistema.Animais.Zoologico.Aves;

public abstract class Ave : Animal
{
    public bool PodeVoar { get; }

    protected Ave(string nome, int idade) : this(nome, idade, Sexo.MACHO, true)
    {
    }

    protected Ave(string nome, int idade, Sexo sexo, bool podeVoar) : base(nome, idade, sexo)
    {
        PodeVoar = podeVoar;
    }

    public void Voar()
    {
        if (PodeVoar)
        {
            Console.WriteLine(Nome + " está voando.");
        }
        else
        {
            Console.WriteLine(Nome + " não pode voar.");
        }
    }
}
