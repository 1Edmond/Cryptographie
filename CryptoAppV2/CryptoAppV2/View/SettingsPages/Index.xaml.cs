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

namespace CryptoAppV2.View.SettingsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index : ContentPage
    {
        public Index()
        {
            InitializeComponent();
            UserName.Text = $"{UserSettings.UserName} {UserSettings.UserPrenom}";
            UserNumber.Text = UserSettings.UserContact;
            ModeleCount.Text =  $"{App.UserModeleManager.Count().Result}";
            HistoriqueCount.Text = $"{App.UserHistoriqueManager.Count().Result}";
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                Task.Run(() =>
                {
                    Annimation();
                });
                return true;
            });
            BtnExit.Clicked += delegate
            {
                Environment.Exit(0);
            };
        }

        private async void DeleteAll_Info(object sender, EventArgs e)
        {
            var options = new ToastOptions()
            {
                BackgroundColor = Color.FromHex("#93178FEB"),
                Duration = new TimeSpan(7500),
                CornerRadius = new Thickness(20),
                MessageOptions = new MessageOptions()
                {
                    Foreground = Color.White,
                    Message = "Apuyez deux fois pour supprimer toutes vos données.",
                    Padding = new Thickness(10)
                }
            };
            await this.DisplayToastAsync(options);
        }
        private void DeleteAll_Tapped(object sender, EventArgs e)
        { 
           _= DeleteAllData();
            ModeleCount.Text = "1";
            HistoriqueCount.Text = "0";
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                try
                {
                    Chat.Open("+22892080770", "");
                }
                catch (Exception)
                {
                    Sms.ComposeAsync(new SmsMessage()
                    {
                        Body = "",
                        Recipients = new List<string>()
                    {
                        "92080770"
                    }
                    });

                }
            else
                Sms.ComposeAsync(new SmsMessage()
                {
                    Body = $"",
                    Recipients = new List<string>()
                    {
                        "92080770"
                    }
                });
        }

        public void Annimation()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _ = WhatsappImage.FadeTo(0, 1500);
                _ = SmsImage.FadeTo(1, 1500);
            }
            else
            {
                _ = SmsImage.FadeTo(0, 1500);
                _ = WhatsappImage.FadeTo(1, 1500);
            }

        }

        private async void UseCase_Tapped(object sender, EventArgs e) => await Shell.Current.GoToAsync("UseCasePage");
        private async Task DeleteAllData()
        {
           _= App.UserHistoriqueManager.DeleteAll();
           _= App.UserModeleManager.DeleteAll();
            var options = new ToastOptions()
            {
                BackgroundColor = Color.FromHex("#b25CE493"),
                Duration = new TimeSpan(7500),
                CornerRadius = new Thickness(20),
                MessageOptions = new MessageOptions()
                {
                    Foreground = Color.White,
                    Message = "Toutes vos données ont été supprimées.",
                    Padding = new Thickness(10)
                }
            };
            await this.DisplayToastAsync(options);
        }
    }
}