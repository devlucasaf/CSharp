using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.Utilitarios.Logs
{
    public class EntradaLog
    {
        private static readonly Regex Padrao =
            new Regex(@"\[(.*?)\]\s+(\w+)\s+(.*)", RegexOptions.Compiled);

        private const string FormatoData = "yyyy-MM-dd HH:mm:ss";

        public DateTime Instante        { get; }
        public string   Nivel           { get; }
        public string   Mensagem        { get; }
        public string   LinhaOriginal   { get; }

        public EntradaLog(DateTime instante, string nivel, string mensagem, string original)
        {
            Instante = instante;
            Nivel = nivel;
            Mensagem = mensagem;
            LinhaOriginal = original;
        }

        public static EntradaLog? Parse(string linha)
        {
            Match m = Padrao.Match(linha);
            if (!m.Success) 
            {
                return null;
            }
            try
            {
                DateTime dt = DateTime.ParseExact(m.Groups[1].Value, FormatoData, CultureInfo.InvariantCulture);
                return new EntradaLog(dt, m.Groups[2].Value.ToUpperInvariant(), m.Groups[3].Value, linha);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}