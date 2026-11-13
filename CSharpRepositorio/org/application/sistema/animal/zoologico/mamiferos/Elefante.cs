namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public class Elefante : Mamifero
{
    public Elefante(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz barulho de trombeta!");
    }
}
