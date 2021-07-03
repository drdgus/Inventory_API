using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Inventory_API.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory_API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthMiddleware> _logger;

        public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
        {
            this._next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, InventoryDbContext _context)
        {
            //using (var reader = new StreamReader(context.Request.Body))
            //{
            //    var body = await reader.ReadToEndAsync();
            //    _logger.LogInformation(body.Replace(",", ",\n"));
            //}

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
