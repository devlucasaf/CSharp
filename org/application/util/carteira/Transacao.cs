using System;
using System.Security.Cryptography;
using System.Text;

namespace Application.Util.Carteira
{
    public class Transacao
    {
        private readonly string remetente;
        private readonly string destinatario;
        private double          valor;
        private readonly long   timestamp;
        private byte[]?         assinatura;
        private RSAParameters?  chavePublicaRemetente;

        public Transacao(string remetente, string destinatario, double valor)
        {
            this.remetente = remetente;
            this.destinatario = destinatario;
            this.valor = valor;
            this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public string DadosParaAssinar()
        {
            return remetente + "|" + destinatario + "|" + valor + "|" + timestamp;
        }

        public void Assinar(Carteira remetenteCarteira)
        {
            assinatura = remetenteCarteira.Assinar(DadosParaAssinar());
            chavePublicaRemetente = remetenteCarteira.ChavePublica;
        }

        public bool Verificar()
        {
            if (assinatura == null || chavePublicaRemetente == null)
            {
                return false;
            }
            return Carteira.Verificar(DadosParaAssinar(), assinatura, chavePublicaRemetente.Value);
        }

        // --- AUXILIAR PARA SIMULAR ADULTERAÇÃO ---
        public void SetValor(double novoValor)
        {
            valor = novoValor;
        }

        public double Valor => valor;

        public override string ToString()
        {
            string assinaturaStr = assinatura == null
                ? "SEM ASSINATURA"
                : Convert.ToBase64String(assinatura).Substring(0, 24) + "...";
            return $"[{remetente} -> {destinatario} | R$ {valor:F2} | {assinaturaStr}]";
        }
    }
}
