using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Hangman.Models;
using System.Windows.Input;
using Hangman.Services;
using Hangman.Views;

namespace Hangman.ViewModels
{
    public class CategoryViewModel : NotifyViewModel
    {
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

        public CategoryViewModel(User user)
        {
            this.user = user;
            Categories = new ObservableCollection<Category>();
            Categories.Add(Category.All);
            Categories.Add(Category.Cars);
            Categories.Add(Category.Mountains);
            Categories.Add(Category.Movies);
            Categories.Add(Category.Rivers);
            Categories.Add(Category.States);
        }

        private Category selectedCategory;
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
                NotifyPropertyChanged("SelectCategory");
            }
        }

        public void CategorySelected(Category category)
        {
            user.GameProperty.CategoryProperty = category;
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
            ChooseWindow window = new ChooseWindow();
            ChooseViewModel chooseVM = new ChooseViewModel(user);
            window.DataContext = chooseVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            window.Show();
        }
    }
}
