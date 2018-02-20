using System;


namespace loginRegistration.Models
{
    public class User
    {
        public int userid {get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime createdAt {get; set; }
        public DateTime updatedAt {get; set; }
    }

}