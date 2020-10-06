using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}
