using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoAppV2.Model
{
    /// <summary>
    /// Classe gérant les informations dans l'application
    /// </summary>
    public class Information
    {
        /// <summary>
        /// propriété description d'une information
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructeur d'une information
        /// </summary>
        public Information(string description)
        {
            Description = description;
        }
        public Information()
        {

        }
        public override string ToString()
        {
            return Description;
        }


    }
}
