namespace ApiTest.Model
{
    using System.Runtime.Serialization;
    using System;

    [DataContract]
    public class User
    {
        public User()
        {
        }

        [DataMember(Name = "id")]
        public byte Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "ci")]
        public int? Ci { get; set; }

        [DataMember(Name = "celPhone")]
        public int? CelPhone { get; set; }

        [DataMember(Name = "enabled")]
        public bool? Enabled { get; set; }

        [DataMember(Name = "creatioDate")]
        public DateTime CreatioDate { get; set; }

        [DataMember(Name = "lastUpdate")]
        public DateTime? LastUpdate { get; set; }
    }
}
