using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Hangman.Models
{
    public enum TypeGame
    {
        Lost,
        Won
    }

    [Serializable]
    public class Statistics : INotifyPropertyChanged
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
        Dictionary<Category, Dictionary<TypeGame, int>> statsGame;

        public Statistics()
        {
            statsGame = new Dictionary<Category, Dictionary<TypeGame, int>>();
            Dictionary<TypeGame, int> keyValue = new Dictionary<TypeGame, int>();
            keyValue.Add(TypeGame.Won, 0);
            keyValue.Add(TypeGame.Lost, 0);
            statsGame.Add(Category.All, keyValue);
            statsGame.Add(Category.Cars, keyValue);
            statsGame.Add(Category.Mountains, keyValue);
            statsGame.Add(Category.Movies, keyValue);
            statsGame.Add(Category.Rivers, keyValue);
            statsGame.Add(Category.States, keyValue);
        }

        //[XmlAttribute]
        //private int wonGamesAll;
        //[XmlAttribute]
        //private int lostGamesAll;
        //[XmlAttribute]
        //private int wonGamesCars;
        //[XmlAttribute]
        //private int lostGamesCars;
        //[XmlAttribute]
        //private int wonGamesMountains;
        //[XmlAttribute]
        //private int lostGamesMountains;
        //[XmlAttribute]
        //private int wonGamesMovies;
        //[XmlAttribute]
        //private int lostGamesMovies;
        //[XmlAttribute]
        //private int wonGamesRivers;
        //[XmlAttribute]
        //private int lostGamesRivers;        
        //[XmlAttribute]
        //private int wonGamesStates;
        //[XmlAttribute]
        //private int lostGamesStates;

        public int WonGamesAll
        {
            get
            {
                return statsGame[Category.All][TypeGame.Won];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Won, value);
                dict.Add(TypeGame.Lost, LostGamesAll);
                statsGame[Category.All] = dict;
                NotifyPropertyChanged("WonGamesAll");
            }
        }
        
        public int LostGamesAll
        {
            get
            {
                return statsGame[Category.All][TypeGame.Lost];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Lost, value);
                dict.Add(TypeGame.Won, WonGamesAll);
                statsGame[Category.All] = dict;
                NotifyPropertyChanged("LostGamesAll");
            }
        }

        public int WonGamesCars
        {
            get
            {
                return statsGame[Category.Cars][TypeGame.Won];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Won, value);
                dict.Add(TypeGame.Lost, LostGamesCars);
                statsGame[Category.Cars] = dict;
                NotifyPropertyChanged("WonGamesCars");
            }
        }

        public int LostGamesCars
        {
            get
            {
                return statsGame[Category.Cars][TypeGame.Lost];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Lost, value);
                dict.Add(TypeGame.Won, WonGamesCars);
                statsGame[Category.Cars] = dict;
                NotifyPropertyChanged("LostGamesCars");
            }
        }

        public int WonGamesMountains
        {
            get
            {
                return statsGame[Category.Mountains][TypeGame.Won];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Won, value);
                dict.Add(TypeGame.Lost, LostGamesMountains);
                statsGame[Category.Mountains] = dict;
                NotifyPropertyChanged("WonGamesMountains");
            }
        }

        public int LostGamesMountains
        {
            get
            {
                return statsGame[Category.Mountains][TypeGame.Lost];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Lost, value);
                dict.Add(TypeGame.Won, WonGamesMountains);
                statsGame[Category.Mountains] = dict;
                NotifyPropertyChanged("LostGamesMountains");
            }
        }

        public int WonGamesMovies
        {
            get
            {
                return statsGame[Category.Movies][TypeGame.Won];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Won, value);
                dict.Add(TypeGame.Lost, LostGamesMovies);
                statsGame[Category.Movies] = dict;
                NotifyPropertyChanged("WonGamesMovies");
            }
        }

        public int LostGamesMovies
        {
            get
            {
                return statsGame[Category.Movies][TypeGame.Lost];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Lost, value);
                dict.Add(TypeGame.Won, WonGamesMovies);
                statsGame[Category.Movies] = dict;
                NotifyPropertyChanged("LostGamesMovies");
            }
        }

        public int WonGamesRivers
        {
            get
            {
                return statsGame[Category.Rivers][TypeGame.Won];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Won, value);
                dict.Add(TypeGame.Lost, LostGamesRivers);
                statsGame[Category.Rivers] = dict;
                NotifyPropertyChanged("WonGamesRivers");
            }
        }

        public int LostGamesRivers
        {
            get
            {
                return statsGame[Category.Rivers][TypeGame.Lost];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Lost, value);
                dict.Add(TypeGame.Won, WonGamesRivers);
                statsGame[Category.Rivers] = dict;
                NotifyPropertyChanged("LostGamesRivers");
            }
        }

        public int WonGamesStates
        {
            get
            {
                return statsGame[Category.States][TypeGame.Won];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Won, value);
                dict.Add(TypeGame.Lost, LostGamesStates);
                statsGame[Category.States] = dict;
                NotifyPropertyChanged("WonGamesStates");
            }
        }

        public int LostGamesStates
        {
            get
            {
                return statsGame[Category.States][TypeGame.Lost];
            }
            set
            {
                Dictionary<TypeGame, int> dict = new Dictionary<TypeGame, int>();
                dict.Add(TypeGame.Lost, value);
                dict.Add(TypeGame.Won, WonGamesStates);
                statsGame[Category.States] = dict;
                NotifyPropertyChanged("LostGamesStates");
            }
        }

        public int TotalWonGames
        {
            get
            {
                return WonGamesAll + WonGamesCars + WonGamesMountains + WonGamesMovies + WonGamesRivers + WonGamesStates;
            }
        }

        public int TotalLostGames
        {
            get
            {
                return LostGamesAll + LostGamesCars + LostGamesMountains + LostGamesMovies + LostGamesRivers + LostGamesStates;
            }
        }

        public int TotalGames
        {
            get
            {
                return TotalWonGames + TotalLostGames;
            }
        }

    }
}
