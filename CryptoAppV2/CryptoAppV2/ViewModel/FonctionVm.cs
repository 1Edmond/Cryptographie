using CryptoAppV2.Model;
using CryptoAppV2.View.Home;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    public class FonctionVm
    {
        public List<MesFonctions> Fonctions { get; set; }
        public List<Information> FonctionInfo { get; set; }
        public FonctionVm()
        {
            FonctionInfo = new List<Information>()
            {

            };
        }

        public FonctionVm(INavigation navigation)
        {
            Fonctions = new List<MesFonctions>()
            {
                new MesFonctions() {
                    Button1 = new BtnFonction() {

                        Text = "Liste des diviseurs d'un nombre positif.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Diviseur")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Inverse modulaire.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Inverse modulaire")); // Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Vérifiez si un nombre est premier.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Vérification premier")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Vérification primalité entre deux nombres.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Vérification primalité entre deux nombres")); // Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Valeur d'un caractère ou d'un mot.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Valeur")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Résoudre le problème du sac à dos.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Sac à dos"));  //Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Calculer la clé publique (RSA).",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Clé publique (RSA)")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Calculer la clé publique (Merkle-Hellman).",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Clé publique (Merkle-Hellman)")); //Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Signature RSA.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Signature RSA")); //Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Nombre de bonnes clés (Le code Affine).",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Bonnes clés (Le code Affine)")); //Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Déterminant d'une matrice carrée 2x2 ou 3x3.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Déterminant d'une matrice")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Inverse d'une matrice carrée 2x2 suivant une base.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Inverse d'une matrice")); // Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Binaire d'un nombre",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Binaire d'un nombre")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Trouver p et q (RSA).",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Trouver p et q (RSA)")); // Fait
                        })
                    }
                },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Fréquence de caractère.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Fréquence de caractère")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Décomposition en facteurs premiers",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Décomposition en facteurs premiers")); // Fait
                        })
                },
            },
                new MesFonctions() {
                    Button1 = new BtnFonction() {
                        Text = "Exponentiation Modulaire.",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Exponentiation Modulaire")); // Fait
                        })
                    },
                    Button2 = new BtnFonction() {
                        Text = "Calculer la Clé publique (El-Gammal)",
                        Navigation = new Command(() => {
                            navigation.ShowPopup(new PopUpPage("Décomposition en facteurs premiers")); //
                        }),
                        Visible = false
                },
            }

            };


        }


    }
}
