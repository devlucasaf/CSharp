namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public class Gorila : Primata
{
    public Gorila(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " bate no peito e grunhe!");
    }
}
