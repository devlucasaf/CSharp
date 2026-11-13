namespace Application.Sistema.Animais.Zoologico.Aves;

public class Ganso : Ave
{
    public Ganso(string nome, int idade) : base(nome, idade)
    {
    }

    public void Nadar()
    {
        Console.WriteLine(Nome + " está nadando no lago.");
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " grasna alto!");
    }
}
