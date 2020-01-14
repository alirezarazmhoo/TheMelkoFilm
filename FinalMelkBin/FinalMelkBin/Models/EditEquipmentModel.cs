using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMelkBin.Models
{
    public class EditEquipmentModel
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string fullName { get; set; }
        public string description { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string mobile { get; set; }

        public string email { get; set; }
        public int price { get; set; }
        public int cityId { get; set; }
        public int subCityId { get; set; }
        public int equipmentCategoriId { get; set; }
        public int equipmentSubCategoriId { get; set; }
        public int childEquipmentSubCategoriId { get; set; }
        public string apiToken { get; set; }
        public byte userType { get; set; }
        public string EquipmentAddress { get; set; }
    }
}