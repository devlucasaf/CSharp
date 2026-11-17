using System;

namespace Games.Narrativo.Detetive
{
    public class Suspeito
    {
        public string   Nome;
        public string   Descricao;
        public string   Motivacao;
        public string   Alibi;
        public bool     ComportamentoSuspeito;

        public Suspeito(string nome, string descricao, string motivacao, string alibi)
        {
            Nome = nome;
            Descricao = descricao;
            Motivacao = motivacao;
            Alibi = alibi;
            ComportamentoSuspeito = false;
        }

        public void Interrogar(int nivelInvestigacao)
        {
            Console.WriteLine("\nInterrogando " + Nome + ":");
            Console.WriteLine("Descrição: " + Descricao);
            Console.WriteLine("Motivação: " + Motivacao);

            if (nivelInvestigacao >= 2)
            {
                Console.WriteLine("Álibi: " + Alibi);
            }
            else
            {
                Console.WriteLine("Ele(a) parece hesitar antes de falar sobre o álibi...");
                ComportamentoSuspeito = true;
            }
        }
    }
}
