namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public class Orangotango : Primata
{
    public Orangotango(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite chamados longos!");
    }
}
