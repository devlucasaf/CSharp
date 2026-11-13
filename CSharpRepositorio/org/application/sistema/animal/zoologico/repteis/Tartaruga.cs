namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Tartaruga : Reptil
{
    public Tartaruga(string nome, int idade) : base(nome, idade)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz sons quase imperceptíveis.");
    }
}
