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
using Xamarin.CommunityToolkit.UI.Views.Options;

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
            CodageMatriceEntry.MyEntryFocus(CodageMatriceFrame);
            CodageTextEntry.MyEntryFocus(CodageTextFrame);
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
                if (String.IsNullOrEmpty(CodageTextEntry.Text))
                    CodageTextEntry.Focus();
                else
                    await Codage();
            };
            CodageTextEntry.Completed += async delegate
            {
                if (String.IsNullOrEmpty(CodageMatriceEntry.Text))
                    CodageMatriceEntry.Focus();
                else
                    await Codage();
            };
            BtnCodage.Clicked += async delegate
            {
                await Codage();
            };
            EtapeCodageBtn.Clicked += async delegate
            {
                var item = CodagePicker.SelectedItem as UserModele;
                var userModele = App.UserModeleManager.GetByName(item.Nom);
                await this.Navigation.PushModalAsync(new EtapePage($"Codage de {CodageTextEntry.Text} avec la matrice A = {CodageMatriceEntry.Text}" +
                    $", modèle = {item.Nom} " +
                    $", base = {userModele.Valeur.Keys.Count}", "Codage de Hill", etapesCodage.ToList()));
                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
            };
            CodagePicker.SelectedItem = App.UserModeleManager.GetByName(UserSettings.UserModele);
            DecodagePicker.SelectedItem = App.UserModeleManager.GetByName(UserSettings.UserModele);
            #endregion

            #region Décodage
            DecodageMatriceEntry.Completed += async delegate
            {
                    if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                    DecodageTextEntry.Focus();
                else
                    await Decodage();
            };

            DecodageTextEntry.Completed += async delegate
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
                await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
            };
            EtapeDecodageBtn.Clicked += async delegate
            {
                var item = DecodagePicker.SelectedItem as UserModele;
                var userModele = App.UserModeleManager.GetByName(item.Nom);
                await this.Navigation.PushModalAsync(new EtapePage($"Décodage de {DecodageTextEntry.Text} avec la matrice = {DecodageMatriceEntry.Text}" +
                    $" modèle = {userModele.Nom}, "+
                    $", base = {userModele.Valeur.Keys.Count}", "Décodage de Hill", etapesDecodage.ToList()));
                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
            };
            #endregion


        }

        private async Task Decodage()
        {
            try
            {
                UserModele tem = CodagePicker.SelectedItem as UserModele;
                if (String.IsNullOrEmpty(DecodageMatriceEntry.Text))
                {

                    await DisplayAlert("Erreur", "Veuillez saisir la matrice de décodage.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
                else
                    if (!DecodageTextEntry.Text.IsInModele(tem.Nom))
                {
                    await DisplayAlert("Erreur", "La base choisie ne contient pas tous les caractères du text saisi.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                }
                else
                    if (String.IsNullOrEmpty(DecodageTextEntry.Text))
                {
                    await DisplayAlert("Erreur", "Veuillez saisir le text à décoder.", "Ok");
                    await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                }
                else
                {
                    (var matrice, string LaBase, string text) = (cryptoFonction.TransformationEnMatrice(DecodageMatriceEntry.Text), tem.Valeur.Keys.Count.ToString(), DecodageTextEntry.Text);
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
                            if (specialCaractere.IsOnlyAphabet())
                            {
                                string result = cryptoCode.DecodageHill(text, DecodageMatriceEntry.Text,  specialCaractere,tem.Nom);
                                decodageResult = result;
                                var etapes = cryptoEtape.DecodageDeHill(text, DecodageMatriceEntry.Text, specialCaractere,tem.Nom);
                                etapesDecodage.Clear();
                                DecodageResult.Text = $"La solution du décodage est : {result}";
                                etapes.ForEach(x =>
                                {
                                    etapesDecodage.Add(x);
                                });
                                await App.UserHistoriqueManager.Add(new UserHistorique()
                                {
                                    DateOperation = DateTime.Now,
                                    Libelle = "Décodage de Hill",
                                    Description = $"Décodage de Hill avec la matrice = {DecodageMatriceEntry.Text} modèle = {tem.Nom}",
                                    Data = text.TransformHistoriqueData(
                                      "Hill", "Decodage",
                                      new Dictionary<string, string>()
                                          {
                                                { "matrice" , $"{DecodageMatriceEntry.Text}" },
                                                { "special" , $"{specialCaractere}" },
                                                { "modele" , $"{tem.Nom}" },
                                          })
                                });
                                await ActivateAndAnimeBtn(BtnDecodage, EtapeDecodageBtn);
                                DecodageResult.IsVisible = true;
                            }
                            else
                            {
                                await DisplayAlert("Erreur", "Le caractère spécial doit être un alphabet.", "Ok");
                                await DeleteAnimation(BtnDecodage, EtapeDecodageBtn);
                            }
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
                UserModele tem = CodagePicker.SelectedItem as UserModele;
                    if (String.IsNullOrEmpty(CodageMatriceEntry.Text))
                {

                    await DisplayAlert("Erreur", "Veuillez saisir la matrice de codage.", "Ok");
                    await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                }
                else
                    if (!CodageTextEntry.Text.IsInModele(tem.Nom))
                {
                    await DisplayAlert("Erreur", "La base choisie ne contient pas tous les caractères du text saisi.", "Ok");
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
                    (var matrice, string LaBase, string text) = (cryptoFonction.TransformationEnMatrice(CodageMatriceEntry.Text), tem.Valeur.Keys.Count.ToString(), CodageTextEntry.Text);
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
                            if (specialCaractere.IsOnlyAphabet())
                            {
                                text = text.ToUpper();
                                string result = cryptoCode.CodageHill(text, CodageMatriceEntry.Text, specialCaractere,tem.Nom);
                                codageResult = result;
                                var etapes = cryptoEtape.CodageDeHill(text, CodageMatriceEntry.Text, specialCaractere,tem.Nom);
                                etapesCodage.Clear();
                                CodageResult.Text = $"La solution du codage est : {result}";
                                etapes.ForEach(x =>
                                {
                                    etapesCodage.Add(x);
                                });
                                await App.UserHistoriqueManager.Add(new UserHistorique()
                                {
                                    DateOperation = DateTime.Now,
                                    Libelle = "Codage de Hill",
                                    Description = $"Codage de Hill avec la matrice = {CodageMatriceEntry.Text} modèle = {tem.Nom}",
                                    Data = text.TransformHistoriqueData(
                                     "Hill", "Codage",
                                     new Dictionary<string, string>()
                                         {
                                                { "matrice" , $"{CodageMatriceEntry.Text}" },
                                                { "special" , $"{specialCaractere}" },
                                                { "modele" , $"{tem.Nom}" },
                                         })
                                });
                                await ActivateAndAnimeBtn(BtnCodage, EtapeCodageBtn);
                                CodageResult.IsVisible = true;

                            }
                            else
                            {
                                await DisplayAlert("Erreur", "Le caractère spécial doit être un alphabet.", "Ok");
                                await DeleteAnimation(BtnCodage, EtapeCodageBtn);
                            }
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
        private Task ActivateAndAnimeBtn(Button btnCodage, ImageButton btn)
        {
            _ = btnCodage.TranslateTo(-95, 0, 3000);
            btn.IsVisible = true;
            _ = btn.FadeTo(1, 3000);
            return Task.CompletedTask;
        }

        private Task DeleteAnimation(Button btnCodage, ImageButton btn)
        {
            btnCodage.Margin = new Thickness() { Left = 0 };
            btnCodage.TranslationX = 0;
            btnCodage.HorizontalOptions = new LayoutOptions() { Alignment = LayoutAlignment.Center, Expands = true };
            btn.FadeTo(0);
            return Task.CompletedTask;
        }
    }
}