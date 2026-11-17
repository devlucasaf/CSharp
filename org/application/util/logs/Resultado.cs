using System;
using System.Collections.Generic;

namespace Application.Utilitarios.Logs
{
    public class Resultado
    {
        public long                             TotalLinhas     { get; }
        public long                             LinhasValidas   { get; }
        public long                             LinhasCasadas   { get; }
        public IReadOnlyDictionary<string, int> PorNivel        { get; }
        public IReadOnlyDictionary<int, int>    PorHora         { get; }
        public DateTime?                        Primeira        { get; }
        public DateTime?                        Ultima          { get; }

        public Resultado(long totalLinhas, long linhasValidas, long linhasCasadas, IReadOnlyDictionary<string, int> porNivel,
                IReadOnlyDictionary<int, int> porHora, DateTime? primeira, DateTime? ultima)
        {
            TotalLinhas = totalLinhas;
            LinhasValidas = linhasValidas;
            LinhasCasadas = linhasCasadas;
            PorNivel = porNivel;
            PorHora = porHora;
            Primeira = primeira;
            Ultima = ultima;
        }
    }
}
