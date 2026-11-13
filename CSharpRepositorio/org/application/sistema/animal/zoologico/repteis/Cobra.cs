namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Cobra : Reptil
{
    public Cobra(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " sibila!");
    }
}
