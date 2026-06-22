using System;
using System.Globalization;

namespace Org.Application.Renda
{
    public class CalculadoraRenda
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== CALCULADORA DE IMPOSTO DE RENDA E INSS ===");
            Console.Write("Informe o valor do salario bruto: R$ ");

            double salarioBruto = LerValor();
            if (salarioBruto < 0)
            {
                Console.WriteLine("Salario invalido.");
                return;
            }

            double inss = CalcularInss(salarioBruto);
            double impostoRenda = CalcularImpostoRenda(salarioBruto);
            double salarioLiquido = salarioBruto - inss - impostoRenda;

            CultureInfo culturaBR = CultureInfo.GetCultureInfo("pt-BR");

            Console.WriteLine();
            Console.WriteLine("------------- RESULTADO -------------");
            Console.WriteLine($"Salario bruto:    R$ {salarioBruto.ToString("N2", culturaBR)}");
            Console.WriteLine($"INSS:             R$ {inss.ToString("N2", culturaBR)}");
            Console.WriteLine($"Imposto de Renda: R$ {impostoRenda.ToString("N2", culturaBR)}");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Salario liquido:  R$ {salarioLiquido.ToString("N2", culturaBR)}");
        }

        private static double CalcularImpostoRenda(double salario)
        {
            if (salario <= 5000.00)
            {
                return 0.0;
            }

            if (salario <= 6500.00)
            {
                return (salario - 5000.00) * 0.075;
            }

            if (salario <= 8000.00)
            {
                return (salario - 6500.00) * 0.15;
            }

            if (salario <= 10000.00)
            {
                return (salario - 8000.00) * 0.225;
            }

            return (salario - 10000.00) * 0.275;
        }

        private static double CalcularInss(double salario)
        {
            if (salario <= 1412.00)
            {
                return salario * 0.075;
            }

            if (salario <= 2666.68)
            {
                return salario * 0.09;
            }

            if (salario <= 4000.03)
            {
                return salario * 0.12;
            }

            if (salario <= 8475.55)
            {
                return salario * 0.14;
            }

            return 8475.55 * 0.14;
        }

        private static double LerValor()
        {
            string linha = Console.ReadLine()?.Trim().Replace(",", ".") ?? "";
            if (double.TryParse(linha, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
            {
                return valor;
            }
            return -1;
        }
    }
}