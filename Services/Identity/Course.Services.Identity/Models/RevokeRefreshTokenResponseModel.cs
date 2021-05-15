using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity.Models
{
    public class RevokeRefreshTokenResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
