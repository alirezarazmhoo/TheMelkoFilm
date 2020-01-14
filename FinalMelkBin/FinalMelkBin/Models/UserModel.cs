using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string apiToken { get; set; }
        public int rolId { get; set; }
        public string abutMe { get; set; }
        public byte userType { get; set; }
        public string userImageUrl { get; set; }
        public string email { get; set; }
    }
}