namespace Application.Sistema.Automovel.Veiculos;

internal class Trem : VeiculoAutoMovel
{
    private int _quantidadeVagoes;
    private readonly int _capacidadePorVagao;
    private int _quantidadePassageiros;
    private int _quantidadePassageirosAtuais;
    private bool _portasAbertas;
    private bool _naEstacao;

    public Trem(string marca, string modelo, double preco, double km, int anoLancamento, int velocidade,
                int quantidadeVagoes, int capacidadePorVagao, int quantidadePassageiros, int quantidadePassageirosAtuais,
                bool portasAbertas, bool naEstacao)
        : base(marca, modelo, preco, km, anoLancamento, velocidade)
    {
        _quantidadeVagoes = quantidadeVagoes;
        _capacidadePorVagao = capacidadePorVagao;
        _quantidadePassageiros = quantidadePassageiros;
        _quantidadePassageirosAtuais = 0;
        _portasAbertas = true;
        _naEstacao = true;
    }

    public void SetQuantidadeVagoes(int quantidadeVagoes)
    {
        if (quantidadeVagoes > 0)
        {
            _quantidadeVagoes = quantidadeVagoes;
        }
    }

    public void SetQuantidadePassageiros(int quantidadePassageiros)
    {
        if (quantidadePassageiros > 0)
        {
            _quantidadePassageiros = quantidadePassageiros;
        }
    }

    public void EmbarcarPassageiros()
    {
        if (_quantidadePassageirosAtuais < _quantidadePassageiros)
        {
            _quantidadePassageirosAtuais++;
            Console.WriteLine("Passageiros embarcando!");
        }
        else
        {
            Console.WriteLine("Trem lotado!");
        }
    }

    public void DesembarcarPassageiros()
    {
        if (Velocidade > 0)
        {
            Console.WriteLine("O trem está em movimento!");
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

    public void AbrirPortas()
    {
        if (Velocidade == 0 && _naEstacao)
        {
            _portasAbertas = true;
            Console.WriteLine("Portas abertas.");
        }
        else
        {
            Console.WriteLine("O trem precisa estar parado na estação.");
        }
    }

    public void ChegarEstacao()
    {
        if (Velocidade == 0)
        {
            _naEstacao = true;
            Console.WriteLine("Trem chegou à estação!");
        }
    }

    public override void MostrarDados()
    {
        base.MostrarDados();
        Console.WriteLine("Quantidade de vagões: " + _quantidadeVagoes);
        Console.WriteLine("Passageiros atuais: " + _quantidadePassageirosAtuais);
        Console.WriteLine("Na estação: " + Booleano(_naEstacao));
        Console.WriteLine("Portas abertas: " + Booleano(_portasAbertas));
    }
}
