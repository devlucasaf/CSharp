using System;

namespace Games.Narrativo.Detetive
{
    public class Pista
    {
        public string   Descricao;
        public bool     Verdadeira;

        public Pista(string descricao, bool verdadeira)
        {
            Descricao = descricao;
            Verdadeira = verdadeira;
        }

        public void Mostrar()
        {
            Console.WriteLine("- " + Descricao);
        }
    }
}
