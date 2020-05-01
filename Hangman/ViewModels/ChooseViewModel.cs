using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Hangman.Models;
using Hangman.Helps;
using Hangman.Views;

namespace Hangman.ViewModels
{
    public class ChooseViewModel : NotifyViewModel
    {
        private SerializationActions serializationActions = new SerializationActions();
        private Images images = new Images();
        //private UsersList users = new UsersList();
        private User user;

        public ChooseViewModel(User user)
        {
            this.user = user;
            if (user.GameProperty.SavedGame)
            {
                CanExecuteCommand = true;
            }
        }

        public string Name
        {
            get
            {
                return user.Name;
            }
        }

        public BitmapImage ImageSource
        {
            get
            {
                return images.Emojis[user.ImageIndex];
            }
        }

        private ICommand newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (newGameCommand == null)
                {
                    newGameCommand = new RelayCommand(NewGame);
                }
                return newGameCommand;
            }
        }

        public void NewGame(object param)
        {
            user.GameProperty = new Game();
            CategoryWindow window = new CategoryWindow();
            CategoryViewModel categoryVM = new CategoryViewModel(user);
            window.DataContext = categoryVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }

        public bool CanExecuteCommand { get; set; }

        private ICommand resumeGameCommand;
        public ICommand ResumeGameCommand
        {
            get
            {
                if (resumeGameCommand == null)
                {
                    resumeGameCommand = new RelayCommand(ResumeGame, param => CanExecuteCommand);
                }
                return resumeGameCommand;
            }
        }

        public void ResumeGame(object param)
        {
            HomeWindow window = new HomeWindow();
            HomeViewModel homeVM = new HomeViewModel(user, true);
            window.DataContext = homeVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new RelayCommand(Back);
                }
                return backCommand;
            }
        }

        public void Back(object param)
        {
            SignInWindow signInWindow = new SignInWindow();
            SignInViewModel signInVM = new SignInViewModel(); // ? deserializare / users
            signInWindow.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = signInWindow;
            signInWindow.Show();
        }
    }
}
