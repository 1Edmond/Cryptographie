using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    public class ImagesVm
    {
    public List<UserImage> Images { get; set; }
        public ImagesVm()
        {
            for (int i = 1; i <= 20; i += 2)
                Images.Add(new UserImage { Image1 = $"{i}.png", Image2 = $"{i+1}.png" });
            ChangeProfile = new Command(async (ord) =>
            {
                UserSettings.UserProfile = (string)ord;
                await Shell.Current.DisplayToastAsync("Modification de votre photo de profile réussie avec succès");
            });
        }
        public ICommand ChangeProfile { get; set; }
    }
}
