namespace CSharpRepositorio.org.application.Veiculo;

public class Trem : VeiculoAutoMovel
{
    private int     quantidadeVagoes;
    private int     capacidadePorVagao;
    private int     quantidadePassageiros;
    private int     quantidadePassageirosAtuais;
    private bool    portasAbertas;
    private bool    naEstacao;

    public Trem(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                int quantidadeVagoes, int capacidadePorVagao, int quantidadePassageiros, int quantidadePassageirosAtuais,
                bool portasAbertas, bool naEstacao)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        this.quantidadeVagoes = quantidadeVagoes;
        this.capacidadePorVagao = capacidadePorVagao;
        this.quantidadePassageiros = quantidadePassageiros;
        this.quantidadePassageirosAtuais = 0;
        this.portasAbertas = true;
        this.naEstacao = true;
    }

    public void SetQuantidadeVagoes(int quantidadeVagoes)
    {
        if (quantidadeVagoes > 0)
        {
            this.quantidadeVagoes = quantidadeVagoes;
        }
    }

    public void SetQuantidadePassageiros(int quantidadePassageiros)
    {
        if (quantidadePassageiros > 0)
        {
            this.quantidadePassageiros = quantidadePassageiros;
        }
    }

    public void EmbarcarPassageiros()
    {
        if (quantidadePassageirosAtuais < quantidadePassageiros)
        {
            quantidadePassageirosAtuais++;
            Console.WriteLine("Passageiros embarcando!");
        }
        else
        {
            Console.WriteLine("Trem lotado!");
        }
    }

    public void DesembarcarPassageiros()
    {
        if (GetVelocidade() > 0)
        {
            Console.WriteLine("O trem está em movimento!");
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

    public void AbrirPortas()
    {
        if (GetVelocidade() == 0 && naEstacao)
        {
            portasAbertas = true;
            Console.WriteLine("Portas abertas.");
        }
        else
        {
            Console.WriteLine("O trem precisa estar parado na estação.");
        }
    }

    public void ChegarEstacao()
    {
        if (GetVelocidade() == 0)
        {
            naEstacao = true;
            Console.WriteLine("Trem chegou à estação!");
        }
    }

    public override void MostrarDados()
    {
        base.MostrarDados();
        Console.WriteLine("Quantidade de vagões: " + quantidadeVagoes);
        Console.WriteLine("Passageiros atuais: " + quantidadePassageirosAtuais);
        Console.WriteLine("Na estação: " + naEstacao);
        Console.WriteLine("Portas abertas: " + portasAbertas);
    }
}