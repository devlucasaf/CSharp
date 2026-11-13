namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Cagado : Reptil
{
    public Cagado(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz um som baixo.");
    }
}
