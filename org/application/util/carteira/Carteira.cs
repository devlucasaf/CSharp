using System;
using System.Security.Cryptography;
using System.Text;

namespace Application.Util.Carteira
{
    public class Carteira
    {
        private readonly string dono;
        private readonly RSA chaves;

        // --- INICIALIZA A CARTEIRA E GERA UM PAR DE CHAVES RSA ---
        public Carteira(string dono)
        {
            this.dono = dono;
            chaves = RSA.Create(2048);
        }

        // --- RETORNA O NOME DO DONO DA CARTEIRA ---
        public string Dono => dono;

        // --- RETORNA A CHAVE PÚBLICA DA CARTEIRA ---
        public RSAParameters ChavePublica => chaves.ExportParameters(false);

        // --- RETORNA A CHAVE PRIVADA DA CARTEIRA ---
        public RSAParameters ChavePrivada => chaves.ExportParameters(true);

        // --- GERA O ENDEREÇO DA CARTEIRA A PARTIR DA CHAVE PÚBLICA ---
        public string GetEndereco()
        {
            byte[] publicKeyBytes = chaves.ExportSubjectPublicKeyInfo();
            byte[] hash = SHA256.HashData(publicKeyBytes);
            string base64 = Convert.ToBase64String(hash);
            return base64.Substring(0, 32);
        }

        // --- ASSINA OS DADOS UTILIZANDO A CHAVE PRIVADA DA CARTEIRA ---
        public byte[] Assinar(string dados)
        {
            byte[] dadosBytes = Encoding.UTF8.GetBytes(dados);
            return chaves.SignData(dadosBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        // --- VERIFICA A AUTENTICIDADE DA ASSINATURA COM A CHAVE PÚBLICA ---
        public static bool Verificar(string dados, byte[] assinatura, RSAParameters chavePublica)
        {
            byte[] dadosBytes = Encoding.UTF8.GetBytes(dados);
            using RSA rsa = RSA.Create();
            rsa.ImportParameters(chavePublica);
            return rsa.VerifyData(dadosBytes, assinatura, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
