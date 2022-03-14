using System;
using System.Collections.Generic;
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

namespace GallaryWPF
{
    /// <summary>
    /// Interaction logic for ImagePage.xaml
    /// </summary>
    
    public partial class ImagePage : Page
    {
        private string source;

        public string Adress
        {
            get { return source; }
            set { source = value; OnPropertyChanged(); }
        }

      
        public List<Image_gallery> images { get; set; }
        public int index { get; set; }
        public ImagePage()
        {
            InitializeComponent();


        }
        public ImagePage(List<Image_gallery> images,int index)
        {
            InitializeComponent();

            //List<Image_gallery> images = new List<Image_gallery>();

            Adress = images[index].source;
            this.images = images;
            this.index = index;  
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
            if (index - 1 < 0) index=images.Count-1;
            else index -=1;
            Adress = images[index].source;
     
        }
    }
}
