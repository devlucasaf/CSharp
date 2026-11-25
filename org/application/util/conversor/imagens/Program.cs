using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Application.Util.Conversor.Imagens
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string pasta = Path.Combine("target", "conversao");
            Directory.CreateDirectory(pasta);

            string origem = Path.Combine(pasta, "origem.png");
            using (var demo = GerarDemo())
            {
                demo.Save(origem, ImageFormat.Png);
            }
            Console.WriteLine("Origem: " + origem + " (" + new FileInfo(origem).Length + " bytes)");

            Console.WriteLine("\n=== CONVERSOES ===");

            string jpg100 = Path.Combine(pasta, "saida-q100.jpg");
            ConversorImagens.Converter(origem, jpg100, "jpg", 1.0f);
            Console.WriteLine("JPG qualidade 100%: " + new FileInfo(jpg100).Length + " bytes");

            string jpg50 = Path.Combine(pasta, "saida-q50.jpg");
            ConversorImagens.Converter(origem, jpg50, "jpg", 0.5f);
            Console.WriteLine("JPG qualidade 50%:  " + new FileInfo(jpg50).Length + " bytes");

            string jpg20 = Path.Combine(pasta, "saida-q20.jpg");
            ConversorImagens.Converter(origem, jpg20, "jpg", 0.2f);
            Console.WriteLine("JPG qualidade 20%:  " + new FileInfo(jpg20).Length + " bytes");

            string bmp = Path.Combine(pasta, "saida.bmp");
            ConversorImagens.Converter(origem, bmp, "bmp", 1.0f);
            Console.WriteLine("BMP:                " + new FileInfo(bmp).Length + " bytes");

            string pngDeJpg = Path.Combine(pasta, "volta-para-png.png");
            ConversorImagens.Converter(jpg50, pngDeJpg, "png", 1.0f);
            Console.WriteLine("PNG a partir do JPG q50%: " + new FileInfo(pngDeJpg).Length + " bytes");

            Console.WriteLine("\nArquivos em: " + Path.GetFullPath(pasta));
        }

        private static Bitmap GerarDemo()
        {
            var imagem = new Bitmap(800, 600, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(imagem))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var random = new Random(1);

                for (int y = 0; y < 600; y += 20)
                {
                    var cor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                    using var brush = new SolidBrush(cor);
                    g.FillRectangle(brush, 0, y, 800, 20);
                }

                using var fonte = new Font("Arial", 48f, FontStyle.Bold);
                using var brushBranco = new SolidBrush(Color.White);
                g.DrawString("CONVERSOR", fonte, brushBranco, 200, 300);
            }
            return imagem;
        }
    }
}
