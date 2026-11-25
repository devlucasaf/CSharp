using System;
using System.Collections.Generic;
using System.IO;

namespace Application.Util.Conversor.Formatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string csv = Path.Combine("target", "conversor-demo.csv");
            string json = Path.Combine("target", "conversor-demo.json");
            string xml = Path.Combine("target", "conversor-demo.xml");
            Directory.CreateDirectory("target");

            var dados = new List<Registro>
            {
                new Registro().Set("id", "1").Set("nome", "Ana").Set("cidade", "Brasilia"),
                new Registro().Set("id", "2").Set("nome", "Bruno, Jr").Set("cidade", "Sao Paulo"),
                new Registro().Set("id", "3").Set("nome", "Carla").Set("cidade", "Rio")
            };

            Console.WriteLine("Escrevendo CSV: " + Path.GetFullPath(csv));
            ConversorCSV.Escrever(dados, csv);

            Console.WriteLine("Escrevendo JSON: " + Path.GetFullPath(json));
            ConversorJSON.Escrever(dados, json);

            Console.WriteLine("Escrevendo XML: " + Path.GetFullPath(xml));
            ConversorXML.Escrever(dados, xml);

            Console.WriteLine("\n=== Round-trip test ===");
            List<Registro> deCsv = ConversorCSV.Ler(csv);
            List<Registro> deJson = ConversorJSON.Ler(json);
            List<Registro> deXml = ConversorXML.Ler(xml);

            Console.WriteLine("Lidos do CSV: " + deCsv.Count);
            deCsv.ForEach(r => Console.WriteLine("  " + r));

            Console.WriteLine("Lidos do JSON: " + deJson.Count);
            deJson.ForEach(r => Console.WriteLine("  " + r));

            Console.WriteLine("Lidos do XML: " + deXml.Count);
            deXml.ForEach(r => Console.WriteLine("  " + r));

            Console.WriteLine("\nSuporte a Excel (.xlsx) requer uma biblioteca externa (ex.: ClosedXML) - ver README.md");
        }
    }
}
