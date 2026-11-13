namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public class Jaguatirica : Felino
{
    public Jaguatirica(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite um miado forte!");
    }
}
