using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.BLL.HelperService
{
    public class PasswordHash
    {
        public static string DoHash(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public async Task<bool> CheckPassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);

    }
}
