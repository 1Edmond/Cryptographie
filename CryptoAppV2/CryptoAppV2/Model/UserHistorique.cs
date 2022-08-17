using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
