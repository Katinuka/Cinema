using Cinema.DAL.Context;
using Cinema.DAL.Implemantations;
using Cinema.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.BLL.Services
{
    public class ApplicationUserServices : GenericRepository<ApplicationUser>
    {
        public ApplicationUserServices(ApplicationDbContext context)
            : base(context)
        {

        }

        public async Task<bool> CheckEmailExist(string email)
        {
            var result = await _context.ApplicationUsers.AnyAsync(e => e.Email == email);

            if (result)
                return true;

            return false;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email) 
            => await _context.ApplicationUsers.FirstOrDefaultAsync(e => e.Email == email);
      

        





    }
}
