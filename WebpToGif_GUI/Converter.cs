using ImageMagick;

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
