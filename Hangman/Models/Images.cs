using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hangman.Models
{
    public class Images
    {
        public ObservableCollection<BitmapImage> Emojis { get; set; }
        public ObservableCollection<BitmapImage> Hangs { get; set; }

        public Images()
        {
            Emojis = new ObservableCollection<BitmapImage>();
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_1.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_2.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_3.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_4.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_5.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_6.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_7.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_8.png", UriKind.Relative)));
            Emojis.Add(new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_9.png", UriKind.Relative)));         
            
            Hangs = new ObservableCollection<BitmapImage>();
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_1.png", UriKind.Relative)));
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_2.png", UriKind.Relative)));
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_3.png", UriKind.Relative)));
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_4.png", UriKind.Relative)));
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_5.png", UriKind.Relative)));
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_6.png", UriKind.Relative)));
            Hangs.Add(new BitmapImage(new Uri(@"/Assets/Hangs/Hang_7.png", UriKind.Relative)));
        }

        //public void AddImages()
        //{
        //}
    }
}
