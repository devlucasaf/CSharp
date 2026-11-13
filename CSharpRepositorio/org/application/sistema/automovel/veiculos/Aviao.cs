namespace Application.Sistema.Automovel.Veiculos;

internal class Aviao : VeiculoAutoMovel
{
    private int     _capacidadePassageiros;
    private int     _quantidadePassageirosAtuais;
    private int     _quantidadeTripulantes;
    private double  _altitude;
    private bool    _comida;
    private bool    _tremPousoAbaixado;
    private bool    _pilotoAutomatico;
    private bool    _wifi;
    private bool    _classeExecutiva;
    private string  _tipoAviao;

    public Aviao(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                int capacidadePassageiros, int quantidadePassageirosAtuais, int quantidadeTripulantes,
                double altitude, bool comida, bool tremPousoAbaixado, bool pilotoAutomatico,
                bool wifi, bool classeExecutiva, string tipoAviao)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        _capacidadePassageiros = capacidadePassageiros;
        _quantidadePassageirosAtuais = 0;
        _quantidadeTripulantes = quantidadeTripulantes;
        _altitude = 0;
        _comida = false;
        _tremPousoAbaixado = true;
        _pilotoAutomatico = pilotoAutomatico;
        _wifi = wifi;
        _classeExecutiva = classeExecutiva;
        _tipoAviao = tipoAviao;
    }

    public void SetCapacidadePassageiros(int capacidadePassageiros)
    {
        if (capacidadePassageiros > 0)
        {
            _capacidadePassageiros = capacidadePassageiros;
        }
    }

    public void EmbarcarPassageiros()
    {
        if (_quantidadePassageirosAtuais < _capacidadePassageiros)
        {
            _quantidadePassageirosAtuais++;
        }
        else
        {
            Console.WriteLine("Avião lotado!");
        }
    }

    public void SetQuantidadeTripulantes(int quantidadeTripulantes)
    {
        if (quantidadeTripulantes > 0)
        {
            _quantidadeTripulantes = quantidadeTripulantes;
        }
    }

    public void SetTipoAviao(string tipoAviao)
    {
        _tipoAviao = tipoAviao;
    }

    public void Decolar()
    {
        if (Velocidade >= 250)
        {
            _altitude = 1000;
            _tremPousoAbaixado = false;
            Console.WriteLine("Avião decolou!");
        }
        else
        {
            Console.WriteLine("Velocidade insuficiente para iniciar a decolagem!");
        }
    }

    public void SubirAviao(double metros)
    {
        if (_altitude > 0)
        {
            _altitude += metros;
        }
    }

    public void SetServirComida()
    {
        if (_altitude >= 10000)
        {
            _comida = true;
            Console.WriteLine("Serviço de bordo iniciado!");
        }
        else
        {
            Console.WriteLine("Altitude insuficiente para iniciar o serviço de bordo!");
        }
    }

    public void DesligarTremPouso()
    {
        if (_altitude >= 100)
        {
            _tremPousoAbaixado = false;
            Console.WriteLine("Avião decolando! Trem de pouso desligado!");
        }
        else
        {
            Console.WriteLine("Avião em solo! Trem de pouso ligado!");
        }
    }

    // --- O PARÂMETRO É IGNORADO, COMO NO CÓDIGO ORIGINAL ---
    public void SetPilotoAutomatico(bool pilotoAutomatico)
    {
        if (_altitude > 10000)
        {
            _pilotoAutomatico = true;
            Console.WriteLine("Piloto automático ligado!");
        }
        else
        {
            Console.WriteLine("Altitude insuficiente para ligar o piloto automático!");
        }
    }

    public void DesligarPilotoAutomatico()
    {
        _pilotoAutomatico = false;
        Console.WriteLine("Piloto automático desligado!");
    }

    public void ConectarInternet()
    {
        if (_altitude >= 5000)
        {
            _wifi = true;
            Console.WriteLine("Wi-fi ligado!");
        }
        else
        {
            Console.WriteLine("Wi-fi não conectado! Só será ativado durante o voo!");
        }
    }

    public void DesligarWifi()
    {
        _wifi = false;
        Console.WriteLine("Wi-fi desligado!");
    }

    public void ConfigurarClasseExecutiva(bool possuiClasseExecutiva)
    {
        _classeExecutiva = possuiClasseExecutiva;

        if (_classeExecutiva)
        {
            Console.WriteLine("Avião com classe executiva.");
        }
        else
        {
            Console.WriteLine("Avião sem classe executiva.");
        }
    }

    public override void MostrarDados()
    {
        base.MostrarDados();

        Console.WriteLine("Tipo de avião: " + _tipoAviao);
        Console.WriteLine("Capacidade de passageiros: " + _capacidadePassageiros);
        Console.WriteLine("Passageiros atuais: " + _quantidadePassageirosAtuais);
        Console.WriteLine("Quantidade de tripulantes: " + _quantidadeTripulantes);
        Console.WriteLine($"Altitude atual: {_altitude:F2} m");
        Console.WriteLine("Trem de pouso abaixado: " + (_tremPousoAbaixado ? "Sim" : "Não"));
        Console.WriteLine("Piloto automático: " + (_pilotoAutomatico ? "Ligado" : "Desligado"));
        Console.WriteLine("Serviço de bordo: " + (_comida ? "Ativo" : "Inativo"));
        Console.WriteLine("Wi-Fi: " + (_wifi ? "Ligado" : "Desligado"));
        Console.WriteLine("Classe executiva: " + (_classeExecutiva ? "Sim" : "Não"));
    }
}
