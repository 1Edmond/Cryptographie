using CryptoAppV2.CrAlgorithme;
using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VernamPage : ContentPage
    {

        CryptoCode cryptoCode = new CryptoCode();
        public VernamPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AnimateCodageCaroussel();
            AnimateDecodageCaroussel();
            #region Codage
            MesControls.MyEntryFocus(CodageCleEntry, CodageCleFrame);

            CodageCleEntry.Completed += async delegate
            {
                CodageCleEntry.Unfocus();
                if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };

            CodageCleEntry.Unfocused += delegate
            {

                if (!String.IsNullOrEmpty(CodageCleEntry.Text))
                {
                    if (!CodageCleEntry.Text.VerfificationCodageFormatBinaire())
                        DisplayAlert("Erreur", "Veuillez revoir la saisie de la clé ", "Ok").ContinueWith((obj) =>
                        {
                            CodageCleEntry.Focus();
                        });
                }
            };

            MesControls.MyEntryFocus(CodageTextEntry, CodageTextFrame);

            CodageTextEntry.Unfocused += delegate
            {
                if (!String.IsNullOrEmpty(CodageTextEntry.Text))
                {
                    if (!CodageTextEntry.Text.VerfificationCodageFormatBinaire())
                        DisplayAlert("Erreur", "Veuillez revoir la saisie de votre text.", "Ok").ContinueWith((ord) =>
                        {
                            CodageTextEntry.Focus();
                        });
                }

            };

            CodageTextEntry.Completed += async delegate
            {
                CodageTextEntry.Unfocus();
                if (String.IsNullOrEmpty(CodageCleEntry.Text))
                    CodageCleEntry.Focus();
                else
                    await Codage();
            };
            BtnCodage.Clicked += async delegate
            {
                await Codage();
            };
            #endregion
            #region Décodage
            MesControls.MyEntryFocus(DecodageCleEntry, DecodageCleFrame);
            DecodageCleEntry.Completed += async delegate
            {
                DecodageCleEntry.Unfocus();
                if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };
            DecodageCleEntry.Unfocused += delegate
            {
                if (!String.IsNullOrEmpty(DecodageCleEntry.Text))
                {
                    if (!DecodageCleEntry.Text.VerfificationCodageFormatBinaire())
                        DisplayAlert("Erreur", "Veuillez revoir la saisie de la clé.", "Ok").ContinueWith((obj) =>
                        {
                            DecodageCleEntry.Focus();
                        });
                }
            };
            MesControls.MyEntryFocus(DecodageTextEntry, DecodageTextFrame);
            DecodageTextEntry.Unfocused += async delegate
            {
                if (!String.IsNullOrEmpty(DecodageTextEntry.Text))
                {
                    if (!DecodageTextEntry.Text.VerfificationCodageFormatBinaire())
                        await DisplayAlert("Erreur", "Veuillez revoir la saisie de votre text.", "Ok").ContinueWith((ord) =>
                        {
                            DecodageTextEntry.Focus();
                        });
                }
            };
            DecodageTextEntry.Completed += async delegate
            {
                DecodageTextEntry.Unfocus();
                if (String.IsNullOrEmpty(DecodageCleEntry.Text))
                    DecodageCleEntry.Focus();
                else
                    await Decodage();
            };
            BtnDecodage.Clicked += async delegate
            {
                await Decodage();
            };
            #endregion
            #region Caroussel boutton
            BackCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position > 0)
                    MyCodageCaroussel.Position--;
                else
                    MyCodageCaroussel.Position = 3;
            };
            NextCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position == 3)
                    MyCodageCaroussel.Position = 0;
                else
                    MyCodageCaroussel.Position++;
            };
            BackDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position > 0)
                    MyDecodageCaroussel.Position--;
                else
                    MyDecodageCaroussel.Position = 3;
            };
            NextDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position == 3)
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
                if (!String.IsNullOrEmpty(CodageCleEntry.Text) && !String.IsNullOrEmpty(CodageTextEntry.Text))
                {
                    if (CodageCleEntry.Text.VerfificationCodageFormatBinaire() && CodageTextEntry.Text.VerfificationCodageFormatBinaire())
                    {
                        if (CodageCleEntry.Text.Length.Equals(CodageTextEntry.Text.Length))
                        {
                            CodageResultStack.IsVisible = true;
                            var result = cryptoCode.VernamCryptoSysteme(CodageTextEntry.Text, CodageCleEntry.Text);
                            CodageResult.Text = "La solution est " + result;
                            MyBoxView.WidthRequest = 10 * result.Length;
                            CodageResultOperation.Text = "+";
                            CodageResultCle.Text = CodageCleEntry.Text;
                            CodageResultTest.Text = CodageTextEntry.Text;
                            CodageResultSolution.Text = result;
                            await App.UserHistoriqueManager.Add(new UserHistorique()
                            {
                                DateOperation = DateTime.Now,
                                Libelle = "Codage de Vernam",
                                Description = $"Codage de Vernam avec la clé = {CodageCleEntry.Text}",
                                Data = CodageTextEntry.Text.TransformHistoriqueData(
                                        "Vernam", "Codage",
                                        new Dictionary<string, string>()
                                            {
                                                { "cle" , $"{CodageCleEntry.Text}" },
                                            })
                            });
#pragma warning disable CS0618 // Le type ou le membre est obsolète
                            CodageResult.GestureRecognizers.Add(new TapGestureRecognizer(async (ord) => { await Clipboard.SetTextAsync(result).ContinueWith(async (ord1) => await this.DisplayToastAsync("Résultat du codage copié avec succès.", 3000)); })
                            { NumberOfTapsRequired = 2 });
#pragma warning restore CS0618 // Le type ou le membre est obsolète

                        }
                        else
                        {
                            await DisplayAlert("Erreur", "La clé et le text doivent avoir la même longueur.", "Ok");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un truc s'est mal passé, réessayer {ex.Message}.", "Ok");
            }

        }

        public async Task Decodage()
        {
            try
            {
                if (!String.IsNullOrEmpty(DecodageCleEntry.Text) && !String.IsNullOrEmpty(DecodageTextEntry.Text))
                {
                    if (DecodageTextEntry.Text.VerfificationCodageFormatBinaire() && DecodageCleEntry.Text.VerfificationCodageFormatBinaire())
                    {
                        if (DecodageCleEntry.Text.Length.Equals(DecodageTextEntry.Text.Length))
                        {
                            DecodageResultStack.IsVisible = true;
                            var result = cryptoCode.VernamCryptoSysteme(DecodageTextEntry.Text, DecodageCleEntry.Text);
                            DecodageResult.Text = "La solution est " + result;
#pragma warning disable CS0618 // Le type ou le membre est obsolète
                            DecodageResult.GestureRecognizers.Add(new TapGestureRecognizer(async (ord) => { await Clipboard.SetTextAsync(result).ContinueWith(async (ord1) => await this.DisplayToastAsync("Résultat du décodage copié avec succès.", 3000)); })
                            { NumberOfTapsRequired = 2 });
#pragma warning restore CS0618 // Le type ou le membre est obsolète
                            await App.UserHistoriqueManager.Add(new UserHistorique()
                            {
                                DateOperation = DateTime.Now,
                                Libelle = "Décodage de Vernam",
                                Description = $"Décodage de Vernam avec la clé = {DecodageCleEntry.Text}",
                                Data = DecodageTextEntry.Text.TransformHistoriqueData(
                                       "Vernam", "Decodage",
                                       new Dictionary<string, string>()
                                           {
                                                { "cle" , $"{DecodageCleEntry.Text}" },
                                           })
                            });
                            MyDecodageBoxView.WidthRequest = 10 * result.Length;
                            DecodageResultOperation.Text = "+";
                            DecodageResultCle.Text = DecodageCleEntry.Text;
                            DecodageResultTest.Text = DecodageTextEntry.Text;
                            DecodageResultSolution.Text = result;
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "La clé et le text doivent avoir la même longueur.", "Ok");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un truc s'est mal passé, réessayer {ex.Message}.", "Ok");
            }

        }
        public void AnimateCodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyCodageCaroussel.Position == 3)
                    MyCodageCaroussel.Position = 0;
                MyCodageCaroussel.Position++;
            };
        }
        public void AnimateDecodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyDecodageCaroussel.Position == 3)
                    MyDecodageCaroussel.Position = 0;
                MyDecodageCaroussel.Position++;
            };
        }
    }
}