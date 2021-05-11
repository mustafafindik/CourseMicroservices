using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Course.Services.Identity.Utilities.Extensions
{

    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim("Email", email));
        }
      
        public static void addId(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim("UserId", nameIdentifier));
        }

       
    }

}
