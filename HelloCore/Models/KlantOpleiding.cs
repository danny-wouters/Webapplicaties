using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Models
{
    public class KlantOpleiding
    {
        public int KlantOpleidingID { get; set; }
        public int KlantID { get; set; }
        public Klant Klant { get; set; }
        public int OpleidingID { get; set; }
        public Opleiding Opleiding { get; set; }
    }
}
