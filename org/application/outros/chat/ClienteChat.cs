using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Outros.Chat
{
    public class ClienteChat
    {
        public static async Task Main(string[] args)
        {
            string host = args.Length > 0 ? args[0] : "localhost";
            int porta = args.Length > 1 ? int.Parse(args[1]) : 5000;

            using var socket = new TcpClient();
            await socket.ConnectAsync(host, porta);
            Console.WriteLine($"Conectado ao servidor {host}:{porta}");

            NetworkStream stream = socket.GetStream();
            using var entradaServidor = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true);
            using var saidaServidor = new StreamWriter(stream, new UTF8Encoding(false), 1024, leaveOpen: true)
            {
                AutoFlush = true,
                NewLine = "\n"
            };

            // Thread de recebimento: roda em background até a conexão cair
            var threadRecebimento = Task.Run(async () =>
            {
                try
                {
                    string? linha;
                    while ((linha = await entradaServidor.ReadLineAsync()) != null)
                    {
                        Console.WriteLine(linha);
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Conexao com o servidor encerrada.");
                }
            });

            Console.WriteLine("Digite mensagens (ou /nome, /sala, /usuarios, /sair):");
            string? linhaDigitada;
            while ((linhaDigitada = Console.ReadLine()) != null)
            {
                saidaServidor.WriteLine(linhaDigitada);
                if (linhaDigitada == "/sair")
                {
                    break;
                }
            }

            await Task.WhenAny(threadRecebimento, Task.Delay(500));
        }
    }
}
