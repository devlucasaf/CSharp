namespace Application.Sistema.Automovel.Veiculos;

internal abstract class VeiculoAutoMovel
{
    private static int _contadorVeiculos = 0;
    private readonly int _numeroVeiculo;

    public string   Marca           { get; }
    public string   Modelo          { get; }
    public double   Preco           { get; }
    public double   Km              { get; }
    public int      AnoLancamento   { get; }
    public int      Velocidade      { get; private set; }

    protected VeiculoAutoMovel(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade)
    {
        Marca = marca;
        Modelo = modelo;
        Preco = preco;
        Km = km;
        AnoLancamento = anoLancamento;
        Velocidade = velocidade;

        _contadorVeiculos++;
        _numeroVeiculo = _contadorVeiculos;
    }

    public virtual void Acelerar(int incremento)
    {
        if (incremento > 0)
        {
            Velocidade += incremento;
        }
    }

    public void Frear(int reducao)
    {
        if (reducao > 0 && Velocidade - reducao >= 0)
        {
            Velocidade -= reducao;
        }
    }

    // --- FORMATA UM BOOLEANO COMO O JAVA ---
    protected static string Booleano(bool valor)
    {
        if (valor)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }

    public virtual void MostrarDados()
    {
        string cor = CoresHexadecimaisTerminal.CorHexadecimal(
            (_numeroVeiculo * 40) % 255,
            (_numeroVeiculo * 80) % 255,
            (_numeroVeiculo * 120) % 255
        );
        Console.WriteLine(
            cor + ">>>>>>>>>> VEÍCULO " + _numeroVeiculo + " (" +
            GetType().Name + " )" + " <<<<<<<<<<\n" + CoresHexadecimaisTerminal.RESET
        );
        Console.WriteLine("Marca: " + Marca);
        Console.WriteLine("Modelo: " + Modelo);
        Console.WriteLine($"Preço: R${Preco:F2}");
        Console.WriteLine($"Quilometragem: {Km:F2} kmh");
        Console.WriteLine("Ano de lançamento: " + AnoLancamento);
        Console.WriteLine("Velocidade: " + Velocidade);
    }
}
