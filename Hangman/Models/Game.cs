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
        [XmlAttribute]
        private int secondsRemaining;
        [XmlAttribute]
        private bool savedGame;

        public Game()
        {
            WordOnDisplay = "";
            WordToGuess = "";
            LevelProperty = 1;
            MistakesProperty = 0;
            CategoryProperty = Category.None;
            SecondsRemaining = 0;
            savedGame = false;
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

        public int SecondsRemaining
        {
            get
            {
                return secondsRemaining;
            }
            set
            {
                secondsRemaining = value;
                NotifyPropertyChanged("SecondsRemaining");
            }
        }

        public bool SavedGame
        {
            get
            {
                return savedGame;
            }
            set
            {
                savedGame = value;
                NotifyPropertyChanged("SavedGame");
            }
        }
    }
}
