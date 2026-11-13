namespace Application.Sistema.Animais.Zoologico.Repteis;

public abstract class Reptil : Animal
{
    public bool Peconhento { get; }

    protected Reptil(string nome, int idade) : this(nome, idade, Sexo.MACHO, false)
    {
    }

    protected Reptil(string nome, int idade, Sexo sexo, bool peconhento) : base(nome, idade, sexo)
    {
        Peconhento = peconhento;
    }

    public void TrocarPele()
    {
        Console.WriteLine(Nome + " está trocando de pele.");
    }

    public void Rastejar()
    {
        Console.WriteLine(Nome + " está se locomovendo lentamente.");
    }
}
