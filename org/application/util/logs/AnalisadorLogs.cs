using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Utilitarios.Logs
{
    public class AnalisadorLogs
    {
        private readonly string arquivo;

        public AnalisadorLogs(string arquivo)
        {
            this.arquivo = arquivo;
        }

        public Resultado Analisar(Regex? filtro, string? nivelFiltro)
        {
            var porNivel = new Dictionary<string, int>();
            var porHora = new Dictionary<int, int>();
            long totalLinhas = 0;
            long linhasValidas = 0;
            long linhasCasadas = 0;
            DateTime? primeira = null;
            DateTime? ultima = null;

            using (var br = new StreamReader(arquivo))
            {
                string? linha;
                while ((linha = br.ReadLine()) != null)
                {
                    totalLinhas++;
                    EntradaLog? e = EntradaLog.Parse(linha);
                    if (e == null) continue;
                    linhasValidas++;

                    if (nivelFiltro != null && !string.Equals(nivelFiltro, e.Nivel, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (filtro != null && !filtro.IsMatch(e.LinhaOriginal))
                    {
                        continue;
                    }

                    linhasCasadas++;
                    porNivel[e.Nivel] = porNivel.GetValueOrDefault(e.Nivel) + 1;
                    porHora[e.Instante.Hour] = porHora.GetValueOrDefault(e.Instante.Hour) + 1;

                    if (primeira == null || e.Instante < primeira.Value)
                    {
                        primeira = e.Instante;
                    }

                    if (ultima == null || e.Instante > ultima.Value)
                    {
                        ultima = e.Instante;
                    }
                }
            }

            return new Resultado(totalLinhas, linhasValidas, linhasCasadas,
                Ordenar(porNivel), OrdenarInt(porHora), primeira, ultima);
        }

        private static IReadOnlyDictionary<string, int> Ordenar(Dictionary<string, int> map)
        {
            return map.OrderByDescending(kv => kv.Value)
                        .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        private static IReadOnlyDictionary<int, int> OrdenarInt(Dictionary<int, int> map)
        {
            return map.OrderBy(kv => kv.Key)
                        .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
