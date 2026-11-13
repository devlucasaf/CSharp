using System.Globalization;
using System.Text;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Enums;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Exceptions;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Model;
using Application.Sistema.Automovel.LocadoraDeVeiculos.Service;

namespace Application.Sistema.Automovel.LocadoraDeVeiculos;

public class Programa
{
    private const string FormatoData = "dd/MM/yyyy";

    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

        var locadora = new Locadora("AutoLux Locadora", "12.345.678/0001-90", "Av. Brasil, 500");
        ICalculadoraDiariaService calculadora = new CalculadoraPadraoService();
        var service = new LocadoraService(locadora, calculadora);
        var relatorio = new RelatorioService(locadora);

        InicializarDados(service);

        int opcao;
        do
        {
            ExibirMenu();
            opcao = LerInteiro("Escolha uma opção: ");
            switch (opcao)
            {
                case 1: 
                    CadastrarVeiculo(service); 
                    break;
                case 2: 
                    CadastrarCliente(service); 
                    break;
                case 3: 
                    relatorio.ExibirVeiculosDisponiveis(); 
                    break;
                case 4: 
                    RealizarLocacao(service); 
                    break;
                case 5: 
                    DevolverVeiculo(service); 
                    break;
                case 6: 
                    relatorio.ExibirLocacoesAtivas(); 
                    break;
                case 7: 
                    relatorio.ExibirHistoricoLocacoes(); 
                    break;
                case 8: 
                    relatorio.ExibirRelatorioFinanceiro(); 
                    break;
                case 9: 
                    relatorio.ExibirClientesFieis(); 
                    break;
                case 0: 
                    Console.WriteLine("Saindo..."); 
                    break;
                default: 
                    Console.WriteLine("Opção inválida!"); 
                    break;
            }
        }
        while (opcao != 0);
    }

    private static void ExibirMenu()
    {
        Console.WriteLine("\n===== LOCADORA DE VEÍCULOS =====");
        Console.WriteLine("1 - Cadastrar veículo");
        Console.WriteLine("2 - Cadastrar cliente");
        Console.WriteLine("3 - Listar veículos disponíveis");
        Console.WriteLine("4 - Realizar locação");
        Console.WriteLine("5 - Registrar devolução");
        Console.WriteLine("6 - Exibir locações ativas");
        Console.WriteLine("7 - Exibir histórico de locações");
        Console.WriteLine("8 - Relatório financeiro");
        Console.WriteLine("9 - Clientes fiéis (mais pontos)");
        Console.WriteLine("0 - Sair");
    }

    // --- LÊ UMA LINHA; SE A ENTRADA ACABAR, ENCERRA O PROGRAMA ---
    private static string LerLinha()
    {
        string? linha = Console.ReadLine();
        if (linha == null)
        {
            Environment.Exit(0);
        }

        return linha;
    }

    private static int LerInteiro(string mensagem)
    {
        Console.Write(mensagem);
        int valor;
        while (!int.TryParse(LerLinha().Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out valor))
        {
            Console.Write("Valor inválido. Digite um número: ");
        }

        return valor;
    }

    private static double LerDouble(string mensagem)
    {
        Console.Write(mensagem);
        double valor;
        while (!double.TryParse(LerLinha().Trim(), NumberStyles.Float, CultureInfo.CurrentCulture, out valor))
        {
            Console.Write("Valor inválido. Digite um número: ");
        }

        return valor;
    }

    private static string LerString(string mensagem)
    {
        Console.Write(mensagem);
        return LerLinha().Trim();
    }

    private static DateOnly? LerData(string mensagem)
    {
        Console.Write(mensagem);
        string dataStr = LerLinha().Trim();

        if (DateOnly.TryParseExact(dataStr, FormatoData, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly data))
        {
            return data;
        }

        Console.WriteLine("Formato inválido. Use dd/MM/yyyy.");
        return null;
    }

    // --- CONVERTE UM TEXTO EM ENUM; RETORNA O PADRÃO SE FOR INVÁLIDO ---
    private static T LerEnum<T>(string texto, T padrao, string mensagemErro) where T : struct, Enum
    {
        if (Enum.TryParse(texto, false, out T valor) && Enum.IsDefined(valor))
        {
            return valor;
        }

        Console.WriteLine(mensagemErro);
        return padrao;
    }

    private static void InicializarDados(LocadoraService service)
    {
        try
        {
            var veiculo1 = new Veiculo("ABC-1234", "Fiesta", "Ford", 2020, CategoriaVeiculo.ECONOMICO, 80.0, 15000);
            var veiculo2 = new Veiculo("XYZ-5678", "Civic", "Honda", 2022, CategoriaVeiculo.INTERMEDIARIO, 120.0, 8000);
            var veiculo3 = new Veiculo("MNO-9012", "Compass", "Jeep", 2023, CategoriaVeiculo.SUV, 180.0, 5000);
            service.CadastrarVeiculo(veiculo1);
            service.CadastrarVeiculo(veiculo2);
            service.CadastrarVeiculo(veiculo3);

            var cliente1 = new Cliente("João Silva", "111.111.111-11", "(11) 99999-1111", "joao@email.com",
                    "123456789", DateOnly.ParseExact("10/12/2026", FormatoData, CultureInfo.InvariantCulture));
            var cliente2 = new Cliente("Maria Souza", "222.222.222-22", "(11) 98888-2222", "maria@email.com",
                    "987654321", DateOnly.ParseExact("05/05/2025", FormatoData, CultureInfo.InvariantCulture));
            service.CadastrarCliente(cliente1);
            service.CadastrarCliente(cliente2);

            Console.WriteLine("Dados iniciais carregados com sucesso.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro ao inicializar dados: " + e.Message);
        }
    }

    private static void CadastrarVeiculo(LocadoraService service)
    {
        Console.WriteLine("\n--- CADASTRO DE VEÍCULO ---");
        string placa = LerString("Placa: ");
        string modelo = LerString("Modelo: ");
        string marca = LerString("Marca: ");
        int ano = LerInteiro("Ano: ");
        Console.WriteLine("Categorias: ECONOMICO, INTERMEDIARIO, EXECUTIVO, SUV, LUXO");
        string catStr = LerString("Categoria: ").ToUpperInvariant();
        CategoriaVeiculo categoria = LerEnum(catStr, CategoriaVeiculo.ECONOMICO, "Categoria inválida. Usando ECONOMICO.");

        double diaria = LerDouble("Valor da diária: R$");
        double km = LerDouble("Quilometragem atual: ");

        var veiculo = new Veiculo(placa, modelo, marca, ano, categoria, diaria, km);
        service.CadastrarVeiculo(veiculo);
        Console.WriteLine("Veículo cadastrado com sucesso!");
    }

    private static void CadastrarCliente(LocadoraService service)
    {
        Console.WriteLine("\n--- CADASTRO DE CLIENTE ---");
        string nome = LerString("Nome: ");
        string cpf = LerString("CPF: ");
        string telefone = LerString("Telefone: ");
        string email = LerString("Email: ");
        string cnh = LerString("Número da CNH: ");
        DateOnly? validade = LerData("Data de validade da CNH (dd/MM/yyyy): ");
        if (validade == null)
        {
            return;
        }

        var cliente = new Cliente(nome, cpf, telefone, email, cnh, validade);
        service.CadastrarCliente(cliente);
        Console.WriteLine("Cliente cadastrado com sucesso!");
    }

    private static void RealizarLocacao(LocadoraService service)
    {
        Console.WriteLine("\n--- REALIZAR LOCAÇÃO ---");
        string placa = LerString("Placa do veículo: ");
        string cpf = LerString("CPF do cliente: ");
        DateOnly? retirada = LerData("Data de retirada (dd/MM/yyyy): ");
        if (retirada == null)
        {
            return;
        }

        DateOnly? devolucao = LerData("Data prevista de devolução (dd/MM/yyyy): ");
        if (devolucao == null)
        {
            return;
        }

        Console.WriteLine("Seguros: BASICO, COMPLETO, APENAS_TERCEIROS");
        string seguroStr = LerString("Tipo de seguro: ").ToUpperInvariant();
        TipoSeguro seguro = LerEnum(seguroStr, TipoSeguro.BASICO, "Seguro inválido. Usando BASICO.");

        Console.WriteLine("Formas de pagamento: DINHEIRO, CARTAO_CREDITO, CARTAO_DEBITO, PIX, VALE_ALIMENTACAO");
        string pagStr = LerString("Forma de pagamento: ").ToUpperInvariant();
        FormaPagamento pagamento = LerEnum(pagStr, FormaPagamento.DINHEIRO, "Forma inválida. Usando DINHEIRO.");

        try
        {
            Locacao locacao = service.RealizarLocacao(placa, cpf, retirada.Value, devolucao.Value, seguro, pagamento);
            Console.WriteLine("Locação realizada com sucesso!");
            Console.WriteLine("ID da locação: " + locacao.Id);
            Console.WriteLine($"Valor total: R${locacao.ValorTotal:F2}");
        }
        catch (Exception e) when (e is VeiculoIndisponivelException or ClienteNaoHabilitadoException or DataInvalidaException)
        {
            Console.WriteLine("Erro: " + e.Message);
        }
    }

    private static void DevolverVeiculo(LocadoraService service)
    {
        Console.WriteLine("\n--- DEVOLUÇÃO DE VEÍCULO ---");
        int id = LerInteiro("ID da locação: ");
        DateOnly? dataDevolucao = LerData("Data de devolução real (dd/MM/yyyy): ");

        if (dataDevolucao == null)
        {
            return;
        }

        double km = LerDouble("Quilometragem atual: ");

        try
        {
            service.DevolverVeiculo(id, dataDevolucao.Value, km);
        }
        catch (Exception e) when (e is LocacaoNaoEncontradaException or DataInvalidaException)
        {
            Console.WriteLine("Erro: " + e.Message);
        }
    }
}
