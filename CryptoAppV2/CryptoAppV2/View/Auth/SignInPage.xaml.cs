using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using CryptoAppV2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
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
            NavigationPage.SetHasNavigationBar(this, false);
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
                else
                    SignIn();
            };
            PrenomEntry.Completed += delegate
            {
                if (String.IsNullOrEmpty(TelephoneEntry.Text))
                    TelephoneEntry.Focus();
                else
                    if (String.IsNullOrEmpty(NomEntry.Text))
                    NomEntry.Focus();
                else
                    SignIn();
            };
            TelephoneEntry.Completed += delegate
            {
                if (String.IsNullOrEmpty(NomEntry.Text))
                    NomEntry.Focus();
                else
                    if (String.IsNullOrEmpty(PrenomEntry.Text))
                    PrenomEntry.Focus();
                else
                    SignIn();
            };
            BtnEnregistrer.Clicked += async delegate
            {
                await SignIn();
            };
            BtnExit.Clicked += delegate
            {
                Environment.Exit(0);
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

        public async Task SignIn()
        {
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                DataGrid.Opacity = 0.2;
                MyWaitAnnimation.IsRunning = true;
                if (Verification())
                {
                    ApiResult<Inscription> inscriptionResult = null;
                   _= Task.Run(() =>
                    {
                        var temp = ApiService.ExistInscription(TelephoneEntry.Text).Result;
                        if(temp.StatusCode == "200")
                            if(temp.Message == "false")
                            {
                                DataGrid.Opacity = 1;
                                MyWaitAnnimation.IsRunning = false;
                                inscriptionResult = ApiService.AddInscription(new Inscription()
                                {
                                    Contact = TelephoneEntry.Text,
                                    DateInscription = DateTime.Now,
                                    Etat = 0,
                                    Nom = NomEntry.Text,
                                    Prenom = PrenomEntry.Text
                                }).Result;
                                UserSettings.UserInscriptionId = $"{inscriptionResult.ELement.Id}";
                                UserSettings.UserContact = TelephoneEntry.Text;
                                UserSettings.UserName = NomEntry.Text;
                                UserSettings.UserPrenom = PrenomEntry.Text;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await DisplayAlert("Réussite", "Votre inscription a bien été ajouté", "Ok");
                                    await Navigation.PushAsync(new PayementPage());
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async() =>
                                {
                                    DataGrid.Opacity = 1;
                                    MyWaitAnnimation.IsRunning = false;
                                    await DisplayAlert("Erreur", "Il existe une inscription avec ce numéro de téléphone.", "Ok");
                                });
                            }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                DataGrid.Opacity = 1;
                                MyWaitAnnimation.IsRunning = false;
                                await DisplayAlert("Erreur", $"{temp.Message}", "Ok");
                            });
                        }

                    });
                
                }
                else
                {
                    DataGrid.Opacity = 1;
                    MyWaitAnnimation.IsRunning = false;
                    await DisplayAlert("Erreur","Vérifiez vos entrées.","Ok");
                    if(TelephoneEntry.Text.Length != 8)
                    {
                        var options = new ToastOptions()
                        {
                            BackgroundColor = Color.FromHex("#93E92d3d"),
                            Duration = new TimeSpan(7500),
                            CornerRadius = new Thickness(20),
                            MessageOptions = new MessageOptions()
                            {
                                Foreground = Color.White,
                                Message = "Le numéro de téléphone doit avoir huit caractères.",
                                Padding = new Thickness(10)
                            }
                        };
                        await this.DisplayToastAsync(options);
                    }
                }
            }
            else
            {
                var options = new ToastOptions()
                {
                    BackgroundColor = Color.FromHex("#93E92d3d"),
                    Duration = new TimeSpan(7500),
                    CornerRadius = new Thickness(20),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.White,
                        Message = "Vous devez disposer d'une connection internet pour pouvoir vous inscrire.",
                        Padding = new Thickness(10)
                    }
                };
                await this.DisplayToastAsync(options);
            }
        }

        public bool Verification() => !String.IsNullOrEmpty(NomEntry.Text) && !String.IsNullOrEmpty(PrenomEntry.Text) && !String.IsNullOrEmpty(TelephoneEntry.Text) && TelephoneEntry.Text.Length == 8 ? true :  false; 
    }
}