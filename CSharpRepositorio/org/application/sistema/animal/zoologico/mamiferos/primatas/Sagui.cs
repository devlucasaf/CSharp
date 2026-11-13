namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public class Sagui : Primata
{
    public Sagui(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " pia rapidamente!");
    }
}
