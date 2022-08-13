using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EtapePage : ContentPage
    {
        public EtapePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);



        }
        ObservableCollection<Etape> Etapes = new ObservableCollection<Etape>();
        public EtapePage(string information, string label, List<Etape> etapes)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MyCollection.BindingContext = this;
            MyHeader.Text = label;
            MyData.Text = information;
            Etapes.Clear();
            etapes.ForEach(x => Etapes.Add(x));
            MyCollection.ItemsSource = Etapes;
            _ = ScrollAnimation(etapes);
            Quitter.Clicked += async delegate
            {
                await Navigation.PopModalAsync();
            };

        }

        private async Task ScrollAnimation(List<Etape> etapes) => await Task.Delay(2000).
            ContinueWith((ord) => MyCollection.ScrollTo(etapes.Last())).ContinueWith(async (ord) => await Task.Delay(2000).
            ContinueWith((ord1) => MyCollection.ScrollTo(etapes.First())));
    }
}