using CryptoAppV2.CrAlgorithme;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoAppV2.Custom;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HillPage : ContentPage
    {
        private CryptoCode cryptoCode = new CryptoCode();
        private CryptoEtape cryptoEtape = new CryptoEtape();
        private CryptoFonction cryptoFonction = new CryptoFonction();
        private string codageResult = String.Empty;
        private string decodageResult = String.Empty;
        ObservableCollection<Etape> etapesCodage = new ObservableCollection<Etape>();
        ObservableCollection<Etape> etapesDecodage = new ObservableCollection<Etape>();

        public HillPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AnimateCodageCaroussel();
            AnimateDecodageCaroussel();
            #region Gestion des Annimations de focus
            CodageBaseEntry.MyEntryFocus(CodageBaseFrame);
            CodageMatriceEntry.MyEntryFocus(CodageMatriceFrame);
            CodageTextEntry.MyEntryFocus(CodageTextFrame);
            DecodageBaseEntry.MyEntryFocus(DecodageBaseFrame);
            DecodageMatriceEntry.MyEntryFocus(DecodageMatriceFrame);
            DecodageTextEntry.MyEntryFocus(DecodageTextFrame);
            #endregion
            BackCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position > 0)
                    MyCodageCaroussel.Position--;
                else
                    MyCodageCaroussel.Position = 7;
            };
            NextCodageCarousselBtn.Clicked += delegate
            {
                if (MyCodageCaroussel.Position == 7)
                    MyCodageCaroussel.Position = 0;
                else
                    MyCodageCaroussel.Position++;
            };
            BackDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position > 0)
                    MyDecodageCaroussel.Position--;
                else
                    MyDecodageCaroussel.Position = 5;
            };
            NextDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position == 5)
                    MyDecodageCaroussel.Position = 0;
                else
                    MyDecodageCaroussel.Position++;
            };

            #region Codage
            CodageMatriceEntry.Completed += async delegate
            {
                if (string.IsNullOrEmpty(CodageBaseEntry.Text))
                    CodageBaseEntry.Focus();
                else
                    if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };
            CodageBaseEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    if (String.IsNullOrEmpty(CodageMatriceEntry.Text))
                    CodageMatriceEntry.Focus();
                else
                    await Codage();
            };
            CodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageMatriceEntry.Text))
                    CodageMatriceEntry.Focus();
                else
                    if (String.IsNullOrWhiteSpace(CodageBaseEntry.Text))
                    CodageBaseEntry.Focus();
                else
                    await Codage();
            };
            BtnCodage.Clicked += async delegate
            {
                await Codage();
            };
            EtapeCodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Codage de {CodageTextEntry.Text} avec la matrice A = {CodageMatriceEntry.Text}" +
                    $", base = {CodageBaseEntry.Text}", "Codage de Hill", etapesCodage.ToList()));
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            };

            #endregion

            #region Décodage
            DecodageMatriceEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageBaseEntry.Text))
                    DecodageBaseEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };
            DecodageBaseEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageMatriceEntry.Text))
                    DecodageMatriceEntry.Focus();
                else
                    await Decodage();
            };
            DecodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageMatriceEntry.Text))
                    DecodageBaseEntry.Focus();
                else
                    if (String.IsNullOrEmpty(DecodageBaseEntry.Text))
                    DecodageBaseEntry.Focus();
                else
                    await Decodage();
            };
            BtnDecodage.Clicked += async delegate
            {
                await Decodage();
            };
            EtapeDecodageBtn.Clicked += async delegate
            {
                await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
            };
            EtapeDecodageBtn.Clicked += async delegate
            {
                await this.Navigation.PushModalAsync(new EtapePage($"Décodage de {DecodageTextEntry.Text} avec la matrice = {DecodageMatriceEntry.Text}" +
                    $", base = {DecodageBaseEntry.Text}", "Décodage de Hill", etapesDecodage.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };
            #endregion


        }

        private async Task Decodage()
        {
            try
            {
                if (String.IsNullOrEmpty(DecodageMatriceEntry.Text))
                {

                    await DisplayAlert("Erreur", "Veuillez saisir la matrice de décodage.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
                else
                    if (String.IsNullOrEmpty(DecodageBaseEntry.Text))
                {
                    await DisplayAlert("Erreur", "Veuillez saisir la base.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);

                }
                else
                    if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                {
                    await DisplayAlert("Erreur", "Veuillez saisir le text à décoder.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
                else
                {
                    (var matrice, string LaBase, string text) = (cryptoFonction.TransformationEnMatrice(DecodageMatriceEntry.Text), DecodageBaseEntry.Text.SubstringInteger(), DecodageTextEntry.Text);
                    if (cryptoFonction.IsSquareMatrice(DecodageMatriceEntry.Text))
                    {
                        if (int.Parse(cryptoFonction.MatriceDeterminant(DecodageMatriceEntry.Text)) != 0)
                        {
                            string specialCaractere = "x";
                            if (text.Length % matrice.Keys.Count != 0)
                            {

                                try
                                {
                                    var temp = DisplayPromptAsync("Oups",
                                             $"La longueur du texte ne permet pas de décoder le message par bloc de {matrice.Keys.Count} éléments. Le caractère par défaut ajouté est x.",
                                             "OK",
                                             "Annuler",
                                             "Saisissez la valeur", maxLength: DecodageMatriceEntry.Text.Length % matrice.Keys.Count, keyboard: Keyboard.Text, initialValue: "x");
                                    await temp.ContinueWith((ird) =>
                                    {
                                        specialCaractere = temp.Result;
                                    });
                                }
                                catch (Exception ex)
                                {
                                    await DisplayAlert("Erreur1", $"{ex.Message}.", "Ok");
                                }
                            }
                            if (String.IsNullOrEmpty(specialCaractere))
                                specialCaractere = "x";
                            string result = cryptoCode.DecodageHill(text, DecodageMatriceEntry.Text,  specialCaractere);
                            decodageResult = result;
                            var etapes = cryptoEtape.DecodageDeHill(text, DecodageMatriceEntry.Text, LaBase, specialCaractere);
                            etapesDecodage.Clear();
                            DecodageResult.Text = $"La solution du décodage est : {result}";
                            etapes.ForEach(x =>
                            {
                                etapesDecodage.Add(x);
                            });
                            await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
                            DecodageResult.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Le déterminant de la matrice doit être différent de 0.", "Ok");
                            await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                        }

                    }
                    else
                    {
                        await DisplayAlert("Erreur", "La matrice saisie n'est pas une matrice carrée.", "Ok");
                        await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un problème est survenu {ex.Message}.", "Ok");
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            }


        }

        private async Task Codage()
        {
            try
            {
                if (String.IsNullOrEmpty(CodageMatriceEntry.Text))
                {

                    await DisplayAlert("Erreur", "Veuillez saisir la matrice de codage.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                }
                else
                    if (String.IsNullOrEmpty(CodageBaseEntry.Text))
                {
                    await DisplayAlert("Erreur", "Veuillez saisir la base.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);

                }
                else
                    if (String.IsNullOrEmpty(CodageTextEntry.Text))
                {
                    await DisplayAlert("Erreur", "Veuillez saisir le text à coder.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                }
                else
                {
                    (var matrice, string LaBase, string text) = (cryptoFonction.TransformationEnMatrice(CodageMatriceEntry.Text), CodageBaseEntry.Text.SubstringInteger(), CodageTextEntry.Text);
                    if (cryptoFonction.IsSquareMatrice(CodageMatriceEntry.Text))
                    {
                        if (int.Parse(cryptoFonction.MatriceDeterminant(CodageMatriceEntry.Text)) != 0)
                        {
                            string specialCaractere = "x";
                            if (text.Length % matrice.Keys.Count != 0)
                            {

                                try
                                {
                                    var temp = DisplayPromptAsync("Oups",
                                             $"La longueur du texte ne permet pas de coder le message par bloc de {matrice.Keys.Count} éléments. Le caractère par défaut ajouté est x.",
                                             "OK",
                                             "Annuler",
                                             "Saisissez la valeur", maxLength: CodageMatriceEntry.Text.Length % matrice.Keys.Count, keyboard: Keyboard.Text, initialValue: "x");
                                    await temp.ContinueWith((ird) =>
                                    {
                                        specialCaractere = temp.Result;
                                    });
                                }
                                catch (Exception ex)
                                {
                                    await DisplayAlert("Erreur1", $"{ex.Message}.", "Ok");
                                }
                            }
                            if (String.IsNullOrEmpty(specialCaractere))
                                specialCaractere = "x";
                            string result = cryptoCode.CodageHill(text, CodageMatriceEntry.Text, specialCaractere);
                            codageResult = result;
                            var etapes = cryptoEtape.CodageDeHill(text, CodageMatriceEntry.Text, LaBase, specialCaractere);
                            etapesCodage.Clear();
                            CodageResult.Text = $"La solution du codage est : {result}";
                            etapes.ForEach(x =>
                            {
                                etapesCodage.Add(x);
                            });
                            await ActivateAndAnimeBtn(BtnCodage, EtapeCodageBtn);
                            CodageResult.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Le déterminant de la matrice doit être différent de 0.", "Ok");
                            await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                        }

                    }
                    else
                    {
                        await DisplayAlert("Erreur", "La matrice saisie n'est pas une matrice carrée.", "Ok");
                        await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un problème est survenu {ex.Message}.", "Ok");
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
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
                if (MyDecodageCaroussel.Position == 5)
                    MyDecodageCaroussel.Position = 0;
                MyDecodageCaroussel.Position++;
            };
        }
        private async void ResultCodageTap_Tapped(object sender, EventArgs e) =>
          await Clipboard.SetTextAsync(codageResult).ContinueWith(async (ord) => await this.DisplayToastAsync("Résultat du codage copié avec succès.", 3000));

        private async void DecodageResultTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(decodageResult).ContinueWith(async (ord) => await this.DisplayToastAsync("Résultat du décodage copié avec succès.", 3000));
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