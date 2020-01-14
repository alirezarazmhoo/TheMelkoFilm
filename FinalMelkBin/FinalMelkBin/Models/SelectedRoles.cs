using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMelkBin.Models
{
    public  class SelectedRoles
    {
        public long AdminID { get; set; }
        public listRoles[] listRoles { get; set; }
    }
    public class listRoles
    {
        public long ID { get; set; }
    }
}