using System;

namespace Games.Narrativo.Rpg
{
    public class Personagem
    {
        private string  nome;
        private string  classe;
        private int     vida;
        private int     vidaMaxima;
        private int     ataque;
        private int     defesa;
        private int     nivel;
        private int     experiencia;
        private int     ouro;

        private readonly Inventario inventario;
        private readonly Random random = new Random();

        public Personagem(string nome, string classe)
        {
            this.nome = nome;
            this.classe = classe;
            this.nivel = 1;
            this.experiencia = 0;
            this.ouro = 50;
            this.inventario = new Inventario();

            switch (classe.ToUpperInvariant())
            {
                case "GUERREIRO":
                    vidaMaxima = 120;
                    ataque = 15;
                    defesa = 10;
                    break;
                case "MAGO":
                    vidaMaxima = 80;
                    ataque = 25;
                    defesa = 5;
                    break;
                case "ARQUEIRO":
                    vidaMaxima = 100;
                    ataque = 20;
                    defesa = 7;
                    break;
                default:
                    vidaMaxima = 100;
                    ataque = 15;
                    defesa = 7;
                    break;
            }
            vida = vidaMaxima;
        }

        public int Atacar()
        {
            int dano = ataque + random.Next(10) - 3;
            bool critico = random.Next(100) < 15;
            if (critico)
            {
                dano *= 2;
                Console.WriteLine("⚡ GOLPE CRÍTICO!");
            }
            return Math.Max(dano, 1);
        }

        public int ReceberDano(int dano)
        {
            int danoReal = Math.Max(dano - defesa - random.Next(5), 1);
            vida -= danoReal;
            if (vida < 0) vida = 0;
            return danoReal;
        }

        public void Curar(int quantidade)
        {
            vida = Math.Min(vida + quantidade, vidaMaxima);
        }

        public void GanharExperiencia(int xp)
        {
            experiencia += xp;
            Console.WriteLine("+" + xp + " XP ganho!");
            while (experiencia >= nivel * 100)
            {
                experiencia -= nivel * 100;
                SubirNivel();
            }
        }

        private void SubirNivel()
        {
            nivel++;
            vidaMaxima += 15;
            ataque += 3;
            defesa += 2;
            vida = vidaMaxima;
            Console.WriteLine("  LEVEL UP! Agora você é nível " + nivel + "!");
            Console.WriteLine("   Vida: " + vidaMaxima + " | Ataque: " + ataque + " | Defesa: " + defesa);
        }

        public void GanharOuro(int quantidade) => ouro += quantidade;

        public bool EstaVivo() => vida > 0;

        public void MostrarStatus()
        {
            Console.WriteLine("\n===== STATUS =====");
            Console.WriteLine("Nome: " + nome + " (" + classe + ")");
            Console.WriteLine("Nível: " + nivel + " | XP: " + experiencia + "/" + (nivel * 100));
            Console.WriteLine("Vida: " + vida + "/" + vidaMaxima);
            Console.WriteLine("Ataque: " + ataque + " | Defesa: " + defesa);
            Console.WriteLine("Ouro: " + ouro);
            Console.WriteLine("==================");
        }

        public string Nome => nome;
        public string Classe => classe;
        public int Vida => vida;
        public int VidaMaxima => vidaMaxima;
        public int Ataque => ataque;
        public int Defesa => defesa;
        public int Nivel => nivel;
        public int Ouro => ouro;
        public Inventario Inventario => inventario;
        public void SetOuro(int valor) => ouro = valor;
    }
}
