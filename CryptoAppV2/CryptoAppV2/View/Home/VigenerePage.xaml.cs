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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VigenerePage : ContentPage
    {

        public CryptoCode CryptoCode = new CryptoCode();
        public CryptoEtape CryptoEtape = new CryptoEtape();
        private ObservableCollection<Etape> codageEtapes = new ObservableCollection<Etape>();
        private ObservableCollection<Etape> decodageEtapes = new ObservableCollection<Etape>();
        string codageResult = string.Empty;
        string decodageResult = string.Empty;
        public VigenerePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AnimateDecodageCaroussel();
            AnimateCodageCaroussel();
            #region Codage
            MesControls.MyEntryFocus(CodageCleEntry, CodageCleFrame);
            MesControls.MyEntryFocus(CodageTextEntry, CodageTextFrame);
            CodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageCleEntry.Text))
                    CodageCleEntry.Focus();
                else
                    await Codage();
            };

            CodageCleEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };
            BtnCodage.Clicked += async delegate
            {
                await Codage();
            };
            EtapeCodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Codage de {CodageTextEntry.Text} avec la clé {CodageCleEntry.Text}", "Codage de VIgenère", codageEtapes.ToList()));
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            };
            #endregion
            _ = DeleteAnimation(BtnCodage, EtapeCodageBtn);

            #region Decodage
            MesControls.MyEntryFocus(DecodageTextEntry, DecodageTextFrame);
            MesControls.MyEntryFocus(DecodageCleEntry, DecodageCleFrame);

            DecodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageCleEntry.Text))
                    DecodageCleEntry.Focus();
                else
                    await Decodage();
            };

            DecodageCleEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };
            BtnDecodage.Clicked += async delegate
            {
                await Decodage();
            };
            EtapeDecodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Décodage de {DecodageTextEntry.Text} avec la clé  {DecodageCleEntry.Text}", "Décodage de Vigenère", decodageEtapes.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };
            _ = DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            #endregion

            #region Caroussel Boutton
            BackCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position > 0)
                    MyCodageCaroussel.Position--;
                else
                    MyCodageCaroussel.Position = 6;
            };
            NextCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position == 6)
                    MyCodageCaroussel.Position = 0;
                else
                    MyCodageCaroussel.Position++;
            };
            BackDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position > 0)
                    MyDecodageCaroussel.Position--;
                else
                    MyDecodageCaroussel.Position = 6;
            };
            NextDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position == 6)
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
                if (!String.IsNullOrEmpty(CodageCleEntry.Text))
                {
                    if (!String.IsNullOrEmpty(CodageTextEntry.Text))
                    {
                        if (CodageCleEntry.Text.IsOnlyInteger())
                        {

                            codageEtapes.Clear();
                            string result = CryptoCode.CodageVigenere(CodageTextEntry.Text, CodageCleEntry.Text);
                            CodageResultStack.IsVisible = true;
                            CodageResult.Text = "Le résultat du codage est " + result;
                            codageResult = result;
                            var text = CodageTextEntry.Text.SubstringString();
                            var temp = CryptoEtape.CodageVigenere(text, CodageCleEntry.Text);
                            temp.ForEach(x =>
                            {
                                codageEtapes.Add(x);
                            });
                            await App.UserHistoriqueManager.Add(new UserHistorique()
                            {
                                DateOperation = DateTime.Now,
                                Libelle = "Codage de Vigenère",
                                Description = $"Codage de Vigenère avec la clé = {CodageCleEntry.Text}",
                                Data = text.TransformHistoriqueData(
                                      "Vigenere", "Codage",
                                      new Dictionary<string, string>()
                                          {
                                                { "cle" , $"{CodageCleEntry.Text}" },
                                          })
                            });
                            await ActivateAndAnimeBtn(BtnCodage, EtapeCodageBtn);

                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Vous ne devez saisir que des entiers.", "Ok");
                            await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Vous devez saisir le texte à coder.", "Ok");
                        await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "Vous devez saisir la clé.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("rokf", $"Un truc s'est mal passé, réessayer {ex.Message}", "Ok");
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            }
        }
        public async Task Decodage()
        {
            try
            {
                if (!String.IsNullOrEmpty(DecodageCleEntry.Text))
                {
                    if (!String.IsNullOrEmpty(DecodageTextEntry.Text))
                    {
                        if (DecodageCleEntry.Text.IsOnlyInteger())
                        {
                            decodageEtapes.Clear();
                            string result = CryptoCode.DecodageVigenere(DecodageTextEntry.Text, DecodageCleEntry.Text);
                            DecodageResultStack.IsVisible = true;
                            DecodageResult.Text = "Le résultat du cdéodage est " + result;
                            var temp = CryptoEtape.DecodageVigenere(DecodageTextEntry.Text.SubstringString(), DecodageCleEntry.Text);
                            temp.ForEach(x =>
                            {
                                decodageEtapes.Add(x);
                            });
                            await App.UserHistoriqueManager.Add(new UserHistorique()
                            {
                                DateOperation = DateTime.Now,
                                Libelle = "Décodage de Vigenère",
                                Description = $"Décodage de Vigenère avec la clé = {DecodageCleEntry.Text}",
                                Data = DecodageTextEntry.Text.TransformHistoriqueData(
                                      "Vigenere", "Decodage",
                                      new Dictionary<string, string>()
                                          {
                                                { "cle" , $"{DecodageCleEntry.Text}" },
                                          })
                            });
                            decodageResult = result;
                            await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Vous ne devez saisir que des entiers.", "Ok");
                            await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                        }

                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Vous devez saisir le texte à décoder.", "Ok");
                        await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                    }

                }
                else
                {
                    await DisplayAlert("Erreur", "Vous devez saisir la clé.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("rokf", $"Un truc s'est mal passé, réessayer {ex.Message}", "Ok");
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            }

        }

        private async void ResultCodageTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(codageResult).ContinueWith(async (ord) => await this.
            DisplayToastAsync($"Résultat du codage copié avec succès. {codageResult}", 3000));

        private async void DecodageResultTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(decodageResult).ContinueWith(async (ord) => await this.
            DisplayToastAsync("Résultat du décodage copié avec succès.", 3000));
        private async Task ActivateAndAnimeBtn(Button btnCodage, ImageButton btn)
        {
            await btnCodage.FadeTo(0, 3000);
            btnCodage.Margin = new Thickness() { Left = 20 };
            btnCodage.HorizontalOptions = new LayoutOptions() { Alignment = LayoutAlignment.Start, Expands = true };
            await Task.Delay(500);
            _ = btnCodage.FadeTo(1, 3000);
            _ = btn.FadeTo(1, 3000);
            btn.IsVisible = true;
        }
        private async Task DeleteAnimation(Button btnCodage, ImageButton btn)
        {
            btnCodage.Margin = new Thickness() { Left = 0 };
            btnCodage.HorizontalOptions = new LayoutOptions() { Alignment = LayoutAlignment.Center, Expands = true };
            await btn.FadeTo(0);
        }
        public void AnimateCodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyCodageCaroussel.Position == 6)
                    MyCodageCaroussel.Position = 0;
                MyCodageCaroussel.Position++;
            };
        }
        public void AnimateDecodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyDecodageCaroussel.Position == 6)
                    MyDecodageCaroussel.Position = 0;
                MyDecodageCaroussel.Position++;
            };
        }
    }
}
