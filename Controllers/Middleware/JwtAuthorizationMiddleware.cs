using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static STAReportsAPI.Models.UserGroups_Model;

namespace STAapi.Middleware
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtAuthorizationMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }
        //[ApiController]
        //[Route("auth")]
        //public class AuthController : ControllerBase
        //{
        //    // 🔐 This endpoint is protected by your JwtAuthorizationMiddleware
        //    [Authorize]
        //    [HttpGet("validate")]
        //    public IActionResult Validate()
        //    {
        //        // If execution reaches here, the token is VALID
        //        return Ok(new { valid = true });
        //    }
        //}
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // ✅ SKIP TOKEN VALIDATION FOR FIRST LOGIN & PRE-TOKEN
            if (path.Contains("/auth/generate-token") ||
                path.Contains("/auth/pre-token") ||
                path.Contains("/auth/login") )
                //path.Contains("/userslist") ||
                //path.Contains("/usergroups") ||  
                //path.Contains("/rolemapping")) 
            {
                await _next(context);
                return;
            }
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Missing or invalid Authorization header");
                return;
            }

            var oldToken = authHeader.Substring("Bearer ".Length).Trim();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // 1️⃣ Validate the old token
                ClaimsPrincipal principal = tokenHandler.ValidateToken(
                    oldToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _config["Jwt:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken
                );

                // 2️⃣ Ensure correct algorithm
                if (validatedToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid token algorithm");
                    return;
                }

                // 3️⃣ Attach user claims to context
                context.User = principal;

                var expUtc = jwtToken.ValidTo;
                var timeLeft = expUtc - DateTime.UtcNow;
                string newTokenString = "";

                //// 4️⃣ Generate new token with same claims but new expiry
                //    var newToken = new JwtSecurityToken(
                //    issuer: _config["Jwt:Issuer"],
                //    audience: _config["Jwt:Audience"],
                //    claims: jwtToken.Claims,
                //    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:ExpiryMinutes"])),
                //    signingCredentials: new SigningCredentials(
                //        new SymmetricSecurityKey(key),
                //        SecurityAlgorithms.HmacSha256
                //    )
                //);
                //newTokenString = tokenHandler.WriteToken(newToken);


                //if (timeLeft <= TimeSpan.FromMinutes(1))
                if (timeLeft <= TimeSpan.FromMinutes(Convert.ToInt32(_config["Jwt:Newtoken"])))
                {
                    var newToken = new JwtSecurityToken(
                        issuer: _config["Jwt:Issuer"],
                        audience: _config["Jwt:Audience"],
                        claims: jwtToken.Claims,
                        expires: DateTime.UtcNow.AddMinutes(
                            Convert.ToInt32(_config["Jwt:ExpiryMinutes"])
                        ),
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256
                        )
                    );

                    newTokenString = tokenHandler.WriteToken(newToken);
                }
                else
                {
                    // ✅ reuse existing token
                    newTokenString = oldToken;
                } 

                // 5️⃣ Return new token in response header
                context.Response.Headers["X-New-Token"] = newTokenString;
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token expired");
                return;
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            // 6️⃣ Continue pipeline
            await _next(context);
        }
    }
}
