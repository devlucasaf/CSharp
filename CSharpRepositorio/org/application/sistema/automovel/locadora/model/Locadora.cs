namespace Application.Sistema.Automovel.LocadoraDeVeiculos.Model;

public class Locadora
{
    public string           Nome        { get; }
    public string           Cnpj        { get; }
    public string           Endereco    { get; }
    public List<Veiculo>    Veiculos    { get; }
    public List<Cliente>    Clientes    { get; }
    public List<Locacao>    Locacoes    { get; }
    public double           Caixa       { get; private set; }

    public Locadora(string nome, string cnpj, string endereco)
    {
        Nome = nome;
        Cnpj = cnpj;
        Endereco = endereco;
        Veiculos = new List<Veiculo>();
        Clientes = new List<Cliente>();
        Locacoes = new List<Locacao>();
        Caixa = 0.0;
    }

    public void AdicionarVeiculo(Veiculo v)
    {
        Veiculos.Add(v);
    }

    public void AdicionarCliente(Cliente c)
    {
        Clientes.Add(c);
    }

    public void AdicionarLocacao(Locacao l)
    {
        Locacoes.Add(l);
    }

    public void AdicionarCaixa(double valor)
    {
        Caixa += valor;
    }

    public Veiculo? BuscarVeiculoPorPlaca(string placa)
    {
        foreach (Veiculo v in Veiculos)
        {
            if (v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
            {
                return v;
            }
        }

        return null;
    }

    public Cliente? BuscarClientePorCpf(string cpf)
    {
        foreach (Cliente c in Clientes)
        {
            if (c.Cpf == cpf)
            {
                return c;
            }
        }

        return null;
    }

    public Locacao? BuscarLocacaoPorId(int id)
    {
        foreach (Locacao l in Locacoes)
        {
            if (l.Id == id)
            {
                return l;
            }
        }

        return null;
    }
}
