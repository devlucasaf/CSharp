namespace Application.Sistema.Automovel.Veiculos;

internal class Onibus : VeiculoAutoMovel
{
    private int _quantidadePortas;
    private int _quantidadeAssentos;
    private int _capacidadePassageiros;
    private int _quantidadePassageirosAtuais;
    private int _quantidadeMarchas;
    private bool _portaAberta;
    private readonly bool _arCondicionado;
    private readonly bool _cobrador;
    private readonly bool _tv;

    public Onibus(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                    int quantidadePortas, int quantidadeAssentos, int capacidadePassageiros, int quantidadePassageirosAtuais,
                    int quantidadeMarchas, bool portaAberta, bool arCondicionado, bool cobrador, bool tv)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        _quantidadePortas = quantidadePortas;
        _quantidadeAssentos = quantidadeAssentos;
        _capacidadePassageiros = capacidadePassageiros;
        _quantidadeMarchas = quantidadeMarchas;
        _arCondicionado = arCondicionado;
        _cobrador = cobrador;
        _tv = tv;
        _quantidadePassageirosAtuais = 0;
        _portaAberta = false;
    }

    public void SetQuantidadePortas(int quantidadePortas)
    {
        if (quantidadePortas > 0)
        {
            _quantidadePortas = quantidadePortas;
        }
    }

    public void SetQuantidadeAssentos(int quantidadeAssentos)
    {
        if (quantidadeAssentos > 0)
        {
            _quantidadeAssentos = quantidadeAssentos;
        }
    }

    public void SetCapacidadePassageiros(int capacidadePassageiros)
    {
        if (capacidadePassageiros > 0)
        {
            _capacidadePassageiros = capacidadePassageiros;
        }
    }

    public void SetQuantidadeMarchas(int quantidadeMarchas)
    {
        if (quantidadeMarchas > 0)
        {
            _quantidadeMarchas = quantidadeMarchas;
        }
    }

    public void AbrirPorta()
    {
        if (Velocidade == 0)
        {
            _portaAberta = true;
            Console.WriteLine("Portas abertas!");
        }
        else
        {
            Console.WriteLine("Ônibus em movimento, não pode abrir as portas!");
        }
    }

    public void FecharPorta()
    {
        _portaAberta = false;
        Console.WriteLine("Portas fechadas!");
    }

    public void EmbarcarPassageiro()
    {
        if (_quantidadePassageirosAtuais < _capacidadePassageiros)
        {
            _quantidadePassageirosAtuais++;
        }
        else
        {
            Console.WriteLine("Ônibus lotado!");
        }
    }

    public void DescerPassageiro()
    {
        if (Velocidade > 0)
        {
            Console.WriteLine("O ônibus precisa estar parado para descer passageiros.");
            return;
        }

        if (_quantidadePassageirosAtuais > 0)
        {
            _quantidadePassageirosAtuais--;
            Console.WriteLine("Passageiro desceu! Quantidade de passageiros: " + _quantidadePassageirosAtuais);
        }
        else
        {
            Console.WriteLine("Não há passageiros para descer.");
        }
    }

    public override void Acelerar(int incremento)
    {
        if (_portaAberta)
        {
            Console.WriteLine("Portas abertas! Feche as portas para poder acelerar!");
            return;
        }

        base.Acelerar(incremento);
    }

    public override void MostrarDados()
    {
        base.MostrarDados();

        Console.WriteLine("Quantidade de portas: " + _quantidadePortas);
        Console.WriteLine("Quantidade de assentos: " + _quantidadeAssentos);
        Console.WriteLine("Capacidade de passageiros: " + _capacidadePassageiros);
        Console.WriteLine("Passageiros atuais: " + _quantidadePassageirosAtuais);
        Console.WriteLine("Ar-condicionado: " + (_arCondicionado ? "Sim" : "Não"));
        Console.WriteLine("Cobrador: " + (_cobrador ? "Sim" : "Não"));
        Console.WriteLine("TV: " + (_tv ? "Sim" : "Não"));
    }
}
