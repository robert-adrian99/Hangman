using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using Hangman.Models;
using Hangman.Views;
using Hangman.Services;
using System.Windows;

namespace Hangman.ViewModels
{
    public class SignUpViewModel : NotifyViewModel
    {
        private SerializationActions serializationActions = new SerializationActions();
        private SignInViewModel signInVM = new SignInViewModel();
        private Images images = new Images();

        public SignUpViewModel()
        {
            ImageSource = images.ImagesList.ElementAt(0);

            foreach (var image in images.ImagesList)
            {
                Console.WriteLine(image.UriSource.ToString());
            }

            try
            {
                FileStream fileUsr = new FileStream("Users.xml", FileMode.Open);
                fileUsr.Dispose();
            }
            catch (FileNotFoundException)
            {
                return;
            }

            signInVM = new SignInViewModel(serializationActions.DeserializeSignInVM("Users.xml"));
        }

        private string nameTextBox;

        public string NameTextBox
        {
            get
            {
                return nameTextBox;
            }
            set
            {
                nameTextBox = value;
                CanExecuteCommandPlay = HangmanValidators.CanExecutePlay(NameTextBox);
                NotifyPropertyChanged("NameTextBox");
            }
        }

        private BitmapImage imageSource;
        public BitmapImage ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                CanExecuteCommandNext = HangmanValidators.CanExecuteNext(ImageSource, images);
                CanExecuteCommandPrev = HangmanValidators.CanExecutePrev(ImageSource, images);
                NotifyPropertyChanged("ImageSource");
            }
        }

        public bool CanExecuteCommandPlay { get; set; } = false;
        public bool CanExecuteCommandNext { get; set; } = false;
        public bool CanExecuteCommandPrev { get; set; } = false;

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand(NextMethod, param => CanExecuteCommandNext);
                }
                return nextCommand;
            }
        }

        public void NextMethod(object param)
        {
            int index = images.ImagesList.IndexOf(ImageSource);
            if (index < images.ImagesList.Count - 1)
            {
                ImageSource = images.ImagesList[++index];
            }
        }

        private ICommand prevCommand;
        public ICommand PrevCommand
        {
            get
            {
                if (prevCommand == null)
                {
                    prevCommand = new RelayCommand(PrevMethod, param => CanExecuteCommandPrev);
                }
                return prevCommand;
            }
        }

        public void PrevMethod(object param)
        {
            int index = images.ImagesList.IndexOf(ImageSource);
            if (index > 0)
            {
                ImageSource = images.ImagesList[--index];
            }
        }

        private ICommand signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new RelayCommand(AddUser, param => CanExecuteCommandPlay);
                }
                return signInCommand;
            }
        }

        public void AddUser(object param)
        {
            foreach (var user in signInVM.UsersList)
            {
                if (user.Name == NameTextBox)
                {
                    MessageBox.Show("This nickname is taken.");
                    return;
                }
            }
            int index = images.ImagesList.IndexOf(ImageSource);
            signInVM.UsersList.Add(new User(NameTextBox, index));
            serializationActions.SerializeSignInVM("Users.xml", signInVM);
            SignInWindow window = new SignInWindow();
            window.DataContext = signInVM;
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
            StartWindow window = new StartWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }
    }
}
