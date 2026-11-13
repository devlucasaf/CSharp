namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Lagarto : Reptil
{
    public Lagarto(string nome, int idade) : base(nome, idade)
    {
    }

    public void TomarSol()
    {
        Console.WriteLine(Nome + " está tomando sol para regular a temperatura do corpo.");
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite um som baixo.");
    }
}
