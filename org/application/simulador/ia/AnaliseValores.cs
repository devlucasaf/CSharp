namespace Application.Simulador.Ia
{
    public class AnaliseValores
    {
        private readonly string rotulo;
        private readonly string tipo;
        private readonly double valor;

        public AnaliseValores(string rotulo, double valor, string tipo)
        {
            this.rotulo = rotulo;
            this.valor = valor;
            this.tipo = tipo;
        }

        public string Rotulo => rotulo;
        public double Valor => valor;
        public string Tipo => tipo;
    }
}
