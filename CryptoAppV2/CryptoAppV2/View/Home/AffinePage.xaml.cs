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
    public partial class AffinePage : ContentPage
    {
        CryptoFonction Fonction = new CryptoFonction();
        CryptoCode CryptoCode = new CryptoCode();
        CryptoEtape CryptoEtape = new CryptoEtape();
        ObservableCollection<Etape> etapesCodage = new ObservableCollection<Etape>();
        ObservableCollection<Etape> etapesDecodage = new ObservableCollection<Etape>();
        string codageResult = string.Empty;
        string decodageResult = string.Empty;
        public AffinePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AnimateCodageCaroussel();
            AnimateDecodageCaroussel();

           

            CodageResultStack.IsVisible = false;
            #region Codage
            #region Comportement de la Variable a

            MesControls.MyEntryFocus(CodageAEntry, CodageAFrame);
            CodageAEntry.Unfocused += delegate
            {
                if (String.IsNullOrEmpty(CodageAEntry.Text) || CodageAEntry.Text.Equals("0"))
                    CodageAEntry.Text = "1";
                if (int.Parse(CodageAEntry.Text) < 0) CodageAEntry.Text = $"{-1 * int.Parse(CodageAEntry.Text)}";
            };
            CodageAEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageBEntry.Text))
                    CodageBEntry.Focus();
                else
                    await Codage(CodingEntry);
            };

            #endregion

            #region Comportement de la variable b
            MesControls.MyEntryFocus(CodageBEntry, CodageBFrame);
            CodageBEntry.Unfocused += delegate
            {
                if (String.IsNullOrEmpty(CodageBEntry.Text))
                    CodageBEntry.Text = "3";
            };
            CodageBEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodingEntry.Text))
                    CodingEntry.Focus();
                else
                    await Codage(CodingEntry);
            };

            #endregion

            #region Comportement de la variable Base

            MesControls.MyPickerFocus(CodagePicker, CodageModeleFrame);

            #region Picker
            CodagePicker.SelectedItem = App.UserModeleManager.GetByName(UserSettings.UserModele);
            var tempd = CodagePicker.SelectedItem; 
            CodagePicker.SelectedIndexChanged += async delegate
            {
                var selectedItem = CodagePicker.SelectedItem as UserModele;
                await this.DisplayToastAsync($"La base du modèle choisi est {App.UserModeleManager.GetByName(selectedItem.Nom).Valeur.Keys.Count}.");
            };
            #endregion

            #endregion

            #region Comportement du boutton de codage
            MesControls.MyEntryFocus(CodingEntry, CodingFrame);
            CodingEntry.Completed += async delegate
            {
                await Codage(CodingEntry);
            };
            BtnCodage.Clicked += async delegate
            {
                await Codage(CodingEntry);
            };
            EtapeCodageBtn.Clicked += async delegate
            {
               UserModele tem = CodagePicker.SelectedItem as UserModele;
                await this.Navigation.PushModalAsync(new EtapePage($"Codage de {CodingEntry.Text} avec : a = {CodageAEntry.Text}" +
                    $", b = {CodageBEntry.Text}" +
                    $", modèle = {tem.Nom}" +
                    $", base = {App.UserModeleManager.GetByName(tem.Nom).Valeur.Keys.Count}", "Codage de Affine", etapesCodage.ToList()));
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            };

            #endregion

            #endregion
            _ = DeleteAnimation(BtnCodage, EtapeCodageBtn);
            #region Caroussel button
            BackDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position > 0)
                    MyDecodageCaroussel.Position--;
                else
                    MyDecodageCaroussel.Position = 7;
            };
            NextDecodageCarousselBtn.Clicked += delegate
            {
                if (MyDecodageCaroussel.Position == 7)
                    MyDecodageCaroussel.Position = 0;
                else
                    MyDecodageCaroussel.Position++;
            };
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
            #endregion

            #region Decodage


            #region Comportement de la Variable a

            MesControls.MyEntryFocus(DecodageAEntry, DecodageAFrame);
            DecodageAEntry.Unfocused += delegate
            {
                if (String.IsNullOrEmpty(DecodageAEntry.Text) || DecodageAEntry.Text.Equals("0"))
                    DecodageAEntry.Text = "1";
                if (int.Parse(DecodageAEntry.Text) < 0) DecodageAEntry.Text = $"{-1 * int.Parse(DecodageAEntry.Text)}";
            };
            DecodageAEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodageBEntry.Text))
                    DecodageBEntry.Focus();
                else
                    await Decodage(DecodingEntry);
            };

            #endregion

            #region Comportement de la variable b
            MesControls.MyEntryFocus(DecodageBEntry, DecodageBFrame);
            DecodageBEntry.Unfocused += delegate
            {
                if (String.IsNullOrEmpty(DecodageBEntry.Text))
                    DecodageBEntry.Text = "3";
            };
            DecodageBEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(DecodingEntry.Text))
                    DecodingEntry.Focus();
                else
                    await Decodage(DecodingEntry);
            };

            #endregion

            #region Comportement de la variable Base


            MesControls.MyPickerFocus(DecodagePicker, DecodageModeleFrame);

            #endregion

            #region Comportement du boutton de codage
            MesControls.MyEntryFocus(DecodingEntry, DecodingFrame);
            DecodingEntry.Completed += async delegate
            {
                await Decodage(DecodingEntry);
            };

            #endregion
            BtnDecodage.Clicked += async delegate
            {
                await Decodage(DecodingEntry);
            };
            EtapeDecodageBtn.Clicked += async delegate
            {
                UserModele tem = CodagePicker.SelectedItem as UserModele;
                await this.Navigation.PushModalAsync(new EtapePage($"Décodage de {DecodingEntry.Text} avec : a = {DecodageAEntry.Text}" +
                    $", b = {DecodageBEntry.Text}" +
                    $", modèle = {tem.Nom}" +
                    $", base = {App.UserModeleManager.GetByName(tem.Nom)?.Valeur.Keys.Count}", "Décodage Affine", etapesDecodage.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };
            _ = DeleteAnimation(BtnDecodage, EtapeDecodageBtn);

            #endregion
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
        public async Task Codage(Entry entry)
        {
            try
            {
                if (String.IsNullOrEmpty(entry.Text))
                    await DisplayAlert("Erreur", "Vous devez saisir le text à coder", "Ok");
                else
                {
                    UserModele tem = CodagePicker.SelectedItem as UserModele;
                    entry.Text = entry.Text.ToUpper();
                    if (entry.Text.IsInModele(tem.Nom))
                    {
                        if (Fonction.PremierEntreEux(int.Parse(CodageAEntry.Text.SubstringInteger()), App.UserModeleManager.GetByName(tem.Nom).Valeur.Keys.Count))
                        {
                            CodageResultStack.IsVisible = true;
                            etapesCodage.Clear();
                            var result = CryptoCode.CodageAffine(entry.Text, int.Parse(CodageAEntry.Text.SubstringInteger()),
                                int.Parse(CodageBEntry.Text.SubstringInteger()),tem.Nom);
                            await App.UserHistoriqueManager.Add(new UserHistorique()
                            {
                                DateOperation = DateTime.Now,
                                Libelle = "Codage Affine",
                                Description = $"Codage affine avec a = {int.Parse(CodageAEntry.Text)}, b = {int.Parse(CodageBEntry.Text)}, modèle = {tem.Nom}",
                                Data = entry.TransformHistoriqueData(
                                    "Affine", "Codage",
                                    new Dictionary<string, string>()
                                        {
                                            { "a" , $"{CodageAEntry.Text}" },
                                            { "b" , $"{CodageBEntry.Text}" },
                                            { "modele" , $"{tem.Nom}" },
                                        })
                           });
                            CodageResult.Text = "Le résultat du codage est " + result;
                            var temp = CryptoEtape.CodageAffine(entry.Text.SubstringString(), int.Parse(CodageAEntry.Text.SubstringInteger()),
                                int.Parse(CodageBEntry.Text.SubstringInteger()), tem.Nom);
                            temp.ForEach(x =>
                            {
                                etapesCodage.Add(x);
                            });
                            codageResult = result;
                            await ActivateAndAnimeBtn(BtnCodage, EtapeCodageBtn);
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "La variable a et la base doivent être premier entre eux.", "Ok");
                            await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                        }

                    }
                    else
                        await DisplayAlert("Erreur", "Une ou des caractères du texte ne sont pas présentes dans la base choisie.", "Ok");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un truc s'est mal passé, réessayer {ex.Message}.", "Ok");
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            }

        }
        public async Task Decodage(Entry entry)
        {
            try
            {
                if (String.IsNullOrEmpty(entry.Text))
                    await DisplayAlert("Erreur", "Vous devez saisir le text à décoder", "Ok");
                else
                {
                    UserModele tem = DecodagePicker.SelectedItem as UserModele;
                    entry.Text = entry.Text.ToUpper();
                    if (entry.Text.IsInModele(tem.Nom))
                    {
                        if (Fonction.PremierEntreEux(int.Parse(DecodageAEntry.Text.SubstringInteger()), App.UserModeleManager.GetByName(tem.Nom).Valeur.Keys.Count))
                        {
                            DecodageResultStack.IsVisible = true;
                            etapesDecodage.Clear();
                            var result = CryptoCode.DecodageAffine(entry.Text, int.Parse(DecodageAEntry.Text.SubstringInteger()),
                                int.Parse(DecodageBEntry.Text.SubstringInteger()), tem.Nom);
                            DecodageResult.Text = "Le résultat du décodage est " + result;
                            await App.UserHistoriqueManager.Add(new UserHistorique()
                            {
                                DateOperation = DateTime.Now,
                                Libelle = "Décodage Affine",
                                Description = $"Décodage affine avec a = {int.Parse(DecodageAEntry.Text)}, b = {int.Parse(DecodageBEntry.Text)}, modèle = {tem.Nom}",
                                Data = entry.TransformHistoriqueData(
                                   "Affine", "Decodage",
                                   new Dictionary<string, string>()
                                       {
                                            { "a" , $"{DecodageAEntry.Text}" },
                                            { "b" , $"{DecodageBEntry.Text}" },
                                            { "modele" , $"{tem.Nom}" },
                                       })
                            });
                            var temp = CryptoEtape.DecodageAffine(entry.Text.SubstringString(), int.Parse(DecodageAEntry.Text.SubstringInteger()),
                                int.Parse(DecodageBEntry.Text.SubstringInteger()), tem.Nom);
                            temp.ForEach(x =>
                            {
                                etapesDecodage.Add(x);
                            });
                            decodageResult = result;
                            await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "La variable a et la base doivent être premier entre eux", "Ok");
                            await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                        }
                    }
                    else
                       await DisplayAlert("Erreur", "Une ou des caractères du texte ne sont pas présentes dans la base choisie.", "Ok");
                   
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Un truc s'est mal passé, réessayer {ex.Message}.", "Ok");
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            }

        }
        private async void ResultCodageTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(codageResult).ContinueWith(async (ord) => await this.DisplayToastAsync("Résultat du codage copié avec succès.", 3000));
        private async void DecodageResultTap_Tapped(object sender, EventArgs e) =>
            await Clipboard.SetTextAsync(decodageResult).ContinueWith(async (ord) => await this.DisplayToastAsync("Résultat du décodage copié avec succès.", 3000));
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

    }
}