namespace Application.Sistema.Animais.Zoologico.Aves;

public class Flamingo : Ave
{
    public Flamingo(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite sons nasais.");
    }
}
