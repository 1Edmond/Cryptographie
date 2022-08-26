using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using CryptoAppV2.Service;
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
                   _= MakePayement();
                else
                    await DisplayAlert("Erreur", "Veuillez vérifier la référence de la transaction.", "Ok");
            };
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                SmsImage.FadeTo(0, 200);
            else
                WhatsappImage.FadeTo(0, 200);
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                Task.Run(() =>
                {
                    Annimation(); 
                });
                return true;
            });
        }

        public async Task MakePayement()
        {
            DataGrid.Opacity = 0.2;
            MyActivityIndicator.IsRunning = true;
            var payement = new Payement()
            {
                DatePayement = DateTime.Now,
                Reference = ReferenceEntry.Text
            };
           var result = new ApiResult<Payement>();
           _= Task.Run(() =>
            {
                result = ApiService.AddPayement(payement).Result;
                
            });
            DataGrid.Opacity = 1;
            MyActivityIndicator.IsRunning = false;
           await DisplayAlert("Ok", $"{result.Count}", "Ok");
         
        }

        public bool Verfication() => 
            String.IsNullOrWhiteSpace(ReferenceEntry.Text) ? false : ReferenceEntry.Text.Length != 10 ? false : true;

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

        public void Annimation()
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _ = WhatsappImage.FadeTo(0, 1500);
                _ = SmsImage.FadeTo(1, 1500);
            }
            else
            {
                _= SmsImage.FadeTo(0,1500);
                _ = WhatsappImage.FadeTo(1, 1500);
            }

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