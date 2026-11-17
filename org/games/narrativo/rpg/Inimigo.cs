using System;

namespace Games.Narrativo.Rpg
{
    public class Inimigo
    {
        private string  nome;
        private int     vida;
        private int     vidaMaxima;
        private int     ataque;
        private int     defesa;
        private int     xpRecompensa;
        private int     ouroRecompensa;

        private readonly Random random = new Random();

        public Inimigo(string nome, int vida, int ataque, int defesa, int xp, int ouro)
        {
            this.nome = nome;
            this.vida = vida;
            this.vidaMaxima = vida;
            this.ataque = ataque;
            this.defesa = defesa;
            this.xpRecompensa = xp;
            this.ouroRecompensa = ouro;
        }

        public int Atacar() => ataque + random.Next(8) - 2;

        public int ReceberDano(int dano)
        {
            int danoReal = Math.Max(dano - defesa - random.Next(3), 1);
            vida -= danoReal;
            if (vida < 0) vida = 0;
            return danoReal;
        }

        public bool EstaVivo() => vida > 0;

        public string BarraVida()
        {
            int barras = (int)((double)vida / vidaMaxima * 20);
            barras = Math.Max(barras, 0);
            return new string('\0', barras) + new string('\0', 20 - barras);
        }

        // Fábrica de inimigos por nível
        public static Inimigo GerarInimigo(int nivelJogador)
        {
            Random rnd = new Random();
            int tipo = rnd.Next(5);
            int mult = nivelJogador;

            return tipo switch
            {
                0 => new Inimigo("Goblin", 30 + 10 * mult, 8 + 2 * mult, 2 + mult, 25 * mult, 10 + 5 * mult),
                1 => new Inimigo("Esqueleto", 40 + 12 * mult, 10 + 3 * mult, 3 + mult, 35 * mult, 15 + 5 * mult),
                2 => new Inimigo("Lobo Selvagem", 35 + 8 * mult, 12 + 3 * mult, 1 + mult, 30 * mult, 8 + 5 * mult),
                3 => new Inimigo("Orc", 55 + 15 * mult, 12 + 4 * mult, 5 + mult, 50 * mult, 25 + 10 * mult),
                4 => new Inimigo("Dragão Menor", 80 + 20 * mult, 15 + 5 * mult, 7 + 2 * mult, 80 * mult, 50 + 15 * mult),
                _ => new Inimigo("Slime", 20 + 5 * mult, 5 + mult, 1, 15 * mult, 5 + 3 * mult)
            };
        }

        public string Nome => nome;
        public int Vida => vida;
        public int VidaMaxima => vidaMaxima;
        public int XpRecompensa => xpRecompensa;
        public int OuroRecompensa => ouroRecompensa;
    }
}
