using CryptoAppV2.CrAlgorithme;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CryptoAppV2.ViewModel
{
    public class EtapeViewModem : INotifyPropertyChanged
    {
        CryptoEtape CryptoEtape = new CryptoEtape();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Etape> Etapes { get; set; }
        public EtapeViewModem()
        {
            Etapes = new ObservableCollection<Etape>();
        }
        public EtapeViewModem(string etape, string data)
        {
          


        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
