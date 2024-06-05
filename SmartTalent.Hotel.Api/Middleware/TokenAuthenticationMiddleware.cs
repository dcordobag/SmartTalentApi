namespace SmartTalent.Hotel.Api.Middleware
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;


    public class TokenAuthenticationMiddleware(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
        UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value))
                return Task.FromResult(AuthenticateResult.Fail("Debe estar logueado para ingresar a esta página"));

            try
            {
                var token = value.ToString().Split(' ')[1];
                // Add your token validation logic here
                if (IsValidToken(token))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "username") }; // You can add more claims as needed
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return Task.FromResult(AuthenticateResult.Fail("Expiró la sesion o no está logueado"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail("Expiró la sesion o no está logueado"));
            }
        }

        private static bool IsValidToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Constants.Constants.SecretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true, // You may set these to true if required
                    ValidateAudience = true, // You may set these to true if required,
                    ValidIssuer = Constants.Constants.Issuer,
                    ValidAudience = Constants.Constants.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
