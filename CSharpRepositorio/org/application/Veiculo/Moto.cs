namespace CSharpRepositorio.org.application.Veiculo;

public class Moto : VeiculoAutoMovel
{
    private int     cilindradas;
    private int     quantidadeMarchas;
    private int     capacidadeTanque;
    private string  tipoMoto;

    public Moto(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
        int cilindradas, int quantidadeMarchas, int capacidadeTanque, string tipoMoto)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        this.cilindradas = cilindradas;
        this.quantidadeMarchas = quantidadeMarchas;
        this.capacidadeTanque = capacidadeTanque;
        this.tipoMoto = tipoMoto;
    }

    public void SetCilindradas(int cilindradas)
    {
        if (cilindradas > 0)
        {
            this.cilindradas = cilindradas;
        }
    }

    public void SetQuantidadeMarchas(int quantidadeMarchas)
    {
        if (quantidadeMarchas > 0)
        {
            this.quantidadeMarchas = quantidadeMarchas;
        }
    }

    public void SetCapacidadeTanque(int capacidadeTanque)
    {
        if (capacidadeTanque > 0)
        {
            this.capacidadeTanque = capacidadeTanque;
        }
    }

    public void SetTipoMoto(string tipoMoto)
    {
        this.tipoMoto = tipoMoto;
    }

    public override void Acelerar(int incremento)
    {
        if (incremento > 30)
        {
            Console.WriteLine("Aceleração brusca para moto!");
            return;
        }
        base.Acelerar(incremento);
    }

    public override void MostrarDados()
    {
        base.MostrarDados();
        Console.WriteLine("Cilindradas: " + cilindradas + " cc");
        Console.WriteLine("Marchas: " + quantidadeMarchas);
        Console.WriteLine("Capacidade do tanque: " + capacidadeTanque + " L");
        Console.WriteLine("Tipo de moto: " + tipoMoto);
    }
}