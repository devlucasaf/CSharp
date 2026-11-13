namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public class Pantera : Felino
{
    public Pantera(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " ruge baixo na escuridao!");
    }
}
