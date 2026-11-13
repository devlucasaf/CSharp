namespace Application.Sistema.Animais.Zoologico.Aves;

public class Pavao : Ave
{
    public Pavao(string nome, int idade) : base(nome, idade)
    {
    }

    public void AbrirCauda()
    {
        Console.WriteLine(Nome + " abre sua linda cauda colorida.");
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " emite um chamado alto.");
    }
}
