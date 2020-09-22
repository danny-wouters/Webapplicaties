using System;
using System.ComponentModel.DataAnnotations;

namespace HelloCore.Models
{
    public class Klant
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public string Voornaam { get; set; }

        [DataType(DataType.Date)]
        public DateTime AangemaaktDatum { get; set; }
    }
}
