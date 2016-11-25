using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Medical_tp
{
    class Tools
    {

        public static Byte[][] ImageArrayToByteArray(ObservableCollection<ImageSource> images)
        {
            Byte[][] img = new Byte[images.Count][];
            int i = 0;
            while (i < images.Count)
            {
                BitmapImage bimg= (BitmapImage)images[i];
                img[i] = ImageToByte(bimg);
                i++;
            }
            return img;
        }



        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();

            using (var mem = new MemoryStream(imageData))
            {
                image.CacheOption = BitmapCacheOption.None;
                mem.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.None;
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            return image;
        }

        public static byte[] ImageToByte(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.QualityLevel = 70;
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }


        public static string tabStringToString(string[] tabString)
        {
            string str = "";

            foreach (string stmp in tabString)
                str += stmp + System.Environment.NewLine;


            return str;
        }

        public static string[] stringToTabString(string str)
        {
            string backLine = System.Environment.NewLine;
            return str.Split(backLine.ToCharArray());
        }
       
    }
}
