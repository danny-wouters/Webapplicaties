using HelloCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.ViewModels
{
    public class CreateOpleidingViewModel
    {
        public Opleiding Opleiding { get; set; }
        public IEnumerable<SelectListItem> KlantenLijst { get; set; }
        public IEnumerable<int> GeselecteerdeKlanten { get; set; }
    }
}
