using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Models
{
    public class Opleiding
    {
        public int OpleidingID { get; set; }
        public string Naam { get; set; }
        public Decimal Prijs { get; set; }
        public int AantalLesuren { get; set; }
        public List<KlantOpleiding> KlantOpleidingen { get; set; }
    }
}
