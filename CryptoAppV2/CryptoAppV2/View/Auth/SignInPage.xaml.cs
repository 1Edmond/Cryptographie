using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            AnimateCaroussel();
            MesControls.MyEntryFocus(NomEntry, NomFrame);
            MesControls.MyEntryFocus(PrenomEntry, PrenomFrame);
            MesControls.MyEntryFocus(TelephoneEntry, TelephoneFrame);
            NomEntry.Completed += delegate
            {
                if (String.IsNullOrEmpty(PrenomEntry.Text))
                    PrenomEntry.Focus();
                else
                    if (String.IsNullOrEmpty(TelephoneEntry.Text))
                        TelephoneEntry.Focus();
            };
            PrenomEntry.Completed += delegate
            {
                if (String.IsNullOrEmpty(TelephoneEntry.Text))
                    TelephoneEntry.Focus();
                else
                    if (String.IsNullOrEmpty(NomEntry.Text))
                    NomEntry.Focus();
            };
            TelephoneEntry.Completed += delegate
            {
                if (String.IsNullOrEmpty(NomEntry.Text))
                    NomEntry.Focus();
                else
                    if (String.IsNullOrEmpty(PrenomEntry.Text))
                    PrenomEntry.Focus();
            };
            
        }
        public void AnimateCaroussel()
        {
            Timer timer = new Timer(5000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (Mycaroussel.Position == 7)
                    Mycaroussel.Position = 0;
                Mycaroussel.Position++;
            };
        }

        public async Task SingIn()
        {
            if (Verification())
            {
                UserSettings.UserContact = TelephoneEntry.Text;
                UserSettings.UserName = NomEntry.Text;
                UserSettings.UserPrenom = PrenomEntry.Text;
                await Navigation.PushAsync(new PayementPage());
            }
            else
                await DisplayAlert("Erreur","Vérifiez vos entrées.","Ok");
        }

        public bool Verification() => !String.IsNullOrEmpty(NomEntry.Text) && !String.IsNullOrEmpty(PrenomEntry.Text) && !String.IsNullOrEmpty(TelephoneEntry.Text) ? true : false; 
    }
}