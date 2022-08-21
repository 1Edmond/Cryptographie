using CryptoAppV2.CrAlgorithme;
using CryptoAppV2.Custom;
using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPage : Popup
    {
        private CryptoFonction Fonction = new CryptoFonction();
        CryptoEtape Etapes = new CryptoEtape();

        public PopUpPage()
        {
            InitializeComponent();
        }
        public PopUpPage(string text)
        {
            InitializeComponent();
            var listes = new List<ListView>()
            {
                AffineBonneCleEtape,
                BinaireEtape,
                ChiffrementEtape,
                ChiffrementEtape,
                DecompositionFacteurEtape,
                DeuxPremierEtape,
                DiviseurEtape,
                ExponentiationModulaireEtape,
                FrequenceCaractereEtape,
                MatriceDeterminantEtape,
                MatriceInverseEtape,
                MerklePriveEtape,
                RSAPriveEtape,
                RSASignatureEtape,
                SacADosEtape,
                TrouverPEtQEtape,
                UnPremierEtape
            };
            foreach (var element in listes)
                InfoPop(element);

            switch (text)
            {
                case "Diviseur":
                    {
                        Diviseur.IsVisible = true;
                        MesControls.MyEntryFocus(DiviseurEntry, DiviseurFrame);
                        DiviseurEntry.Completed += async delegate
                        {
                            await this.Navigation.PushAsync(new EtapePage());
                            await DiviseurField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await this.Navigation.PushAsync(new EtapePage());
                            await DiviseurField();
                        };
                    }
                    break;
                case "Inverse modulaire":
                    {
                        InverseModulaire.IsVisible = true;
                        MesControls.MyEntryFocus(InverseModulaireModuleEntry, InverseModulaireModuleFrame);
                        MesControls.MyEntryFocus(InverseModulaireNbrEntry, InverseModulaireNbrFrame);
                        InverseModulaireModuleEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(InverseModulaireNbrEntry.Text))
                                InverseModulaireNbrEntry.Focus();
                            else
                                await InverseModulaireField();
                        };
                        InverseModulaireNbrEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(InverseModulaireModuleEntry.Text))
                                InverseModulaireModuleEntry.Focus();
                            else
                                await InverseModulaireField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await InverseModulaireField();
                        };
                    }
                    break;
                case "Vérification premier":
                    {
                        UnPremier.IsVisible = true;
                        MesControls.MyEntryFocus(UnPremierEntry, UnPremierFrame);
                        UnPremierEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(UnPremierEntry.Text))
                                UnPremierEntry.Focus();
                            else
                                await UnPremierField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await UnPremierField();
                        };
                    }
                    break;
                case "Vérification primalité entre deux nombres":
                    {
                        DeuxPremier.IsVisible = true;
                        MesControls.MyEntryFocus(DeuxPremierNbr1Entry, DeuxPremierNbr1Frame);
                        MesControls.MyEntryFocus(DeuxPremierNbr2Entry, DeuxPremierNbr2Frame);
                        DeuxPremierNbr1Entry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(DeuxPremierNbr2Entry.Text))
                                DeuxPremierNbr2Entry.Focus();
                            else
                                await DeuxPremierField();
                        };
                        DeuxPremierNbr2Entry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(DeuxPremierNbr1Entry.Text))
                                DeuxPremierNbr1Entry.Focus();
                            else
                                await DeuxPremierField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await DeuxPremierField();
                        };
                    }
                    break;
                case "Chhiffrement":
                    {
                        Chiffrement.IsVisible = true;
                        MesControls.MyEntryFocusPicker(ChiffrementBaseEntry, ChiffrementBaseFrame);
                        MesControls.MyEntryFocus(ChiffrementCaractereEntry, ChiffrementCaractereFrame);
                        ChiffrementCaractereEntry.Completed += async delegate
                        {
                            await ChiffrementField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await ChiffrementField();
                        };
                    }
                    break;
                case "Sac à dos":
                    {
                        SacADos.IsVisible = true;
                        MesControls.MyEntryFocus(SacADosSommeEntry, SacADosSommeFrame);
                        MesControls.MyEntryFocus(SacADosSuiteEntry, SacADosSuiteFrame);
                        SacADosSommeEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(SacADosSuiteEntry.Text))
                                SacADosSuiteEntry.Focus();
                            else
                                await SacADosField();
                        };
                        SacADosSuiteEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(SacADosSommeEntry.Text))
                                SacADosSommeEntry.Focus();
                            else
                                await SacADosField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await SacADosField();
                        };
                    }
                    break;
                case "Clé publique (RSA)":
                    {

                        RSAPrive.IsVisible = true;
                        MesControls.MyEntryFocus(RSAPriveEEntry, RSAPriveEFrame);
                        MesControls.MyEntryFocus(RSAPriveNEntry, RSAPriveNFrame);
                        RSAPriveNEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(RSAPriveEEntry.Text))
                                RSAPriveEEntry.Focus();
                            else
                                await ClePiveRSAField();
                        };
                        RSAPriveEEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(RSAPriveNEntry.Text))
                                RSAPriveNEntry.Focus();
                            else
                                await ClePiveRSAField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await ClePiveRSAField();
                        };


                    }
                    break;
                case "Clé publique (Merkle-Hellman)":
                    {
                        MerklePrive.IsVisible = true;
                        MesControls.MyEntryFocus(MerklePriveMEntry, MerklePriveMFrame);
                        MesControls.MyEntryFocus(MerklePriveNEntry, MerklePriveNFrame);
                        MesControls.MyEntryFocus(MerklePriveSuiteEntry, MerklePriveSuiteFrame);
                        MerklePriveMEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(MerklePriveNEntry.Text))
                                MerklePriveNEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(MerklePriveSuiteEntry.Text))
                                MerklePriveSuiteEntry.Focus();
                            else
                                await ClePiveMerkleHellmanField();
                        };
                        MerklePriveNEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(MerklePriveSuiteEntry.Text))
                                MerklePriveSuiteEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(MerklePriveMEntry.Text))
                                MerklePriveMEntry.Focus();
                            else
                                await ClePiveMerkleHellmanField();
                        };
                        MerklePriveSuiteEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(MerklePriveNEntry.Text))
                                MerklePriveNEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(MerklePriveMEntry.Text))
                                MerklePriveMEntry.Focus();
                            else
                                await ClePiveMerkleHellmanField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await ClePiveMerkleHellmanField();
                        };
                    }
                    break;
                case "Signature RSA":
                    {
                        RSASignature.IsVisible = true;
                        MesControls.MyEntryFocus(RSASignatureEEntry, RSASignatureEFrame);
                        MesControls.MyEntryFocus(RSASignatureMessageEntry, RSASignatureMessageFrame);
                        MesControls.MyEntryFocus(RSASignatureNEntry, RSASignatureNFrame);
                        RSASignatureEEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(RSASignatureMessageEntry.Text))
                                RSASignatureMessageEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(RSASignatureNEntry.Text))
                                RSASignatureNEntry.Focus();
                            else
                                await SignatureRSAField();
                        };
                        RSASignatureNEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(RSASignatureEEntry.Text))
                                RSASignatureEEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(RSASignatureMessageEntry.Text))
                                RSASignatureMessageEntry.Focus();
                            else
                                await SignatureRSAField();
                        };
                        RSASignatureMessageEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(RSASignatureNEntry.Text))
                                RSASignatureNEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(RSASignatureEEntry.Text))
                                RSASignatureEEntry.Focus();
                            else
                                await SignatureRSAField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await SignatureRSAField();
                        };
                    }
                    break;
                case "Bonnes clés (Le code Affine)":
                    {
                        AffineBonneCle.IsVisible = true;
                        MesControls.MyEntryFocus(AffineBonneCleEntry, AffineBonneCleFrame);
                        AffineBonneCleEntry.Completed += async delegate
                        {
                            await AffineBonneCLeField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await AffineBonneCLeField();
                        };
                    }
                    break;
                case "Déterminant d'une matrice":
                    {
                        MatriceDeterminant.IsVisible = true;
                        MesControls.MyEntryFocus(MatriceDeterminantEntry, MatriceDeterminantFrame);
                        MatriceDeterminantEntry.Completed += async delegate
                        {
                            await MatriceDeterminantField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await MatriceDeterminantField();
                        };
                    }
                    break;
                case "Inverse d'une matrice":
                    {
                        MatriceInverse.IsVisible = true;
                        MesControls.MyEntryFocus(MatriceInverseEntry, MatriceInverseFrame);
                        MesControls.MyEntryFocus(MatriceInverseBaseEntry, MatriceInverseBaseFrame);

                        MatriceInverseEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(MatriceInverseBaseEntry.Text))
                                MatriceInverseBaseEntry.Focus();
                            else
                                await MatriceInverseField();
                        };
                        MatriceInverseBaseEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(MatriceInverseEntry.Text))
                                MatriceInverseEntry.Focus();
                            else
                                await MatriceInverseField();
                        };

                        Valider.Clicked += async delegate
                        {
                            await MatriceInverseField();
                        };
                    }
                    break;
                case "Binaire d'un nombre":
                    {
                        Binaire.IsVisible = true;
                        MesControls.MyEntryFocus(BinaireEntry, BinaireFrame);
                        BinaireEntry.Completed += async delegate
                        {
                            await BinaireField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await BinaireField();
                        };
                    }
                    break;
                case "Trouver p et q (RSA)":
                    {
                        TrouverPEtQ.IsVisible = true;
                        MesControls.MyEntryFocus(TrouverPEtQEntry, TrouverPEtQFrame);
                        TrouverPEtQEntry.Completed += async delegate
                        {
                            await RSAPEtQField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await RSAPEtQField();
                        };
                    }
                    break;
                case "Décomposition en facteurs premiers":
                    {
                        DecompositionFacteur.IsVisible = true;
                        MesControls.MyEntryFocus(DecompositionFacteurEntry, DecompositionFacteurFrame);
                        DecompositionFacteurEntry.Completed += async delegate
                        {
                            await DecompositionFacteurField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await DecompositionFacteurField();
                        };
                    }
                    break;
                case "Fréquence de caractère":
                    {
                        FrequenceCaractere.IsVisible = true;
                        MesControls.MyEntryFocus(FrequenceCaractereEntry, FrequenceCaractereFrame);
                        FrequenceCaractereEntry.Completed += async delegate
                        {
                            await FrequenceCaractereField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await FrequenceCaractereField();
                        };
                    }
                    break;
                case "Exponentiation Modulaire":
                    {
                        ExponentiationModulaire.IsVisible = true;
                        MesControls.MyEntryFocus(ExponentiationModulaireExEntry, ExponentiationModulaireExFrame);
                        MesControls.MyEntryFocus(ExponentiationModulaireModuleEntry, ExponentiationModulaireModuleFrame);
                        MesControls.MyEntryFocus(ExponentiationModulaireNbrEntry, ExponentiationModulaireNbrFrame);
                        ExponentiationModulaireModuleEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(ExponentiationModulaireNbrEntry.Text))
                                ExponentiationModulaireNbrEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(ExponentiationModulaireExEntry.Text))
                                ExponentiationModulaireExEntry.Focus();
                            else
                                await ExponentiationModulaireField();
                        };
                        ExponentiationModulaireNbrEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(ExponentiationModulaireExEntry.Text))
                                ExponentiationModulaireExEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(ExponentiationModulaireModuleEntry.Text))
                                ExponentiationModulaireModuleEntry.Focus();
                            else
                                await ExponentiationModulaireField();
                        };
                        ExponentiationModulaireExEntry.Completed += async delegate
                        {
                            if (String.IsNullOrEmpty(ExponentiationModulaireModuleEntry.Text))
                                ExponentiationModulaireModuleEntry.Focus();
                            else
                                if (String.IsNullOrEmpty(ExponentiationModulaireNbrEntry.Text))
                                ExponentiationModulaireNbrEntry.Focus();
                            else
                                await ExponentiationModulaireField();
                        };
                        Valider.Clicked += async delegate
                        {
                            await ExponentiationModulaireField();
                        };


                    }
                    break;
            }
            MyHeader.Text = text;
            Quitter.Clicked += delegate
            {
                Dismiss("");
            };
        }
        private async Task DiviseurField()
        {

            try
            {
                if (!String.IsNullOrEmpty(DiviseurEntry.Text))
                {
                    int nbr = int.Parse(DiviseurEntry.Text.SubstringInteger());
                    if (nbr != 0)
                    {
                        if (nbr < 0) nbr *= -1;
                        DiviseurStack.IsVisible = true;
                        var result = Fonction.Diviseur(nbr);
                        if (result.Count() > 1)
                            DiviseurResult.Text = $"Les diviseurs de {nbr} sont : {String.Join(", ", result)}.";
                        else
                            DiviseurResult.Text = $"Le diviseur de {nbr} est {String.Join(",", result)}";
                        var etapes = Etapes.EtapeDiviseur(nbr.ToString());
                        DiviseurEtape.BindingContext = this;
                        DiviseurEtape.ItemsSource = etapes;
                        DiviseurListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                        DiviseurEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        DiviseurEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        DiviseurResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                        await BtnFrame.DisplayToastAsync("L'on ne peut chercher les diviseurs de 0", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur", 3000);

            }
            catch (Exception)
            {
                DiviseurStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);
            }

        }
        private async Task UnPremierField()
        {
            try
            {
                if (!String.IsNullOrEmpty(UnPremierEntry.Text))
                {
                    int nbr = int.Parse(UnPremierEntry.Text.SubstringInteger());
                    if (nbr != 0)
                    {
                        //if (nbr < 0) nbr *= -1;
                        UnPremierStack.IsVisible = true;
                        var result = Fonction.Premier(nbr);
                        if (result)
                            UnPremierResult.Text = $"Le nombre {nbr} est premier.";
                        else
                            UnPremierResult.Text = $"Le nombre {nbr} n'est pas premier.";
                        var etapes = Etapes.EtapeDiviseur(nbr.ToString());
                        UnPremierEtape.BindingContext = this;
                        UnPremierEtape.ItemsSource = etapes;
                        if (!result)
                            UnPremierListeResult.Text = $"Le nombre {nbr} a {Fonction.Diviseur(nbr).Count} diviseurs donc il n'est pas premier.";
                        else
                            UnPremierListeResult.Text = $"Le nombre {nbr} a {Fonction.Diviseur(nbr).Count} diviseurs 1 et {nbr} lui même donc il est premier.";

                        UnPremierEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        UnPremierEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        UnPremierResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                        await BtnFrame.DisplayToastAsync("L'on ne peut chercher les diviseurs de 0", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur", 3000);

            }
            catch (Exception)
            {
                UnPremierStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task DeuxPremierField()
        {
            try
            {
                if (!String.IsNullOrEmpty(DeuxPremierNbr1Entry.Text) && !String.IsNullOrEmpty(DeuxPremierNbr2Entry.Text))
                {
                    int nbr1 = int.Parse(DeuxPremierNbr1Entry.Text.SubstringInteger());
                    int nbr2 = int.Parse(DeuxPremierNbr2Entry.Text.SubstringInteger());
                    if (nbr1 != 0 && nbr2 != 0)
                    {
                        if (nbr1 < 0) nbr1 *= -1;
                        if (nbr2 < 0) nbr2 *= -1;
                        var result = Fonction.PremierEntreEux(nbr1, nbr2);
                        DeuxPremierStack.IsVisible = true;
                        if (result)
                        {
                            DeuxPremierResult.Text = $"Le nombre {nbr1} et {nbr2} sont premiers entre eux.";
                            DeuxPremierListeResult.Text = $"Le nombre {nbr1} et {nbr2} sont premier entre car diviseur commun ";

                        }
                        else
                        {
                            var div1 = Fonction.Diviseur(nbr1);
                            var div2 = Fonction.Diviseur(nbr2);
                            var commun = new List<int>();
                            div1.ForEach(x =>
                            {
                                if (div2.Contains(x))
                                    commun.Add(x);
                            });
                            DeuxPremierResult.Text = $"Le nombre {nbr1} et {nbr2} ne sont pas premier.";
                            DeuxPremierListeResult.Text = $"Le nombre {nbr1} et {nbr2} ont {string.Join(",", commun)} comme diviseur commun ";
                        }
                        var etapes1 = Etapes.EtapeDiviseur(nbr1.ToString());
                        etapes1.Insert(0, new Etape() { Info = $"Les diviseurs de {nbr1}" });
                        var etapes2 = Etapes.EtapeDiviseur(nbr2.ToString());
                        etapes2.Insert(0, new Etape() { Info = $"Les diviseurs de {nbr2}" });
                        DeuxPremierEtape.BindingContext = this;
                        var etapes = new List<Etape>(etapes2);
                        etapes1.ForEach(x =>
                        {
                            etapes.Add(x);
                        });
                        DeuxPremierEtape.ItemsSource = etapes;
                        DeuxPremierEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        DeuxPremierEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        DeuxPremierResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                        await BtnFrame.DisplayToastAsync("L'on ne peut chercher les diviseurs de 0", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur", 3000);

            }
            catch (Exception)
            {
                DeuxPremierStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task SacADosField()
        {
            try
            {
                if (!String.IsNullOrEmpty(SacADosSommeEntry.Text) && !String.IsNullOrEmpty(SacADosSuiteEntry.Text))
                {
                    if (SacADosSuiteEntry.Text.IsOnlyInteger())
                    {
                        int nbr1 = int.Parse(SacADosSommeEntry.Text.SubstringInteger());
                        var text = SacADosSuiteEntry.Text;
                        if (nbr1 != 0)
                        {
                            if (nbr1 < 0) nbr1 *= -1;
                            SacADosStack.IsVisible = true;
                            var result = Fonction.SacADos(text, nbr1);
                            var etapes = Etapes.SacADos(text, nbr1);
                            SacADosEtape.BindingContext = this;
                            SacADosEtape.ItemsSource = etapes;
                            if (result.Count > 1)
                            {
                                SacADosResult.Text = $"Les solutions sont au nombre de {result.Count}, {String.Join(",", result)}.";
                                SacADosListeResult.Text = $"Les solutions sont au nombre de {result.Count}, {String.Join(",", result)}.";
                                SacADosEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                                await Task.Delay(2000);
                                SacADosEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                                await Task.Delay(2000);
                            }
                            else
                                if (result.Count == 1)
                            {

                                SacADosListeResult.Text = $"Le solution sont est {String.Join(",", result)}.";
                                SacADosResult.Text = $"La solution est {result[0]}";
                            }
                            else
                            {
                                SacADosResult.Text = $"Il n'y a aucuns solution.";
                                SacADosStack.IsVisible = false;
                            }

                            SacADosResult.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                NumberOfTapsRequired = 2,
                                Command = new Command(async () =>
                                {
                                    await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                      await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                })
                            });
                        }
                        else
                            await BtnFrame.DisplayToastAsync("Pas besoin, utilisez votre tête pour trouver la solution.", 3000);
                    }
                    else
                        await BtnFrame.DisplayToastAsync("Erreur, Revoyez le format de votre suite.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir toutes les entrées", 3000);
            }
            catch (Exception ex)
            {
                SacADosStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync($"Erreur, un problème est survenu. {ex.Message}", 3000);

            }

        }
        private async Task InverseModulaireField()
        {
            try
            {
                if (!String.IsNullOrEmpty(InverseModulaireModuleEntry.Text) && !String.IsNullOrEmpty(InverseModulaireNbrEntry.Text))
                {
                    int nbr = int.Parse(InverseModulaireNbrEntry.Text.SubstringInteger());
                    int module = int.Parse(InverseModulaireModuleEntry.Text.SubstringInteger());
                    if (nbr != 0 && module != 0)
                    {
                        while (nbr < 0) nbr += module;
                        if (Fonction.PremierEntreEux(nbr, module))
                        {
                            InverseModulaireStack.IsVisible = true;
                            var result = Fonction.InverseModulo(nbr, module);
                            InverseModulaireResult.Text = $"La solution est {result}.";
                            InverseModulaireResult.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                NumberOfTapsRequired = 2,
                                Command = new Command(async () =>
                                {
                                    await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                      await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                })
                            });
                        }
                        else
                        {
                            await BtnFrame.DisplayToastAsync($"{nbr} n'admet pas d'inverse dans la base {module}.", 3000);
                        }
                    }

                    else
                        await BtnFrame.DisplayToastAsync("Les valeurs doivent être différent de 0.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir toutes les valeurs valeur", 3000);

            }
            catch (Exception)
            {
                InverseModulaireStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);
            }

        }
        private async Task ChiffrementField()
        {
            try
            {
                var selection = ChiffrementBaseEntry.SelectedItem as UserModele;
                if (selection != null && !String.IsNullOrEmpty(ChiffrementCaractereEntry.Text))
                {
                    var modele = App.UserModeleManager.GetByName(selection.Nom);
                    var text = ChiffrementCaractereEntry.Text.ToUpper();
                        if (text.IsInModele(modele.Nom))
                        {
                            var nbr = modele.NbrElement;
                            if (nbr <= 0) nbr *= -1;
                            ChiffrementStack.IsVisible = true;
                            var result = Fonction.ChiffrementModele(text, modele.Nom);
                            ChiffrementResult.Text = $"Le chiffrement de {text} donne {String.Join(", ", result)}";
                            var etapes = Etapes.ChiffrementModele(text, modele.Nom);
                            ChiffrementEtape.BindingContext = this;
                            ChiffrementEtape.ItemsSource = etapes;
                            ChiffrementListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                            ChiffrementEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                            await Task.Delay(2000);
                            ChiffrementEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                            await Task.Delay(2000);
                            ChiffrementResult.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                NumberOfTapsRequired = 2,
                                Command = new Command(async () =>
                                {
                                    await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                      await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                })
                            });

                        }
                        else
                        {
                            await BtnFrame.DisplayToastAsync("Le modèle choisi ne contient pas tous les caractères du texte.", 3000);
                        }
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir les données", 3000);

            }
            catch (Exception)
            {
                ChiffrementStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu ou les conditions de chiffrement ne sont pas remplis.", 3000);
            }

        }
        private async Task ClePiveRSAField()
        {
            try
            {
                if (!String.IsNullOrEmpty(RSAPriveEEntry.Text) && !String.IsNullOrEmpty(RSAPriveNEntry.Text))
                {
                    int nbr1 = int.Parse(RSAPriveEEntry.Text.SubstringInteger());
                    int nbr2 = int.Parse(RSAPriveNEntry.Text.SubstringInteger());
                    if (nbr1 != 0 && nbr2 != 0)
                    {
                        if (nbr1 < 0) nbr1 *= -1;
                        if (nbr2 < 0) nbr2 *= -1;
                        if (Fonction.PremierEntreEux(nbr1, int.Parse(Fonction.RSAPhi(nbr2.ToString()))))
                        {
                            RSAPriveStack.IsVisible = true;
                            var result = Fonction.RSAClePrive(nbr2.ToString(), nbr1.ToString());
                            if (!result.Equals($"-1"))
                            {
                                RSAPriveResult.Text = $"La solution est {result}.";
                                RSAPriveResult.GestureRecognizers.Add(new TapGestureRecognizer()
                                {
                                    NumberOfTapsRequired = 2,
                                    Command = new Command(async () =>
                                    {
                                        await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                          await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                    })
                                });
                                var etapes = Etapes.RSAPriveEtape(nbr2.ToString(), nbr1.ToString());
                                RSAPriveEtape.BindingContext = this;
                                RSAPriveEtape.ItemsSource = etapes;
                                RSAPriveListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                                RSAPriveEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                                await Task.Delay(2000);
                                RSAPriveEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                                await Task.Delay(2000);
                            }
                            else
                            {
                                RSAPriveStack.IsVisible = false;
                                await BtnFrame.DisplayToastAsync("Le nombre e n'est pas premier avec ϕ(n).", 3000);
                            }
                        }
                        else
                            await BtnFrame.DisplayToastAsync("Les valeurs doivent être premiers entre eux.", 3000);
                    }
                    else
                        await BtnFrame.DisplayToastAsync("Les valeurs doivent être différents de 0.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir toutes les valeurs.", 3000);

            }
            catch (Exception ex)
            {
                RSAPriveStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync($"Erreur, un problème est survenu {ex.Message}.", 3000);
            }

        }
        private async Task ClePiveMerkleHellmanField()
        {
            try
            {
                if (!String.IsNullOrEmpty(MerklePriveMEntry.Text) && !String.IsNullOrEmpty(MerklePriveNEntry.Text) && !String.IsNullOrEmpty(MerklePriveSuiteEntry.Text))
                {
                    int m = int.Parse(MerklePriveMEntry.Text.SubstringInteger());
                    int n = int.Parse(MerklePriveNEntry.Text.SubstringInteger());
                    string suite = MerklePriveSuiteEntry.Text;
                    if (suite.IsOnlyInteger())
                    {
                        if (suite.SuperSuite())
                        {
                            if (m != 0 && n != 0)
                            {
                                if (n < 0) n *= -1;
                                if (m < 0) m *= -1;
                                if (Fonction.PremierEntreEux(n, int.Parse(Fonction.RSAPhi(m.ToString()))))
                                {
                                    var listeS = suite.Split(',').ToList().SommeRSaList();
                                    if (n > listeS)
                                    {
                                        if (Fonction.PremierEntreEux(listeS, n))
                                        {
                                            MerklePriveStack.IsVisible = true;
                                            var result = Fonction.MerkleHellmanClePublique(suite, n.ToString(), m.ToString());
                                            MerklePriveResult.Text = $"La solution est {result}.";
                                            var etapes = Etapes.MerkleHelmanClePublique(suite, n.ToString(), m.ToString());
                                            MerklePriveEtape.BindingContext = this;
                                            MerklePriveEtape.ItemsSource = etapes;
                                            MerklePriveListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                                            MerklePriveEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                                            await Task.Delay(2000);
                                            MerklePriveEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                                            await Task.Delay(2000);
                                            MerklePriveResult.GestureRecognizers.Add(new TapGestureRecognizer()
                                            {
                                                NumberOfTapsRequired = 2,
                                                Command = new Command(async () =>
                                                {
                                                    await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                                      await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                                })
                                            });
                                        }
                                        else
                                            await BtnFrame.DisplayToastAsync($"Le nombre {n} doit être premier avec la somme {listeS} des éléments de la suite.", 3000);
                                    }
                                    else
                                        await BtnFrame.DisplayToastAsync($"Le nombre {n} doit être supérieur à la somme {listeS} des éléments de la suite.", 3000);
                                }
                                else
                                    await BtnFrame.DisplayToastAsync($"Les valeurs {n} et {m} doivent être premiers entre eux.", 3000);
                            }
                            else
                                await BtnFrame.DisplayToastAsync("Les valeurs doivent être différents de 0.", 3000);
                        }
                        else
                            await BtnFrame.DisplayToastAsync("La suite saisie n'est pas une super suite.", 3000);
                    }
                    else
                        await BtnFrame.DisplayToastAsync("La suite saisie ne doit contenire ques des valeurs numériques.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir toutes les valeurs.", 3000);
            }
            catch (Exception ex)
            {
                MerklePriveStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync($"Erreur, un problème est survenu {ex.Message}.", 3000);
            }

        }
        private async Task SignatureRSAField()
        {
            try
            {
                if (!String.IsNullOrEmpty(RSASignatureEEntry.Text) && !String.IsNullOrEmpty(RSASignatureNEntry.Text) && !String.IsNullOrEmpty(RSASignatureMessageEntry.Text))
                {
                    int e = int.Parse(RSASignatureEEntry.Text.SubstringInteger());
                    int n = int.Parse(RSASignatureNEntry.Text.SubstringInteger());
                    int message = int.Parse(RSASignatureMessageEntry.Text.SubstringInteger());
                    if (e != 0 && n != 0 && message != 0)
                    {
                        while (n < 0) n *= -1;
                        while (e < 0) e *= -1;
                        if (Fonction.PremierEntreEux(e, int.Parse(Fonction.RSAPhi(n.ToString()))))
                        {
                            RSASignatureStack.IsVisible = true;
                            var result = Fonction.RSASignature(message.ToString(), n.ToString(), e.ToString());
                            RSASignatureResult.Text = $"La solution est {result}.";
                            RSASignatureResult.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                NumberOfTapsRequired = 2,
                                Command = new Command(async () =>
                                {
                                    await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                      await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                })
                            });
                            var etapes = Etapes.RSASignatureEtape(message.ToString(), n.ToString(), e.ToString());
                            RSASignatureEtape.BindingContext = this;
                            RSASignatureEtape.ItemsSource = etapes;
                            RSASignatureListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                            RSASignatureEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                            await Task.Delay(2000);
                            RSASignatureEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                            await Task.Delay(2000);
                        }
                        else
                            await BtnFrame.DisplayToastAsync($"Les valeurs {n} et {Fonction.RSAPhi(n.ToString())} doivent être premiers entre eux.", 3000);
                    }
                    else
                        await BtnFrame.DisplayToastAsync("Les valeurs doivent être différents de 0.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir toutes les valeurs.", 3000);

            }
            catch (Exception ex)
            {
                RSASignature.IsVisible = false;
                await BtnFrame.DisplayToastAsync($"Erreur, un problème est survenu {ex.Message}.", 3000);

            }

        }
        private async Task AffineBonneCLeField()
        {
            try
            {
                if (!String.IsNullOrEmpty(AffineBonneCleEntry.Text))
                {
                    int nbr = int.Parse(AffineBonneCleEntry.Text.SubstringInteger());
                    if (nbr != 0)
                    {
                        if (nbr < 0) nbr *= -1;
                        AffineBonneCleStack.IsVisible = true;
                        var result = Fonction.AffineBonneCle($"{nbr}");
                        AffineBonneCleResult.Text = $"La solution est {String.Join(",", result)}";
                        var etapes = Etapes.BonneCleAffine(nbr.ToString());
                        AffineBonneCleEtape.BindingContext = this;
                        AffineBonneCleEtape.ItemsSource = etapes;
                        AffineBonneCleListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                        AffineBonneCleEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        AffineBonneCleEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        AffineBonneCleResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                        await BtnFrame.DisplayToastAsync("L'entrée doit être différente de 0", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception)
            {
                AffineBonneCleStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task MatriceDeterminantField()
        {
            try
            {
                if (!String.IsNullOrEmpty(MatriceDeterminantEntry.Text))
                {
                    string matrice = MatriceDeterminantEntry.Text;
                    if (Fonction.IsSquareMatrice(matrice))
                    {
                        if (Fonction.TransformationEnMatrice(matrice).Keys.Count == 2 || Fonction.TransformationEnMatrice(matrice).Keys.Count == 3)
                        {
                            MatriceDeterminantStack.IsVisible = true;
                            var result = Fonction.MatriceDeterminant(matrice);
                            DiviseurResult.Text = $"Le déterminant de la matrice {matrice} est {String.Join(",", result)}";
                            var etapes = Etapes.MatriceDeterminant(matrice);
                            MatriceDeterminantEtape.BindingContext = this;
                            MatriceDeterminantEtape.ItemsSource = etapes;
                            MatriceDeterminantListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                            MatriceDeterminantEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                            await Task.Delay(2000);
                            MatriceDeterminantEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                            await Task.Delay(2000);
                            MatriceDeterminantResult.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                NumberOfTapsRequired = 2,
                                Command = new Command(async () =>
                                {
                                    await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                      await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                })
                            });

                        }
                        else
                            await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une matrice 2x2 ou 3x3.", 3000);
                    }
                    else
                        await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une matrice carrée.", 3000);

                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception ex)
            {
                MatriceDeterminantStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync($"Erreur, un problème est survenu {ex.Message}.", 3000);

            }

        }
        private async Task MatriceInverseField()
        {
            try
            {
                if (!String.IsNullOrEmpty(MatriceInverseEntry.Text) && !String.IsNullOrEmpty(MatriceInverseBaseEntry.Text))
                {
                    if (Fonction.IsSquareMatrice(MatriceInverseEntry.Text))
                    {
                        if (Fonction.TransformationEnMatrice(MatriceInverseEntry.Text).Keys.Count == 2)
                        {
                            MatriceInverseStack.IsVisible = true;
                            string matrice = MatriceInverseEntry.Text;
                            string n = MatriceInverseBaseEntry.Text;
                            var det = Fonction.MatriceDeterminant(matrice);
                            if (Fonction.InverseModulo(int.Parse(det), int.Parse(n)) > 0)
                            {
                                var result = Fonction.MatriceInverse(matrice, n);
                                MatriceInverseResult.Text = $"L'inverse de la matrice {matrice} est {String.Join(",", result)}";
                                var etapes = Etapes.MatriceInverse(matrice, n);
                                MatriceInverseEtape.BindingContext = this;
                                MatriceInverseEtape.ItemsSource = etapes;
                                MatriceInverseListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                                MatriceInverseEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                                await Task.Delay(2000);
                                MatriceInverseEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                                await Task.Delay(2000);
                                MatriceInverseResult.GestureRecognizers.Add(new TapGestureRecognizer()
                                {
                                    NumberOfTapsRequired = 2,
                                    Command = new Command(async () =>
                                    {
                                        await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                            await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                                    })
                                });
                            }
                            else
                                await BtnFrame.DisplayToastAsync($"Erreur l'inverse du déterminant {det} n'existe pas dans la base {n}.", 3000);
                        }
                        else
                            await BtnFrame.DisplayToastAsync("Erreur vous devez saisir matrice 2x2.", 3000);
                    }
                    else
                        await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une matrice carrée.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception)
            {
                MatriceInverseStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task BinaireField()
        {
            try
            {
                if (!String.IsNullOrEmpty(BinaireEntry.Text))
                {
                    string binaire = BinaireEntry.Text.SubstringInteger();
                    if (binaire.IsOnlyInteger())
                    {
                        BinaireStack.IsVisible = true;
                        var result = Fonction.Binaire(int.Parse(binaire));
                        BinaireResult.Text = $"Le bianire de {binaire} est {String.Join(",", result)}";
                        var etapes = Etapes.Binaire(binaire);
                        BinaireEtape.BindingContext = this;
                        BinaireEtape.ItemsSource = etapes;
                        BinaireListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                        BinaireEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        BinaireEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        BinaireResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                    {
                        await BtnFrame.DisplayToastAsync("Erreur vous devez saisir que des entiers.", 3000);
                    }
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception)
            {
                BinaireStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task RSAPEtQField()
        {
            try
            {
                if (!String.IsNullOrEmpty(TrouverPEtQEntry.Text))
                {
                    string n = TrouverPEtQEntry.Text.SubstringInteger();
                    if (int.Parse(n) > 0)
                    {
                        TrouverPEtQStack.IsVisible = true;
                        var result = Fonction.RSAPEtQ(n);
                        TrouverPEtQResult.Text = $"La solution est {String.Join(",", result)}";
                        var etapes = Etapes.RSAPEtQ(n);
                        TrouverPEtQEtape.BindingContext = this;
                        TrouverPEtQEtape.ItemsSource = etapes;
                        TrouverPEtQListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                        TrouverPEtQEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        TrouverPEtQEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        TrouverPEtQResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                    {
                        await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur supérieure à 0.", 3000);
                    }
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception)
            {
                TrouverPEtQStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task FrequenceCaractereField()
        {
            try
            {
                if (!String.IsNullOrEmpty(FrequenceCaractereEntry.Text))
                {
                    if (FrequenceCaractereEntry.Text.IsOnlyAphabet())
                    {
                        string n = FrequenceCaractereEntry.Text.SubstringString();
                        FrequenceCaractereStack.IsVisible = true;
                        var etapes = Etapes.FrequenceCaractere(n);
                        FrequenceCaractereEtape.BindingContext = this;
                        FrequenceCaractereEtape.ItemsSource = etapes;
                        FrequenceCaractereEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        FrequenceCaractereEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);

                    }
                    else
                    {
                        await BtnFrame.DisplayToastAsync("Erreur vous devez saisir que des alphabets.", 3000);
                    }
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception)
            {
                FrequenceCaractereStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task DecompositionFacteurField()
        {
            try
            {
                if (!String.IsNullOrEmpty(DecompositionFacteurEntry.Text))
                {
                    if (DecompositionFacteurEntry.Text.IsOnlyInteger())
                    {
                        string n = DecompositionFacteurEntry.Text.SubstringInteger();
                        DecompositionFacteurStack.IsVisible = true;
                        var result = Fonction.DecompositonEnFacteurPremier(n);
                        DecompositionFacteurResult.Text = $"La solution est {String.Join(", ", result)}";
                        var etapes = Etapes.DecompositionFacteur(n);
                        DecompositionFacteurEtape.BindingContext = this;
                        DecompositionFacteurEtape.ItemsSource = etapes;
                        DecompositionFacteurListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                        DecompositionFacteurEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        DecompositionFacteurEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        DecompositionFacteurResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                    }
                    else
                    {
                        await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur supérieur à 0.", 3000);
                    }
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir une valeur.", 3000);

            }
            catch (Exception)
            {
                DecompositionFacteurStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync("Erreur, un problème est survenu.", 3000);

            }

        }
        private async Task ExponentiationModulaireField()
        {
            try
            {
                if (!String.IsNullOrEmpty(ExponentiationModulaireExEntry.Text) && !String.IsNullOrEmpty(ExponentiationModulaireNbrEntry.Text) &&
                    !String.IsNullOrEmpty(ExponentiationModulaireModuleEntry.Text))
                {
                    int exposant = int.Parse(ExponentiationModulaireExEntry.Text.SubstringInteger());
                    int nbr = int.Parse(ExponentiationModulaireNbrEntry.Text.SubstringInteger());
                    int module = int.Parse(ExponentiationModulaireModuleEntry.Text.SubstringInteger());
                    if (exposant != 0 && nbr != 0 && module != 0)
                    {
                        while (nbr < 0) nbr *= -1;
                        while (exposant < 0) exposant *= -1;
                        /*   if (Fonction.PremierEntreEux(exposant, nbr) && Fonction.PremierEntreEux(exposant,module) && Fonction.PremierEntreEux(nbr,module))
                           {
                        */
                        ExponentiationModulaireStack.IsVisible = true;
                        var result = Fonction.ExponentiationModulaire(nbr, exposant, module);
                        ExponentiationModulaireResult.Text = $"La solution est {result}.";
                        ExponentiationModulaireResult.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 2,
                            Command = new Command(async () =>
                            {
                                await Clipboard.SetTextAsync(String.Join(",", result)).ContinueWith(async (ord) =>
                                  await BtnFrame.DisplayToastAsync("Résultat copié avec succès.", 3000));
                            })
                        });
                        var etapes = Etapes.ExponentiationModulaire(nbr, exposant, module);
                        ExponentiationModulaireEtape.BindingContext = this;
                        ExponentiationModulaireEtape.ItemsSource = etapes;
                        ExponentiationModulaireListeResult.Text = $"La solution est donc {String.Join(", ", result)}.";
                        ExponentiationModulaireEtape.ScrollTo(etapes.Last(), ScrollToPosition.End, true);
                        await Task.Delay(2000);
                        ExponentiationModulaireEtape.ScrollTo(etapes.First(), ScrollToPosition.MakeVisible | ScrollToPosition.Start, true);
                        await Task.Delay(2000);
                        /* }
                         else
                             await BtnFrame.DisplayToastAsync($"Les valeurs {nbr} , {exposant} et {module} doivent être premiers entre eux.", 3000);
                        */
                    }
                    else
                        await BtnFrame.DisplayToastAsync("Les valeurs doivent être différents de 0.", 3000);
                }
                else
                    await BtnFrame.DisplayToastAsync("Erreur vous devez saisir toutes les valeurs.", 3000);

            }
            catch (Exception ex)
            {
                ExponentiationModulaireStack.IsVisible = false;
                await BtnFrame.DisplayToastAsync($"Erreur, un problème est survenu {ex.Message}.", 3000);
            }
        }
        private void InfoPop(ListView listView)
        {
            listView.SelectionMode = ListViewSelectionMode.Single;
            listView.ItemSelected += async delegate (object sender, SelectedItemChangedEventArgs e)
            {
                if (e.SelectedItem is Etape etape)
                {
                    listView.SelectedItem = null;
                    await this.ShowPopupAsync(new InfoPopUp(etape.Info));
                }
            };
        }

    }
}