using Hangman.Models;
using Hangman.Helps;
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
                FileStream file = new FileStream(Constants.UsersFile, FileMode.Open);
                file.Dispose();
                CanExecuteCommandSignIn = true;
            }
            catch (FileNotFoundException)
            {
                CanExecuteCommandSignIn = false;
            }

            try
            {
                FileStream file = new FileStream(Constants.WordsFile, FileMode.Open);
                file.Dispose();
            }
            catch(FileNotFoundException)
            {
                Words words = new Words();
                words.AddWords();
                SerializationActions actions = new SerializationActions();
                actions.SerializeWords(Constants.WordsFile, words);
            }

            //try
            //{
            //    FileStream file = new FileStream("Images.xml", FileMode.Open);
            //    file.Dispose();
            //}
            //catch (FileNotFoundException)
            //{
            //    Images images = new Images();
            //    images.AddImages();
            //    SerializationActions actions = new SerializationActions();
            //    actions.SerializeImages("Images.xml", images);
            //}
        }

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
            SignInWindow signInWindow = new SignInWindow();
            SignInViewModel signInVM = new SignInViewModel();
            signInWindow.DataContext = signInVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = signInWindow;
            signInWindow.Show();
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
