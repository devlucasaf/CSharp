namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public class Tigre : Felino
{
    public Tigre(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " rosna!");
    }
}
