using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Hangman.Models;
using System.Windows.Input;
using Hangman.Helps;
using Hangman.Views;

namespace Hangman.ViewModels
{
    public class CategoryViewModel : NotifyViewModel
    {
        private bool fromSignUp = false;
        private User user;
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Categories.Add(Category.All);
            Categories.Add(Category.Cars);
            Categories.Add(Category.Mountains);
            Categories.Add(Category.Movies);
            Categories.Add(Category.Rivers);
            Categories.Add(Category.States);
        }

        public CategoryViewModel(User user, bool fromSignUp = false)
        {
            this.user = user;
            Categories = new ObservableCollection<Category>();
            Categories.Add(Category.All);
            Categories.Add(Category.Cars);
            Categories.Add(Category.Mountains);
            Categories.Add(Category.Movies);
            Categories.Add(Category.Rivers);
            Categories.Add(Category.States);
            this.fromSignUp = fromSignUp;
        }

        private Category selectedCategory = Category.None;
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                CategorySelected(selectedCategory);
                NotifyPropertyChanged("SelectedCategory");
            }
        }

        public void CategorySelected(Category category)
        {
            user.GameProperty.CategoryProperty = category;
            user.GameProperty.LevelProperty = 1;
            user.GameProperty.MistakesProperty = 0;
            user.GameProperty.WordOnDisplay = "";
            user.GameProperty.WordToGuess = "";
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
            if(fromSignUp == false)
            {
                ChooseWindow chooseWindow = new ChooseWindow();
                ChooseViewModel chooseVM = new ChooseViewModel(user);
                chooseWindow.DataContext = chooseVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = chooseWindow;
                chooseWindow.Show();
            }
            else
            {
                SignInWindow signInWindow = new SignInWindow();
                SignInViewModel signInVM = new SignInViewModel();
                signInWindow.DataContext = signInVM;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = signInWindow;
                signInWindow.Show();
            }
        }
    }
}
