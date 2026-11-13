namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public class Ariranha : Mamifero
{
    public Ariranha(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite sons agudos!");
    }
}
