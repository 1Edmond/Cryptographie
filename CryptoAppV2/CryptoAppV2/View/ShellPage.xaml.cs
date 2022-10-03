using CryptoAppV2.Model;
using CryptoAppV2.View.HistoriquePages;
using CryptoAppV2.View.Home;
using CryptoAppV2.View.ModelePages;
using CryptoAppV2.View.SettingsPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        public ShellPage()
        {
            InitializeComponent();
            Routing.RegisterRoute("AffinePage", typeof(AffinePage));
            Routing.RegisterRoute("UseCasePage", typeof(UseCasePage));
            Routing.RegisterRoute("EtapePage", typeof(EtapePage));
            Routing.RegisterRoute("AcceuilPage", typeof(AcceuilPage));
            Routing.RegisterRoute("HillPage", typeof(HillPage));
            Routing.RegisterRoute("MerkleHellmanPage", typeof(MerkleHellmanPage));
            Routing.RegisterRoute("RSAPage", typeof(RSAPage));
            Routing.RegisterRoute("VernamPage", typeof(VernamPage));
            Routing.RegisterRoute("ModelesPage", typeof(ModelesPage));
            Routing.RegisterRoute("VigenerePage", typeof(VigenerePage));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("Index", typeof(Index));
            Routing.RegisterRoute("HistoriquePage", typeof(HistoriquePage));
            Routing.RegisterRoute("HistoriqueDetailsPage", typeof(HistoriqueDetailsPage));
            ParametreItem.Command = new Command(async () =>
            {
                await Shell.Current.GoToAsync("Index");
                Current.FlyoutIsPresented = false;
            });
            CryptographieItem.Command = new Command(async () =>
            {
                await Shell.Current.GoToAsync("AcceuilPage");
                Current.FlyoutIsPresented = false;
            });
            ModelesItem.Command = new Command(async () =>
            {
                await Current.GoToAsync("ModelesPage");
                Current.FlyoutIsPresented = false;
            });
            HistoriquesItem.Command = new Command(async () =>
            {
                await Current.GoToAsync("HistoriquePage");
                Current.FlyoutIsPresented = false;
            });
            BtnExit.Clicked += delegate
            {
                Environment.Exit(0);
            };
            UserName.Text = $"{UserSettings.UserName} {UserSettings.UserPrenom}";
        }

    }
}