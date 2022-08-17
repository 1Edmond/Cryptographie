using CryptoAppV2.Data;
using CryptoAppV2.Model;
using CryptoAppV2.View.HistoriquePages;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    [QueryProperty(nameof(HistoriqueLibelle), nameof(HistoriqueLibelle))]
    public class UserHistoriqueDetailsVm : BaseViewModel
    {
        UserHistoriqueManager UserHistoriqueManager { get; set; }
        public UserHistorique UserHistorique { get; set; }
        public UserHistoriqueDetailsVm()
        {
            UserHistoriqueManager = App.UserHistoriqueManager;
        }
        public async void GetHistorique(string his)
        {
            UserHistorique = await UserHistoriqueManager.GetByLibelle(his);

        }

        public ICommand OnHistoriqueSelected = new Command(async (ord) =>
        {
            if (ord == null)
                return;
            var userHistorique = ord as UserHistorique;
            await Shell.Current.GoToAsync($"{nameof(HistoriqueDetailsPage)}?HistoriqueId={userHistorique.Id}");
        });
        public string HistoriqueLibelle { 
            get => UserHistorique.Libelle;
            set
            {
                UserHistorique.Libelle = value;
                GetHistorique(value);
            }
        }
    }
}
