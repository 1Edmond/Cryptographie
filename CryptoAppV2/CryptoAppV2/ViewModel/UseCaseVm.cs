using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    public class UseCaseVm
    {

        public List<UseCase> Uses { get; set; }
        public Information Header { get; set; }


        public UseCaseVm()
        {
            Uses = new List<UseCase>()
            {
                new UseCase() {
                    Titre = "ACCEPTATION DES CONDITIONS",
                    Numbrer = "1.",
                    Description = "Les services que l'application Crypto fournit à ses utilisateurs sont sujets aux conditions" +
                    " d'utilisation suivantes. Wicode se réservele droit de modifier et de mettre à jour les Conditions d'utilisation à tout moment sans notification aucune. La vision la" +
                    " plus récente des Conditions d'utilisation peut être lue en cliquant sur le lien Conditions d'utilisation dans les paramètres de l'application."
                },
                new UseCase() {
                    Titre = "",
                    TitreVisiblity = false,
                    Alignement = "CenterAndExpand",
                    Numbrer = "A.",
                    Description = "Le présent contrat, qui contient par référence d'autres dispositions applicables à l'utilisation de Crypto, " +
                    "dont entre autres les conditionssupplémentaires  prévues par le présent contrat et qui régissent l'utilisation de certains éléments " +
                    "contenus sur Crypto ainsi que les conditions qui s'appliquent à son utilisation par les utilisateurs. " +
                    "En utilisant l'application Crypto de Wicode (autrement que par la lecture du présent contrat pour la première fois)," +
                    " l'utisateur s'engagent à se conformer à toutes conditions contenues dans le présent contrat. Le droit d'utiliser Crypto " +
                    "est personnel à l'utilisateur et n'est pas transférable à aucune personne ou entité. L'utilisateur est responsable de toutes " +
                    "les utilisations de contrat (nom d'utilisateur et mot de passe) et de l'assurance que tous les utilisateurs de son compte se conforment entièrement aux dispositions du présent contrat." +
                    " L'utilisateur sera responsable de la protection et de la confidentialité de son mot de passe."
                },
                new UseCase() {
                    Titre = "",
                    Numbrer = "B.",
                    TitreVisiblity = false,
                    Alignement = "CenterAndExpand",
                    Description = " Wicode aura le droit de changer ou de mettre fin à une caractéristique de Crypto dont entre le contenu," +
                    " les fonctionnalités, ainsi que les moyens nécessaires à son utilisation."
                },
                new UseCase() {
                    Titre = "ACCEPTATION DES CONDITIONS",
                    Numbrer = "2.",
                    Description = "Wicode aura le droit de changer les conditions applicables à l'utilisation de Crypto par l'utilisateur, " +
                    "ou d'imposer de nouvelles conditions dont entre autres, l'ajout de nouveaux frais avant son utilisation." +
                    " Ces changements, modifications, ajouts ou suppressions entreront en vigueur suivant une notification par message whatsapp" +
                    " par lequel l'utilisateur sera informé." +
                    " Toute utilisation de Crypto par l'utilisateur après la modifications des conditions constitue une acceptation des changements par l'utilisateur."
                },
                new UseCase() {
                    Titre = "DESCRIPTIONS DES SERVICES",
                    Numbrer = "3.",
                    Description = "Par le biais son Crytpo, Wicode offre à ses utilisateurs l'accès à diverses ressources dont les différentes modules " +
                    "étudiés au cours de Cryptographie de l'année 2021-2022 à IAI-TOGO, ainsi que certaines fonctions pour aider à la résolution de certains" +
                    " problèmes."
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = false,
                    Description = "Liste des différents chapitres couverts par Crypto :",
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Le code Affine.",
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Le code Vigenère.",
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    HasBoxView = true,
                    Description = "Le code Hill.",
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Le code Vernam.",
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Le code Merkle-Hellman.",
                },
                new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "RSA.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = false,
                    Description = "Liste des différents Fonctionnalités couvertes par Crypto :",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Binaire d'un nombre.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Calculer la clé publique (Merkle-Hellman).",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Calculer la clé privée (RSA).",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Chiffrement d'un caractère ou d'un mot.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Décomposition en facteurs premiers.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Déterminant d'une matrice carrée 2x2 ou 3x3.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Exponentiation Modulaire.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Fréquence de caractère.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Inverse d'une matrice carrée (2x2).",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Inverse modulaire.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Liste des diviseurs postifs d'un nombre.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Nombre de bonnes clés (Le code Affine).",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Résoudre le problème du sac à dos..",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Signature RSA.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Trouver p et q (RSA).",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Vérification primalité entre deux nombres.",
                },
                    new UseCase()
                {
                    NumberVisibility = false,
                    HasBoxView = true,
                    BoxViewMargin = new Thickness() { Bottom = 0, Left = 50, Right = 0, Top = 0},
                    Description = "Vérifiez si un nombre est premier.",
                },

            };
            Header = new Information()
            {
                Description = "En vous inscrivant sur l'application, vous vous engagez à respecter les conditions d'utilisations suivantes :"
            };


        }


    }
}
