using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMelkBin.Models
{
    public class RegisterJob
    {
        public string title { get; set; }
        public string fullName { get; set; }

        public double lon { get; set; }
        public double lat { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public int cityId { get; set; }
        public int subCityId { get; set; }
        public byte tahsilat { get; set; }
        public int jobCategoriId { get; set; }
        public int jobSubCategoriId { get; set; }
        public int childjobSubCategoriId { get; set; }
        public string sharheVazayef { get; set; }
        public string vazayefRoozaneVaHaftegiVaMahane { get; set; }
  
        public string Mablagh { get; set; }
        public string apiToken { get; set; }
        public byte userType { get; set; }
        public byte advertisementType { get; set; }
        public string JobAddress { get; set; }
        public string sabeghe { get; set; }
        public string otherMaharat { get; set; }

    }
}