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
            UpdateInscriptionTest();
            CheckInscriptionValidation();

            if (UserSettings.UserInscriptionId == "")
                MainPage = new NavigationPage(new SignInPage());
            else
            {
                if (!DateTime.TryParse(UserSettings.UserInscriptionTest, out _))
                {
                    if (UserSettings.UserInscriptionValide == "")
                        MainPage = new NavigationPage(new PayementPage());
                    else
                        MainPage = new ShellPage();
                }
                else
                {
                    if (DateTime.Parse(UserSettings.UserInscriptionTest).CompareTo(DateTime.Now) < 0)
                        MainPage = new NavigationPage(new PayementPage());
                    else
                        MainPage = new ShellPage();
                }
            }
        }

        public void UpdateInscriptionTest()
        {
            if(DateTime.TryParse(UserSettings.UserInscriptionTest, out _))
            {
                if(DateTime.Parse(UserSettings.UserInscriptionTest).CompareTo(DateTime.Now) < 0)
                    UserSettings.UserInscriptionTest = "Expirée";
            }
        }
        public void CheckInscriptionValidation()
        {
            if (UserSettings.UserInscriptionId != "")
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if(UserSettings.UserInscriptionTest == "")
                    {
                        var result = ApiService.GetInscription();
                        if (result.Result.ELement != null)
                            if (result.Result.ELement.Etat == 3)
                            {
                                UserSettings.UserInscriptionTest = result.Result.ELement.DateInscription.ToString();
                                return;
                            }
                            else
                                if(result.Result.ELement.Etat == 2)
                                {
                                    UserSettings.UserInscriptionValide = "true";
                                    return;
                                }
                    }
                    else
                    if(UserSettings.UserInscriptionTest == "Expirée")
                        {
                        if (UserSettings.UserInscriptionValide == "")
                        {
                            var result = ApiService.GetInscription();
                            if (result.Result.ELement != null)
                                if (result.Result.ELement.Etat == 2)
                                {
                                    UserSettings.UserInscriptionValide = "true";
                                    return;
                                }
                        }
                        }
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
