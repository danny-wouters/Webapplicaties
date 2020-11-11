using HelloCore.Areas.Identity.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HelloCore.Models
{
    public class Klant
    {
        public int KlantID { get; set; }
        public string Naam { get; set; }

        public string Voornaam { get; set; }

        public string VolledigeNaam => $"{Voornaam} {Naam}";

        [Display(Name = "Datum aangemaakt")]
        [DataType(DataType.Date)]
        public DateTime AangemaaktDatum { get; set; }
        public ICollection<Bestelling> Bestellingen { get; set; }
        public List<KlantOpleiding> KlantOpleidingen { get; set; }

        //[ForeignKey("CustomUser")]
        public string UserID { get; set; }
        public CustomUser CustomUser { get; set; }

        public override string ToString()
        {
            StringBuilder stringbuilder = new StringBuilder();
            stringbuilder.Append($"KlantID: {KlantID}; ");
            stringbuilder.Append($"Voornaam: {Voornaam}; ");
            stringbuilder.Append($"Naam: {Naam}; ");
            stringbuilder.Append($"Datum aangemaakt: {AangemaaktDatum}; ");

            return stringbuilder.ToString();
        }
    }
}
