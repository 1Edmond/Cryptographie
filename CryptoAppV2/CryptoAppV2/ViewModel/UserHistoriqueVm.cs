using CryptoAppV2.Data;
using CryptoAppV2.Model;
using CryptoAppV2.View.HistoriquePages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    [QueryProperty(nameof(HistoriqueLibelle), nameof(HistoriqueLibelle))]
    public class UserHistoriqueVm
    {
        public ObservableCollection<UserHistorique> Historiques { get; private set; }

        public UserHistoriqueManager manager = App.UserHistoriqueManager;
        public UserHistorique UserHistorique { get; set; }
        public string HistoriqueLibelle
        {
            get;set;
        }

        public UserHistoriqueVm()
        {
            manager = new UserHistoriqueManager(App.database);
            var dos = manager.GetAll();
            OnHistoriqueSelected = new Command(async (ord) =>
            {
                if (ord == null)
                    return;
                UserHistorique userHistorique = ord as UserHistorique;
                var url = $"{nameof(HistoriqueDetailsPage)}?HistoriqueId={userHistorique.Id}";
                await Shell.Current.GoToAsync(url);
            });
            Historiques = new ObservableCollection<UserHistorique>(dos);
        }

        public ICommand OnHistoriqueSelected { get; set; }


    }
}
