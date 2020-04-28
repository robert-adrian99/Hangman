using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Hangman.Models
{
    [Serializable]
    public class Words
    {
        [XmlArray]
        public ObservableCollection<string> Cars { get; set; }
        [XmlArray]
        public ObservableCollection<string> Mountains { get; set; }
        [XmlArray]
        public ObservableCollection<string> Movies { get; set; }
        [XmlArray]
        public ObservableCollection<string> Rivers { get; set; }
        [XmlArray]
        public ObservableCollection<string> States { get; set; }

        public Words()
        {
            Cars = new ObservableCollection<string>();
            Mountains = new ObservableCollection<string>();
            Movies = new ObservableCollection<string>();
            Rivers = new ObservableCollection<string>();
            States = new ObservableCollection<string>();
        }

        public void AddWords()
        {
            Cars = new ObservableCollection<string>();
            Cars.Add("FORD");
            Cars.Add("MERCEDES");
            Cars.Add("AUDI");
            Cars.Add("OPEL");
            Cars.Add("DACIA");
            Cars.Add("VOLKSWAGEN");
            Cars.Add("FERRARI");
            Cars.Add("HONDA");
            Cars.Add("SUBARU");
            Cars.Add("TOYOTA");
            Cars.Add("HYUNDAI");
            Cars.Add("RENAULT");
            Cars.Add("RANGE ROVER");
            Cars.Add("LAND ROVER");
            Cars.Add("PEUGEOUT");
            Cars.Add("ASTON MARTIN");
            Cars.Add("LAMBORGHINI");
            Cars.Add("BUGATTI");
            Cars.Add("BENTLEY");
            Cars.Add("PORSCHE");

            Mountains = new ObservableCollection<string>();
            Mountains.Add("HIMALAYAS");
            Mountains.Add("ROCKY");
            Mountains.Add("SIERRA NEVADA");
            Mountains.Add("CARPATHIAN");
            Mountains.Add("ALPS");
            Mountains.Add("PYRENEES");
            Mountains.Add("APPENNINE");
            Mountains.Add("URAL");
            Mountains.Add("BALKAN");
            Mountains.Add("CAUCASUS");
            Mountains.Add("SCANDINAVIAN");
            Mountains.Add("PENNINES");
            Mountains.Add("FUJI");
            Mountains.Add("KILIMANJARO");
            Mountains.Add("MONT BLANC");
            Mountains.Add("OLYMPUS");
            Mountains.Add("EVEREST");
            Mountains.Add("APUSENI");
            Mountains.Add("FAGARAS");
            Mountains.Add("BUCEGI");

            Movies = new ObservableCollection<string>();
            Movies.Add("VIKINGS");
            Movies.Add("PEAKY BLINDERS");
            Movies.Add("LUCIFER");
            Movies.Add("LA CASA DE PAPEL");
            Movies.Add("CHERNOBYL");
            Movies.Add("FRIENDS");
            Movies.Add("FROZEN");
            Movies.Add("SUICIDE SQUAD");
            Movies.Add("BATMAN");
            Movies.Add("SUPERMAN");
            Movies.Add("SPIERDMAN");
            Movies.Add("LION KING");
            Movies.Add("JUMANJI");
            Movies.Add("RIVERDALE");
            Movies.Add("BREAKING BAD");
            Movies.Add("AVANGERS");
            Movies.Add("MIAMI BICI");
            Movies.Add("MIRAGE");
            Movies.Add("STAR WARS");
            Movies.Add("STAR TREK");

            Rivers = new ObservableCollection<string>();
            Rivers.Add("DANUBE");
            Rivers.Add("VOLGA");
            Rivers.Add("NILE");
            Rivers.Add("THAMES");
            Rivers.Add("SENA");
            Rivers.Add("PRUT");
            Rivers.Add("MURES");
            Rivers.Add("OLT");
            Rivers.Add("TIBER");
            Rivers.Add("AMAZON");
            Rivers.Add("TIMIS");
            Rivers.Add("MISSISSIPPI");
            Rivers.Add("MISSOURI");
            Rivers.Add("SEINE");
            Rivers.Add("RHINE");
            Rivers.Add("ELBE");
            Rivers.Add("MAIN");
            Rivers.Add("GANGES");
            Rivers.Add("BRAHMAPUTRA");
            Rivers.Add("NIAGARA");

            States = new ObservableCollection<string>();
            States.Add("ALABAMA");
            States.Add("ALASKA");
            States.Add("CONNECTICUT");
            States.Add("MASSACHUSETTS");
            States.Add("COLORADO");
            States.Add("WASHINGTON");
            States.Add("CALIFORNIA");
            States.Add("FLORIDA");
            States.Add("HAWAII");
            States.Add("TEXAS");
            States.Add("TENNESSEE");
            States.Add("MICHIGAN");
            States.Add("PENNSYLVANIA");
            States.Add("NEW YORK");
            States.Add("NEW JERSEY");
            States.Add("ARIZONA");
            States.Add("NORTH CAROLINA");
            States.Add("ILLINOIS");
            States.Add("SOUTH CAROLINA");
            States.Add("MINNESOTA");
        }
    }
}
