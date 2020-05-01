using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hangman.ViewModels
{
    public class StatisticsViewModel
    {
        private User user;
        private Images images = new Images();
        public StatisticsViewModel(User user)
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

        public BitmapImage UserImageSource
        {
            get
            {
                return images.Emojis.ElementAt(user.ImageIndex);
            }
        }

        public int WonGamesAll
        {
            get
            {
                return user.StatisticsProperty.WonGamesAll;
            }
        }

        public int LostGamesAll
        {
            get
            {
                return user.StatisticsProperty.LostGamesAll;
            }
        }

        public int WonGamesCars
        {
            get
            {
                return user.StatisticsProperty.WonGamesCars;
            }
        }

        public int LostGamesCars
        {
            get
            {
                return user.StatisticsProperty.LostGamesCars;
            }
        }

        public int WonGamesMountains
        {
            get
            {
                return user.StatisticsProperty.WonGamesMountains;
            }
        }

        public int LostGamesMountains
        {
            get
            {
                return user.StatisticsProperty.LostGamesMountains;
            }
        }

        public int WonGamesMovies
        {
            get
            {
                return user.StatisticsProperty.WonGamesMovies;
            }
        }

        public int LostGamesMovies
        {
            get
            {
                return user.StatisticsProperty.LostGamesMovies;
            }
        }

        public int WonGamesRivers
        {
            get
            {
                return user.StatisticsProperty.WonGamesRivers;
            }
        }

        public int LostGamesRivers
        {
            get
            {
                return user.StatisticsProperty.LostGamesRivers;
            }
        }

        public int WonGamesStates
        {
            get
            {
                return user.StatisticsProperty.WonGamesStates;
            }
        }

        public int LostGamesStates
        {
            get
            {
                return user.StatisticsProperty.LostGamesStates;
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
