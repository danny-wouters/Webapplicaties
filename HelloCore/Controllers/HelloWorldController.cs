using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloCore.Controllers
{
    public class HelloWorldController : Controller
    {
        public string Index()
        {
            return "Dit is de index action method";
        }

        public String Welkom()
        {
            return "Dit is de welkom action method";
        }

        public String Bestelling(int id)
        {
            return "Dit is de bestelling met id: " + id;
        }

        public String Boodschap(string voornaam, string boodschap)
        {
            return "Boodschap van " + voornaam + ": " + boodschap;
        }

    }
}
