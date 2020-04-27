using Hangman.Models;
using Hangman.Services;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class StartViewModel : NotifyViewModel
    {
        public StartViewModel()
        {
            try
            {
                FileStream file = new FileStream("Users.xml", FileMode.Open);
                file.Dispose();
                CanExecuteCommandSignIn = true;
            }
            catch (FileNotFoundException)
            {
                CanExecuteCommandSignIn = false;
                //return;
            }
        }

        SerializationActions actions = new SerializationActions();

        public bool CanExecuteCommandSignIn { get; set; } = false;

        private ICommand signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new RelayCommand(SignIn, param => CanExecuteCommandSignIn);
                }
                return signInCommand;
            }
        }

        public void SignIn(object param)
        {
            SignInWindow window = new SignInWindow();
            SignInViewModel signInVM = new SignInViewModel(actions.DeserializeSignInVM("Users.xml"));
            window.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }

        private ICommand signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                if (signUpCommand == null)
                {
                    signUpCommand = new RelayCommand(SignUp);
                }
                return signUpCommand;
            }
        }

        public void SignUp(object param)
        {
            SignUpWindow window = new SignUpWindow();
            SignUpViewModel signUpVM = new SignUpViewModel();
            window.DataContext = signUpVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(Exit);
                }
                return exitCommand;
            }
        }

        public void Exit(object param)
        {
            App.Current.MainWindow.Close();
            App.Current.Shutdown();
        }
    }
}
