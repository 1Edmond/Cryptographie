using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    public class UseCondition
    {
        public String Titre { get; set; }
        public String Condition
        {
            get
            {
                return this.Condition;
            }
            set
            {
                Condition = value;
            }
        }
        public int Numero
        {
            get
            {
                return ++Indixe;
            }
            private set
            {
                Numero = value;
            }
        }
        public static int Indixe { get; set; } = 0;
    }
}
