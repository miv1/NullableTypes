namespace UiTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public class Todo
    {
        public Todo()
        {
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}