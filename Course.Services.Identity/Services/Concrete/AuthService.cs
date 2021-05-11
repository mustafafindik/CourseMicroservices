using Course.Services.Identity.Dtos;
using Course.Services.Identity.Entities;
using Course.Services.Identity.Services.Abstract;
using Course.Services.Identity.Utilities.Jwt;
using Course.Shared.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity.Services.Concrete
{
    public class AuthService: IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService(UserManager<ApplicationUser> userManager, ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
        }

        public  IDataResult<AccessToken> CreateAccessToken(ApplicationUser user)
        {          
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, "Token Oluştu");
        }

        public async Task<IDataResult<ApplicationUser>> Login(LoginDto loginDto)
        {
            var userToCheck =await _userManager.FindByEmailAsync(loginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<ApplicationUser>("Kullanıcı Yok");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(userToCheck, loginDto.Password);
            if (!checkPassword)
            {
                return new ErrorDataResult<ApplicationUser>("Şifre Yanlış");
            }

            return new SuccessDataResult<ApplicationUser>(userToCheck, "Giriş Başarılı");
        }

        public async Task<IDataResult<ApplicationUser>> SignUp(SignupDto registerDto)
        {
            var result =await _userManager.CreateAsync(new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email }, registerDto.Password);
            if (result.Succeeded)
            {
                var user =await _userManager.FindByEmailAsync(registerDto.Email);
                return new SuccessDataResult<ApplicationUser>(user,"Başaıyla kaydedildi");
            }
            return new ErrorDataResult<ApplicationUser>("Kayıt Başarısız");
        }

        public async Task<IResult> UserExists(string email)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return new ErrorResult("Kullanıcı zaten var");
            }
            return new SuccessResult();
        }
    }
}
