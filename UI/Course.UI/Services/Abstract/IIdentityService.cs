using Course.Shared.Results;
using Course.UI.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.UI.Services.Abstract
{
    public interface IIdentityService
    {
        Task<IResult> SignIn(SignInModel signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task<IResult> RevokeRefreshToken();
    }
}
