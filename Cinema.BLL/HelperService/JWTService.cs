using Cinema.DAL.Implemantations;
using Cinema.DAL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.BLL.HelperService
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;

        public JWTService(IConfiguration configuration,
            UnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public string GenerateToken(ApplicationUser applicationUser)
        {

            var claims = new List<Claim>
            {
                new Claim("Email",applicationUser.Email),
                new Claim("Role",applicationUser.Role)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: sign,
                expires: DateTime.Now.AddDays(1),
                issuer: "localhost",
                audience: "localhost");


            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
