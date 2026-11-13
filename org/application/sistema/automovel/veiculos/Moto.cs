namespace Application.Sistema.Automovel.Veiculos;

internal class Moto : VeiculoAutoMovel
{
    private int _cilindradas;
    private int _quantidadeMarchas;
    private int _capacidadeTanque;
    private string _tipoMoto;

    public Moto(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                int cilindradas, int quantidadeMarchas, int capacidadeTanque, string tipoMoto)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        _cilindradas = cilindradas;
        _quantidadeMarchas = quantidadeMarchas;
        _capacidadeTanque = capacidadeTanque;
        _tipoMoto = tipoMoto;
    }

    public void SetCilindradas(int cilindradas)
    {
        if (cilindradas > 0)
        {
            _cilindradas = cilindradas;
        }
    }

    public void SetQuantidadeMarchas(int quantidadeMarchas)
    {
        if (quantidadeMarchas > 0)
        {
            _quantidadeMarchas = quantidadeMarchas;
        }
    }

    public void SetCapacidadeTanque(int capacidadeTanque)
    {
        if (capacidadeTanque > 0)
        {
            _capacidadeTanque = capacidadeTanque;
        }
    }

    public void SetTipoMoto(string tipoMoto)
    {
        _tipoMoto = tipoMoto;
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
        Console.WriteLine("Cilindradas: " + _cilindradas + " cc");
        Console.WriteLine("Marchas: " + _quantidadeMarchas);
        Console.WriteLine("Capacidade do tanque: " + _capacidadeTanque + " L");
        Console.WriteLine("Tipo de moto: " + _tipoMoto);
    }
}
