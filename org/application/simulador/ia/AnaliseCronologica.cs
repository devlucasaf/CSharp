using System.Collections.Generic;

namespace Application.Simulador.Ia
{
    public class AnaliseCronologica
    {
        private readonly List<AnaliseValores> historico;
        private readonly string resumo;
        private readonly string estilo;

        public AnaliseCronologica(List<AnaliseValores> historico, string resumo, string estilo)
        {
            this.historico = historico;
            this.resumo = resumo;
            this.estilo = estilo;
        }

        public List<AnaliseValores> Historico => historico;
        public string Resumo => resumo;
        public string Estilo => estilo;
    }
}
