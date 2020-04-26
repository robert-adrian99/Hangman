using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Hangman.Models;

namespace Hangman.Services
{
    class HangmanValidators
    {
        public static bool CanExecutePlay(string nameTextBox)
        {
            return nameTextBox == "" ? false : true;
        }
        public static bool CanExecuteNext(BitmapImage image, Images images)
        {
            return images.ImagesList.IndexOf(image) < images.ImagesList.Count - 1;
        }
        public static bool CanExecutePrev(BitmapImage image, Images images)
        {
            return images.ImagesList.IndexOf(image) > 0;
        }
    }
}
