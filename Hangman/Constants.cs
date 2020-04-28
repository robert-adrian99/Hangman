using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Hangman.Models;

namespace Hangman
{
    class Constants
    {
        public static string UsersFile { get; } = @"..\..\_XmlFiles\Users.xml";
        public static string WordsFile { get; } = @"..\..\_XmlFiles\Words.xml";
        public static string ImagesFile { get; } = @"..\..\_XmlFiles\Images.xml";
        public static int numberOfWords { get; } = 5;
        public static int numberOfCategories { get; } = 5;
    }
}
