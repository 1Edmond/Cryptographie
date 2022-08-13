using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UseCasePage : ContentPage
    {
        const string Password = "pass";
        public UseCasePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Accepter.Clicked += async delegate
            {
                await Navigation.PushAsync(new WelcomePage());
                Navigation.RemovePage(this);
            };
            Refuser.Clicked += delegate
            {
                Environment.Exit(0);
            };
            if (Application.Current.Properties.ContainsKey(Password))
                CondiditionFrame.IsVisible = false;
        }
    }
}