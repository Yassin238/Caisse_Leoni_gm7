using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caisse_Leoni_gm7.Models
{
    public class Depense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Montant { get; set; }
        public DateTime Date { get; set; }
        public string Motif { get; set; } = string.Empty;
    }
}
