using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman.Models;

namespace Hangman.ViewModels
{
    public class HomeViewModel : NotifyViewModel
    {
        private User user;
        public HomeViewModel()
        {
            this.user = new User();
        }
        public HomeViewModel(User user)
        {
            this.user = user;
        }

        public string Name
        {
            get
            {
                return user.Name;
            }
        }

        public string Category
        {
            get
            {
                return user.GameProperty.CategoryProperty.ToString();
            }
        }

        public string Level
        {
            get
            {
                return user.GameProperty.LevelProperty.ToString();
            }
        }
    }
}
