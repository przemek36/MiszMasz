using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiszMasz.Pages.Authenticate
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("Username");
        }
    }
}
