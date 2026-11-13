namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public class Leao : Felino
{
    public Leao(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " ruge!");
    }
}
