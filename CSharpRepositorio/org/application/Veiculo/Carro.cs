namespace CSharpRepositorio.org.application.Veiculo;

public class Carro : VeiculoAutoMovel
{
    private int     quantidadePortas;
    private int     quantidadeMarchas;
    private int     quantidadePassageiros;
    private int     passageirosAtuais;
    private string  tipoCambio;
    private string  tipoCombustivel;
    private double  capacidadePortaMalas;
    private bool    pilotoAutomatico;

    public Carro(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                int quantidadePortas, int quantidadeMarchas, int quantidadePassageiros, int passageirosAtuais,
                string tipoCambio, string tipoCombustivel, double capacidadePortaMalas, bool pilotoAutomatico)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        this.quantidadePortas = quantidadePortas;
        this.quantidadeMarchas = quantidadeMarchas;
        this.quantidadePassageiros = quantidadePassageiros;
        this.passageirosAtuais = passageirosAtuais;
        this.tipoCambio = tipoCambio;
        this.tipoCombustivel = tipoCombustivel;
        this.capacidadePortaMalas = capacidadePortaMalas;
        this.pilotoAutomatico = pilotoAutomatico;
        this.passageirosAtuais = 0;
    }

    public void SetQuantidadePortas(int quantidadePortas)
    {
        if (quantidadePortas > 0)
        {
            this.quantidadePortas = quantidadePortas;
        }
    }

    public void SetQuantidadeMarchas(int quantidadeMarchas)
    {
        if (quantidadeMarchas > 0)
        {
            this.quantidadeMarchas = quantidadeMarchas;
        }
    }

    public void SetQuantidadePassageiros(int quantidadePassageiros)
    {
        if (quantidadePassageiros > 0)
        {
            this.quantidadePassageiros = quantidadePassageiros;
        }
    }

    public void SetTipoCombustivel(string tipoCombustivel)
    {
        this.tipoCombustivel = tipoCombustivel;
    }

    public void SetTipoCambio(string tipoCambio)
    {
        this.tipoCambio = tipoCambio;
    }

    public void SetCapacidadePortaMalas(double capacidadePortaMalas)
    {
        if (capacidadePortaMalas > 0)
        {
            this.capacidadePortaMalas = capacidadePortaMalas;
        }
    }

    public void SetPilotoAutomatico(bool pilotoAutomatico)
    {
        this.pilotoAutomatico = pilotoAutomatico;
    }

    public void EntrarPassageiro()
    {
        if (passageirosAtuais < quantidadePassageiros)
        {
            passageirosAtuais++;
            Console.WriteLine("Passageiro entrou! Quantidade de passageiros: " + passageirosAtuais);
        }
        else
        {
            Console.WriteLine("Carro lotado!");
        }
    }

    public void SairPassageiro()
    {
        if (passageirosAtuais > 0)
        {
            passageirosAtuais--;
            Console.WriteLine("Passageiro saiu! Quantidade de passageiros: " + passageirosAtuais);
        }
        else
        {
            Console.WriteLine("Não há passageiros no carro!");
        }
    }

    public void AtivarPilotoAutomatico()
    {
        if (!pilotoAutomatico)
        {
            Console.WriteLine("Este carro não possui piloto automático.");
            return;
        }

        if (GetVelocidade() < 40)
        {
            Console.WriteLine("Velocidade mínima de 40km/h.");
            return;
        }

        Console.WriteLine("Piloto automático ativado.");
    }

    public override void MostrarDados()
    {
        base.MostrarDados();
        Console.WriteLine("Quantidade de portas: " + quantidadePortas);
        Console.WriteLine("Tipo de câmbio: " + tipoCambio);
        Console.WriteLine("Quantidade de marchas: " + quantidadeMarchas);
        Console.WriteLine("Quantidade de passageiros permitido: " + quantidadePassageiros);
        Console.WriteLine("Tipo de combustível: " + tipoCombustivel);
        Console.WriteLine("Capacidade do porta malas: " + capacidadePortaMalas + " L");
        Console.WriteLine("Tem piloto automático: " + pilotoAutomatico);
    }
}