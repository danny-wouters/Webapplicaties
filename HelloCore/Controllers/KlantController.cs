using System.Collections.Generic;
using HelloCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCore.Controllers
{
    public class KlantController : Controller
    {
        public IActionResult Index()
        {
            List<Klant> klanten = new List<Klant>();

            klanten.Add(new Klant() { Id = 1, Naam = "De Neve", Voornaam = "Anneleen", AangemaaktDatum = new System.DateTime(2019, 1, 20) });
            klanten.Add(new Klant() { Id = 2, Naam = "Bruynseels", Voornaam = "Nele", AangemaaktDatum = new System.DateTime(2020, 2, 4) });
            klanten.Add(new Klant() { Id = 3, Naam = "Naert", Voornaam = "Joris", AangemaaktDatum = new System.DateTime(2020, 1, 5) });

            return View(klanten);
        }
    }
}
