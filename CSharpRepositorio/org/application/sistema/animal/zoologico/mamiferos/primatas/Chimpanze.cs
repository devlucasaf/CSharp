namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public class Chimpanze : Primata
{
    public Chimpanze(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " grita e pula!");
    }
}
