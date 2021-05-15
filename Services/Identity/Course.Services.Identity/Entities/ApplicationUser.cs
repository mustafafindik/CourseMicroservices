using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Course.Services.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
