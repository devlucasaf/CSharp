namespace CSharpRepositorio.org.application.Veiculo;

public class Onibus : VeiculoAutoMovel
{
    private int     quantidadePortas;
    private int     quantidadeAssentos;
    private int     capacidadePassageiros;
    private int     quantidadePassageirosAtuais;
    private int     quantidadeMarchas;
    private bool    portaAberta;
    private bool    arCondicionado;
    private bool    cobrador;
    private bool    tv;

    public Onibus(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                  int quantidadePortas, int quantidadeAssentos, int capacidadePassageiros, int quantidadePassageirosAtuais,
                  int quantidadeMarchas, bool portaAberta, bool arCondicionado, bool cobrador, bool tv)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        this.quantidadePortas = quantidadePortas;
        this.quantidadeAssentos = quantidadeAssentos;
        this.capacidadePassageiros = capacidadePassageiros;
        this.quantidadePassageirosAtuais = quantidadePassageirosAtuais;
        this.quantidadeMarchas = quantidadeMarchas;
        this.portaAberta = portaAberta;
        this.arCondicionado = arCondicionado;
        this.cobrador = cobrador;
        this.tv = tv;
        this.quantidadePassageirosAtuais = 0;
        this.portaAberta = false;
    }

    public void SetQuantidadePortas(int quantidadePortas)
    {
        if (quantidadePortas > 0)
        {
            this.quantidadePortas = quantidadePortas;
        }
    }

    public void SetQuantidadeAssentos(int quantidadeAssentos)
    {
        if (quantidadeAssentos > 0)
        {
            this.quantidadeAssentos = quantidadeAssentos;
        }
    }

    public void SetCapacidadePassageiros(int capacidadePassageiros)
    {
        if (capacidadePassageiros > 0)
        {
            this.capacidadePassageiros = capacidadePassageiros;
        }
    }

    public void SetQuantidadeMarchas(int quantidadeMarchas)
    {
        if (quantidadeMarchas > 0)
        {
            this.quantidadeMarchas = quantidadeMarchas;
        }
    }

    public void AbrirPorta()
    {
        if (GetVelocidade() == 0)
        {
            portaAberta = true;
            Console.WriteLine("Portas abertas!");
        }
        else
        {
            Console.WriteLine("Ônibus em movimento, não pode abrir as portas!");
        }
    }

    public void FecharPorta()
    {
        portaAberta = false;
        Console.WriteLine("Portas fechadas!");
    }

    public void EmbarcarPassageiro()
    {
        if (quantidadePassageirosAtuais < capacidadePassageiros)
        {
            quantidadePassageirosAtuais++;
        }
        else
        {
            Console.WriteLine("Ônibus lotado!");
        }
    }

    public void DescerPassageiro()
    {
        if (GetVelocidade() > 0)
        {
            Console.WriteLine("O ônibus precisa estar parado para descer passageiros.");
            return;
        }

        if (quantidadePassageirosAtuais > 0)
        {
            quantidadePassageirosAtuais--;
            Console.WriteLine("Passageiro desceu! Quantidade de passageiros: " + quantidadePassageirosAtuais);
        }
        else
        {
            Console.WriteLine("Não há passageiros para descer.");
        }
    }

    public override void Acelerar(int incremento)
    {
        if (portaAberta)
        {
            Console.WriteLine("Portas abertas! Feche as portas para poder acelerar!");
            return;
        }
        base.Acelerar(incremento);
    }

    public override void MostrarDados()
    {
        base.MostrarDados();
        Console.WriteLine("Quantidade de portas: " + quantidadePortas);
        Console.WriteLine("Quantidade de assentos: " + quantidadeAssentos);
        Console.WriteLine("Capacidade de passageiros: " + capacidadePassageiros);
        Console.WriteLine("Passageiros atuais: " + quantidadePassageirosAtuais);
        Console.WriteLine("Ar-condicionado: " + (arCondicionado ? "Sim" : "Não"));
        Console.WriteLine("Cobrador: " + (cobrador ? "Sim" : "Não"));
        Console.WriteLine("TV: " + (tv ? "Sim" : "Não"));
    }
}