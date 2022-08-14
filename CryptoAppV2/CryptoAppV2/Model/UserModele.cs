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
                    Valeur = new Dictionary<int, char>()
                    {
                        {65 , 'A' },
                        {66 , 'B' },
                        {67 , 'C' },
                        {68 , 'D' },
                        {69 , 'E' },
                        {70 , 'F' },
                        {71 , 'G' },
                        {72 , 'H' },
                        {73 , 'I' },
                        {74 , 'J' },
                        {75 , 'K' },
                        {76 , 'L' },
                        {77 , 'M' },
                        {78 , 'N' },
                        {79 , 'O' },
                        {80 , 'P' },
                        {81 , 'Q' },
                        {82 , 'R' },
                        {83 , 'S' },
                        {84 , 'T' },
                        {85 , 'U' },
                        {86 , 'V' },
                        {87 , 'W' },
                        {88 , 'X' },
                        {89 , 'Y' },
                        {90 , 'Z' },
                    }
                };
            }
        }
        [PrimaryKey, AutoIncrement]
        public int Id{ get; set; }
        public string Nom { get; set; }
        [Ignore]
        public Dictionary<int, char> Valeur { get; set; }
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
            Valeur = new Dictionary<int, char>();
        }
    }
}
