using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace GallaryWPF
{
    /// <summary>
    /// Interaction logic for ImagePage.xaml
    /// </summary>

    public partial class ImagePage : Page
    {
        public Frame ikinci { get; set; }

        private string adress;

        public string Adress
        {
            get => adress;
            set
            {
                adress = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Image_gallery> Images { get; set; }
      
        public int Index { get; set; }

        DispatcherTimer timer = new DispatcherTimer();
        public ImagePage()
        {
            InitializeComponent();


        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            if (Index + 1 > Images.Count - 1)
                Index = 0;
            else
                Index += 1;
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(Images[Index].Source);
            logo.EndInit();
            selectimage.Source = logo;
            Adress = Images[Index].Source;

            CommandManager.InvalidateRequerySuggested();
        }

        public ImagePage(ObservableCollection<Image_gallery> images, int index ,Frame ikinci)
        {
            InitializeComponent();
            Adress = images[index].Source;
            this.Images = images;
            this.Index = index;
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(Images[Index].Source);
            logo.EndInit();
            selectimage.Source = logo;
            Adress = Images[Index].Source;

            timer.Tick += dispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);

            this.ikinci = ikinci;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private void evvelki_Click(object sender, RoutedEventArgs e)
        {
            if (Index - 1 < 0)
                Index = Images.Count - 1;
            else
                Index -= 1;
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(Images[Index].Source);
            logo.EndInit();
            selectimage.Source = logo;
            Adress = Images[Index].Source;


        }

        private void sonraki_Click(object sender, RoutedEventArgs e)
        {
            if (Index + 1 > Images.Count-1)
                Index = 0;
            else
                Index += 1;
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(Images[Index].Source);
            logo.EndInit();
            selectimage.Source = logo;
            Adress = Images[Index].Source;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            ikinci.Visibility = Visibility.Hidden;

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }
}
