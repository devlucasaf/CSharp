using System;

namespace Application.Util.Carteira
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== CARTEIRA DIGITAL ===\n");

            Carteira alice = new Carteira("Alice");
            Carteira bob = new Carteira("Bob");
            Carteira eve = new Carteira("Eve (maliciosa)");

            Console.WriteLine("Endereco Alice: " + alice.GetEndereco());
            Console.WriteLine("Endereco Bob:   " + bob.GetEndereco());
            Console.WriteLine();

            Transacao transacao1 = new Transacao(alice.GetEndereco(), bob.GetEndereco(), 100.0);
            transacao1.Assinar(alice);
            Console.WriteLine("Transacao valida: " + transacao1);
            Console.WriteLine("Verificacao: " + transacao1.Verificar());
            Console.WriteLine();

            Transacao transacao2 = new Transacao(alice.GetEndereco(), eve.GetEndereco(), 1000.0);
            transacao2.Assinar(eve);
            Console.WriteLine("Transacao forjada por Eve (dizendo ser Alice): " + transacao2);
            Console.WriteLine("Verificacao (esperado false pois eve nao e alice, mas assinatura e valida com sua chave):");
            Console.WriteLine("  -> " + transacao2.Verificar() + " (chave publica de Eve valida sua propria assinatura)");
            Console.WriteLine("  No blockchain real, o remetente sempre e derivado da chave publica.");
            Console.WriteLine();

            Transacao transacao3 = new Transacao(alice.GetEndereco(), bob.GetEndereco(), 50.0);
            transacao3.Assinar(alice);
            Console.WriteLine("Antes da adulteracao: " + transacao3.Verificar());
            // Substitui o reflection do Java por um método auxiliar (mais seguro e idiomático em C#)
            transacao3.SetValor(5000.0);
            Console.WriteLine("Apos adulterar valor para 5000: " + transacao3.Verificar() + " (assinatura invalida)");
        }
    }
}
