using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    [Table("UserModele")]
    public class UserModele
    {
        public static UserModele BaseModel
        {
            get
            {
                return new UserModele()
                {
                    Nom = "Modèle de base",
                    DateAjout = DateTime.Now ,
                    ModeleValue = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z",
                   
                };
            }
        }
        [PrimaryKey, AutoIncrement]
        public int Id{ get; set; }
        public string Nom { get; set; }
        [Ignore]
        public Dictionary<int, char> Valeur { 
            get
            {
                var temp = new Dictionary<int, char>();
                var newValue = ModeleValue.Split(',');
                for (int i = 0; i < newValue.Length; i++)
                    temp[i] = Convert.ToChar(newValue[i]);
                return temp;
            }
        }
        public bool IsUsed { 
            get 
            {
                if (Nom == UserSettings.UserModele)
                    return true;
                return false;
            }
         }
        public int NbrElement { get
            {
                return ModeleValue.Split(',').Length;
            }
        }

        public DateTime DateAjout { get; set; }

        public string Avatar { 
            get 
            {
                return Nom.Substring(0,1).ToUpper();
            }
        }

        public string ModeleValue { get; set; } = String.Empty;
        public UserModele()
        {
           
        }
    }
}
