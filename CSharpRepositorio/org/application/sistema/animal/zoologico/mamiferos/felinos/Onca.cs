namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public class Onca : Felino
{
    public Onca(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " ruge fortemente!");
    }
}
