namespace ApiTest.Services
{
    using System.Collections.Generic;

    using ApiTest.Model;

    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User GetUser(int id);

        User Create(User user);

        User Update(int id, User user);

       // User Login(UserLogin userLogin);

        void Delete(int id);
    }
}
