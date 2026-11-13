using System.Text;

namespace Games.Texto.Criptografia
{
    public static class CifraCesar
    {
        public static string Criptografar(string texto, int chave)
        {
            StringBuilder resultado = new StringBuilder();
            chave = ((chave % 26) + 26) % 26;

            foreach (char caractere in texto)
            {
                if (EhLetraAscii(caractere))
                {
                    char caractereBase;

                    if (char.IsUpper(caractere))
                    {
                        caractereBase = 'A';
                    }
                    else
                    {
                        caractereBase = 'a';
                    }

                    resultado.Append((char)((caractere - caractereBase + chave) % 26 + caractereBase));
                }
                else
                {
                    resultado.Append(caractere);
                }
            }

            return resultado.ToString();
        }

        public static string Descriptografar(string texto, int chave)
        {
            return Criptografar(texto, -chave);
        }

        public static string[] ForcaBruta(string textoCifrado)
        {
            string[] tentativas = new string[26];

            for (int i = 0; i < 26; i++)
            {
                tentativas[i] = Descriptografar(textoCifrado, i);
            }

            return tentativas;
        }

        private static bool EhLetraAscii(char caractere)
        {
            return caractere >= 'A' && caractere <= 'Z' || caractere >= 'a' && caractere <= 'z';
        }
    }
}
