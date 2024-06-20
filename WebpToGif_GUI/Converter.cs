using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebpToGif_GUI
{
    internal class Converter
    {
        public static void ConvertWebPToGif(string webpFilePath, string gifFilePath)
        {
            using (var collection = new MagickImageCollection(webpFilePath))
            {
                collection.Write(gifFilePath);
            }
        }
    }
}
