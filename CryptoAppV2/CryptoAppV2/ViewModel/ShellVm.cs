using CryptoAppV2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoAppV2.ViewModel
{
    public class ShellVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string ProfileImage { get => UserSettings.UserProfile; }
        public int AnimationDelay { get; set; } = 20;
        public ShellVm()
        {
            var ImageListe = new List<string>()
            {
                
            };
            for (int i = 0; i < 5; i++)
                ImageListe.Add($"IM{i+1}.png");            
            var random = new Random();
            Device.StartTimer(TimeSpan.FromSeconds(AnimationDelay), () =>
            {
                Task.Run(() =>
                {
                    UserSettings.UserProfile = $"{ImageListe[random.Next(ImageListe.Count)]}";
                    OnPropertyChanged(nameof(ProfileImage));
                });
                return true;
            });
        }
      
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            else
                return;
        }

    }
}
