namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public abstract class Felino : Mamifero
{
    protected Felino(string nome, int idade) : base(nome, idade)
    {
    }

    protected Felino(string nome, int idade, Sexo sexo) : base(nome, idade, sexo)
    {
    }

    public void Cacar()
    {
        Console.WriteLine(Nome + " esta caçando silenciosamente.");
    }

    public void AfiarGarras()
    {
        Console.WriteLine(Nome + " esta afiando as garras.");
    }
}
