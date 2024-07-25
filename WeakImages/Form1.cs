using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeakImages
{
    public partial class Form1 : Form
    {

        internal Bitmap bitmap =new Bitmap(64,64);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LogSys.BeginConsole();
            LogSys.InfoLog("Debug console started");

        }
        internal void InitializeBitmap(int width, int height)
        {
            bitmap = new Bitmap(width, height);

        }
        internal void ReloadPicture()
        {
            pictureBox1.Image = bitmap;
        }
        public PictureBox PictureBox1
        {
            get { return pictureBox1; }
        }
        internal void PaintPixel(int x, int y, string hexColor)
        {
            if (x < ImageProcessor.bmp.Width && y < ImageProcessor.bmp.Height)
            {
                Color color = ColorTranslator.FromHtml(hexColor);
                //  BeginBitmap(bmp.Width, bmp.Height);

                try
                {
                    bitmap.SetPixel(x, y, color);
                    LogSys.InfoLog("X ; " + x + "  Y : " + y + " color  : " + color.Name);

                }
                catch
                {
                    LogSys.ErrorLog("out of range ");

                }
                
            }
        }

        internal int CountCharacter(string input, char character)
        {
            int count = 0;
            foreach (char c in input)
            {
                if (c == character)
                {
                    count++;
                }
            }
            return count;
        }
        internal void InitWeakText(int width,int height,string _weaktext)
        {
            string weaktext = _weaktext;
            int x = 0, y = 0;

            for (int i1 = 0; i1 < CountCharacter(_weaktext,'#'); i1++)
            {
                LogSys.InfoLog("# count " + CountCharacter(_weaktext, '#'));


                if(weaktext.Length <=  1)
                {
                    break;
                }
                string wrepeathex = weaktext.Substring(0, weaktext.IndexOf("#"));
                string hexcolor = weaktext.Substring(weaktext.IndexOf("#"), 7);

                if (string.IsNullOrEmpty(wrepeathex))
                {
                    wrepeathex = "1";
                }
                LogSys.ErrorLog(" WREPETT" + wrepeathex);
                LogSys.ErrorLog("hex color " + hexcolor);
                

                
                for (int i = 0; i < Convert.ToInt64(wrepeathex); i++)
                {
                    if (x == width)
                    {
                        y++;
                        x = 0;
                    }
                    if (y == height)
                    {

                        break;
                    }
                    x++;
                    PaintPixel(x, y, hexcolor);
                }
                if(wrepeathex != "1")
                {
                    weaktext = RemoveFirstOccurrence(weaktext, wrepeathex);
                }
                
                weaktext = RemoveFirstOccurrence(weaktext, hexcolor);
            }
            ReloadPicture();
            PictureBox1.Invalidate();
        }

        internal void ClearBitmap(Bitmap bitmap, Color color)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    bitmap.SetPixel(x, y, color);
                }
            }
        }
        internal string RemoveFirstOccurrence(string input, string substringToRemove)
        {
            int index = input.IndexOf(substringToRemove);
            if (index == -1)
            {
                // Alt string bulunamazsa, orijinal string'i döndür
                return input;
            }
            // Alt string'i kaldır
            return input.Remove(index, substringToRemove.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearBitmap(bitmap, Color.White);
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            openFileDialog1.Title = "Bir dosya seçin";

            // OpenFileDialog'u göster ve kullanıcı bir dosya seçtiyse
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosyanın yolunu alın
                string filePath = openFileDialog1.FileName;

                // Dosya yolunu bir MessageBox ile gösterin
                ImageProcessor.SetImagePath(filePath);
                Thread.Sleep(500);
                ImageProcessor.ExportPixelColorsToText();
                Thread.Sleep(500);
                InitWeakText(ImageProcessor.bmp.Width,ImageProcessor.bmp.Height,ImageProcessor.weaktext);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bitmap.Save(@"C:\Users\stati\OneDrive\Masaüstü\bitmap saves\qweqw.png");
        }
    }
}