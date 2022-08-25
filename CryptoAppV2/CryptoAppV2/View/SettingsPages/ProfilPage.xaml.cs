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
    public partial class ProfilPage : ContentPage
    {
        public ProfilPage()
        {
            InitializeComponent();
            LoadInformation();
            var ListEntry = new List<Entry>()
            { 
                Nom,
                Contact,
                Password,
                Pseudo
            };
            BtnAnnuler.Clicked += delegate
            {
                ListEntry.ForEach(x =>
                {
                    x.IsEnabled = false;
                });
                LoadInformation();
                BtnAnnuler.IsVisible = false;
                BtnModifier.Text = "Modifier";
            };
            BtnModifier.Clicked += delegate
            {
                if (!Nom.IsEnabled)
                {
                    BtnModifier.Text = "Valider";
                    BtnAnnuler.IsVisible = true;
                    ListEntry.ForEach(x =>
                    {
                        x.IsEnabled = true;
                    });
                }
                else
                    ChangeInformation();
            };
        }
        public async void ChangeInformation()
        {
            if (ControlInformation())
            {
                UserSettings.UserName = Nom.Text;
                UserSettings.UserContact = Contact.Text;
                await DisplayAlert("Réussite", "Modification de vos informations réussie", "Ok");
            }
            else
                await DisplayAlert("Erreur","L'une ou plusieurs de vos entrées ne sont pas correctes","Ok");
        }
        public bool ControlInformation() 
            => !String.IsNullOrEmpty(Nom.Text) && !String.IsNullOrEmpty(Contact.Text) && !String.IsNullOrEmpty(Pseudo.Text)
            && !String.IsNullOrEmpty(Password.Text)
            ? true : false; 
        public void LoadInformation()
        {
            Nom.Text = UserSettings.UserName;
            Contact.Text = UserSettings.UserContact;
        }
        
    }
}