using System;
using System.IO;

namespace Application.Util.Brainfuck
{
    public class Program
    {
        private const string HelloWorld =
            "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]" +
            ">>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.";

        private const string SomaDoisNumeros =
            ",>++++++[<-------->-],[<+>-]<.";

        public static void Main(string[] args)
        {
            Interpretador interp = new Interpretador();

            if (args.Length > 0)
            {
                string programa = File.ReadAllText(args[0]);
                Console.WriteLine(interp.Executar(programa));
                return;
            }

            Console.WriteLine("=== Hello World em Brainfuck ===");
            Console.WriteLine(interp.Executar(HelloWorld));

            interp.Reset();
            Console.WriteLine("\n=== Programa: Soma de dois digitos ===");
            Console.WriteLine("Digite 2 caracteres numericos (ex: 3 e 4)...");
            Console.WriteLine("Programa: " + SomaDoisNumeros);
        }
    }
}
