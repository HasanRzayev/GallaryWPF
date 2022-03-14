using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

public class Image_gallery
{
    public Image_gallery(BitmapImage image, string source)
    {
        this.Image = image;
        this.Source = source;
    }

    public BitmapImage Image { get; set; }

    public string Source { get; set; }
}
namespace GallaryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Image_gallery> images = new List<Image_gallery>();
        public MainWindow()
        {
            InitializeComponent();

         

            DataContext = this;
        }
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private void Open_Click(object sender, RoutedEventArgs e)
        {

            int count = 0;
            string adress = null;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK) adress = dialog.SelectedPath;

            }
            if (adress != null)
            {
                string[] files = Directory.GetFiles($@"{adress}");
                foreach (var f in files)
                {
                    if (ImageExtensions.Contains(System.IO.Path.GetExtension(f).ToUpperInvariant()))
                    {

                    }
                    else { count++; }
                }

                if (count == 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        BitmapImage myBitmapImage = new BitmapImage();


                        myBitmapImage.BeginInit();
                        myBitmapImage.UriSource = new Uri($@"{files[i]}");


                        myBitmapImage.DecodePixelWidth = 200;
                        myBitmapImage.EndInit();

                        Image lazimli = new Image();
                        Image_gallery sadesekil = new Image_gallery(myBitmapImage, files[i]);
                        images.Add(sadesekil);


                    }


                    galeryicindekiler.ItemsSource = images;
                }
            }

        }

        private void Listvievform_Click(object sender, RoutedEventArgs e)
        {
            Style style = FindResource("List") as Style;

            qallery.Style = style;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                qallery.Visibility = Visibility.Hidden;
                ikinci.Width = 1000;
                ikinci.Height = 800;
                ImagePage imagepage = new ImagePage(images, images.IndexOf(images.FindAll(n => n.Source == btn.Tag.ToString()).ToList()[0]));

                ikinci.Navigate(imagepage);
            }

        }
    }
}
