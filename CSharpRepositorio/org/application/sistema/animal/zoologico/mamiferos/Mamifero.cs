namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public abstract class Mamifero : Animal
{
    protected Mamifero(string nome, int idade) : base(nome, idade)
    {
    }

    protected Mamifero(string nome, int idade, Sexo sexo) : base(nome, idade, sexo)
    {
    }

    public void Amamentar()
    {
        if (Sexo == Sexo.FEMEA)
        {
            Console.WriteLine(Nome + " esta amamentando.");
        }
        else
        {
            Console.WriteLine(Nome + " nao pode amamentar.");
        }
    }
}
