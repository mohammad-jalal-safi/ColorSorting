using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorSorting.Model
{
    public sealed class ImageModel : INotifyPropertyChanged
    {
        private BitmapImage _bitmapImage;
        private static int _imageHeight = 250;
        private static int _imageWidth = 300;

        public BitmapImage BitmapImage
        {
            get => _bitmapImage;
            set
            {
                _bitmapImage = value;
                OnPropertyChanged(nameof(BitmapImage));
            }
        }
        public int ImageHeight { get => _imageHeight; }
        public int ImageWidth { get => _imageWidth; }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
