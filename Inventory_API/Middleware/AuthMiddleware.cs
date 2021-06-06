using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, InventoryDbContext _context)
        {
            if (context.Request.Path.Value.Contains("/ChangesHub"))
            {
                await _next.Invoke(context);
                return;
            }

            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                //Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                Encoding encoding = Encoding.GetEncoding("utf-8");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));    

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                var user = _context.Users
                    .AsNoTracking()
                    .Include(u => u.Password)
                    .SingleOrDefault(u => u.Username == username);

                if (user != default && BCrypt.Net.BCrypt.Verify(password, user.Password.EncryptedPassword))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Authenticate\"");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Authenticate\"");
                return;
            }
        }
    }
}
