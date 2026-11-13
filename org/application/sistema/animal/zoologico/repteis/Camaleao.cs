namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Camaleao : Reptil
{
    public Camaleao(string nome, int idade) : base(nome, idade)
    {
    }

    public void MudarCor()
    {
        Console.WriteLine(Nome + " está mudando de cor para se camuflar.");
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite um som sutil.");
    }
}
