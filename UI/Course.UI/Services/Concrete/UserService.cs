using Course.UI.Models.User;
using Course.UI.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.UI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpclient;

        public UserService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _httpclient.GetFromJsonAsync<UserViewModel>("/api/Auth/getUser");
        }

       
    }
}
