using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ais.ImageSplitter.Api.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IOptions<SecurityOptions> _securityOptions;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptions<SecurityOptions> securityOptions) : base(options, logger, encoder, clock)
        {
            _securityOptions = securityOptions;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return await Task.Run(() => AuthenticateResult.Fail("Missing Authorization Header"));
            }

            try
            {
                AuthenticationHeaderValue authHeader =
                    AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                string username = credentials[0];
                string password = credentials[1];

                if (!UserIsAuthenticated(username, password))
                {
                    return await Task.Run(() => AuthenticateResult.Fail("Invalid Username or Password"));
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return await Task.Run(() => AuthenticateResult.Success(ticket));
            }
            catch
            {
                return await Task.Run(() => AuthenticateResult.Fail("Invalid Authorization Header"));
            }
        }

        private bool UserIsAuthenticated(string username, string password)
        {
            return username.Equals(_securityOptions.Value.UserName) && password.Equals(_securityOptions.Value.Password);
        }
    }
}