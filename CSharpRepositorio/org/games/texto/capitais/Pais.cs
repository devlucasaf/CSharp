namespace Games.Texto.Capitais
{
    public class Pais
    {
        public string Nome          { get; }
        public string Capital       { get; }
        public string Continente    { get; }

        public Pais(string nome, string capital, string continente)
        {
            Nome = nome;
            Capital = capital;
            Continente = continente;
        }
    }
}
