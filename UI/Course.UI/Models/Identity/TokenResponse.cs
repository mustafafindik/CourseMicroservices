using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.UI.Models.Identity
{
    public class TokenResponse
    {
        public string RefleshToken { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
