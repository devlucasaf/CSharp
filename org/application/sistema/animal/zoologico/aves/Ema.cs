namespace Application.Sistema.Animais.Zoologico.Aves;

public class Ema : Ave
{
    public Ema(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite um som grave.");
    }
}
