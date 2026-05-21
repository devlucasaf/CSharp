namespace CSharpRepositorio.org.application.Veiculo;

public class Aviao : VeiculoAutoMovel
{
    private int capacidadePassageiros;
    private int quantidadePassageirosAtuais;
    private int quantidadeTripulantes;
    private double altitude;
    private bool comida;
    private bool tremPousoAbaixado;
    private bool pilotoAutomatico;
    private bool wifi;
    private bool classeExecutiva;
    private string tipoAviao;

    public Aviao(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                 int capacidadePassageiros, int quantidadePassageirosAtuais, int quantidadeTripulantes,
                 double altitude, bool comida, bool tremPousoAbaixado, bool pilotoAutomatico,
                 bool wifi, bool classeExecutiva, string tipoAviao)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        this.capacidadePassageiros = capacidadePassageiros;
        this.quantidadePassageirosAtuais = 0;
        this.quantidadeTripulantes = quantidadeTripulantes;
        this.altitude = 0;
        this.comida = false;
        this.tremPousoAbaixado = true;
        this.pilotoAutomatico = pilotoAutomatico;
        this.wifi = wifi;
        this.classeExecutiva = classeExecutiva;
        this.tipoAviao = tipoAviao;
    }

    public void SetCapacidadePassageiros(int capacidadePassageiros)
    {
        if (capacidadePassageiros > 0)
        {
            this.capacidadePassageiros = capacidadePassageiros;
        }
    }

    public void EmbarcarPassageiros()
    {
        if (quantidadePassageirosAtuais < capacidadePassageiros)
        {
            quantidadePassageirosAtuais++;
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
            this.quantidadeTripulantes = quantidadeTripulantes;
        }
    }

    public void SetTipoAviao(string tipoAviao)
    {
        this.tipoAviao = tipoAviao;
    }

    public void Decolar()
    {
        if (GetVelocidade() >= 250)
        {
            altitude = 1000;
            tremPousoAbaixado = false;
            Console.WriteLine("Avião decolou!");
        }
        else
        {
            Console.WriteLine("Velocidade insuficiente para iniciar a decolagem!");
        }
    }

    public void SubirAviao(double metros)
    {
        if (altitude > 0)
        {
            altitude += metros;
        }
    }

    public void SetServirComida()
    {
        if (altitude >= 10000)
        {
            this.comida = true;
            Console.WriteLine("Serviço de bordo iniciado!");
        }
        else
        {
            Console.WriteLine("Altitude insuficiente para iniciar o serviço de bordo!");
        }
    }

    public void DesligarTremPouso()
    {
        if (altitude >= 100)
        {
            this.tremPousoAbaixado = false;
            Console.WriteLine("Avião decolando! Trem de pouso desligado!");
        }
        else
        {
            Console.WriteLine("Avião em solo! Trem de pouso ligado!");
        }
    }

    public void SetPilotoAutomatico(bool pilotoAutomatico)
    {
        if (altitude > 10000)
        {
            this.pilotoAutomatico = true;
            Console.WriteLine("Piloto automático ligado!");
        }
        else
        {
            Console.WriteLine("Altitude insuficiente para ligar o piloto automático!");
        }
    }

    public void DesligarPilotoAutomatico()
    {
        pilotoAutomatico = false;
        Console.WriteLine("Piloto automático desligado!");
    }

    public void ConectarInternet()
    {
        if (altitude >= 5000)
        {
            wifi = true;
            Console.WriteLine("Wi-fi ligado!");
        }
        else
        {
            Console.WriteLine("Wi-fi não conectado! Só será ativado durante o voo!");
        }
    }

    public void DesligarWifi()
    {
        wifi = false;
        Console.WriteLine("Wi-fi desligado!");
    }

    public void ConfigurarClasseExecutiva(bool possuiClasseExecutiva)
    {
        this.classeExecutiva = possuiClasseExecutiva;

        if (classeExecutiva)
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

        Console.WriteLine("Tipo de avião: " + tipoAviao);
        Console.WriteLine("Capacidade de passageiros: " + capacidadePassageiros);
        Console.WriteLine("Passageiros atuais: " + quantidadePassageirosAtuais);
        Console.WriteLine("Quantidade de tripulantes: " + quantidadeTripulantes);
        Console.WriteLine("Altitude atual: " + altitude + " m");
        Console.WriteLine("Trem de pouso abaixado: " + (tremPousoAbaixado ? "Sim" : "Não"));
        Console.WriteLine("Piloto automático: " + (pilotoAutomatico ? "Ligado" : "Desligado"));
        Console.WriteLine("Serviço de bordo: " + (comida ? "Ativo" : "Inativo"));
        Console.WriteLine("Wi-Fi: " + (wifi ? "Ligado" : "Desligado"));
        Console.WriteLine("Classe executiva: " + (classeExecutiva ? "Sim" : "Não"));
    }
} 
