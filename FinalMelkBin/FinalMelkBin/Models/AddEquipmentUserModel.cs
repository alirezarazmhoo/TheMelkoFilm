using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMelkBin.Models
{
    public class AddEquipmentUserModel
    {
        public string aptToken { get; set; }
        public string mobile { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string abutMe { get; set; }
        public byte userType { get; set; }
    }
}