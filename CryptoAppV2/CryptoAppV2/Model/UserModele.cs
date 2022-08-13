using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    internal class UserModele
    {
        public static UserModele BaseModel
        {
            get
            {
                return new UserModele()
                {
                    Nom = "BaseModele",
                };
            }
            set { BaseModel = value; }
        }
        public string Nom { get; set; }
        public Dictionary<int, char> Valeur { get; set; }
        public UserModele()
        {
            Valeur = new Dictionary<int, char>();
            for (int i = 0; i < 26; i++)
                Valeur[i] = Convert.ToChar(i + 65);
        }
    }
}
