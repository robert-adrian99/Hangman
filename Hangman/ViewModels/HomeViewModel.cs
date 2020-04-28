using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Runtime;
using Hangman.Models;
using Hangman.Helps;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hangman.ViewModels
{
    public class HomeViewModel : NotifyViewModel
    {

        // H E ＿ L ＿
        private User user;
        private Words words;
        private Images images = new Images();
        private DispatcherTimer timer = new DispatcherTimer();
        private int delay = 21;
        private DateTime deadline;
        private bool firstCreation = true;
        private int mistakes = 0;

        public HomeViewModel()
        {

        }
        public HomeViewModel(User user)
        {
            this.user = user;
            HangImageSource = images.Hangs[mistakes];
            SerializationActions serializationActions = new SerializationActions();
            words = serializationActions.DeserializeWords(Constants.WordsFile);
            PickWord();
            CreateWordOnDisplay(user.GameProperty.WordToGuess);
            timer.Tick += new EventHandler(TimerTick);
            deadline = DateTime.Now.AddSeconds(delay);
            timer.Start();
        }

        private void PickWord()
        {
            var random = new Random();
            switch (user.GameProperty.CategoryProperty)
            {
                case Models.Category.All:
                    int randomCategory = random.Next(Constants.numberOfCategories);
                    switch (randomCategory)
                    {
                        case 0:
                            user.GameProperty.WordToGuess = words.Cars[random.Next(words.Cars.Count)];
                            break;
                        case 1:
                            user.GameProperty.WordToGuess = words.Movies[random.Next(words.Movies.Count)];
                            break;
                        case 2:
                            user.GameProperty.WordToGuess = words.Mountains[random.Next(words.Mountains.Count)];
                            break;
                        case 3:
                            user.GameProperty.WordToGuess = words.Rivers[random.Next(words.Rivers.Count)];
                            break;
                        case 4:
                            user.GameProperty.WordToGuess = words.States[random.Next(words.States.Count)];
                            break;
                        default:
                            break;
                    }
                    break;
                case Models.Category.Cars:
                    user.GameProperty.WordToGuess = words.Cars[random.Next(words.Cars.Count)];
                    break;
                case Models.Category.Movies:
                    user.GameProperty.WordToGuess = words.Movies[random.Next(words.Movies.Count)];
                    break;
                case Models.Category.States:
                    user.GameProperty.WordToGuess = words.States[random.Next(words.States.Count)];
                    break;
                case Models.Category.Mountains:
                    user.GameProperty.WordToGuess = words.Mountains[random.Next(words.Mountains.Count)];
                    break;
                case Models.Category.Rivers:
                    user.GameProperty.WordToGuess = words.Rivers[random.Next(words.Rivers.Count)];
                    break;
                default:
                    break;
            }
        }

        public string Name
        {
            get
            {
                return user.Name;
            }
        }

        public string Category
        {
            get
            {
                return user.GameProperty.CategoryProperty.ToString();
            }
        }

        public string Level
        {
            get
            {
                return user.GameProperty.LevelProperty.ToString();
            }
        }

        public BitmapImage UserImageSource
        {
            get
            {
                return images.Emojis[user.ImageIndex];
            }
        }

        private BitmapImage hangImageSource;
        public BitmapImage HangImageSource
        {
            get
            {
                return hangImageSource;
            }
            set
            {
                hangImageSource = value;
                NotifyPropertyChanged("HangImageSource");
            }
        }

        public string WordOnDisplay
        {
            get
            {
                return user.GameProperty.WordOnDisplay;
            }
            set
            {
                user.GameProperty.WordOnDisplay = value;
                NotifyPropertyChanged("WordOnDisplay");
            }
        }

        private void CreateWordOnDisplay(string word)
        {
            word = new Regex(@"\s").Replace(word, "   ");
            if (firstCreation)
            {
                user.GameProperty.WordOnDisplay = new Regex(@"\S").Replace(word, "＿ ");
            }
            else
            {
                user.GameProperty.WordOnDisplay = new Regex(@"＿").Replace(word, "＿ ");
            }
            WordOnDisplay = user.GameProperty.WordOnDisplay;
            firstCreation = false;
        }

        private ICommand pressCommand;
        public ICommand PressCommand
        {
            get
            {
                if (pressCommand == null)
                {
                    pressCommand = new RelayCommand(LetterPressed);
                }
                return pressCommand;
            }
        }

        public string Letter { get; }

        public void LetterPressed(object buttonClicked)
        {
            bool foundLetter = false;
            char letterPressed = (buttonClicked as Button).Content.ToString()[0];
            for (int index = 0; index < user.GameProperty.WordToGuess.Length; ++index)
            {
                if (user.GameProperty.WordToGuess[index] == letterPressed)
                {
                    string word = WordOnDisplay;
                    word = new Regex(@"\s\s\s").Replace(word, " ");
                    word = new Regex(@"＿\s").Replace(word, "＿");
                    StringBuilder newString = new StringBuilder(word);
                    newString[index] = letterPressed;
                    CreateWordOnDisplay(newString.ToString());
                    foundLetter = true;
                }
            }
            if(foundLetter)
            {
                return;
            }
            if (mistakes < 5)
            {
                HangImageSource = images.Hangs[++mistakes];
            }
            else
            {
                HangImageSource = images.Hangs[++mistakes];
                MessageBox.Show("You lose!");
                mistakes = 0;
            }
        }

        public string Timer
        {
            get
            {
                if((deadline - DateTime.Now).Seconds < 10)
                {
                    return (deadline - DateTime.Now).Minutes.ToString() + ":0" + (deadline - DateTime.Now).Seconds.ToString(); 
                }
                return (deadline - DateTime.Now).Minutes.ToString() + ":" + (deadline - DateTime.Now).Seconds.ToString();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //int secondsRemaining = (deadline - DateTime.Now).Seconds;
            //NotifyPropertyChanged("Timer");
            //if (secondsRemaining == 0)
            //{
            //    timer.Stop();
            //    timer.IsEnabled = false;
            //    MessageBox.Show("Time has expired!");
            //    delay = 21;
            //}
        }

        //private void TimerStart()
        //{

        //}

        // PT COMANDA DE EXIT
        //private void TimerStop()
        //{
        //    timer.Stop();
        //    delay = (deadline - DateTime.Now).Seconds;
        //    deadline = DateTime.Now.AddSeconds(delay);
        //}
    }
}
