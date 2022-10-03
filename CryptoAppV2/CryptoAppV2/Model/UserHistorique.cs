using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CryptoAppV2.Model
{
    [Table("UserHistorique")]
    public class UserHistorique
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public DateTime DateOperation { get; set; }
        public string AvatarText { get =>
                !Libelle.Contains("de") ?
                    Libelle.Substring(0, 1).ToUpper() + Libelle.Substring(Libelle.IndexOf(" ") + 1, 1).ToUpper() :
                Libelle.Substring(0, 1).ToUpper() + Libelle.Substring(Libelle.IndexOf(" de") + 1, 1).ToUpper()
                ; 
                }
        public Color AvatarColor
        {
            get
            {
                var ListColor = new List<Color>()
                {
                   
                    Color.Gray,
                    Color.Khaki,
                    Color.LightCyan,
                    Color.LightBlue,
                    Color.LightSkyBlue,
                    Color.SlateBlue,
                    Color.SlateGray,
                    Color.Teal,
                    Color.WhiteSmoke,

                };
                return ListColor[new Random().Next(0, ListColor.Count)];
            }
        }
    }
}
