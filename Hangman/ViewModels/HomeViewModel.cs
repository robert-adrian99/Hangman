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
        private Users users;
        private Images images = new Images();
        private DispatcherTimer timer = new DispatcherTimer();
        private int delay = 61;
        private DateTime deadline;
        private bool firstCreation = true;
        private bool resumeGame = false;
        private ObservableCollection<Button> buttons = new ObservableCollection<Button>();
        private SerializationActions serializationActions = new SerializationActions();

        public HomeViewModel()
        {

        }
        public HomeViewModel(User user, bool resumeGame = false)
        {
            users = serializationActions.DeserializeUsers(Constants.UsersFile);
            words = serializationActions.DeserializeWords(Constants.WordsFile);
            foreach (var userInList in users.List)
            {
                if (userInList.Name == user.Name)
                {
                    users.List.Remove(userInList);
                    users.List.Add(user);
                    break;
                }
            }
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            users = serializationActions.DeserializeUsers(Constants.UsersFile);
            this.user = user;
            if (!resumeGame)
            {
                PickWord();
                CreateWordOnDisplay(user.GameProperty.WordToGuess);
                delay = 61;
            }
            else
            {
                switch (user.GameProperty.CategoryProperty)
                {
                    case Models.Category.All:
                        AllIsSelected = true;
                        break;
                    case Models.Category.Cars:
                        CarsIsSelected = true;
                        break;
                    case Models.Category.Movies:
                        MoviesIsSelected = true;
                        break;
                    case Models.Category.States:
                        StatesIsSelected = true;
                        break;
                    case Models.Category.Mountains:
                        MountainsIsSelected = true;
                        break;
                    case Models.Category.Rivers:
                        RiversIsSelected = true;
                        break;
                    default:
                        break;
                }
                delay = user.GameProperty.SecondsRemaining;
                firstCreation = false;
                this.resumeGame = resumeGame;
            }
            HangImageSource = images.Hangs[user.GameProperty.MistakesProperty];
            timer.Tick += new EventHandler(TimerTick);
            StartTimer(delay);
        }

        private void PickWord()
        {
            var random = new Random();
            switch (user.GameProperty.CategoryProperty)
            {
                case Models.Category.All:
                    AllIsSelected = true;
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
                    CarsIsSelected = true;
                    user.GameProperty.WordToGuess = words.Cars[random.Next(words.Cars.Count)];
                    break;
                case Models.Category.Movies:
                    MoviesIsSelected = true;
                    user.GameProperty.WordToGuess = words.Movies[random.Next(words.Movies.Count)];
                    break;
                case Models.Category.States:
                    StatesIsSelected = true;
                    user.GameProperty.WordToGuess = words.States[random.Next(words.States.Count)];
                    break;
                case Models.Category.Mountains:
                    MountainsIsSelected = true;
                    user.GameProperty.WordToGuess = words.Mountains[random.Next(words.Mountains.Count)];
                    break;
                case Models.Category.Rivers:
                    RiversIsSelected = true;
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

        public Category CategoryLabel
        {
            get
            {
                return user.GameProperty.CategoryProperty;
            }
            set
            {
                user.GameProperty.CategoryProperty = value;
                NotifyPropertyChanged("CategoryLabel");
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
                if (user.GameProperty.LevelProperty >= 5)
                {
                    switch (user.GameProperty.CategoryProperty)
                    {
                        case Category.All:
                            user.StatisticsProperty.WonGamesAll += 1;
                            break;
                        case Category.Cars:
                            user.StatisticsProperty.WonGamesCars += 1;
                            break;
                        case Category.Movies:
                            user.StatisticsProperty.WonGamesMovies += 1;
                            break;
                        case Category.States:
                            user.StatisticsProperty.WonGamesStates += 1;
                            break;
                        case Category.Mountains:
                            user.StatisticsProperty.WonGamesMountains += 1;
                            break;
                        case Category.Rivers:
                            user.StatisticsProperty.WonGamesRivers += 1;
                            break;
                        default:
                            break;
                    }
                    foreach(var userInList in users.List)
                    {
                        if (userInList.Name == user.Name)
                        {
                            userInList.StatisticsProperty = user.StatisticsProperty;
                        }
                    }
                    serializationActions.SerializeUsers(Constants.UsersFile, users);
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
                switch (user.GameProperty.CategoryProperty)
                {
                    case Category.All:
                        user.StatisticsProperty.LostGamesAll += 1;
                        break;
                    case Category.Cars:
                        user.StatisticsProperty.LostGamesCars += 1;
                        break;
                    case Category.Movies:
                        user.StatisticsProperty.LostGamesMovies += 1;
                        break;
                    case Category.States:
                        user.StatisticsProperty.LostGamesStates += 1;
                        break;
                    case Category.Mountains:
                        user.StatisticsProperty.LostGamesMountains += 1;
                        break;
                    case Category.Rivers:
                        user.StatisticsProperty.LostGamesRivers += 1;
                        break;
                    default:
                        break;
                }
                foreach (var userInList in users.List)
                {
                    if (userInList.Name == user.Name)
                    {
                        userInList.StatisticsProperty = user.StatisticsProperty;
                    }
                }
                serializationActions.SerializeUsers(Constants.UsersFile, users);
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
            delay = 61;
            user.GameProperty.MistakesProperty = 0;
            HangImageSource = images.Hangs[user.GameProperty.MistakesProperty];
            StartTimer(delay);
            Level = user.GameProperty.LevelProperty;
        }

        private void ShowMessageBox(string title, string details, MessageBoxImage messageBoxImage)
        {
            StopTimer();
            MessageBoxResult messageBoxResult = MessageBox.Show(details, title, MessageBoxButton.YesNo, messageBoxImage);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Level = user.GameProperty.LevelProperty = 1;
                ReloadGame();
            }
            else
            {
                user.GameProperty.MistakesProperty = 0;
                user.GameProperty.LevelProperty = 1;
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
            switch (user.GameProperty.CategoryProperty)
            {
                case Category.All:
                    user.StatisticsProperty.LostGamesAll += 1;
                    break;                  
                case Category.Cars:         
                    user.StatisticsProperty.LostGamesCars += 1;
                    break;                  
                case Category.Movies:       
                    user.StatisticsProperty.LostGamesMovies += 1;
                    break;                  
                case Category.States:       
                    user.StatisticsProperty.LostGamesStates += 1;
                    break;                  
                case Category.Mountains:    
                    user.StatisticsProperty.LostGamesMountains += 1;
                    break;                  
                case Category.Rivers:       
                    user.StatisticsProperty.LostGamesRivers += 1;
                    break;
                default:
                    break;
            }
            foreach (var userInList in users.List)
            {
                if (userInList.Name == user.Name)
                {
                    userInList.StatisticsProperty = user.StatisticsProperty;
                    if (resumeGame)
                    {
                        userInList.GameProperty = new Game();
                    }
                }
            }
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            ShowMessageBox("Time has expired", "Time has expired!\nDo you want to start a new game on the same category?", MessageBoxImage.Warning);
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(SaveGame);
                }
                return saveCommand;
            }
        }

        public void SaveGame(object param)
        {
            StopTimer();
            user.GameProperty.SecondsRemaining = (deadline - DateTime.Now).Seconds;
            users = serializationActions.DeserializeUsers(Constants.UsersFile);
            foreach (var userInList in users.List)
            {
                if (user.Name == userInList.Name)
                {
                    users.List.Remove(userInList);
                    break;
                }
            }
            user.GameProperty.SavedGame = true;
            users.List.Add(user);
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            SignInWindow signInWindow = new SignInWindow();
            SignInViewModel signInVM = new SignInViewModel();
            signInWindow.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = signInWindow;
            signInWindow.Show();
        }

        private ICommand aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                {
                    aboutCommand = new RelayCommand(About);
                }
                return aboutCommand;
            }
        }

        public void About(object param)
        {
            int seconds = (deadline - DateTime.Now).Seconds;
            StopTimer();
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
            StartTimer(seconds);
        }

        private Category oldCategory = Category.None;

        private ICommand selectCategoryCommand;
        public ICommand SelectCategoryCommand
        {
            get
            {
                if (selectCategoryCommand == null)
                {
                    if (AllIsSelected)
                    {
                        oldCategory = Category.All;
                    }
                    if (CarsIsSelected)
                    {
                        oldCategory = Category.Cars;
                    }
                    if (MountainsIsSelected)
                    {
                        oldCategory = Category.Mountains;
                    }
                    if (MoviesIsSelected)
                    {
                        oldCategory = Category.Movies;
                    }
                    if (RiversIsSelected)
                    {
                        oldCategory = Category.Rivers;
                    }
                    if (StatesIsSelected)
                    {
                        oldCategory = Category.States;
                    }
                    selectCategoryCommand = new RelayCommand(SelectCategory);
                }
                return selectCategoryCommand;
            }
        }

        private ICommand statisticsCommand;
        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommand == null)
                {
                    statisticsCommand = new RelayCommand(StatisticsPressed);
                }
                return statisticsCommand;
            }
        }

        public void StatisticsPressed(object param)
        {
            int seconds = (deadline - DateTime.Now).Seconds;
            StopTimer();
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            StatisticsViewModel statisticsVM = new StatisticsViewModel(user);
            statisticsWindow.DataContext = statisticsVM;
            statisticsWindow.ShowDialog();
            StartTimer(seconds);
        }

        private ICommand newCommand;
        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(NewPressed);
                }
                return newCommand;
            }
        }

        public void NewPressed(object param)
        {
            int seconds = (deadline - DateTime.Now).Seconds;
            StopTimer();
            MessageBoxResult messageBoxResult = MessageBox.Show("If you start a new game this game will count as lost.\nAre you sure you want to start a new game?", "New game", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                user.GameProperty.MistakesProperty = 0;
                user.GameProperty.LevelProperty = 1;
                switch (user.GameProperty.CategoryProperty)
                {
                    case Category.All:
                        user.StatisticsProperty.LostGamesAll += 1;
                        break;
                    case Category.Cars:
                        user.StatisticsProperty.LostGamesCars += 1;
                        break;
                    case Category.Movies:
                        user.StatisticsProperty.LostGamesMovies += 1;
                        break;
                    case Category.States:
                        user.StatisticsProperty.LostGamesStates += 1;
                        break;
                    case Category.Mountains:
                        user.StatisticsProperty.LostGamesMountains += 1;
                        break;
                    case Category.Rivers:
                        user.StatisticsProperty.LostGamesRivers += 1;
                        break;
                    default:
                        break;
                }
                foreach (var userInList in users.List)
                {
                    if (userInList.Name == user.Name)
                    {
                        userInList.StatisticsProperty = user.StatisticsProperty;
                        if (resumeGame)
                        {
                            userInList.GameProperty = new Game();
                        }
                    }
                }
                serializationActions.SerializeUsers(Constants.UsersFile, users);
                CategoryWindow categoryWindow = new CategoryWindow();
                CategoryViewModel categoryVM = new CategoryViewModel(user);
                categoryWindow.DataContext = categoryVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = categoryWindow;
                categoryWindow.Show();
            }
            else
            {
                StartTimer(seconds);
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(ExitPressed);
                }
                return exitCommand;
            }
        }

        public void ExitPressed(object param)
        {
            //int seconds = (deadline - DateTime.Now).Seconds;
            StopTimer();
            //MessageBoxResult messageBoxResult = MessageBox.Show("If you exit this game will count as lost.\nAre you sure you want to exit?", "Exit game", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (messageBoxResult == MessageBoxResult.Yes)
            //{
                user.GameProperty.MistakesProperty = 0;
                user.GameProperty.LevelProperty = 1;
                //switch (user.GameProperty.CategoryProperty)
                //{
                //    case Category.All:
                //        user.StatisticsProperty.LostGamesAll += 1;
                //        break;
                //    case Category.Cars:
                //        user.StatisticsProperty.LostGamesCars += 1;
                //        break;
                //    case Category.Movies:
                //        user.StatisticsProperty.LostGamesMovies += 1;
                //        break;
                //    case Category.States:
                //        user.StatisticsProperty.LostGamesStates += 1;
                //        break;
                //    case Category.Mountains:
                //        user.StatisticsProperty.LostGamesMountains += 1;
                //        break;
                //    case Category.Rivers:
                //        user.StatisticsProperty.LostGamesRivers += 1;
                //        break;
                //    default:
                //        break;
                //}
                foreach (var userInList in users.List)
                {
                    if (userInList.Name == user.Name)
                    {
                        userInList.StatisticsProperty = user.StatisticsProperty;
                        if (resumeGame)
                        {
                            userInList.GameProperty = new Game();
                        }
                    }
                }
                serializationActions.SerializeUsers(Constants.UsersFile, users);
                SignInWindow signInWindow = new SignInWindow();
                SignInViewModel signInVM = new SignInViewModel(users);
                signInWindow.DataContext = signInVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = signInWindow;
                signInWindow.Show();
            //}
            //else
            //{
                //StartTimer(seconds);
            //}
        }

        public void ResetButtons()
        {
            foreach(var button in buttons)
            {
                button.Foreground = Brushes.White;
                button.IsEnabled = true;
            }
        }

        private ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
        public void SelectCategory(object menuItem)
        {
            if ((menuItem as MenuItem).Header.ToString() != CategoryLabel.ToString())
            {
                int seconds = (deadline - DateTime.Now).Seconds;
                StopTimer();
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to change the category?\nIf you change it you will lose this game!", "Change category?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    switch (oldCategory)
                    {
                        case Category.All:
                            user.StatisticsProperty.LostGamesAll += 1;
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.All;
                            break;
                        case Category.Cars:
                            user.StatisticsProperty.LostGamesCars += 1;
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Cars;
                            break;
                        case Category.Movies:
                            user.StatisticsProperty.LostGamesMovies += 1;
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Movies;
                            break;
                        case Category.States:
                            user.StatisticsProperty.LostGamesStates += 1;
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.States;
                            break;
                        case Category.Mountains:
                            user.StatisticsProperty.LostGamesMountains += 1;
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Mountains;
                            break;
                        case Category.Rivers:
                            user.StatisticsProperty.LostGamesRivers += 1;
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Rivers;
                            break;
                        default:
                            break;
                    }
                    switch ((menuItem as MenuItem).Header)
                    {
                        case "All":
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.All;
                            AllIsSelected = true;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case "Cars":
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Cars;
                            AllIsSelected = false;
                            CarsIsSelected = true;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case "Mountains":
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Mountains;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = true;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case "Movies":
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Movies;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = true;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case "Rivers":
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Rivers;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = true;
                            StatesIsSelected = false;
                            break;
                        case "States":
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.States;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = true;
                            break;
                        default:
                            break;
                    }
                    user.GameProperty.LevelProperty = 1;
                    user.GameProperty.MistakesProperty = 0;
                    foreach (var userInList in users.List)
                    {
                        if (userInList.Name == user.Name)
                        {
                            userInList.StatisticsProperty = user.StatisticsProperty;
                            if (resumeGame)
                            {
                                userInList.GameProperty = new Game();
                                userInList.GameProperty.CategoryProperty = user.GameProperty.CategoryProperty;
                            }
                        }
                    }
                    serializationActions.SerializeUsers(Constants.UsersFile, users);
                    ResetButtons();
                    HomeViewModel homeViewModel = new HomeViewModel(user);
                    App.Current.MainWindow.DataContext = homeViewModel;
                }
                else
                {
                    switch (oldCategory)
                    {
                        case Category.All:
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.All;
                            AllIsSelected = true;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case Category.Cars:
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Cars;
                            AllIsSelected = false;
                            CarsIsSelected = true;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case Category.Movies:
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Movies;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = true;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case Category.States:
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.States;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = false;
                            StatesIsSelected = true;
                            break;
                        case Category.Mountains:
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Mountains;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = true;
                            RiversIsSelected = false;
                            StatesIsSelected = false;
                            break;
                        case Category.Rivers:
                            CategoryLabel = user.GameProperty.CategoryProperty = Category.Rivers;
                            AllIsSelected = false;
                            CarsIsSelected = false;
                            MoviesIsSelected = false;
                            MountainsIsSelected = false;
                            RiversIsSelected = true;
                            StatesIsSelected = false;
                            break;
                        default:
                            break;
                    }
                    StartTimer(seconds);
                }
            }
            else
            {
                switch (oldCategory)
                {
                    case Category.All:
                        CategoryLabel = user.GameProperty.CategoryProperty = Category.All;
                        AllIsSelected = true;
                        CarsIsSelected = false;
                        MoviesIsSelected = false;
                        MountainsIsSelected = false;
                        RiversIsSelected = false;
                        StatesIsSelected = false;
                        break;
                    case Category.Cars:
                        CategoryLabel = user.GameProperty.CategoryProperty = Category.Cars;
                        AllIsSelected = false;
                        CarsIsSelected = true;
                        MoviesIsSelected = false;
                        MountainsIsSelected = false;
                        RiversIsSelected = false;
                        StatesIsSelected = false;
                        break;
                    case Category.Movies:
                        CategoryLabel = user.GameProperty.CategoryProperty = Category.Movies;
                        AllIsSelected = false;
                        CarsIsSelected = false;
                        MoviesIsSelected = true;
                        MountainsIsSelected = false;
                        RiversIsSelected = false;
                        StatesIsSelected = false;
                        break;
                    case Category.States:
                        CategoryLabel = user.GameProperty.CategoryProperty = Category.States;
                        AllIsSelected = false;
                        CarsIsSelected = false;
                        MoviesIsSelected = false;
                        MountainsIsSelected = false;
                        RiversIsSelected = false;
                        StatesIsSelected = true;
                        break;
                    case Category.Mountains:
                        CategoryLabel = user.GameProperty.CategoryProperty = Category.Mountains;
                        AllIsSelected = false;
                        CarsIsSelected = false;
                        MoviesIsSelected = false;
                        MountainsIsSelected = true;
                        RiversIsSelected = false;
                        StatesIsSelected = false;
                        break;
                    case Category.Rivers:
                        CategoryLabel = user.GameProperty.CategoryProperty = Category.Rivers;
                        AllIsSelected = false;
                        CarsIsSelected = false;
                        MoviesIsSelected = false;
                        MountainsIsSelected = false;
                        RiversIsSelected = true;
                        StatesIsSelected = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool allIsSelected;
        public bool AllIsSelected
        {
            get
            {
                return allIsSelected;
            }
            set
            {
                allIsSelected = value;
                NotifyPropertyChanged("AllIsSelected");
            }
        }

        private bool carsIsSelected;
        public bool CarsIsSelected
        {
            get
            {
                return carsIsSelected;
            }
            set
            {
                carsIsSelected = value;
                NotifyPropertyChanged("CarsIsSelected");
            }
        }

        private bool mountainsIsSelected;
        public bool MountainsIsSelected
        {
            get
            {
                return mountainsIsSelected;
            }
            set
            {
                mountainsIsSelected = value;
                NotifyPropertyChanged("MountainsIsSelected");
            }
        }

        private bool moviesIsSelected;
        public bool MoviesIsSelected
        {
            get
            {
                return moviesIsSelected;
            }
            set
            {
                moviesIsSelected = value;
                NotifyPropertyChanged("MoviesIsSelected");
            }
        }

        private bool riversIsSelected;
        public bool RiversIsSelected
        {
            get
            {
                return riversIsSelected;
            }
            set
            {
                riversIsSelected = value;
                NotifyPropertyChanged("RiversIsSelected");
            }
        }

        private bool statesIsSelected;
        public bool StatesIsSelected
        {
            get
            {
                return statesIsSelected;
            }
            set
            {
                statesIsSelected = value;
                NotifyPropertyChanged("StatesIsSelected");
            }
        }

        private void StartTimer(int seconds)
        {
            deadline = DateTime.Now.AddSeconds(seconds);
            timer.Start();
        }
        private void StopTimer()
        {
            timer.Stop();
            delay = (deadline - DateTime.Now).Seconds;
            deadline = DateTime.Now.AddSeconds(delay);
        }
    }
}
