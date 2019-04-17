namespace Jalasoft.NullableTypes.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.NullableTypes.Api.Model;
    using Jalasoft.NullableTypes.Api.Services;

    public class UserService : IUserService
    {
        private List<User> users;

        public UserService()
        {
            this.users = new List<User>()
            {
               new User()
               {
                    Id = 1,
                    Name = "Chuck",
                    Ci = 123456,
                    CelPhone = 7777777,
                    Enabled = true,
                    CreatioDate = DateTime.Today
               },
                  new User()
                  {
                    Id = 2,
                    Name = "Silvester",
                    Ci = 12423,
                    CelPhone = 777545,
                    Enabled = false,
                    CreatioDate = DateTime.Today
                },
            };
        }

        public IEnumerable<User> GetAll()
        {
            return this.users;
        }

        public User Create(User user)
        {
            if (user.Name.Length > 4 && user.Name != null && user.Ci >= 1000000 && user.Ci <= 8000000)
            {
                if (user.CelPhone < 6000000 || user.CelPhone > 8000000)
                   {
                     user.CelPhone = null;
                    }

                if (user.Enabled == null)
                {
                    user.Enabled = true;
                }
               
                byte pointer = this.users.Max(x => x.Id);
                pointer++;
                user.Id = pointer;
           
                user.CreatioDate = DateTime.Now;

                this.users.Add(user);
            }

            return user;
        }

        public User Update(int id, User user)
        {
            var aimUser = this.users.FirstOrDefault(u => u.Id == id);
            if (aimUser != null)
            {
                User newUser = this.users[this.users.FindIndex(u => u.Id == id)];
                if (user.Name != null)
                {
                    newUser.Name = user.Name;
                }

                newUser.Ci = user.Ci ?? newUser.Ci;

                newUser.Enabled = user.Enabled ?? newUser.Enabled;
                newUser.CelPhone = user.CelPhone;
                newUser.LastUpdate = DateTime.Now;
            }

            return user;
        }

        public void Delete(int id)
        {
            var aimUser = this.users.FirstOrDefault(u => u.Id == id);
            if (aimUser != null)
            {
                this.users.Remove(aimUser);
            }
        }

        public User GetUser(int id)
        {
            var aimUser = this.users.FirstOrDefault(u => u.Id == id);
            if (aimUser != null)
            {
                return aimUser;
            }

            return new User();
        }
    }
}
