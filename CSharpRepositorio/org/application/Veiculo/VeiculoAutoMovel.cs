namespace CSharpRepositorio.org.application.Veiculo;

public abstract class VeiculoAutoMovel
{
    private static int contadorVeiculos = 0;
    private int     numeroVeiculo;
    private string  marca;
    private string  modelo;
    private double  preco;
    private double  km;
    private int     anoLancamento;
    private int     velocidade;

    public VeiculoAutoMovel(string marca, string modelo, double preco,
                            double km, int anoLancamento, int velocidade)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.preco = preco;
        this.km = km;
        this.anoLancamento = anoLancamento;
        this.velocidade = velocidade;

        contadorVeiculos++;
        this.numeroVeiculo = contadorVeiculos;
    }

    public string GetMarca() => marca;
    public string GetModelo() => modelo;
    public double GetPreco() => preco;
    public double GetKm() => km;
    public int GetAnoLancamento() => anoLancamento;
    public int GetVelocidade() => velocidade;

    public virtual void Acelerar(int incremento)
    {
        if (incremento > 0)
        {
            velocidade += incremento;
        }
    }

    public virtual void Frear(int reducao)
    {
        if (reducao > 0 && velocidade - reducao >= 0)
        {
            velocidade -= reducao;
        }
    }

    public virtual void MostrarDados()
    {
        string cor = CoresHexadecimaisTerminal.CorHexadecimal(
            (numeroVeiculo * 40) % 255,
            (numeroVeiculo * 80) % 255,
            (numeroVeiculo * 120) % 255
        );
        Console.WriteLine(
            cor + ">>>>>>>>>> VEÍCULO " + numeroVeiculo + " (" +
            GetType().Name + " )" + " <<<<<<<<<<\n" + CoresHexadecimaisTerminal.RESET
        );
        Console.WriteLine("Marca: " + GetMarca());
        Console.WriteLine("Modelo: " + GetModelo());
        Console.WriteLine("Preço: R$" + GetPreco());
        Console.WriteLine("Quilometragem: " + GetKm() + " kmh");
        Console.WriteLine("Ano de lançamento: " + GetAnoLancamento());
        Console.WriteLine("Velocidade: " + GetVelocidade());
    }
}