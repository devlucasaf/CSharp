namespace Application.Sistema.Animais.Zoologico.Repteis;

public class Iguana : Reptil
{
    public Iguana(string nome, int idade) : base(nome, idade)
    {
    }

    public void SubirArvore()
    {
        Console.WriteLine(Nome + " está subindo em uma árvore.");
    }

    public override void EmitirSom()
    {
        Console.WriteLine(Nome + " faz um som grave ao se sentir ameaçada.");
    }
}
