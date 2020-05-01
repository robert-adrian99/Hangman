using Hangman.Models;
using Hangman.Helps;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hangman.ViewModels
{
    public class SignInViewModel : NotifyViewModel
    {
        private SerializationActions serializationActions = new SerializationActions();
        private Images images = new Images();
        private Users users = new Users();

        public SignInViewModel()
        {

            this.users = serializationActions.DeserializeUsers(Constants.UsersFile);
            ImageSource = users.List.Count > 0 ? new BitmapImage(new Uri(images.Emojis.ElementAt(users.List[0].ImageIndex).UriSource.ToString(), UriKind.Relative)) : new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_1.png", UriKind.Relative));
        }

        public SignInViewModel(Users users)
        {
            this.users = users;
            serializationActions.SerializeUsers(Constants.UsersFile, users);
            ImageSource = users.List.Count > 0 ? new BitmapImage(new Uri(images.Emojis.ElementAt(users.List[0].ImageIndex).UriSource.ToString(), UriKind.Relative)) : new BitmapImage(new Uri(@"/Assets/Emojis/Emoji_1.png", UriKind.Relative));
        }

        public ObservableCollection<User> Users
        {
            get
            {
                return users.List;
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
                NotifyPropertyChanged("ImageSource");
            }
        }

        private User selectedUser;
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
                    CanExecuteCommands = true;
                }
                NotifyPropertyChanged("SelectUser");
            }
        }

        public void ChangeImage(User user)
        {
            ImageSource = images.Emojis[user.ImageIndex];
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(Add);
                }
                return addCommand;
            }
        }

        public void Add(object parameter)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            SignUpViewModel signUpVM = new SignUpViewModel(users);
            signUpWindow.DataContext = signUpVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = signUpWindow;
            signUpWindow.Show();
        }

        public bool CanExecuteCommands { get; set; } = false;

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(Edit, param => CanExecuteCommands);
                }
                return editCommand;
            }
        }

        public void Edit(object param)
        {
            SignUpWindow SignUpWindow = new SignUpWindow();
            SignUpViewModel signUpVM = new SignUpViewModel(SelectedUser, users);
            SignUpWindow.DataContext = signUpVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = SignUpWindow;
            SignUpWindow.Show();
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(Delete, param => CanExecuteCommands);
                }
                return deleteCommand;
            }
        }

        public void Delete(object parameter)
        {
            foreach (var user in users.List)
            {
                if (user.Name == SelectedUser.Name)
                {
                    users.List.Remove(user);
                    NotifyPropertyChanged("UsersList");
                    break;
                }
            }
            SelectedUser = null;
            CanExecuteCommands = false;
            serializationActions.SerializeUsers(Constants.UsersFile, users);
        }


        private ICommand playCommand;
        public ICommand PlayCommand
        {
            get
            {
                if (playCommand == null)
                {
                    playCommand = new RelayCommand(Play, param => CanExecuteCommands);
                }
                return playCommand;
            }
        }

        public void Play(object param)
        {
            if (SelectedUser.GameProperty == null)
            {
                SelectedUser.GameProperty = new Game();
            }
            ChooseWindow chooseWindow = new ChooseWindow();
            ChooseViewModel chooseVM = new ChooseViewModel(SelectedUser);
            chooseWindow.DataContext = chooseVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = chooseWindow;
            chooseWindow.Show();
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
            StartWindow startWindow = new StartWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = startWindow;
            startWindow.Show();
        }
    }
}
