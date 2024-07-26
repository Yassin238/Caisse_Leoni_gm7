using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caisse_Leoni_gm7.Models
{
    public class Alimentation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Montant { get; set; }
        public DateTime Date { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
