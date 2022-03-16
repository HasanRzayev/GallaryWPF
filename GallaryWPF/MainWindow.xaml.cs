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
        NavigationService service;


        List<Image_gallery> images = new List<Image_gallery>();
        public MainWindow()
        {
            InitializeComponent();
            service = ikinci.NavigationService;
            service.Navigate("MainWindow.xaml.cs", UriKind.Relative);
   
         

            DataContext = this;
        }
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private void AddFile_Click(object sender, RoutedEventArgs e)
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
                ImagePage imagepage = new ImagePage(images, images.IndexOf(images.FindAll(n => n.Source == btn.Tag.ToString()).ToList()[0]),ikinci);

                ikinci.Navigate(imagepage);
            }

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BitmapImage myBitmapImage = new BitmapImage();


                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri($@"{new Uri(op.FileName)}");


                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();

                Image lazimli = new Image();
                Image_gallery sadesekil = new Image_gallery(myBitmapImage, new Uri(op.FileName).ToString());
                images.Add(sadesekil);
            }
      
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog file=new SaveFileDialog();
            file.DefaultExt = "jpg";
            file.FileName = ".jpg";
            file.Filter = "jpf files (*.jpg)";
            if (file.ShowDialog() == true)
            {
              
                    var encoder = new JpegBitmapEncoder();
                for (int i = 0; i < images.Count; i++)
                {
                    encoder.Frames.Add(BitmapFrame.Create(images[i].Image));
                }
                    using (var stream = file.OpenFile())
                    {
                        encoder.Save(stream);
                    }
                
               
            }
        }

        private void Saveas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void smallvievform_Click(object sender, RoutedEventArgs e)
        {

            Style style = FindResource("Small") as Style;

            qallery.Style = style;
        }

        private void Extravievform_Click(object sender, RoutedEventArgs e)
        {
            
            Style style = FindResource("Extra large") as Style;

            qallery.Style = style;
        }

        private void Largevievform_Click(object sender, RoutedEventArgs e)
        {
            Style style = FindResource("Large") as Style;

            qallery.Style = style;
        }
    }
}
