using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Application.Util.Conversor.Imagens
{
    public static class ConversorImagens
    {
        public static void Converter(string entrada, string saida, string formato, float qualidade)
        {
            using Image original = Image.FromFile(entrada);
            string f = formato.ToLowerInvariant();

            Image compativel = original;
            bool precisaFundoBranco = f == "jpg" || f == "jpeg" || f == "bmp";

            if (precisaFundoBranco)
            {
                var bmpRgb = new Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb);
                using (var graphics = Graphics.FromImage(bmpRgb))
                {
                    graphics.Clear(Color.White);
                    graphics.DrawImage(original, 0, 0, original.Width, original.Height);
                }
                compativel = bmpRgb;
            }

            try
            {
                if (f == "jpg" || f == "jpeg")
                {
                    EscreverComQualidade(compativel, saida, qualidade);
                }
                else
                {
                    ImageFormat imageFormat = ObterFormato(f);
                    compativel.Save(saida, imageFormat);
                }
            }
            finally
            {
                if (!ReferenceEquals(compativel, original))
                {
                    compativel.Dispose();
                }
            }
        }

        private static void EscreverComQualidade(Image img, string destino, float qualidade)
        {
            ImageCodecInfo? codec = ObterCodecJpeg();
            if (codec == null)
            {
                throw new InvalidOperationException("Codec JPEG não encontrado.");
            }

            using var parametros = new EncoderParameters(1);
            long q = Math.Clamp((long)(qualidade * 100f), 0L, 100L);
            parametros.Param[0] = new EncoderParameter(Encoder.Quality, q);

            img.Save(destino, codec, parametros);
        }

        private static ImageFormat ObterFormato(string f)
        {
            return f switch
            {
                "jpg" => ImageFormat.Jpeg,
                "jpeg" => ImageFormat.Jpeg,
                "png" => ImageFormat.Png,
                "bmp" => ImageFormat.Bmp,
                "gif" => ImageFormat.Gif,
                "tiff" => ImageFormat.Tiff,
                "tif" => ImageFormat.Tiff,
                _ => throw new ArgumentException("Formato não suportado: " + f)
            };
        }

        private static ImageCodecInfo? ObterCodecJpeg()
        {
            foreach (var codec in ImageCodecInfo.GetImageEncoders())
            {
                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
