using HelloCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Areas.Identity.Data
{
    public class CustomUser : IdentityUser
    {
        [PersonalData]
        public Klant Klant { get; set; }
    }
}
