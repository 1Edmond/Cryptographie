using CryptoAppV2.Data;
using CryptoAppV2.Model;
using CryptoAppV2.View.HistoriquePages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    [QueryProperty(nameof(HistoriqueLibelle), nameof(HistoriqueLibelle))]
    public class UserHistoriqueVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
            OnHistoriqueDetailsSwipe = new Command(async (ord) =>
            {
                if (ord == null)
                    return;
                UserHistorique userHistorique = ord as UserHistorique;
                var url = $"{nameof(HistoriqueDetailsPage)}?HistoriqueId={userHistorique.Id}";
                await Shell.Current.GoToAsync(url);
            });
            OnHistoriqueDeleteSwipe = new Command(async (ord) =>
            {
                if (ord == null)
                    return;
                UserHistorique userHistorique = ord as UserHistorique;
               await manager.Delete(userHistorique);
                var doss = manager.GetAll();
                Historiques = new ObservableCollection<UserHistorique>(doss);
                OnPropertyChanged(nameof(Historiques));
                var options = new ToastOptions()
                {
                    BackgroundColor = Color.FromHex("#93178FEB"),
                    Duration = new TimeSpan(7500),
                    CornerRadius = new Thickness(20),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.White,
                        Message = "Suppression de l'historique réussie.",
                        Padding = new Thickness(10)
                    }
                };
                await Shell.Current.DisplayToastAsync(options);
            });
            var dos = manager.GetAll();
            Historiques = new ObservableCollection<UserHistorique>(dos);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            else
                return;
        }


        public ICommand OnHistoriqueDetailsSwipe { get; set; }
        
        public ICommand OnHistoriqueDeleteSwipe { get; set; }


    }
}
