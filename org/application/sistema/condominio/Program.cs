using System;

namespace Application.System.Condominio;

public class Program
{
    public static void Main(string[] args)
    {
        Condominio condominio = new("Residencial Horizonte", "Rua Central, 500");

        Unidade unidade101 = condominio.CadastrarUnidade("A", "101", 1);
        Unidade unidade202 = condominio.CadastrarUnidade("B", "202", 2);

        Morador moradorJoao = condominio.CadastrarMorador(unidade101, "João da Silva", "12345678900",
            new DateOnly(1985, 3, 15), "(61) 99999-1111", "joao@email.com", TipoMorador.Proprietario);
        Morador moradoraMaria = condominio.CadastrarMorador(unidade202, "Maria Souza", "98765432100",
            new DateOnly(1990, 7, 20), "(61) 98888-2222", "maria@email.com", TipoMorador.Inquilino);

        AreaComum salaoFestas = condominio.CadastrarAreaComum("Salão de festas", 60, 150.00m,
            new TimeOnly(8, 0), new TimeOnly(23, 0));
        AreaComum churrasqueira = condominio.CadastrarAreaComum("Churrasqueira", 20, 80.00m,
            new TimeOnly(9, 0), new TimeOnly(22, 0));

        DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);

        Reserva reservaSalao = condominio.ReservarArea(salaoFestas, unidade101, moradorJoao,
            hoje.AddDays(10), new TimeOnly(18, 0), new TimeOnly(22, 0));
        Reserva reservaChurrasqueira = condominio.ReservarArea(churrasqueira, unidade202, moradoraMaria,
            hoje.AddDays(7), new TimeOnly(12, 0), new TimeOnly(16, 0));

        Boleto taxaCondominial = condominio.EmitirBoleto(unidade101, "Taxa condominial", 650.00m,
            hoje.AddDays(10)
        );

        Ocorrencia ocorrencia = condominio.RegistrarOcorrencia(unidade202, moradoraMaria,
            TipoOcorrencia.Barulho, "Barulho excessivo vindo da unidade vizinha após as 23 horas."
        );
        ocorrencia.IniciarAnalise();
        ocorrencia.Resolver("O responsável pela unidade foi advertido.");

        Multa multa = condominio.AplicarMulta(unidade101, ocorrencia,
            "Descumprimento das regras de silêncio", 250.00m, hoje.AddDays(15)
        );

        taxaCondominial.Pagar(taxaCondominial.CalcularValorAtualizado(hoje));

        ExibirResumo(condominio, reservaSalao, reservaChurrasqueira, multa);
    }

    private static void ExibirResumo(Condominio condominio, Reserva primeiraReserva, Reserva segundaReserva, Multa multa)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("              GESTÃO DE CONDOMÍNIO");
        Console.WriteLine("==================================================");
        Console.WriteLine("Condomínio: " + condominio.Nome);
        Console.WriteLine("Endereço: " + condominio.Endereco);
        Console.WriteLine("Unidades: " + condominio.Unidades.Count);
        Console.WriteLine("Moradores: " + condominio.Moradores.Count);
        Console.WriteLine("Áreas comuns: " + condominio.AreasComuns.Count);
        Console.WriteLine("Reservas: " + condominio.ListarReservasAtivas().Count);
        Console.WriteLine("Boletos: " + condominio.Boletos.Count);
        Console.WriteLine("Ocorrências: " + condominio.Ocorrencias.Count);
        Console.WriteLine("Multas: " + condominio.Multas.Count);

        Console.WriteLine("\nRESERVAS");
        Console.WriteLine(primeiraReserva.AreaComum.Nome + " para " + primeiraReserva.Unidade.GetIdentificacaoCompleta());
        Console.WriteLine(segundaReserva.AreaComum.Nome + " para " + segundaReserva.Unidade.GetIdentificacaoCompleta());

        Console.WriteLine("\nMULTA");
        Console.WriteLine("Unidade: " + multa.Unidade.GetIdentificacaoCompleta());
        Console.WriteLine("Motivo: " + multa.Motivo);
        Console.WriteLine("Valor: R$ " + multa.Valor);

        Console.WriteLine("\nTOTAL EM ABERTO");
        foreach (Unidade unidade in condominio.Unidades)
        {
            Console.WriteLine(unidade.GetIdentificacaoCompleta() + ": R$ " + condominio.CalcularTotalEmAberto(unidade));
        }
    }
}

