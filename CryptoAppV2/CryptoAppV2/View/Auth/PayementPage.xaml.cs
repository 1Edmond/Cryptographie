using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayementPage : ContentPage
    {
        public PayementPage()
        {
            InitializeComponent();
            MesControls.MyEntryFocus(ReferenceEntry,ReferenceFrame);
            BtnValider.Clicked += async delegate
            {
                if (Verfication())
                    MakePayement();
                else
                    await DisplayAlert("Erreur", "Vous devez saisir la référence", "Ok");
            };
        }

        public void MakePayement()
        {
            var payement = new Payement()
            {
                DatePayement = DateTime.Now,
                Reference = ReferenceEntry.Text
            };

        }

        public bool Verfication() => String.IsNullOrEmpty(ReferenceEntry.Text) ? false : true;

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
                try
                {
                    Chat.Open("+22892080770", $"Je viens d'effectuer un payement avec la référence {ReferenceEntry.Text}.");
                }
                catch (Exception)
                {
                    Sms.ComposeAsync(new SmsMessage()
                    {
                        Body = $"Je viens d'effectuer un payement avec la référence {ReferenceEntry.Text}.",
                        Recipients = new List<string>()
                    {
                        "92080770"
                    }
                    });

                }
            else
                Sms.ComposeAsync(new SmsMessage()
                {
                    Body = $"Je viens d'effectuer un payement avec la référence {ReferenceEntry.Text}.",
                    Recipients = new List<string>()
                    {
                        "92080770"
                    }
                });
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        =>
            Clipboard.SetTextAsync("92080770").ContinueWith(async(ord) => {
                var options = new ToastOptions()
                {
                    BackgroundColor = Color.FromHex("#93178FEB"),
                    Duration = new TimeSpan(7500),
                    CornerRadius = new Thickness(20),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.White,
                        Message = "Le numéro 92 08 07 70 a été copié dans votre presse papier",
                        Padding = new Thickness(10)
                    }
                };
                await this.DisplayToastAsync(options);
               });
        
    }
}