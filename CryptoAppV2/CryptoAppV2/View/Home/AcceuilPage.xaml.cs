using CryptoAppV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcceuilPage : ContentPage
    {

        const string Password = "pass";
        const string Nom = "nom";
        const string Numero = "numero";
        const string User = "userPass";
        const string Session = "UserSession";
        public AcceuilPage()
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new CryptoChapitreVm();
            MyTabView.Scrolled += delegate
            {
                if (AccueilItem.IsSelected)
                    MyHeader.Text = "Les différents cryptosystèmes";
                else
                    MyHeader.Text = "Les différentes fonctions";
            };
           /* if (Application.Current.Properties.ContainsKey(Session))
            {
                string session = (string)Application.Current.Properties[Session];
                if (session == "Oui")
                    SessionOui.IsChecked = true;
                else
                    SessionNon.IsChecked = true;
            }

            Contacter.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        Chat.Open("+228 92 08 07 70", "");
                    }
                    catch (Exception)
                    {
                        try
                        {
                            var message = new SmsMessage("", new[] { "+228 92 08 07 70" });
                            await Sms.ComposeAsync(message);
                        }
                        catch (Exception)
                        {
                            await DisplayAlert("Erreur", "Whatsapp et Sms non supportés ", "");
                        }
                    }
                }
                )
            }
            );
            Contacter.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await Contacter.DisplayToastAsync("Contact : +228 92 08 07 70, Nom : Wicode", 2500);
                }
                ),
                NumberOfTapsRequired = 2
            }
            );
            Conditions.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushModalAsync(new UseCasePage());
                }
               )
            }
            );

            if (Application.Current.Properties.ContainsKey(Password))
            {
                if (!Application.Current.Properties[Password].ToString().Equals("none"))
                    ChangePassWord.IsVisible = true;
                else
                    ChangePassWord.IsVisible = false;
            }

            if (OnBackButtonPressed())
                Environment.Exit(0);
            
            #region Paramètre
            SessionOui.CheckedChanged += async delegate
            {
                SessionNon.IsChecked = !SessionOui.IsChecked;
                if (!SessionNon.IsChecked)
                {
                    if (Application.Current.Properties.ContainsKey(Session))
                        Application.Current.Properties[Session] = "Oui";
                    else
                        Application.Current.Properties.Add(Session, "Oui");

                    await Application.Current.SavePropertiesAsync();
                }
                else
                {
                    if (Application.Current.Properties.ContainsKey(Session))
                        Application.Current.Properties[Session] = "Non";
                    else
                        Application.Current.Properties.Add(Session, "Non");

                    await Application.Current.SavePropertiesAsync();

                }
            };
            SessionNon.CheckedChanged += delegate
            {
                SessionOui.IsChecked = !SessionNon.IsChecked;
            };

            Deconnecte.Clicked += async delegate
            {
                if (Application.Current.Properties.ContainsKey(Session))
                    Application.Current.Properties[Session] = "Non";
                await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(new WelcomePage());
            };
            ParametreMonCompteLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() =>
                {
                  //  this.ShowPopupAsync(new UserPopUp());
                }),

            });
            #endregion

*/
            #region Fonction

            #region My carousel action
            var fonction = new FonctionVm(Navigation);
            Mycarousel.BindingContext = fonction;
            Mycarousel.ItemsSource = fonction.Fonctions;
            MyCarouselLabel.Text = $"Page {Mycarousel.Position + 1} / {fonction.Fonctions.Count}";
            Mycarousel.IsSwipeEnabled = false;
            Next.Clicked += delegate
            {
                if (Mycarousel.Position < fonction.Fonctions.Count - 1)
                    Mycarousel.Position++;
                else
                    Mycarousel.Position = 0;
                MyCarouselLabel.Text = $"Page {Mycarousel.Position + 1} / {fonction.Fonctions.Count}";
            };
            Previous.Clicked += delegate
            {
                if (Mycarousel.Position != 0)
                    Mycarousel.Position--;
                else
                    Mycarousel.Position = fonction.Fonctions.Count - 1;
                MyCarouselLabel.Text = $"Page {Mycarousel.Position + 1} / {fonction.Fonctions.Count}";
            };


            #endregion

            #endregion

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {


            if (Application.Current.Properties.ContainsKey(Password))
            {
                Application.Current.Properties[Password] = "none";
                await Application.Current.SavePropertiesAsync();
                await this.DisplayToastAsync("Mot de passe modifié avec succès", 5000);
                //ChangePassWord.IsVisible = false;
            }


        }
    }
}