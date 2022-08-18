using CryptoAppV2.CrAlgorithme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.HistoriquePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(HistoriqueId), "HistoriqueId")]
    public partial class HistoriqueDetailsPage : ContentPage, IQueryAttributable
    {
        public CryptoCode cryptoCode { get; set; } = new CryptoCode();
        public CryptoEtape cryptoEtape{ get; set; } = new CryptoEtape();
        public HistoriqueDetailsPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            HistoriqueId = int.Parse(query["HistoriqueId"]);
            DisplayHistoriqueInformation();
        }
        public async void DisplayHistoriqueInformation()
        {
            if (HistoriqueId > 0)
            {
                var Historique = App._UserHistoriqueManager.Get(HistoriqueId);
                Libelle.Text = Historique.Libelle;
                Description.Text = Historique.Description;
                DateOperation.Text = $"{Historique.DateOperation}";
                var data = Historique.Data.Split('*');
                var type = data[1];
                var nom = data[0];
                switch (type)
                {
                    case "Codage":
                        {
                            switch (nom)
                            {
                                case "Affine":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du codage affine du message {data[2]}. La solution est  " + cryptoCode.CodageAffine(
                                            data[2],
                                            int.Parse(valeurs[0]),
                                            int.Parse(valeurs[1]),
                                            valeurs[2]
                                            );
                                        var etapes = cryptoEtape.CodageAffine(
                                             data[2],
                                            int.Parse(valeurs[0]),
                                            int.Parse(valeurs[1]),
                                            valeurs[2]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "RSA":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du codage RSA avec le message {data[2]}. La solution est " + cryptoCode.CodageRSA(
                                            data[2],
                                            valeurs[0],
                                            valeurs[1]
                                            );
                                        var etapes = cryptoEtape.CodageRSA(
                                             data[2],
                                            valeurs[0],
                                            valeurs[1]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "Vernam":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du codage de Vernam avec le message {data[2]}. La                         solution est " + cryptoCode.VernamCryptoSysteme(
                                            data[2],
                                            valeurs[0]
                                            );

                                    }
                                    break;
                                case "Vigenere":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du codage de Vigenère avec le message {data[2]}. La solution est " + cryptoCode.CodageVigenere(
                                            data[2],
                                            valeurs[0]
                                            );
                                        var etapes = cryptoEtape.CodageVigenere(
                                             data[2],
                                            valeurs[0]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "Merkle Hellman":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du codage de Merkle Hellman avec le message {data[2]}. La solution est " + cryptoCode.CodageBinaireMerkleHellman(
                                            data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        var etapes = cryptoEtape.CodageMerkleHellman(
                                             data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "Hill":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du codage de Hill avec le message {data[2]}. La solution est " + cryptoCode.CodageHill(
                                            data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        var etapes = cryptoEtape.CodageDeHill(
                                             data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;

                            }
                        }
                        break;
                    case "Decodage":
                        {
                            switch (nom)
                            {
                                case "Affine":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du décodage affine du message {data[2]}. La solution est " + cryptoCode.DecodageAffine(
                                            data[2],
                                            int.Parse(valeurs[0]),
                                            int.Parse(valeurs[1]),
                                            valeurs[2]
                                            );
                                        var etapes = cryptoEtape.DecodageAffine(
                                             data[2],
                                            int.Parse(valeurs[0]),
                                            int.Parse(valeurs[1]),
                                            valeurs[2]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "RSA":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du décodage RSA avec le message {data[2]}. La solution est " + cryptoCode.DecodageRSA(
                                            data[2],
                                            valeurs[0],
                                            valeurs[1]
                                            );
                                        var etapes = cryptoEtape.DecodageRSA(
                                             data[2],
                                            valeurs[0],
                                            valeurs[1]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "Vernam":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du décodage de Vernam avec le message {data[2]}. La                         solution est " + cryptoCode.VernamCryptoSysteme(
                                            data[2],
                                            valeurs[0]
                                            );

                                    }
                                    break;
                                case "Vigenere":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du décodage de Vigenère avec le message {data[2]}. La           solution est " + cryptoCode.DecodageVigenere(
                                            data[2],
                                            valeurs[0]
                                            );
                                        var etapes = cryptoEtape.DecodageVigenere(
                                             data[2],
                                            valeurs[0]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "Merkle Hellman":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du décodage de Merkle Hellman avec le message {data[2]}. La solution est " + cryptoCode.DecodageMerkleHellman(
                                            data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        var etapes = cryptoEtape.DecodageMerkleHellman(
                                             data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                                case "Hill":
                                    {
                                        var valeurs = data[3].Split(':')[1].Split('+');
                                        Result.Text = $"Il s'agit du décodage de Hill avec le message {data[2]}. La solution est " + cryptoCode.DecodageHill(
                                            data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        var etapes = cryptoEtape.DecodageDeHill(
                                             data[2],
                                            valeurs[0],
                                            valeurs[1],
                                            valeurs[2]
                                            );
                                        MyCollection.ItemsSource = etapes;
                                        MyCollection.ScrollTo(etapes.Last());
                                        await Task.Delay(2000);
                                        MyCollection.ScrollTo(etapes.First());
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }
        public int HistoriqueId { get; set; }
    }
}