namespace Games.Narrativo.Rpg
{
    public class ItemRPG
    {
        private string  nome;
        private string  tipo;
        private int     valor;
        private int     preco;

        public ItemRPG(string nome, string tipo, int valor, int preco)
        {
            this.nome = nome;
            this.tipo = tipo;
            this.valor = valor;
            this.preco = preco;
        }

        public string Nome => nome;
        public string Tipo => tipo;
        public int Valor => valor;
        public int Preco => preco;

        public override string ToString()
        {
            string descricao = tipo switch
            {
                "cura" => "Cura " + valor + " HP",
                "ataque" => "+" + valor + " Ataque",
                "defesa" => "+" + valor + " Defesa",
                _ => "Efeito: " + valor
            };
            return nome + " [" + descricao + "] (R$ " + preco + ")";
        }
    }
}
