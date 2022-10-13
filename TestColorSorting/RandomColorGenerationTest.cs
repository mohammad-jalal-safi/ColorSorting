using ColorSorting.ViewModel;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace TestColorSorting
{
    public class RandomColorGenerationTest
    {
        ImageViewModel Test;
        [SetUp]
        public void Setup()
        {
            Test = new ImageViewModel();
        }

        [Test]
        public void RandomBitmapTestForWidthArgument()
        {
            Assert.Throws<ArgumentException>(() => Test.RandomBitmap(0, 100));
        }
        [Test]
        public void RandomBitmapTestForHeigthArgument()
        {
            Assert.Throws<ArgumentException>(() => Test.RandomBitmap(100, -10));
        }

        [Test]
        public void RandomBitmapTestForGeneration()
        {
            Bitmap bitmap = new Bitmap(100, 100);
            Random rand = new Random();
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    int a = rand.Next(256);
                    int r = rand.Next(256);
                    int g = rand.Next(256);
                    int b = rand.Next(256);
                    bitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            Assert.AreNotEqual(bitmap, Test.RandomBitmap(100, 100));
        }

        [Test]
        public void RandomBitmapTestForGenerationWithTwiceCall()
        {
            Bitmap bitmap = new Bitmap(100, 100);
            Random rand = new Random();
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    int a = rand.Next(256);
                    int r = rand.Next(256);
                    int g = rand.Next(256);
                    int b = rand.Next(256);
                    bitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            Assert.AreNotEqual(Test.RandomBitmap(100, 100), Test.RandomBitmap(100, 100));
        }

    }
}