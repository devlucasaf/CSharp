namespace Application.Sistema.Animais.Zoologico.Mamiferos.Primatas;

public abstract class Primata : Mamifero
{
    protected Primata(string nome, int idade) : base(nome, idade)
    {
    }

    protected Primata(string nome, int idade, Sexo sexo) : base(nome, idade, sexo)
    {
    }

    public void SubirArvore()
    {
        Console.WriteLine(Nome + " esta subindo na arvore.");
    }

    public void UsarFerramenta()
    {
        Console.WriteLine(Nome + " esta usando uma ferramenta.");
    }
}
