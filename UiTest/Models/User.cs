namespace UiTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class User
    {
        public User()
        {
        }
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}