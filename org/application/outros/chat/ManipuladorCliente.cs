using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Application.Outros.Chat
{
    public class ManipuladorCliente
    {
        private readonly TcpClient socket;
        private readonly ServidorChat servidor;
        private StreamReader? entrada;
        private StreamWriter? saida;

        private string nomeUsuario = "Anonimo";
        private string salaAtual = "geral";
        private bool desconectado = false;

        public ManipuladorCliente(TcpClient socket, ServidorChat servidor)
        {
            this.socket = socket;
            this.servidor = servidor;
        }

        public string NomeUsuario => nomeUsuario;

        // --- ENVIA UMA LINHA DE TEXTO PARA ESTE CLIENTE ---
        internal void Enviar(string mensagem)
        {
            saida?.WriteLine(mensagem);
        }

        // --- LOOP PRINCIPAL DA CONEXÃO DO CLIENTE ---
        public async Task ExecutarAsync()
        {
            try
            {
                NetworkStream stream = socket.GetStream();
                entrada = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true);
                saida = new StreamWriter(stream, new UTF8Encoding(false), 1024, leaveOpen: true)
                {
                    AutoFlush = true,
                    NewLine = "\n"
                };

                saida.WriteLine("Bem-vindo ao chat! Use /nome <apelido> para se identificar.");
                servidor.EntrarNaSala(salaAtual, this);
                servidor.EnviarParaSala(salaAtual, "* " + nomeUsuario + " entrou na sala *", this);

                string? linha;
                while ((linha = await entrada.ReadLineAsync()) != null)
                {
                    ProcessarMensagem(linha);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Conexao encerrada com erro: " + e.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        // --- INTERPRETA COMANDOS OU ENVIA MENSAGEM PARA A SALA ---
        private void ProcessarMensagem(string linha)
        {
            if (linha.StartsWith("/nome "))
            {
                string novoNome = linha.Substring("/nome ".Length).Trim();
                string nomeAntigo = nomeUsuario;
                nomeUsuario = string.IsNullOrEmpty(novoNome) ? nomeUsuario : novoNome;
                servidor.EnviarParaSala(salaAtual, "* " + nomeAntigo + " agora se chama " + nomeUsuario + " *", null);
            }
            else if (linha.StartsWith("/sala "))
            {
                string novaSala = linha.Substring("/sala ".Length).Trim();
                servidor.SairDaSala(salaAtual, this);
                servidor.EnviarParaSala(salaAtual, "* " + nomeUsuario + " saiu da sala *", this);
                salaAtual = string.IsNullOrEmpty(novaSala) ? "geral" : novaSala;
                servidor.EntrarNaSala(salaAtual, this);
                servidor.EnviarParaSala(salaAtual, "* " + nomeUsuario + " entrou na sala *", this);
                Enviar("Voce agora esta na sala: " + salaAtual);
            }
            else if (linha == "/usuarios")
            {
                List<string> usuarios = servidor.ListarUsuariosDaSala(salaAtual);
                Enviar("Usuarios na sala '" + salaAtual + "': " + string.Join(", ", usuarios));
            }
            else if (linha == "/sair")
            {
                Enviar("Ate mais, " + nomeUsuario + "!");
                Desconectar();
            }
            else
            {
                servidor.EnviarParaSala(salaAtual, "[" + salaAtual + "] " + nomeUsuario + ": " + linha, this);
            }
        }

        // --- REMOVE O CLIENTE DA SALA E FECHA O SOCKET ---
        private void Desconectar()
        {
            if (desconectado)
            {
                return;
            }
            desconectado = true;
            try
            {
                servidor.SairDaSala(salaAtual, this);
                servidor.EnviarParaSala(salaAtual, "* " + nomeUsuario + " saiu do chat *", this);
                socket.Close();
            }
            catch (IOException)
            {
                // --- CONEXAO JA PODE TER CAIDO ---
            }
        }
    }
}
