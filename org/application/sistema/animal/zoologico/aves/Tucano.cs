namespace Application.Sistema.Animais.Zoologico.Aves;

public class Tucano : Ave
{
    public Tucano(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz um som alto com o bico!");
    }
}
