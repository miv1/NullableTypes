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
            if (this.VerifyNullable(user.Ci) != -1)
            {
                if (this.VerifyCondition(1000000, 8000000, Convert.ToInt32(user.Ci)))
                {
                    if (user.Name.Length > 4 && user.Name != null)
                    {
                        if (this.VerifyNullable(user.CelPhone) != -1)
                        {
                            if (!this.VerifyCondition(6000000, 8000000, Convert.ToInt32(user.CelPhone)))
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
                    }
                }

                return user;
            }

            return new User();
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

        private bool VerifyCondition(int minValue, int maxValue, int input)
        {
            if (minValue <= input && input <= maxValue)
            {
                return true;
            }

            return false;
        }

        private int VerifyNullable(int? value)
        {
            int result = value ?? -1;
            return result;
        }
    }
}
