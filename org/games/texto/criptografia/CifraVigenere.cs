using System;
using System.Text;

namespace Games.Texto.Criptografia
{
    public static class CifraVigenere
    {
        public static string Criptografar(string texto, string chave)
        {
            ValidarChave(chave);

            StringBuilder resultado = new StringBuilder();
            chave = chave.ToUpperInvariant();
            int indiceChave = 0;

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

                    int deslocamento = chave[indiceChave % chave.Length] - 'A';

                    resultado.Append((char)((caractere - caractereBase + deslocamento) % 26 + caractereBase));

                    indiceChave++;
                }
                else
                {
                    resultado.Append(caractere);
                }
            }

            return resultado.ToString();
        }

        public static string Descriptografar(string texto, string chave)
        {
            ValidarChave(chave);

            StringBuilder resultado = new StringBuilder();
            chave = chave.ToUpperInvariant();
            int indiceChave = 0;

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

                    int deslocamento = chave[indiceChave % chave.Length] - 'A';

                    resultado.Append((char)((caractere - caractereBase - deslocamento + 26) % 26 + caractereBase));

                    indiceChave++;
                }
                else
                {
                    resultado.Append(caractere);
                }
            }

            return resultado.ToString();
        }

        private static void ValidarChave(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave))
            {
                throw new ArgumentException("A chave não pode estar vazia.", nameof(chave));
            }

            foreach (char caractere in chave)
            {
                if (!EhLetraAscii(caractere))
                {
                    throw new ArgumentException("A chave deve conter somente letras de A a Z.", nameof(chave));
                }
            }
        }

        private static bool EhLetraAscii(char caractere)
        {
            return caractere >= 'A' && caractere <= 'Z' || caractere >= 'a' && caractere <= 'z';
        }
    }
}
