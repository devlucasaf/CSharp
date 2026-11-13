namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public class Capivara : Mamifero
{
    public Capivara(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz um som baixo de capivara.");
    }
}
