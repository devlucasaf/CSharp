namespace Application.Sistema.Animais.Zoologico.Aves;

public class Pato : Ave
{
    public Pato(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz quack quack!");
    }
}
