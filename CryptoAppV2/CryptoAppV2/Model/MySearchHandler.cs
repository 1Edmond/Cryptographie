using CryptoAppV2.Data;
using CryptoAppV2.View.HistoriquePages;
using CryptoAppV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CryptoAppV2.Model
{
    public class MySearchHandler : SearchHandler
    {
        public UserHistoriqueManager UserHistoriqueManager { get; set; }
        public MySearchHandler()
        {
            UserHistoriqueManager = App.UserHistoriqueManager;
        }
        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            ItemsSource = null;
            if (!String.IsNullOrEmpty(newValue))
            {
                ItemsSource = await UserHistoriqueManager.GetByValue(newValue);
            }
        }
        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
           var url = $"{nameof(HistoriqueDetailsPage)}?" 
                + $"HistoriqueId={((UserHistorique)item).Id}";
            Shell.Current.GoToAsync(url);
           
        }
    }
}
