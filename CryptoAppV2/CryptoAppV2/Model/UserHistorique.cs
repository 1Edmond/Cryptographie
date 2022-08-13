using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    public class UserHistorique
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
        public DateTime DateOperation { get; set; }
    }
}
