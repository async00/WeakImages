using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeakImages
{
    internal partial class ImageProcessor : Form
    {
        internal static Form1 form = new Form1();
        internal static void BeginBitmap(int width, int height)
        {

            form.PictureBox1.Height = height;
            form.PictureBox1.Width = width;

        }


        internal  static Bitmap bmp;
        private static string imagePath = "edit here ";
        private static string previousweakhex = string.Empty;
        private static int weakcombo = 0;
        internal static string weaktext = string.Empty;

        internal static void SetImagePath(string path)
        {
            imagePath = path;
        }



        private static void FormatImage(string hex, int maxsize)
        {
            //buralari duzenle berkay suan sadece bir olasilik icin dogru duzgun calisiyor 
            //prepare combo 
            if (hex == previousweakhex)
            {
                LogSys.InfoLog("combo ++");
                weakcombo++;
            }
            else if (hex != previousweakhex || weakcombo == maxsize - 1)
            {
                
                    if (weakcombo > 0)
                    {
                        LogSys.ErrorLog("combo ended with  " + weakcombo + " " + hex);
                        weaktext += Convert.ToString(weakcombo + 1) + hex;
                        weakcombo = 0;
                    }
                    else
                    {
                        weaktext += Convert.ToString(hex);
                    }
                

                previousweakhex = hex;
            }

        }

        public static void ExportPixelColorsToText()
        {
            try
            {
                bmp = new Bitmap(imagePath);

                form.InitializeBitmap(bmp.Width, bmp.Height);
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);
                        string hexColor = $"#{pixelColor.R:X2}{pixelColor.G:X2}{pixelColor.B:X2}";
                        FormatImage(hexColor, bmp.Width * bmp.Height);
                    }
                }
                LogSys.ErrorLog(weakcombo.ToString());
                LogSys.InfoLog(weaktext);
            

            }
            catch (Exception ex)
            {
                LogSys.ErrorLog($"Hata: {ex.Message}");
            }
        }

    }
}
