using CryptoAppV2.Model;
using CryptoAppV2.View.Home;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    public class CryptoChapitreVm
    {
        public List<CryptoChapitre> Chapitres { get; set; }

        public INavigation MyNavigation { get; set; }



        public CryptoChapitreVm(INavigation navigation, Page page)
        {

            MyNavigation = navigation;

            Chapitres = new List<CryptoChapitre>()
            {
                new CryptoChapitre() {
                    Titre = "Cryptographie Affine" ,
                    Difficulte="Facile" ,
                    Details="Le chiffre affine est une méthode de cryptographie basée sur un chiffrement par substitution" +
                    " mono-alphabétique, c'est-à-dire que la lettre d'origine n'est remplacée que par une unique autre lettre," +
                    " contrairement au chiffre de Hill. Il s'agit d'un code simple à appréhender mais aussi un des plus faciles à casser.",
                    AuteurImage="CryptoImg",
                    AuteurName = "Inconnu",
                    NavigationCommand = new Command(async () =>
                    {
                       var a = page.FadeTo(0,1200);
                        await navigation.PushModalAsync(new AffinePage());
                        await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },
                new CryptoChapitre() {
                    Titre = "Cryptographie de Vernam" ,
                    Difficulte="Facile" ,
                    Details="Le masque jetable, également appelé chiffre de Vernam, est un algorithme de cryptographie inventé par" +
                    " Gilbert Vernam en 1917 et perfectionné par Joseph Mauborgne, qui rajouta la notion de clé aléatoire. Cependant," +
                    " le banquier américain Frank Miller en avait posé les bases dès 1882. Bien que simple, facile et rapide, tant pour " +
                    "le codage que pour le décodage, ce chiffrement est théoriquement impossible à casser, mais le fait que le masque soit " +
                    "à usage unique impose de le transmettre au préalable par un autre moyen," +
                    " ce qui soulève des difficultés de mise en œuvre pour la sécurisation des échanges sur Internet",
                    AuteurName = "Gilbert Vernam",
                    AuteurImage = "Joseph",
                    NavigationCommand = new Command(async () =>
                    {
                        var a = page.FadeTo(0,1200);
                        await navigation.PushModalAsync(new VernamPage());
                        await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },
                new CryptoChapitre() {
                    Titre = "Cryptographie de Vigenère" ,
                    Difficulte="Facile" ,
                    Details="Le chiffre de Vigenère est un système de chiffrement par substitution polyalphabétique dans lequel une même" +
                    " lettre du message clair peut, suivant sa position dans celui-ci, être remplacée par des lettres différentes, contrairement à " +
                    "un système de chiffrement mono alphabétique comme le chiffre de César (qu'il utilise cependant comme " +
                    "composant). Cette méthode résiste ainsi à l'analyse de fréquences, ce qui est un avantage décisif sur les " +
                    "chiffrements mono alphabétiques. Cependant le chiffre de Vigenère a été percé par le major prussien Friedrich Kasiski qui a publié " +
                    "sa méthode en 1863. Depuis cette époque, il n‘offre plus aucune sécurité.",
                    AuteurName="Blaise de Vigenère",
                    AuteurImage="Vigenere",
                    NavigationCommand = new Command(async () =>
                    {
                        var a = page.FadeTo(0,1200);

                        await navigation.PushModalAsync(new VigenerePage());

                         await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },
                new CryptoChapitre() {
                    Titre = "Cryptographie de Hill" ,
                    Difficulte="Facile" ,
                    Details="En cryptographie symétrique, le chiffre de Hill est un modèle simple d'extension du chiffrement affine à un bloc. Ce système étudié par Lester S." +
                    " Hill, utilise les propriétés de l'arithmétique modulaire et des matrices. Il consiste à chiffrer le message en substituant les lettres du message, " +
                    "non plus lettre à lettre, mais par groupe de lettres. Il permet ainsi de rendre plus difficile le cassage du code par observation des fréquences.Lester S." +
                    " Hill a aussi conçu une machine capable de réaliser mécaniquement un tel codage.",
                    AuteurName ="Lester S. Hill",
                    AuteurImage="Hill",
                    NavigationCommand = new Command(async () =>
                    {
                        var a = page.FadeTo(0,1200);
                        await navigation.PushModalAsync(new HillPage());
                         await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },
                new CryptoChapitre() {
                    Titre = "Cryptosystème de Merkle-Hel..." ,
                    Difficulte = "Facile" ,
                    Details = "Merkle-Hellman est un cryptosystème asymétrique. Cependant, contrairement à RSA, il est à sens unique, c'est-à-dire que la clé publique" +
                    " est utilisée uniquement pour le chiffrement, et la clé privée uniquement pour le déchiffrement. Il ne peut donc pas être utilisé pour un protocole" +
                    " d'authentification. Il est basé sur le problème de la somme de sous-ensembles (un cas spécial du problème du sac à dos): étant donnés n entiers et un entier p," +
                    " existe-t-il un sous-ensemble de ces éléments dont la somme des valeurs est p ? Ce problème est NP-Complet. Une instance composé de n entiers s'appelle un sac. " +
                    "On dit qu'un sac est supercroissant lorsque chaque élément est plus grand que la somme des éléments qui sont plus petits que lui. Le problème restreint aux instances" +
                    " supercroissantes est décidable en temps polynomial avec un algorithme glouton.",
                    AuteurName= " Ralph Merkle et Martin Hellman",
                    AuteurImage = "CryptoImg",
                    NavigationCommand = new Command(async () =>
                    {
                        var a = page.FadeTo(0,1200);
                        await navigation.PushModalAsync(new MerkleHellmanPage());
                         await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },
                new CryptoChapitre() {
                    Titre = "Chiffrement RSA" ,
                    Difficulte="Facile" ,
                    Details="Le chiffrement RSA est asymétrique : il utilise une paire de clés (des nombres entiers) composée d'une clé publique pour " +
                    "chiffrer et d'une clé privée pour déchiffrer des données confidentielles. Les deux clés sont créées par une personne, souvent nommée par " +
                    "convention Alice, qui souhaite que lui soient envoyées des données confidentielles. Alice rend la clé publique accessible. Cette clé est " +
                    "utilisée par ses correspondants (Bob, etc.) pour chiffrer les données qui lui sont envoyées. La clé privée est quant à elle réservée à Alice, et lui " +
                    "permet de déchiffrer ces données. La clé privée peut aussi être utilisée par Alice pour signer une donnée qu'elle envoie, la clé publique permettant à n'importe lequel " +
                    "de ses correspondants de vérifier la signature.",
                    AuteurName = "Ronald Rivest, Adi Shamir et...",
                    AuteurImage = "CryptoImg",
                    NavigationCommand = new Command(async () =>
                    {
                        var a = page.FadeTo(0,1200);
                        await navigation.PushModalAsync(new RSAPage());
                         await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },
/*
                new CryptoChapitre() {
                    Titre = "Le chiffrement ElGamal" ,
                    Difficulte="Facile" ,
                    Details="L'algorithme est décrit pour un groupe cyclique fini au sein duquel le problème de décision de Diffie-Hellman (DDH) est difficile. Des informations plus précises" +
                    " sont données dans la section Résistance aux attaques CPA.On peut remarquer que DDH est une hypothèse de travail plus forte que celle du logarithme discret, puisqu’elle tient si " +
                    "jamais le problème du logarithme discret est difficile. Il existe par ailleurs des groupes où le problème DDH est facile, mais où on n'a pas d'algorithme efficace pour résoudre le " +
                    "logarithme discret",
                    AuteurName = "Taher Elgamal",
                    AuteurImage = "Elgamal",
                    NavigationCommand = new Command(async () =>
                    {
                        var a = page.FadeTo(0,1200);
                        await navigation.PushModalAsync(new ElGamalPage());
                         await a.ContinueWith((ord) =>
                        {
                            page.Opacity = 1;
                        });
                    })
                },*/

            };
        }

    }
}
