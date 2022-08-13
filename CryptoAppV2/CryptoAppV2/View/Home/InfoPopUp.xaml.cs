using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPopUp : Popup
    {
        public InfoPopUp()
        {
            InitializeComponent();
        }
        public InfoPopUp(string texte)
        {
            InitializeComponent();
            Info.Text = texte;
            Quitter.Clicked += delegate
            {
                //  this.BackgroundColor = Color.Transparent;
                // await this.ScaleTo(0, 2000);
                Dismiss("");
            };

        }
    }
}