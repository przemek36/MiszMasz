using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text;

namespace MiszMasz.Pages.Shared
{
    public class AuthorizedPageModel : PageModel
    {
        public int Authorize()
        {
            if (HttpContext.Session.TryGetValue("UserID", out var UserIdBytes) 
                && int.TryParse(Encoding.Default.GetString(UserIdBytes), out var UserId))
            {
                return UserId;
            }

            throw new Exception("Nie masz dostepu do tej strony.");
        }
    }
}
