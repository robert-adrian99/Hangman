using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Hangman.Models
{
    [Serializable]
    public class Users
    {
        [XmlArray]
        public ObservableCollection<User> List { get; set; } = new ObservableCollection<User>();
    }
}
