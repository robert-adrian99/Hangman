using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Hangman.ViewModels
{
    [Serializable]
    public class SignInViewModel
    {

        public SignInViewModel()
        {
            UsersList = new ObservableCollection<User>();
        }
        
        [XmlArray]
        public ObservableCollection<User> UsersList { get; set; }

        private int indexSelected = 0;
        public int IndexSelected
        {
            get
            {
                return indexSelected;
            }
        }

        private bool canExecuteCommand = false;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommand;
            }

            set
            {
                if (canExecuteCommand == value)
                {
                    return;
                }
                canExecuteCommand = value;
            }
        }

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
            //Console.WriteLine(this.UsersList);
            //Console.WriteLine(IndexSelected);
            Console.WriteLine("Am sters!");
        }

        public void SerializeSignUp()
        {

        }
    }
}
