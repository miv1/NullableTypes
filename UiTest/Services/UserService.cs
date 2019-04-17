namespace UiTest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Text;
    using System.Threading.Tasks;
    using UiTest.Models;

    public class UserService : HttpService
    {
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users;
            users = await this.Get<List<User>>(path: $"{BaseUrl}/users");
            return users;
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            UserLogin login1 = new UserLogin
            {
                Email = email,
                Password = password
            };
            var user2 = await Validate(login1, path: $"{BaseUrl}/login");
            return user2;
        }

        public async Task<User> CreateNewUser(User newUser)
        {
            var response = await Post(newUser, path: $"{BaseUrl}/create");
            return response;
        }
    }
}
