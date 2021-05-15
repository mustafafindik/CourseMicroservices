using Course.Services.Identity.Data;
using Course.Services.Identity.Dtos;
using Course.Services.Identity.Entities;
using Course.Services.Identity.Services.Abstract;
using Course.Services.Identity.Utilities.Jwt;
using Course.Shared.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Course.Services.Identity.Services.Concrete
{
    public class AuthService: IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
            _context = context;
        }

        public  IDataResult<AccessToken> CreateAccessToken(ApplicationUser user,string ipAddress)
        {          
            var accessToken = _tokenHelper.CreateToken(user);
            var refreshToken = generateRefreshToken(ipAddress);
            accessToken.RefleshToken = refreshToken.Token;

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            _context.Update(user);
            _context.SaveChanges();

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


        public async Task<IDataResult<AccessToken>> RefreshToken(string token, string ipAddress)
        {

            var user =  _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;
           
            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            await _context.SaveChangesAsync();

            var accessToken = _tokenHelper.CreateToken(user);      
            accessToken.RefleshToken = newRefreshToken.Token;
            return new SuccessDataResult<AccessToken>(accessToken, "Token Oluştu");
        }


        public async Task<IResult> RevokeToken(string token, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return new ErrorResult("Bu Token'a ait kullanıcı yok");

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return new ErrorResult("Reflesh token aktif değil");

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return new SuccessResult("Token Revoked");
        }


        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
