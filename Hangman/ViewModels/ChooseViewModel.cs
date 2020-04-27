using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Hangman.Models;
using Hangman.Services;
using Hangman.Views;

namespace Hangman.ViewModels
{
    public class ChooseViewModel : NotifyViewModel
    {
        private SignInViewModel signInVM = new SignInViewModel();
        private SerializationActions serializationActions = new SerializationActions();
        private Images images = new Images();
        private User user;

        public ChooseViewModel()
        {
            signInVM = new SignInViewModel(serializationActions.DeserializeSignInVM("Users.xml"));
        }

        public ChooseViewModel(User user)
        {
            this.user = user;
            signInVM = new SignInViewModel(serializationActions.DeserializeSignInVM("Users.xml"));
            foreach (var userFromList in signInVM.UsersList)
            {
                if (userFromList.Name == userFromList.Name)
                {
                    if (user.GameProperty != null)
                    {
                        CanExecuteCommand = true;
                    }
                }
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
                return images.ImagesList[user.ImageIndex];
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
            user.GameProperty.LevelProperty = 0;
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
            HomeViewModel homeVM = new HomeViewModel(user);
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
            SignInWindow window = new SignInWindow();
            //signInVM = new SignInViewModel(serializationActions.DeserializeSignInVM("Users.xml"));
            window.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }
    }
}
