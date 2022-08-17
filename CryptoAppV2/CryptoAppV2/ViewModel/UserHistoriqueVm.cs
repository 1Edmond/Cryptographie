using CryptoAppV2.Data;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CryptoAppV2.ViewModel
{
    public class UserHistoriqueVm
    {
        public ObservableCollection<UserHistorique> Historiques { get; private set; }

        public UserHistoriqueManager manager = App.UserHistoriqueManager;

        public UserHistoriqueVm()
        {
            manager = new UserHistoriqueManager(App.database);
            var dos = manager.GetAll();
            Historiques = new ObservableCollection<UserHistorique>(dos);
        }


    }
}
