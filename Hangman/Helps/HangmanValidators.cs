using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Hangman.Models;

namespace Hangman.Helps
{
    class HangmanValidators
    {
        public static bool CanExecutePlay(string nameTextBox)
        {
            return nameTextBox == "" ? false : true;
        }
        public static bool CanExecuteNext(BitmapImage image, Images images)
        {
            return images.Emojis.IndexOf(image) < images.Emojis.Count - 1;
        }
        public static bool CanExecutePrev(BitmapImage image, Images images)
        {
            return images.Emojis.IndexOf(image) > 0;
        }
        public static bool CanAddUser(string name, Users users)
        {
            foreach (var user in users.List)
            {
                if (user.Name == name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
