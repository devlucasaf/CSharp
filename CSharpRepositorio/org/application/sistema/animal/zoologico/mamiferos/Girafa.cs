namespace Application.Sistema.Animais.Zoologico.Mamiferos;

public class Girafa : Mamifero
{
    public Girafa(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite sons quase inaudíveis.");
    }
}
