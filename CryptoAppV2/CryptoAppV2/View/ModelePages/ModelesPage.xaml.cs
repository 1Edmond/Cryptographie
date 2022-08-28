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
using Xamarin.CommunityToolkit.UI.Views.Options;
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
      
        private async void ModeleCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(sender is CheckBox checkBox)
            {
                if (checkBox.IsChecked)
                {
                    UserModele UsedModele = checkBox.BindingContext as UserModele;
                    UserSettings.UserModele = UsedModele.Nom;
                    var options = new ToastOptions()
                    {
                        BackgroundColor = Color.FromHex("#28C2FF"),
                        Duration = new TimeSpan(7500),
                        CornerRadius = new Thickness(20),
                        MessageOptions = new MessageOptions()
                        {
                            Foreground = Color.White,
                            Message = "Veuillez raffraichir la liste pour observer les modifications.",
                            Padding = new Thickness(10)
                        }
                    };
                    await this.DisplayToastAsync(options);
                }
            }
        }
    }
}