using Microsoft.AspNetCore.Http;
using System;

namespace MiszMasz.Helpers
{
    public class AuthEndpoint : Attribute
    {
        public AuthEndpoint(HttpContext context)
        {
            if (context.Session.TryGetValue("Username", out var username))
                throw new Exception("Nie masz dostepu do tej strony.");
        }
    }
}
