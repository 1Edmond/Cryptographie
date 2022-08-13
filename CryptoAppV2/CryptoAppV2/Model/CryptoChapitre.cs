using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CryptoAppV2.Model
{
    public class CryptoChapitre
    {
        public string AuteurImage { get; set; }
        public string AuteurName { get; set; }
        public string Difficulte { get; set; }
        public string Details { get; set; }
        public string Titre { get; set; }

        public ICommand NavigationCommand { get; set; }

    }
    }
