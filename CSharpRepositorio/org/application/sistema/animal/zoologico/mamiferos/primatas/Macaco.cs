namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public class Macaco : Primata
{
    public Macaco(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " grita e pula!");
    }
}
