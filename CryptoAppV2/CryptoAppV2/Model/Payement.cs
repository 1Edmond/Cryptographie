using CryptoAppV2.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CryptoAppV2.Model
{
    public class Payement
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int InscriptionId { get; set; }
        public DateTime DatePayement { get; set; } = DateTime.Now;

        public int Etat { get; set; }
        public Payement(string reference)
        {
            DatePayement = DateTime.Now;
            Reference = reference;
            InscriptionId = int.Parse(UserSettings.UserInscriptionId);
            Etat = 0;
        }
        
    }
}
