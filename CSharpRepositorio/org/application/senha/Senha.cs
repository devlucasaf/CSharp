using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Org.Application.Senha
{
    public class Senha
    {
        private string app;
        private string senhaFinal;
        private static readonly Random random = new Random();

        private const string LETRAS = "abcdefghijklmnopqrstuvwxyz";
        private const string NUMEROS = "0123456789";
        private const string PONTUACAO = "!#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        private static readonly string unirTodosAtributos = LETRAS + NUMEROS + PONTUACAO;

        public void SetNomeAPP(string nome)
        {
            this.app = nome;
        }

        public string GerarSenha(int tamanho)
        {
            if (tamanho <= 4)
            {
                Console.Write("A senha precisa conter no mínimo 5 caracteres!");
                return null;
            }

            List<char> listaSenha = new List<char>();

            listaSenha.Add(LETRAS[random.Next(LETRAS.Length)]);
            listaSenha.Add(NUMEROS[random.Next(NUMEROS.Length)]);
            listaSenha.Add(PONTUACAO[random.Next(PONTUACAO.Length)]);

            for (int i = 0; i < (tamanho - 3); i++)
            {
                listaSenha.Add(unirTodosAtributos[random.Next(unirTodosAtributos.Length)]);
            }

            Embaralhar(listaSenha);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in listaSenha)
            {
                stringBuilder.Append(c);
            }
            this.senhaFinal = stringBuilder.ToString();

            return this.senhaFinal;
        }

        public void GravarSenha()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("não entre.txt", true))
                {
                    writer.WriteLine("       Senhas para login        \n");
                    writer.WriteLine("+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=\n");
                    writer.WriteLine("APP/Site: " + this.app + "\n");
                    writer.WriteLine("Senha: " + this.senhaFinal + "\n");
                    writer.WriteLine("+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=\n");
                    writer.WriteLine("\n");
                    writer.WriteLine();
                }
            }
            catch (IOException ioException)
            {
                Console.Write("Erro ao gravar arquivo: " + ioException.Message);
            }
        }

        private void Embaralhar(List<char> lista)
        {
            int n = lista.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                char valor = lista[k];
                lista[k] = lista[n];
                lista[n] = valor;
            }
        }
    }
}