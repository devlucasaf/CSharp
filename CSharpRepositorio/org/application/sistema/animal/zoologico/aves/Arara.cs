namespace Application.Sistema.Animais.Zoologico.Aves;

public class Arara : Ave
{
    public Arara(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " grita!");
    }
}
