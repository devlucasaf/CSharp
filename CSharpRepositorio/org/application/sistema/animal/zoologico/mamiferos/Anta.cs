namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public class Anta : Mamifero
{
    public Anta(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " resmunga.");
    }
}
