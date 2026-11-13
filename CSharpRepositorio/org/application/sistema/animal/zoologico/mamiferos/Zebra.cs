namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public class Zebra : Mamifero
{
    public Zebra(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " relincha!");
    }
}
