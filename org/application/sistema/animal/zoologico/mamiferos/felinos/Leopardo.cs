namespace Application.Sistema.Animais.Zoologico.Mamiferos.Felinos;

public class Leopardo : Felino
{
    public Leopardo(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " ronrona alto!");
    }
}
