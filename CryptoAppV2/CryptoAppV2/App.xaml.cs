using CryptoAppV2.Data;
using CryptoAppV2.View;
using CryptoAppV2.View.Home;
using CryptoAppV2.View.ModelePages;
using System;
using System.IO;
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
           // UserHistoriqueManager = new UserHistoriqueManager(database);
            UserModeleManager = new UserModeleManager(database);
            var navigationPage = new ShellPage();
            MainPage = navigationPage;
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
