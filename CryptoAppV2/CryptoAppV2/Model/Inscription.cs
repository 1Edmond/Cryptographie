using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    public class Inscription
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Contact { get; set; }
        public int Etat { get; set; } = 0;
        public DateTime DateInscription { get; set; } = DateTime.Now;
        public int Id { get; set; }
    }
}
