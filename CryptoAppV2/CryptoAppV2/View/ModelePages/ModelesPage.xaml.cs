using CryptoAppV2.Data;
using CryptoAppV2.Model;
using CryptoAppV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2.View.ModelePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModelesPage : ContentPage
    {
        public ModelesPage()
        {
            InitializeComponent();
            EventHandler value = async delegate
                        {
                            Myrefresh.IsRefreshing = true;
                            await Task.Delay(TimeSpan.FromSeconds(3));
                            Myrefresh.IsRefreshing = false;
                        };
            Myrefresh.Refreshing += value;
            
        }
      
        private void ModeleCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked)
                {
                    UserModele UsedModele = checkBox.BindingContext as UserModele;
                    UserSettings.UserModele = UsedModele.Nom;
                    this.DisplayToastAsync("Veuillez raffraichir la liste pour observer les modifications.");
                }
            }
        }
    }
}