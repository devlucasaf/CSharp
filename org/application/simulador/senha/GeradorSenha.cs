using System;

namespace Org.Application.Senha
{
    public class GeradorSenha
    {
        public static void Run()
        {
            Senha s = new Senha();

            Console.Write("Digite o número de sites ou apps: ");
            int nomeAplicativos = int.Parse(Console.ReadLine());

            for (int i = 0; i < nomeAplicativos; i++)
            {
                Console.Write("Digite o nome do app/site: ");
                s.SetNomeAPP(Console.ReadLine());

                Console.Write("Digite o tamanho da senha: ");
                int comprimento = int.Parse(Console.ReadLine());

                string senha = s.GerarSenha(comprimento);
                if (senha != null)
                {
                    Console.WriteLine("Senha gerada com sucesso! Sua senha: " + senha);
                    s.GravarSenha();
                }
            }
        }
    }
}