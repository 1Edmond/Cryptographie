using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.SettingsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index : ContentPage
    {
        public Index()
        {
            InitializeComponent();
            UserName.Text = UserSettings.UserName;
            UserNumber.Text = UserSettings.UserContact;
            ModeleCount.Text =  $"{App.UserModeleManager.Count().Result}";
            HistoriqueCount.Text = $"{App.UserHistoriqueManager.Count().Result}";
        }

        private async void Information_Tapped(object sender, EventArgs e) => await Shell.Current.GoToAsync("ProfilPage");
        private async void UseCase_Tapped(object sender, EventArgs e) => await Shell.Current.GoToAsync("UseCasePage");
        
    }
}