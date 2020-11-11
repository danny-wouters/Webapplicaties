using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HelloCore.Areas.Identity.Data;
using HelloCore.Data;
using HelloCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HelloCore.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly HelloCoreContext _context;

        public DownloadPersonalDataModel(
            UserManager<CustomUser> userManager,
            ILogger<DownloadPersonalDataModel> logger,
            HelloCoreContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            Klant klant = await _context.Klanten.FirstOrDefaultAsync(x => x.UserID == user.Id);
            user.Klant = klant;
            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(CustomUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
        }
    }
}
