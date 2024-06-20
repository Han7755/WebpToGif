using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebpToGif_GUI
{
    public static class FileNameHelper
    {
        public static string GetUniqueFileName(string directory, string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string uniqueFileName = fileName;
            int counter = 1;

            while (File.Exists(Path.Combine(directory, uniqueFileName)))
            {
                uniqueFileName = $"{fileNameWithoutExtension} ({counter}){fileExtension}";
                counter++;
            }

            return uniqueFileName;
        }
    }
}
