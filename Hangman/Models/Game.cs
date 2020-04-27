using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hangman.Models
{
    public enum Category
    {
        All,
        Cars,
        Movies,
        States,
        Mountains,
        Rivers
    }

    [Serializable]
    public class Game : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        [XmlAttribute]
        private string wordOnDisplay;
        [XmlAttribute]
        private string wordToGuess;
        [XmlAttribute]
        private int level;
        [XmlAttribute]
        private Category category;

        public int LevelProperty
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                NotifyPropertyChanged("Level");
            }
        }

        public Category CategoryProperty
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                NotifyPropertyChanged("Category");
            }
        }

        public string WordOnDisplay
        {
            get
            {
                return wordOnDisplay;
            }
            set
            {
                wordOnDisplay = value;
                NotifyPropertyChanged("WordOnDisplay");
            }
        }

        public string WordToGuess
        {
            get
            {
                return wordToGuess;
            }
            set
            {
                wordToGuess = value;
                NotifyPropertyChanged("WordToGuess");
            }
        }
    }
}
