using System.Collections.Generic;
using System.Linq;

namespace Application.Util.Conversor.Formatos
{
    public class Registro
    {
        private readonly Dictionary<string, string> campos = new Dictionary<string, string>();

        public Registro Set(string chave, string valor)
        {
            campos[chave] = valor;
            return this;
        }

        public string? Get(string chave)
        {
            return campos.TryGetValue(chave, out string? valor) ? valor : null;
        }

        public IReadOnlyDictionary<string, string> GetCampos() => campos;

        internal Dictionary<string, string> CamposMutaveis => campos;

        public override string ToString()
        {
            return "{" + string.Join(", ", campos.Select(kv => kv.Key + "=" + kv.Value)) + "}";
        }
    }
}
