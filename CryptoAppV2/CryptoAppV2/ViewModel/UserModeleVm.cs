using CryptoAppV2.Data;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CryptoAppV2.ViewModel
{
    public class UserModeleVm
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UserModele> Modeles { get; private set; }

        public UserModeleManager manager = App.UserModeleManager;

        public UserModeleVm()
        {
            var dos =  manager.GetAll();
            Modeles = new ObservableCollection<UserModele>(dos.Result);
        }


    }
}
