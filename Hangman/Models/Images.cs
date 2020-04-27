using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hangman.Models
{
    class Images
    {
        public ObservableCollection<BitmapImage> ImagesList { get; set; }

        public Images()
        {
            ImagesList = new ObservableCollection<BitmapImage>();
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_1.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_2.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_3.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_4.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_5.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_6.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_7.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_8.png", UriKind.Relative)));
            ImagesList.Add(new BitmapImage(new Uri(@"/Assets/Emoji_9.png", UriKind.Relative)));
        }
    }
}
