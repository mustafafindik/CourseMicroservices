using Course.Services.Identity.Dtos;
using Course.Services.Identity.Entities;
using Course.Services.Identity.Utilities.Jwt;
using Course.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity.Services.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<ApplicationUser>> SignUp(SignupDto registerDto);
        Task<IDataResult<ApplicationUser>> Login(LoginDto loginDto);
        Task<IResult> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(ApplicationUser user,string ipAddress);
        Task<IDataResult<AccessToken>> RefreshToken(string token, string ipAddress);
        Task<IResult> RevokeToken(string token, string ipAddress);
    }
}
