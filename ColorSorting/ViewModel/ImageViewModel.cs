using ColorSorting.Command;
using ColorSorting.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ColorSorting.ViewModel
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        private ImageModel _imageModel;
        public ImageModel ImageModel { get { return _imageModel; } }
        private bool _randomButtonStatus = true;
        private bool _sortButtonStatus = false;


        public ImageViewModel()
        {
            _imageModel = new ImageModel();
        }

        public bool RandomButtonStatus
        {
            get => _randomButtonStatus;
            set
            {
                _randomButtonStatus = value;
                OnPropertyChanged(nameof(RandomButtonStatus));
            }
        }

        public bool SortButtonStatus
        {
            get => _sortButtonStatus;
            set
            {
                _sortButtonStatus = value;
                OnPropertyChanged(nameof(SortButtonStatus));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// color generation command
        /// </summary>
        private CommandHandler _generateColorCommand;
        public CommandHandler GenerateColorCommand
        {
            get
            {
                return _generateColorCommand ??
                       (_generateColorCommand = new CommandHandler(obj =>
                       {
                           try
                           {
                               // set image source
                               _imageModel.BitmapImage = BitmapToBitmapImage(RandomBitmap(_imageModel.ImageWidth,_imageModel.ImageHeight));
                               SortButtonStatus = true;

                           }
                           catch (Exception ex)
                           {
                               Console.Write(ex.ToString());
                           }
                       }));
            }
        }


        /// <summary>
        /// sorting generated color command
        /// </summary>
        private CommandHandler _sortColorCommand;
        public CommandHandler SortColorCommand
        {
            get
            {
                return _sortColorCommand ??
                       (_sortColorCommand = new CommandHandler(async obj =>
                       {
                           try
                           {
                               RandomButtonStatus = false;
                               SortButtonStatus = false;
                               await Task.Run(() =>
                               {
                                   _imageModel.BitmapImage = BitmapToBitmapImage(Sort(BitmapImageToBitmap(_imageModel.BitmapImage)));
                                   
                               });
                               RandomButtonStatus = true;
                               SortButtonStatus = true;
                               // sort and replace image
                           }
                           catch (Exception ex)
                           {
                               Console.Write(ex.ToString());
                           }
                       }));
            }
        }

        private BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        private Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            Bitmap bitmap;
            using (var memory = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(memory);
                bitmap = new Bitmap(memory);
            }
            return new Bitmap(bitmap);
        }

        public Bitmap RandomBitmap(int width,int height)
        {
            // exception handling
            //1. empty image
            if(width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and height should be declared!");
            }

            //bitmap
            Bitmap bitmap = new Bitmap(width, height);
            //random number
            Random rand = new Random();
            //create random pixels
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //generate random ARGB value
                    int a = rand.Next(256);
                    int r = rand.Next(256);
                    int g = rand.Next(256);
                    int b = rand.Next(256);

                    //set ARGB value
                    bitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            return bitmap;
        }

        public Bitmap Sort(Bitmap bitmap)
        {
            //Exception Handling
            //1. check if bitmap is null of empty
            if (bitmap == null || bitmap.Width <= 0 || bitmap.Height <= 0)
            {
                throw new FileFormatException("Generated file is not in a correct format!");
            }
            int height = bitmap.Height;
            int width = bitmap.Width;
            // sorting the image by bubble sort algorithm
            for (int y = 0; y < height; y++)
            {
                bool check = true;
                for (int i = 1; (i <= (width - 1)) && check; i++)
                {
                    check = false;
                    for (int k = 0; k < (width - 1); k++)
                    {
                        Color currentPixelColor = bitmap.GetPixel(k, y);
                        float currentHue = currentPixelColor.GetHue();

                        Color nextPixelColor = bitmap.GetPixel(k + 1, y);
                        float nextPixelHue = nextPixelColor.GetHue();

                        if (currentHue > nextPixelHue)
                        {
                            Color temp = currentPixelColor;
                            bitmap.SetPixel(k, y, nextPixelColor);
                            bitmap.SetPixel(k + 1, y, temp);
                            check = true;
                        }
                    }
                }
            }
            return bitmap;
        }

    }
}
