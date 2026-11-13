namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Calango : Reptil
{
    public Calango(string nome, int idade) : base(nome, idade)
    {
    }

    public void Correr()
    {
        Console.WriteLine(Nome + " corre rapidamente pelas pedras.");
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz um leve chiado.");
    }
}
