using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.ViewModel
{
    public class InformationViewModel
    {
        public List<Information> Infos { get; set; }
        public List<Information> FonctionInfo { get; set; }
        public List<Information> ProblemeInfo { get; set; }

        public Information CryproInfo { get; set; }
        public InformationViewModel()
        {
            CryproInfo = new Information() { Description = "Le mot cryptographie vient du grec ”kruptos” qui signifie ”caché” et de ”graphein” qui veut dire ”écrire” La cryptographie est l’ensemble des techniques permettant de protéger une communication au moyen de codes secrets." };

            ProblemeInfo = new List<Information>()
            {
                new Information() { Description = "Supperposition de certains champs sur d'autres."},
                new Information() { Description = "Décallage de certains éléments."},
            };
            Infos = new List<Information>()
            {
                new Information() { Description = "L'application vous offre la possiblité de faire " },
                new Information() { Description = "toute sorte d'opération dans le domaine de la cryptographie." },
                new Information() { Description = "Vous devez diposer d'une connexion internet pour pouvoir vous inscrire" },
                new Information() { Description = "L'application n'est pas gratuite. Après votre inscription, " },
                new Information() { Description = "vous devez envoyé le message suivant au numéro +228 92 08 07 70 sur whatsapp" },
                new Information() { Description = "avec votre numéro d'inscription tout en changeant le" },
                new Information() { Description = "nom par celui utilisé lors de votre inscription" },
                new Information() { Description = "WiCode, inscription sous le nom de Wilfried" },

            };
            FonctionInfo = new List<Information>()
            {
                new Information {Description = "Liste des diviseurs d'un nombre positif."},
                new Information {Description = "Inverse modulaire."},
                new Information {Description = "Vérifiez si un nombre est premier."},
                new Information {Description = "Vérification primalité entre deux nombres."},
                new Information {Description = "Chiffrement d'un caractère."},
                new Information {Description = "Résoudre le problème du sac à dos."},
                new Information {Description = "Calculer la clé privée (RSA)."},
                new Information {Description = "Calculer la clé privée (Merkle-Hellman)."},
                new Information {Description = "Signature RSA."},
                new Information {Description = "Nombre de bonnes clés (Le code Affine)."},
                new Information {Description = "Déterminant d'une matrice carrée."},
                new Information {Description = "Inverse d'une matrice carrée."},
                new Information {Description = "Binaire d'un nombre."},
                new Information {Description = "Trouver p et q (RSA)."},
                new Information {Description = "Fréquence de caractère."},
                new Information {Description = "Décomposition en facteurs premiers."},
                new Information {Description = "Exponentiation Modulaire."},
            };
            FonctionInfo.Sort((x, y) => x.Description.CompareTo(y.Description));
        }


    }
}
