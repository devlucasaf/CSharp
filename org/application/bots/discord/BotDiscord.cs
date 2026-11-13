using System;
using System.Collections.Generic;

namespace Application.Bots.Discord
{
    public class BotDiscord
    {
        private readonly string token;
        private readonly Dictionary<string, ManipuladorComando> comandos = new Dictionary<string, ManipuladorComando>();

        public BotDiscord(string token)
        {
            this.token = token;
        }

        public void RegistrarComando(string nome, ManipuladorComando handler)
        {
            comandos[nome] = handler;
        }

        public void Iniciar()
        {
            Console.WriteLine("Bot Discord aguardando: adicione a biblioteca Discord.Net para conectar.");
            Console.WriteLine($"Token configurado: {(string.IsNullOrEmpty(token) ? "AUSENTE" : "OK")}");
            Console.WriteLine($"Comandos registrados: {string.Join(", ", comandos.Keys)}");
            Console.WriteLine("\nVer README.md para instruções de dependência.");
        }

        public static void Main(string[] args)
        {
            string token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

            BotDiscord discord = new BotDiscord(token);

            discord.RegistrarComando("ping", (u, a) => "Pong!");

            discord.RegistrarComando("oi", (u, a) => $"Olá, {u}!");

            discord.RegistrarComando("dado", (u, a) =>
            {
                int lados = string.IsNullOrWhiteSpace(a) ? 6 : int.Parse(a);
                Random random = new Random();

                return $"Rolou {random.Next(1, lados + 1)}";
            });

            discord.Iniciar();
        }
    }
}
