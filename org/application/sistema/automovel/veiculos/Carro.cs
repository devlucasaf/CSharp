namespace Application.Sistema.Automovel.Veiculos;

internal class Carro : VeiculoAutoMovel
{
    private int _quantidadePortas;
    private int _quantidadeMarchas;
    private int _quantidadePassageiros;
    private int _passageirosAtuais;
    private string _tipoCambio;
    private string _tipoCombustivel;
    private double _capacidadePortaMalas;
    private bool _pilotoAutomatico;

    public Carro(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                int quantidadePortas, int quantidadeMarchas, int quantidadePassageiros, int passageirosAtuais,
                string tipoCambio, string tipoCombustivel, double capacidadePortaMalas, bool pilotoAutomatico)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        _quantidadePortas = quantidadePortas;
        _quantidadeMarchas = quantidadeMarchas;
        _quantidadePassageiros = quantidadePassageiros;
        _tipoCambio = tipoCambio;
        _tipoCombustivel = tipoCombustivel;
        _capacidadePortaMalas = capacidadePortaMalas;
        _pilotoAutomatico = pilotoAutomatico;
        _passageirosAtuais = 0;
    }

    public void SetQuantidadePortas(int quantidadePortas)
    {
        if (quantidadePortas > 0)
        {
            _quantidadePortas = quantidadePortas;
        }
    }

    public void SetQuantidadeMarchas(int quantidadeMarchas)
    {
        if (quantidadeMarchas > 0)
        {
            _quantidadeMarchas = quantidadeMarchas;
        }
    }

    public void SetQuantidadePassageiros(int quantidadePassageiros)
    {
        if (quantidadePassageiros > 0)
        {
            _quantidadePassageiros = quantidadePassageiros;
        }
    }

    public void SetTipoCombustivel(string tipoCombustivel)
    {
        _tipoCombustivel = tipoCombustivel;
    }

    public void SetTipoCambio(string tipoCambio)
    {
        _tipoCambio = tipoCambio;
    }

    public void SetCapacidadePortaMalas(double capacidadePortaMalas)
    {
        if (capacidadePortaMalas > 0)
        {
            _capacidadePortaMalas = capacidadePortaMalas;
        }
    }

    public void SetPilotoAutomatico(bool pilotoAutomatico)
    {
        _pilotoAutomatico = pilotoAutomatico;
    }

    public void EntrarPassageiro()
    {
        if (_passageirosAtuais < _quantidadePassageiros)
        {
            _passageirosAtuais++;
            Console.WriteLine("Passageiro entrou! Quantidade de passageiros: " + _passageirosAtuais);
        }
        else
        {
            Console.WriteLine("Carro lotado!");
        }
    }

    public void SairPassageiro()
    {
        if (_passageirosAtuais > 0)
        {
            _passageirosAtuais--;
            Console.WriteLine("Passageiro saiu! Quantidade de passageiros: " + _passageirosAtuais);
        }
        else
        {
            Console.WriteLine("Não há passageiros no carro!");
        }
    }

    public void AtivarPilotoAutomatico()
    {
        if (!_pilotoAutomatico)
        {
            Console.WriteLine("Este carro não possui piloto automático.");
            return;
        }

        if (Velocidade < 40)
        {
            Console.WriteLine("Velocidade mínima de 40km/h.");
            return;
        }

        Console.WriteLine("Piloto automático ativado.");
    }

    public override void MostrarDados()
    {
        base.MostrarDados();
        Console.WriteLine("Quantidade de portas: " + _quantidadePortas);
        Console.WriteLine("Tipo de câmbio: " + _tipoCambio);
        Console.WriteLine("Quantidade de marchas: " + _quantidadeMarchas);
        Console.WriteLine("Quantidade de passageiros permitido: " + _quantidadePassageiros);
        Console.WriteLine("Tipo de combustível: " + _tipoCombustivel);
        Console.WriteLine($"Capacidade do porta malas: {_capacidadePortaMalas:F2} L");
        Console.WriteLine("Tem piloto automático: " + Booleano(_pilotoAutomatico));
    }
}
