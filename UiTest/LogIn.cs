namespace UiTest
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ApiTest.Services;
    using UiTest.Models;

    public class LogIn
    {
        public User UserLog(string mail, string pass)
        {
            User user1 = new User();
            UserService newUser = new UserService();
            user1.Email = "1";
            user1.Password = "123";
            user1.Role = "user";
            user1.Name = "juan";
            user1.LastName = "Perez";
            return user1;
        }
    }
}
