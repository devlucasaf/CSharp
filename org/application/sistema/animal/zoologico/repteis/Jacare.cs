namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Jacare : Reptil
{
    public Jacare(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " estala a mandíbula!");
    }
}
