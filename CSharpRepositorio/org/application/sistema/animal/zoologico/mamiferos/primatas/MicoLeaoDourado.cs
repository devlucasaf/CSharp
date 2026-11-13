namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public class MicoLeaoDourado : Primata
{
    public MicoLeaoDourado(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " guincha agudo!");
    }
}
