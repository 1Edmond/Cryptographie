using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.ViewModel
{
    public class CryptoInformationVm
    {
        public CryptoInformationVm()
        {

            /*Conditions = new List<UseCondition>()
            {
                new UseCondition {
                    Titre = "ACCEPTATION DES CONDITIONS ",
                    Condition = "Les services que l'application Crypto fournit à ses utilisateurs sont sujets aux conditions d'utilisation suivantes. Wicode se réserve " +
                    "le droit de modifier et de mettre à jour les Conditions d'utilisation à tout moment sans notification aucune. La vision la plus récente des Conditions d'utilisation peut" +
                    " être lue en cliquant sur le lient Conditions d'utilisation dans les paramètres de l'application. \n\n" +
                    "A. \t\t  Le présent contrat, qui contient par référence d'autres dispositions applicables à l'utilisation de Crypto, dont entre autres les conditions supplémentaires" +
                    " prévues par le présent contrat et qui régissent l'utilisation de certains éléments contenu sur Crypto ainsi que les conditions qui s'appliquent à son utilisation par les utilisateurs." +
                    " En utilisant l'application Crypto de Wicode (autrement que par la lecture du présent contrat pour la première fois), l'utisateur s'engagent à se conformer à toutes conditions" +
                    " contenus dans le présent contrat. Le droit d'utiliser Crypto est personnel à l'utilisateur et n'est pas transférable à aucune personne ou entité." +
                    " L'utilisateur est responsable de toutes les utilisations de contrat (nom d'utilisateur et mot de passe) et de l'assurance que tous les utilisateurs de son compte se conforment" +
                    " entièrement aux dispositions du présent contrat. L'utilisateur sera responsable de la protection et de la confidentialité de son mot de passe.\n\n" +
                    "B. \t\t  Wicode aura le droit de changer ou de mettre fin à une caractéristique de Crypto dont entre le contenus, les fonctionnalités, ainsi que les moyens nécessaires à son utilisation." +
                    ""
                },
                new UseCondition {
                    Titre = "MODIFICATION DES CONDITIONS ",
                    Condition = "Wicode aura le droit de changer les conditions applicables à l'utilisation de crypto par l'utilisateur, ou d'imposer de nouvelles conditions dont entre autres, l'ajout de nouveaux frais avant son utilisation. Ces changements, modifications, ajouts ou suppressions entreront en vigueur suivant une notification par message whatsapp par lequel l'utilisateur sera informé. Toute utilisation de Crypto par l'utilisateur après la modifications des conditions constitue une acceptation des changements par l'utilisateur."
                },
               new UseCondition
               {
                   Titre = "DESCRIPTIONS DES SERVICES",
                   Condition = "Par le biais son Crytpo, Wicode offre à ses utilisateurs l'accès à diverses ressources dont les différentes modules étudiés au cours de Cryptographie de l'année 2021-2022 à IAI-TOGO, ainsi que certaines fonctions pour aider à la résolution de certaines problèmes."
               },
                
                new UseCondition {Condition = "Vous utilisez cette application et avez accès à ces fonctionnalités à vos propres risques."},
                new UseCondition {Condition = "Aucune information n'est collecté via l'application, vos informations d'inscriptions ne sont stockés que sur votre mobile."},
                new UseCondition {Condition = "."},
            };
            */
            CodageAffine = new List<Information>()
            {
                new Information() { Description = "Le code Affine généralise le code de César"},
                new Information() { Description = "Sa formule de codage est c = (am + b) mod la base."},
                new Information() { Description = "Le choix de la variable a n'est pas au hasard,"},
                new Information() { Description = "la variable a doit être premier avec la base."},
                new Information() { Description = "Celui de César consiste à prendre a = 1 et b = 3."},
            };

            DecodageAffine = new List<Information>()
            {
                new Information() {Description = "Le décodage du code Affine n'est pas compliqué,"},
                new Information() {Description = "il suffit de calculer l'inverse modulo la base de la variable a."},
                new Information() {Description = "Ce qui explique pourquoi la variable a doit être premier avec la base."},
                new Information() {Description = "La formule de décodage est m = z(c - b) mod la base,"},
                new Information() {Description = "avec z l'inverse modulo la base de la variable a."},
                new Information() {Description = "De ce fait pour le code de César étant donné que a = 1 et b = 3,"},
                new Information() {Description = "l'inverser modulo de 1 dans n'importe quelle base donne 1."},
                new Information() {Description = "Il suffit de m = (c - 3) mod la base."},
            };

            CryptographieVernam = new List<Information>()
            {
                new Information() {Description = "Le Codage ou le décodage de Vernam nécessite une clé de même longeur que le message."},
                new Information() {Description = "La clé ainsi que le message sont transfomés en binaire."},
                new Information() {Description = "En suite l'on procéde à l'ajout modulo 2 bit à bit du message et de la clé."},
                new Information() {Description = "L'ajout effectué demontre pourquoi le message et la clé doit être de même longeur."},
            };

            CryptographieVigere = new List<Information>()
            {
                new Information() {Description = "C’est aussi une amélioration du chiffre de César. Son but est de contrer" },
                new Information() {Description = "le décodage par analyse statistique des codes par substitution monoalphabétique."},
                new Information() {Description = "Ici on regroupe les lettres par blocs de longueur k."},
                new Information() {Description = "On choisit ensuite une clé constituée de k nombres de 0 à 25 soit (n1,· · ·, nk)."},
                new Information() {Description = "Ensuite l'on découpe le message en bloque de k lettres."},
                new Information() {Description = "Puis l'on fait un décalage vers l'avant de n1 pour la première lettre,"},
                new Information() {Description = "n2 pour la seconde lettre et ainsi de suite."},
                new Information() {Description = "Le décodage se fait en appliquant un décalage vers l'arrière."},
            };

            CodageHill = new List<Information>()
            {
                new Information(){Description = "C’est l’exemple le plus simple de chiffrements polygraphiques."},
                new Information(){Description = "L'on choisit une matrice carrée :"},
                new Information(){Description = "A = | a b ; c d |"},
                new Information(){Description = "On chiffre deux lettres par deux autres."},
                new Information(){Description = "Chaque digramme clair (m1, m2) sera chiffré par"},
                new Information(){Description = "(c1, c2) selon le système de codage."},
                new Information(){Description = "c1 ­­­­≡ am1 + bm2 mod n \n c2 ≡ cm1 + dm2 mod n."},
                new Information(){Description = "Avec n le nombre de caractères de l'unité."},
            };

            DecodageHill = new List<Information>()
            {
                new Information{Description ="Le décodage passe par le calcule de l'inverse de la matrice A"},
                new Information{Description ="Si la matrice inverse de la matrice A est la matrice :"},
                new Information{Description ="B = | e f ; g h |"},
                new Information{Description ="On décode deux lettres par deux autres. Ainsi l'on a :"},
                new Information{Description ="m1 ≡ ec1 + fc2 mod n \n m2 ≡ gc1 + h c2 mod n "},
                new Information{Description = "Avec n le nombre de caractères de l'unité."},
            };

            CryposystemeMerkle = new List<Information>()
            {
                new Information{Description = "On considère une suite super-croissante de k entiers (généralement appelée le sac) noté S."},
                new Information{Description = "On choisit ensuite un multiplicateur m et un module n tels que n soit premier"},
                new Information{Description = "et supérieur à la somme des éléments dans S et m et n soient premiers entre eux"},
                new Information{Description = "On obtient la clé publique en multipliant modulo n le nombre m par les éléments de S."},
                new Information{Description = "tandis que la clé secrète est (S, m, n). Le message clair est"},
                new Information{Description = "composé de blocs de h bits (h = longueur du sac)."},
                new Information{Description = "Le codage se réalise en calculant le produit de des éléments de la clé par"},
                new Information{Description = "les éléments du message position par position."},
                new Information{Description = "Pour obtenir le déchiffrement le destinataire qui connaît S, m et n, détermine l'inverse modulo n de m."},
            };

            RSA = new List<Information>()
            {
                new Information{Description ="L'on choisit deux grands nombres premiers p et q."},
                new Information{Description ="Puis, l'on calcule n = p x q et φ(n) = (p -1) x (q - 1)."},
                new Information{Description ="L'on Génère un nombre e aléatoire entre 1 et φ(n) et premier avec φ(n)."},
                new Information{Description ="La clé privée d est obtenue en calculant l'inverse modulo φ(n) de e par l'algorithme d'Euclide."},
                new Information{Description ="La clé publique est (n,e) et celle privée est d."},
                new Information{Description ="La formule de codage est c = mˆe mod n."},
                new Information{Description ="La formule de décodage est m = cˆd mod n."},
            };

            ElGammal = new List<Information>()
            {
                new Information() {Description = "L'on choisit un nombre premier p très grand tel que p − 1 ait un grand facteur premier."},
                new Information() {Description = "Puis l'on produit une clé privée a telle que a ∈ {1,· · ·, p − 2}."},
                new Information() {Description = "L'on produit une clé publique (p, α, β) telle que β = αˆa mod p,"},
                new Information() {Description = "le nombre α est pris tel que α ∈ {0, · · · , p − 1} "},
                new Information() {Description = "et pour tout k ∈ {1, · · · , p − 2},αˆk ≠ 1 mod p."},
                new Information() {Description = "L'on code le message avec la formule c1 = αˆk mod p et c2 = mβˆk mod p."},
                new Information() {Description = "Le message chiffré est alors c = (c1, c2). C’est un message deux fois plus long que le message clair."},
                new Information() {Description = "Le principe de déchiffrement est le suivant : à la réception,"},
                new Information() {Description = "l'on calcule r1 = c1ˆa mod p ensuite le message déchiffré est m = c2r1ˆ-1 mod p."},
                new Information() {Description = "Pour décrypter le message il faut trouver la clé privée a solution de l’équation β = αˆa mod p."},
            };
        }

        public List<Information> CodageAffine { get; set; }
        public List<Information> DecodageAffine { get; set; }
        public List<Information> CryptographieVernam { get; set; }
        public List<Information> CryptographieVigere { get; set; }
        public List<Information> CodageHill { get; set; }
        public List<Information> DecodageHill { get; set; }
        public List<Information> CryposystemeMerkle { get; set; }
        public List<Information> RSA { get; set; }
        public List<Information> ElGammal { get; set; }
        public List<UseCondition> Conditions { get; set; }

    }
}
