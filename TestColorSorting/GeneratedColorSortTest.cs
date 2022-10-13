using ColorSorting.ViewModel;
using System.Drawing;

namespace TestColorSorting
{
    public class GeneratedColorSortTest
    {
        ImageViewModel Test;
        [SetUp]
        public void Setup()
        {
            Test = new ImageViewModel();
        }

        [Test]
        public void CheckForImageIsNotNull()
        {
            Assert.Throws<FileFormatException>(() => Test.Sort(null));
        }

        [Test]
        public void CheckForLargeSortedImage()
        {
            Bitmap bitmap =(Bitmap)Bitmap.FromFile(@"Image/largesorted.png");
            Assert.AreEqual(BitmapToByte(bitmap), BitmapToByte(Test.Sort(bitmap)));
        }

        [Test]
        public void CheckForRanndomImage()
        {
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(@"Image/random.png");
            Assert.AreNotEqual(BitmapToByte(bitmap), BitmapToByte(Test.Sort(bitmap)));
        }

        [Test]
        public void CheckForSmallSortedImage()
        {
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(@"Image/smalsorted.png");
            Assert.AreEqual(BitmapToByte(bitmap), BitmapToByte(Test.Sort(bitmap)));
        }

        [Test]
        public void CheckForOnePixelChanged()
        {
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(@"Image/onepixelchange.png");
            Assert.AreNotEqual(BitmapToByte(bitmap), BitmapToByte(Test.Sort(bitmap)));
        }

        [Test]
        public void CheckForManyPixelChanged()
        {
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(@"Image/manypixelchange.png");
            Assert.AreNotEqual(BitmapToByte(bitmap), BitmapToByte(Test.Sort(bitmap)));
        }

        private byte[] BitmapToByte(Bitmap bitmap)
        {
            Byte[] bytes;
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                bytes = stream.ToArray();
            }
            return bytes;
        }
    }
}
