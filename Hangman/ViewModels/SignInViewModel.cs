using Hangman.Models;
using Hangman.Services;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Hangman.ViewModels
{
    [Serializable]
    public class SignInViewModel : NotifyViewModel
    {
        SerializationActions actions = new SerializationActions();
        private Images images = new Images();
        [XmlArray]
        public ObservableCollection<User> UsersList { get; set; }

        public SignInViewModel()
        {
            UsersList = new ObservableCollection<User>();
            ImageSource = new BitmapImage(new Uri(@"/Assets/Emoji_1.png", UriKind.Relative));
        }

        public SignInViewModel(SignInViewModel signInVM)
        {
            this.UsersList = signInVM.UsersList;
            ImageSource = UsersList.Count > 0 ? new BitmapImage(new Uri(images.ImagesList.ElementAt(UsersList[0].ImageIndex).UriSource.ToString(), UriKind.Relative)) : new BitmapImage(new Uri(@"/Assets/Emoji_1.png", UriKind.Relative));
        }

        private BitmapImage imageSource;

        [XmlIgnore]
        public BitmapImage ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }

        [XmlIgnore]
        public bool CanExecuteCommand { get; set; } = false;

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(Delete, param => CanExecuteCommand);
                }
                return deleteCommand;
            }
        }

        public void Delete(object parameter)
        {
            foreach (var user in UsersList)
            {
                if (user.Name == SelectedUser.Name)
                {
                    UsersList.Remove(user);
                    NotifyPropertyChanged("UsersList");
                    break;
                }
            }
            SelectedUser = null;
            CanExecuteCommand = false;
            actions.SerializeSignInVM("Users.xml", this as SignInViewModel);
        }

        private User selectedUser;
        [XmlIgnore]
        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                if (selectedUser != null)
                {
                    ChangeImage(selectedUser);
                    CanExecuteCommand = true;
                }
                NotifyPropertyChanged("SelectUser");
            }
        }

        public void ChangeImage(User user)
        {
            ImageSource = images.ImagesList[user.ImageIndex];
        }

        private ICommand playCommand;
        public ICommand PlayCommand
        {
            get
            {
                if (playCommand == null)
                {
                    playCommand = new RelayCommand(Play, param => CanExecuteCommand);
                }
                return playCommand;
            }
        }

        public void Play(object param)
        {
            ChooseWindow window = new ChooseWindow();
            ChooseViewModel chooseVM = new ChooseViewModel(SelectedUser);
            window.DataContext = chooseVM;
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
