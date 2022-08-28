using CryptoAppV2.Data;
using CryptoAppV2.Model;
using CryptoAppV2.Service;
using CryptoAppV2.View;
using CryptoAppV2.View.Auth;
using CryptoAppV2.View.Home;
using CryptoAppV2.View.ModelePages;
using CryptoAppV2.View.SettingsPages;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoAppV2
{
    public partial class App : Application
    {

        public static string DBNAME = "CryptographieDb.db3";
        public static string database = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME);
        
        public static UserHistoriqueManager _UserHistoriqueManager;
        public static UserHistoriqueManager UserHistoriqueManager
        {
            get
            {
                if (_UserHistoriqueManager == null)
                    _UserHistoriqueManager = new UserHistoriqueManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME));
                return _UserHistoriqueManager;
            }
            set
            {
                if (value == null)
                    value = new UserHistoriqueManager(database);
                _UserHistoriqueManager = value;
            }
        }
       
        public static UserModeleManager _UserModeleManager;
        public static UserModeleManager UserModeleManager
        {
            get
            {
                if (_UserModeleManager == null)
                    _UserModeleManager = new UserModeleManager(database);
                return _UserModeleManager;
            }
            set
            {
                _UserModeleManager = value;
            }
        }


        public App()
        {
            InitializeComponent();
            UserHistoriqueManager = new UserHistoriqueManager(database);
            UserModeleManager = new UserModeleManager(database);
            //var navigationPage = new ProfilPage();
            //MainPage = navigationPage;
            CheckInscriptionValidation();
            if(UserSettings.UserInscriptionId == "")
                MainPage = new NavigationPage(new SignInPage());
            else
            {
                if(UserSettings.UserInscriptionValide == "")
                    MainPage = new NavigationPage(new PayementPage());
                else
                    MainPage = new ShellPage();
            }
            
                  //  MainPage = new ShellPage();
               
           // ApiService.GetInscription();
        }

        public void CheckInscriptionValidation()
        {
            if (UserSettings.UserInscriptionId != "")
            {
                if(Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var result = ApiService.GetInscription();
                    if(result.Result.ELement != null)
                        if(result.Result.ELement.Etat == 2)
                            UserSettings.UserInscriptionValide = "true";
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
