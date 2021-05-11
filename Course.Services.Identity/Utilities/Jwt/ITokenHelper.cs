using Course.Services.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity.Utilities.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(ApplicationUser user);
    }
}
