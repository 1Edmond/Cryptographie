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

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RSAPage : ContentPage
    {
        private CryptoCode cryptoCode = new CryptoCode();
        private CryptoEtape cryptoEtape = new CryptoEtape();
        private CryptoFonction cryptoFonction = new CryptoFonction();
        private string codageResult = String.Empty;
        private string decodageResult = String.Empty;
        ObservableCollection<Etape> etapesCodage = new ObservableCollection<Etape>();
        ObservableCollection<Etape> etapesDecodage = new ObservableCollection<Etape>();

        public RSAPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AnimateCodageCaroussel();
            AnimateDecodageCaroussel();
            CodageNEntry.MyEntryFocus(CodageNFrame);
            CodageTextEntry.MyEntryFocus(CodageTextFrame);
            CodageEEntry.MyEntryFocus(CodageEFrame);
            DecodageEEntry.MyEntryFocus(DecodageEFrame);
            DecodageTextEntry.MyEntryFocus(DecodageTextFrame);
            DecodageNEntry.MyEntryFocus(DecodageNFrame);
            #region Caroussel button
            BackCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position > 0)
                    MyCodageCaroussel.Position--;
                else
                    MyCodageCaroussel.Position = 4;
            };
            NextCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position == 4)
                    MyCodageCaroussel.Position = 0;
                else
                    MyCodageCaroussel.Position++;
            };
            BackDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position > 0)
                    MyDecodageCaroussel.Position--;
                else
                    MyDecodageCaroussel.Position = 4;
            };
            NextDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position == 4)
                    MyDecodageCaroussel.Position = 0;
                else
                    MyDecodageCaroussel.Position++;
            };
            #endregion

            #region Codage
            CodageEEntry.Completed += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(CodageNEntry.Text))
                    CodageNEntry.Focus();
                else
                    if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };

            CodageNEntry.Completed += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    if (String.IsNullOrEmpty(CodageNEntry.Text))
                    CodageNEntry.Focus();
                else
                    await Codage();
            };

            CodageTextEntry.Completed += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(CodageEEntry.Text))
                    CodageEEntry.Focus();
                else
                    if (String.IsNullOrEmpty(CodageNEntry.Text))
                    CodageNEntry.Focus();
                else
                    await Codage();
            };

            BtnCodage.Clicked += async delegate
            {
                await Codage();
            };

            EtapeCodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Codage de {CodageTextEntry.Text} avec : n = {DecodageNEntry.Text}" +
                   $", e = {CodageEEntry.Text}", "Codage RSA", etapesCodage.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };

            #endregion

            #region Décodage
            DecodageEEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageNEntry.Text))
                    DecodageNEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };

            DecodageNEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageEEntry.Text))
                    DecodageEEntry.Focus();
                else
                    await Decodage();

            };

            DecodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageEEntry.Text))
                    DecodageEEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageNEntry.Text))
                    DecodageNEntry.Focus();
                else
                    await Decodage();
            };

            BtnDecodage.Clicked += async delegate
            {
                await Decodage();
            };

            EtapeDecodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Décodage de {DecodageTextEntry.Text} avec : n = {DecodageNEntry.Text}" +
                    $", e = {DecodageEEntry.Text}", "Décodage RSA", etapesDecodage.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };

            #endregion

        }

        private async Task Decodage()
        {
            try
            {
                if (!String.IsNullOrEmpty(DecodageEEntry.Text))
                {
                    if (!String.IsNullOrEmpty(DecodageNEntry.Text))
                    {
                        if (!String.IsNullOrEmpty(DecodageTextEntry.Text))
                        {
                            var (text, n, e) = (DecodageTextEntry.Text.SubstringInteger(), DecodageNEntry.Text.SubstringInteger(), DecodageEEntry.Text.SubstringInteger());
                            var PetQ = cryptoFonction.RSAPEtQ(n);
                            if (!PetQ.Equals(""))
                            {
                                int phie = cryptoFonction.RSAPEtQ(n).Split(',').ToList().ProduitRSaList();
                                if (int.Parse(e) >= 1)
                                {
                                    if (cryptoFonction.PremierEntreEux(phie, int.Parse(e)))
                                    {
                                        etapesDecodage.Clear();
                                        string result = cryptoCode.DecodageRSA(text, n, e);
                                        DecodageResultStack.IsVisible = true;
                                        decodageResult = result;
                                        DecodageResult.Text = "Le résultat du décodage est " + result;
                                        var temp = cryptoEtape.DecodageRSA(text, n, e);
                                        temp.ForEach(x =>
                                        {
                                            etapesDecodage.Add(x);
                                        });
                                        await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);

                                    }
                                    else
                                    {
                                        await DisplayAlert("Erreur", $"La varaible e : {e} doit être premier avec φ(n = {n}) = {phie}.", "Ok");
                                        await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                                    }
                                }
                                else
                                {
                                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                                    await DisplayAlert("Erreur", $"La varaible e : {e} doit être supérieur ou égale à 1.", "Ok");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Erreur", $"La variable n : {n} n'admet pas de decompositon en facteur premier.", "Ok");
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
                        await DisplayAlert("Erreur", "Vous devez saisir la variable n.", "Ok");
                        await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                    }

                }
                else
                {
                    await DisplayAlert("Erreur", "Vous devez saisir la variable e.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un problème est survenu, réessayer {ex.Message}.", "Ok");
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            }
        }

        private async Task Codage()
        {
            try
            {
                if (!String.IsNullOrEmpty(CodageEEntry.Text))
                {
                    if (!String.IsNullOrEmpty(CodageNEntry.Text))
                    {
                        if (!String.IsNullOrEmpty(CodageTextEntry.Text))
                        {
                            var (text, n, e) = (CodageTextEntry.Text.SubstringInteger(), CodageNEntry.Text.SubstringInteger(), CodageEEntry.Text.SubstringInteger());

                            var PetQ = cryptoFonction.RSAPEtQ(n);
                            if (!PetQ.Equals(""))
                            {
                                int phie = PetQ.Split(',').ToList().ProduitRSaList();
                                if (int.Parse(e) >= 1)
                                {
                                    if (cryptoFonction.PremierEntreEux(phie, int.Parse(e)))
                                    {
                                        etapesCodage.Clear();
                                        string result = cryptoCode.CodageRSA(text, n, e);
                                        CodageResultStack.IsVisible = true;
                                        codageResult = result;
                                        CodageResult.Text = "Le résultat du codage est " + result;
                                        var temp = cryptoEtape.CodageRSA(text, n, e);
                                        temp.ForEach(x =>
                                        {
                                            etapesCodage.Add(x);
                                        });
                                        await ActivateAndAnimeBtn(BtnCodage, EtapeCodageBtn);

                                    }
                                    else
                                    {
                                        await DisplayAlert("Erreur", $"La varaible e : {e} doit être premier avec φ(n = {n}) = {phie}.", "Ok");
                                        await DeleteAnimation(BtnCodage, EtapeCodageBtn);

                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Erreur", $"La varaible e : {e} doit être supérieur ou égale à 1.", "Ok");
                                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                                }
                            }
                            else
                            {
                                await DisplayAlert("Erreur", $"La variable n : {n} n'admet pas de decompositon en facteur premier .", "Ok");
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
                        await DisplayAlert("Erreur", "Vous devez saisir la variable n.", "Ok");
                        await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                    }

                }
                else
                {
                    await DisplayAlert("Erreur", "Vous devez saisir la variable e.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un problème est survenu, réessayer {ex.Message}.", "Ok");
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            }
        }

        public void AnimateCodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyCodageCaroussel.Position == 4)
                    MyCodageCaroussel.Position = 0;
                MyCodageCaroussel.Position++;
            };
        }

        private async void ResultCodageTap_Tapped(object sender, EventArgs e) =>
           await Clipboard.SetTextAsync(codageResult).ContinueWith(async (ord) => await this.DisplayToastAsync("Résultat du codage copié avec succès.", 3000));

        private async void DecodageResultTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(decodageResult).ContinueWith(async (ord) => await this.DisplayToastAsync("Résultat du décodage copié avec succès.", 3000));
        public void AnimateDecodageCaroussel()
        {
            Timer timer = new Timer(10000) { AutoReset = true, Enabled = true };
            timer.Elapsed += (s, e) =>
            {
                if (MyDecodageCaroussel.Position == 4)
                    MyDecodageCaroussel.Position = 0;
                MyDecodageCaroussel.Position++;
            };
        }

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