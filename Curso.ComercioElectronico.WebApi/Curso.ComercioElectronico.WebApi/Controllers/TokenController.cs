using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtConfiguration jwtConfiguration;


        public TokenController(IOptions<JwtConfiguration> options)
        {
            this.jwtConfiguration = options.Value;
        }


        [HttpPost]
        public async Task<string> TokenAsync(UserInput input)
        {
            //1. Validar User.
            var userTest = new[] {
                new {Name="foo",Roles=new[]{ "Admin" } },
                new {Name="foo1",Roles=new[]{ "Admin", "Consulta" }},
                new {Name="foo2",Roles=new[]{ "Soporte" }},
                } ;
            if (!userTest.Any(x=>x.Name.Equals(input.UserName)) || input.Password != "123")
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }

            var claims = new List<Claim>();

            var user = userTest.Single(x=>x.Name.Equals(input.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
            claims.Add(new Claim("UserName", input.UserName));
            foreach(var claim in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, claim));
            }
            claims.Add(new Claim("Licencia", true.ToString()));
            claims.Add(new Claim("Ecuatoriano", true.ToString()));
            claims.Add(new Claim("Seguro", "seguro123"));




            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return jwt;
        }


    }

    public class UserInput
    {

        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}

