using CryptoAppV2.CrAlgorithme;
using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MerkleHellmanPage : ContentPage
    {
        private readonly CryptoCode CryptoCode = new CryptoCode();
        private readonly CryptoEtape CryptoEtape = new CryptoEtape();
        ObservableCollection<Etape> etapesCodage = new ObservableCollection<Etape>();
        ObservableCollection<Etape> etapesDecodage = new ObservableCollection<Etape>();
        string codageResult = string.Empty;
        string decodageResult = string.Empty;
        public MerkleHellmanPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AnimateDecodageCaroussel();
            AnimateCodageCaroussel();

            #region Visuel du Focus
            CodageTextEntry.MyEntryFocus(CodageTextFrame);
            CodageSuiteEntry.MyEntryFocus(CodageSuiteFrame);
            CodageNEntry.MyEntryFocus(CodageNFrame);
            CodageMEntry.MyEntryFocus(CodageMFrame);
            DecodageTextEntry.MyEntryFocus(DecodageTextFrame);
            DecodageSuiteEntry.MyEntryFocus(DecodageSuiteFrame);
            DecodageNEntry.MyEntryFocus(DecodageNFrame);
            DecodageMEntry.MyEntryFocus(DecodageMFrame);
            #endregion

            #region Codage
            CodageMEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageNEntry.Text))
                    CodageNEntry.Focus();
                else
                    if (String.IsNullOrEmpty(CodageSuiteEntry.Text))
                    CodageSuiteEntry.Focus();

                else if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };

            CodageNEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageMEntry.Text))
                    CodageMEntry.Focus();
                else if (String.IsNullOrEmpty(CodageSuiteEntry.Text))
                    CodageSuiteEntry.Focus();
                else if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };

            CodageSuiteEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else if (String.IsNullOrEmpty(CodageNEntry.Text))
                    CodageNEntry.Focus();
                else if (String.IsNullOrEmpty(CodageMEntry.Text))
                    CodageMEntry.Focus();
                else
                    await Codage();
            };

            CodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageNEntry.Text))
                    CodageNEntry.Focus();
                else if (String.IsNullOrEmpty(CodageMEntry.Text))
                    CodageMEntry.Focus();
                else if (String.IsNullOrEmpty(CodageSuiteEntry.Text))
                    CodageSuiteEntry.Focus();
                else
                    await Codage();
            };

            BtnCodage.Clicked += async (sender, e) =>
            {
                await Codage();
            };

            EtapeCodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Codage de {CodageTextEntry.Text} avec : n = {CodageNEntry.Text}" +
                    $", m = {CodageMEntry.Text}" +
                    $", suite = {CodageSuiteEntry.Text}", "Codage de Merkle Hellman", etapesCodage.ToList()));
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            };
            #endregion

            #region Décodage
            DecodageMEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageNEntry.Text))
                    DecodageNEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageSuiteEntry.Text))
                    DecodageSuiteEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };

            DecodageNEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageMEntry.Text))
                    DecodageMEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageSuiteEntry.Text))
                    DecodageSuiteEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };

            DecodageSuiteEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageNEntry.Text))
                    DecodageNEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageMEntry.Text))
                    DecodageMEntry.Focus();
                else
                    await Decodage();
            };

            DecodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageNEntry.Text))
                    DecodageNEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageMEntry.Text))
                    DecodageMEntry.Focus();
                else if (String.IsNullOrEmpty(DecodageSuiteEntry.Text))
                    DecodageSuiteEntry.Focus();
                else
                    await Decodage();
            };

            BtnDecodage.Clicked += async (sender, e) =>
            {
                await Decodage();
            };

            EtapeDecodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Décodage de {DecodageTextEntry.Text} avec : n = {DecodageNEntry.Text}" +
                    $", m = {DecodageMEntry.Text}" +
                    $", suite = {DecodageSuiteEntry.Text}", "Décodage de Merkle Hellman", etapesDecodage.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };

            #endregion

            #region Action sur les caroussels
            BackCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position > 0)
                    MyCodageCaroussel.Position--;
                else
                    MyCodageCaroussel.Position = 8;
            };
            NextCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position == 8)
                    MyCodageCaroussel.Position = 0;
                else
                    MyCodageCaroussel.Position++;
            };
            BackDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position > 0)
                    MyDecodageCaroussel.Position--;
                else
                    MyDecodageCaroussel.Position = 8;
            };
            NextDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position == 8)
                    MyDecodageCaroussel.Position = 0;
                else
                    MyDecodageCaroussel.Position++;
            };
            #endregion

        }

        public async Task Codage()
        {
            try
            {
                string text, cle, n, m;
                if (!String.IsNullOrEmpty(CodageTextEntry.Text))
                {
                    if (!String.IsNullOrEmpty(CodageMEntry.Text))
                    {
                        if (!String.IsNullOrEmpty(CodageNEntry.Text))
                        {
                            if (!String.IsNullOrEmpty(CodageSuiteEntry.Text))
                            {
                                (text, cle, n, m) = (CodageTextEntry.Text, CodageSuiteEntry.Text, CodageNEntry.Text.SubstringInteger(), CodageMEntry.Text.SubstringInteger());
                                if (text.Length % cle.Split(',').Length == 0)
                                {
                                    if (cle.IsOnlyInteger())
                                    {
                                        if (cle.SuperSuite())
                                        {
                                            if (text.VerfificationCodageFormatBinaire())
                                            {

                                                CodageResultStack.IsVisible = true;
                                                etapesCodage.Clear();
                                                var result = CryptoCode.CodageBinaireMerkleHellman(text, cle, n, m);
                                                CodageResult.Text = "Le résultat du codage est " + result;
                                                var temp = CryptoEtape.CodageMerkleHellman(text, cle, n, m);
                                                await App.UserHistoriqueManager.Add(new UserHistorique()
                                                {
                                                    DateOperation = DateTime.Now,
                                                    Libelle = "Codage de Merkle Hellman",
                                                    Description = $"Codage de Merkle Hellman avec la clé = {cle}, n =                                       {n}, m = {m}",
                                                    Data = text.TransformHistoriqueData(
                                                     "Merkle Hellman", "Codage",
                                                     new Dictionary<string, string>()
                                                         {
                                                                { "cle" , $"{cle}" },
                                                                { "n" , $"{n}" },
                                                                { "m" , $"{m}" },
                                                         })
                                                });

                                                temp.ForEach(x =>
                                                {
                                                    etapesCodage.Add(x);
                                                });
                                                codageResult = result;
                                                await ActivateAndAnimeBtn(BtnCodage, EtapeCodageBtn);

                                            }
                                            else
                                            {
                                                await DisplayAlert("Erreur", "Le texte doit être en binaire.", "Ok");
                                                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                                            }
                                        }
                                        else
                                        {
                                            await DisplayAlert("Erreur", "La clé n'est pas une super croissante.", "Ok");
                                            await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                                        }
                                    }
                                    else
                                    {
                                        await DisplayAlert("Erreur", "La clé ne doit être que des entiers.", "Ok");
                                        await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                                    }
                                }
                                else
                                {
                                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                                    await DisplayAlert("Erreur", $"La longueur du message ({text.Length}) doit être être un multiple de la longeur de la cle ({cle.Split(',').Length}).", "Ok");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Erreur", "Vous n'avez pas saisi la suite.", "Ok");
                                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                            }
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Vous n'avez pas saisi la variable n.", "Ok");
                            await DeleteAnimation(BtnCodage, EtapeCodageBtn);

                        }
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Vous n'avez pas saisi la variable m", "Ok");
                        await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                    }

                }
                else
                {
                    await DisplayAlert("Erreur", "Vous n'avez pas saisi le texte à coder.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);

                }
            }
            catch (Exception)
            {
                await DisplayAlert("Erreur", "Un problème est survenu, réessayer. Si cela persiste veuillez nous contacter.", "Ok");
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);

            }
        }
        public async Task Decodage()
        {
            try
            {
                string text, cle, n, m;
                if (!String.IsNullOrEmpty(DecodageTextEntry.Text))
                {
                    if (!String.IsNullOrEmpty(DecodageMEntry.Text))
                    {
                        if (!String.IsNullOrEmpty(DecodageNEntry.Text))
                        {
                            if (!String.IsNullOrEmpty(DecodageSuiteEntry.Text))
                            {
                                (text, cle, n, m) = (DecodageTextEntry.Text, DecodageSuiteEntry.Text, DecodageNEntry.Text.SubstringInteger(), DecodageMEntry.Text.SubstringInteger());
                                /*  if (//text.Split(',').Length % cle.Split(',').Length == 0
                                      true)
                                  {*/
                                if (cle.IsOnlyInteger())
                                {
                                    if (cle.SuperSuite())
                                    {
                                        if (text.IsOnlyInteger())
                                        {

                                            DecodageResultStack.IsVisible = true;
                                            etapesDecodage.Clear();
                                            var result = CryptoCode.DecodageMerkleHellman(text, cle, n, m);
                                            DecodageResult.Text = "Le résultat du codage est " + result;
                                            var temp = CryptoEtape.DecodageMerkleHellman(text, cle, n, m);
                                            temp.ForEach(x =>
                                            {
                                                etapesDecodage.Add(x);
                                            });
                                            await App.UserHistoriqueManager.Add(new UserHistorique()
                                            {
                                                DateOperation = DateTime.Now,
                                                Libelle = "Décodage de Merkle Hellman",
                                                Description = $"Décodage de Merkle Hellman avec la clé = {cle}, n =                                       {n}, m = {m}",
                                                Data = text.TransformHistoriqueData(
                                                    "Merkle Hellman", "Decodage",
                                                    new Dictionary<string, string>()
                                                        {
                                                                { "cle" , $"{cle}" },
                                                                { "n" , $"{n}" },
                                                                { "m" , $"{m}" },
                                                        })
                                            });
                                            decodageResult = result;
                                            await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
                                        }
                                        else
                                        {
                                            await DisplayAlert("Erreur", "Le texte ne peut être que des entiers.", "Ok");
                                            await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                                        }
                                    }
                                    else
                                    {
                                        await DisplayAlert("Erreur", "La clé n'est pas une super croissante.", "Ok");
                                        await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Erreur", "La clé ne doit être que des entiers.", "Ok");
                                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                                }
                                /*   }
                                 else
                                  {
                                      await DisplayAlert("Erreur", $"La longueur du message ({text.Length}) doit être être égale à la longeur de la cle ({cle.Split(',').Length}).", "Ok");
                                      await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                                  }
  */
                            }
                            else
                            {
                                await DisplayAlert("Erreur", "Vous n'avez pas saisi la suite.", "Ok");
                                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                            }
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Vous n'avez pas saisi la variable n.", "Ok");
                            await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Vous n'avez pas saisi la variable m", "Ok");
                        await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                    }

                }
                else
                {
                    await DisplayAlert("Erreur", "Vous n'avez pas saisi le texte à décoder.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un problème est survenu, réessayer {ex.Message}.", "Ok");
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            }
        }
        public void AnimateCodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyCodageCaroussel.Position == 7)
                    MyCodageCaroussel.Position = 0;
                MyCodageCaroussel.Position++;
            };
        }
        public void AnimateDecodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyDecodageCaroussel.Position == 7)
                    MyDecodageCaroussel.Position = 0;
                MyDecodageCaroussel.Position++;
            };
        }
        private async void ResultCodageTap_Tapped(object sender, EventArgs e) =>
           await Clipboard.SetTextAsync(codageResult).ContinueWith(async (ord) => {
               var options = new ToastOptions()
               {
                   BackgroundColor = Color.FromHex("#28C2FF"),
                   Duration = new TimeSpan(7500),
                   CornerRadius = new Thickness(20),
                   MessageOptions = new MessageOptions()
                   {
                       Foreground = Color.White,
                       Message = "Résultat du codage copié avec succès.",
                       Padding = new Thickness(10)
                   }
               };
               await this.DisplayToastAsync(options);
           });
        private async void DecodageResultTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(decodageResult).ContinueWith(async (ord) => {
                var options = new ToastOptions()
                {
                    BackgroundColor = Color.FromHex("#28C2FF"),
                    Duration = new TimeSpan(7500),
                    CornerRadius = new Thickness(20),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.White,
                        Message = "Résultat du décodage copié avec succès.",
                        Padding = new Thickness(10)
                    }
                };
                await this.DisplayToastAsync(options);
            });
        private async Task ActivateAndAnimeBtn(Button btnCodage, ImageButton btn)
        {
            await btnCodage.FadeTo(0, 3000);
            // btnCodage.Margin = new Thickness() { Left = 20, Bottom = 20 };
            btnCodage.HorizontalOptions = new LayoutOptions() { Alignment = LayoutAlignment.Start, Expands = true };
            await Task.Delay(500);
            _ = btnCodage.FadeTo(1, 3000);
            _ = btn.FadeTo(1, 3000);
            // btn.Margin = new Thickness() { Right = 20, Bottom = 20 };
            btn.IsVisible = true;
        }
        private async Task DeleteAnimation(Button btnCodage, ImageButton btn)
        {
            // btnCodage.Margin = new Thickness() { Left = 0,Bottom = 0 };
            btnCodage.HorizontalOptions = new LayoutOptions() { Alignment = LayoutAlignment.Center, Expands = true };
            await btn.FadeTo(0);
        }
    }
}