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
using System.Windows.Media;
using System.Collections.ObjectModel;
using Hangman.Views;

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
        private ObservableCollection<Button> buttons = new ObservableCollection<Button>();

        public HomeViewModel()
        {

        }
        public HomeViewModel(User user)
        {
            this.user = user;
            HangImageSource = images.Hangs[user.GameProperty.MistakesProperty];
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

        public int Level
        {
            get
            {
                return user.GameProperty.LevelProperty;
            }
            set
            {
                user.GameProperty.LevelProperty = value;
                NotifyPropertyChanged("Level");
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
            buttons.Add(buttonClicked as Button);
            (buttonClicked as Button).IsEnabled = false;
            (buttonClicked as Button).Foreground = Brushes.LimeGreen;
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
            if (!Regex.Match(WordOnDisplay, "＿").Success)
            {
                Level = user.GameProperty.LevelProperty + 1;
                if (user.GameProperty.LevelProperty >= 2)
                {
                    ShowMessageBox("You won", "You won a game in category " + user.GameProperty.CategoryProperty.ToString() + ".\nDo you want to start a new game on the same category?", MessageBoxImage.Information);
                    return;
                }
                ReloadGame();
            }
            if(foundLetter)
            {
                return;
            }
            (buttonClicked as Button).Foreground = Brushes.Red;
            if (user.GameProperty.MistakesProperty < 5)
            {
                HangImageSource = images.Hangs[++user.GameProperty.MistakesProperty];
                return;
            }
            else
            {
                HangImageSource = images.Hangs[++user.GameProperty.MistakesProperty];
                ShowMessageBox("You lost", "You lost this game on " + user.GameProperty.CategoryProperty.ToString() + ".\nDo you want to start a new game on the same category?", MessageBoxImage.Error);
            }
        }

        private void ReloadGame()
        {
            foreach(var button in buttons)
            {
                button.IsEnabled = true;
                button.Foreground = Brushes.White;
            }
            PickWord();
            firstCreation = true;
            CreateWordOnDisplay(user.GameProperty.WordToGuess);
            delay = 21;
            user.GameProperty.MistakesProperty = 0;
            HangImageSource = images.Hangs[user.GameProperty.MistakesProperty];
            deadline = DateTime.Now.AddSeconds(delay);
            timer.Start();
            Level = user.GameProperty.LevelProperty;
        }

        private void ShowMessageBox(string title, string details, MessageBoxImage messageBoxImage)
        {
            timer.Stop();
            MessageBoxResult messageBoxResult = MessageBox.Show(details, title, MessageBoxButton.YesNo, messageBoxImage);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                user.GameProperty.LevelProperty = 1;
                Level = user.GameProperty.LevelProperty;
                ReloadGame();
            }
            else
            {
                user.GameProperty.MistakesProperty = 0;
                CategoryWindow categoryWindow = new CategoryWindow();
                CategoryViewModel categoryVM = new CategoryViewModel(user);
                categoryWindow.DataContext = categoryVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = categoryWindow;
                categoryWindow.Show();
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
            int secondsRemaining = (deadline - DateTime.Now).Seconds;
            int minutesRemaining = (deadline - DateTime.Now).Minutes;
            NotifyPropertyChanged("Timer");
            if (secondsRemaining == 0 && minutesRemaining == 0)
            {
                TimeExpired();
            }
        }

        private void TimeExpired()
        {
            ShowMessageBox("Time has expired", "Time has expired!\nDo you want to start a new game on the same category?", MessageBoxImage.Warning);
        }
    }
}
