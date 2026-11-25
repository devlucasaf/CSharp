using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Outros.Chat
{
    public class ServidorChat
    {
        private readonly int porta;
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<ManipuladorCliente, byte>> salas
            = new ConcurrentDictionary<string, ConcurrentDictionary<ManipuladorCliente, byte>>();

        public ServidorChat(int porta)
        {
            this.porta = porta;
        }

        // --- INICIA O SERVIDOR E ACEITA CLIENTES EM LOOP ---
        public async Task IniciarAsync()
        {
            var listener = new TcpListener(IPAddress.Any, porta);
            listener.Start();
            Console.WriteLine($"Servidor de chat ouvindo na porta {porta}...");

            try
            {
                while (true)
                {
                    TcpClient cliente = await listener.AcceptTcpClientAsync();
                    var manipulador = new ManipuladorCliente(cliente, this);
                    _ = Task.Run(() => manipulador.ExecutarAsync());
                }
            }
            finally
            {
                listener.Stop();
            }
        }

        // --- ADICIONA O CLIENTE À SALA INFORMADA ---
        internal void EntrarNaSala(string sala, ManipuladorCliente cliente)
        {
            var membros = salas.GetOrAdd(sala,
                _ => new ConcurrentDictionary<ManipuladorCliente, byte>());
            membros[cliente] = 0;
        }

        // --- REMOVE O CLIENTE DA SALA INFORMADA ---
        internal void SairDaSala(string sala, ManipuladorCliente cliente)
        {
            if (salas.TryGetValue(sala, out var membros))
            {
                membros.TryRemove(cliente, out _);
            }
        }

        // --- LISTA OS NOMES DOS USUÁRIOS PRESENTES NA SALA ---
        internal List<string> ListarUsuariosDaSala(string sala)
        {
            var nomes = new List<string>();
            if (salas.TryGetValue(sala, out var membros))
            {
                foreach (var c in membros.Keys)
                {
                    nomes.Add(c.NomeUsuario);
                }
            }
            return nomes;
        }

        // --- ENVIA A MENSAGEM PARA TODOS DA SALA EXCETO O REMETENTE ---
        internal void EnviarParaSala(string sala, string mensagem, ManipuladorCliente? remetente)
        {
            if (!salas.TryGetValue(sala, out var membros))
            {
                return;
            }

            foreach (var cliente in membros.Keys)
            {
                if (!ReferenceEquals(cliente, remetente))
                {
                    cliente.Enviar(mensagem);
                }
            }
        }

        // --- PONTO DE ENTRADA ---
        public static async Task Main(string[] args)
        {
            int porta = args.Length > 0 ? int.Parse(args[0]) : 5000;
            var servidor = new ServidorChat(porta);
            await servidor.IniciarAsync();
        }
    }
}
