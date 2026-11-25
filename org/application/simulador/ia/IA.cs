using System;
using System.Collections.Generic;
using System.IO;

namespace Application.Simulador.Ia
{
    public class IA
    {
        // --- VALIDA A CHAVE DA API E REALIZA A ANÁLISE DO ARQUIVO PDF ---
        public static AnaliseCronologica Analise(string caminhoPdf)
        {
            string? apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("Defina a variavel de ambiente GEMINI_API_KEY.");
            }

            return new AnaliseCronologica(new List<AnaliseValores>(), "", "");
        }

        // --- RECEBE OS DADOS DA ANÁLISE PARA GERAR UM GRÁFICO DE BARRAS ---
        public static void GraficoBarras(AnaliseCronologica dados, string caminhoSaida)
        {
            List<AnaliseValores> historico = dados.Historico;
            Console.WriteLine("Itens recebidos para o gráfico de barras: " + historico.Count);
        }

        // --- RECEBE OS DADOS DA ANÁLISE PARA GERAR UM GRÁFICO DE PIZZA ---
        public static void GraficoPizza(AnaliseCronologica dados, string caminhoSaida)
        {
            List<AnaliseValores> historico = dados.Historico;
            Console.WriteLine("Itens recebidos para o gráfico de pizza: " + historico.Count);
        }

        // --- GERA O RELATÓRIO PDF COM OS DADOS E O GRÁFICO INFORMADO ---
        public static void GerarPdf(AnaliseCronologica dados, string graficoPath)
        {
            var pdf = new RelatorioPDF();
            pdf.Gerar(dados, graficoPath, "Relatorio_Final.pdf");
        }

        // --- EXECUTA A ANÁLISE, GERA O GRÁFICO E CRIA O RELATÓRIO FINAL ---
        public static void Main(string[] args)
        {
            string caminhoPdf = Path.Combine("C:", "VSCode", "GitHub", "IA",
                "leitor-de-documentos-ia", "teste_pizza.pdf");

            try
            {
                AnaliseCronologica dados = Analise(caminhoPdf);
                string grafico = "Grafico_Pizza.png";
                GraficoPizza(dados, grafico);
                GerarPdf(dados, grafico);
            }
            catch (Exception e) when (e is IOException || e is InvalidOperationException)
            {
                Console.Error.WriteLine("Erro: " + e.Message);
            }
        }
    }
}
