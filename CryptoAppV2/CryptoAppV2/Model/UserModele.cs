using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    public class UserModele
    {
        public static UserModele BaseModel
        {
            get
            {
                return new UserModele()
                {
                    Nom = "BaseModele",
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

        public int Id{ get; set; }
        public string Nom { get; set; }
        public Dictionary<int, char> Valeur { get; set; }
        public UserModele()
        {
            Valeur = new Dictionary<int, char>();
        }
    }
}
