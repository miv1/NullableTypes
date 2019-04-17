namespace Jalasoft.NullableTypes.Api.Services
{
    using System.Collections.Generic;

    using Jalasoft.NullableTypes.Api.Model;

    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User GetUser(int id);

        User Create(User user);

        User Update(int id, User user);

        void Delete(int id);
    }
}
