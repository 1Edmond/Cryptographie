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
