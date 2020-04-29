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
        None,
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
        private int mistakes;
        [XmlAttribute]
        private Category category;

        public Game()
        {
            wordOnDisplay = "";
            wordToGuess = "";
            level = 1;
            mistakes = 0;
            category = Category.All;
        }

        public int LevelProperty
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                NotifyPropertyChanged("LevelProperty");
            }
        }        
        
        public int MistakesProperty
        {
            get
            {
                return mistakes;
            }
            set
            {
                mistakes = value;
                NotifyPropertyChanged("MistakesProperty");
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
                NotifyPropertyChanged("CategoryProperty");
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
