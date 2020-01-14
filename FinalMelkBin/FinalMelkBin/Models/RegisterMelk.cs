using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalMelkBin.Models
{
    public class RegisterMelk
    {
        
           public string title { get; set; }
           public string imageUrl { get; set; }
           public string film { get; set; }
           public string telephone { get; set; }
           public string mobile { get; set; }
           public string address { get; set; }
           public double lon { get; set; }
           public double lat { get; set; }
           public string tozihatTakmily { get; set; }
           public byte parking { get; set; }
           public bool isNegahban { get; set; }
           public bool isSystemGarmayeshiMarkazi { get; set; }
           public bool isKabinet { get; set; }
           public bool isHayat { get; set; }
           public bool isSalonEjtema { get; set; }
           public bool isLaby { get; set; }
           public bool isEstakhrVaSona { get; set; }
           public bool isSalonVarzeshi { get; set; }
           public bool isPentHouse { get; set; }
           public bool isFazayeSabz { get; set; }
           public bool IsDastshooyiFarangi { get; set; }
           public bool IsMaster { get; set; }
           public bool vaziyatSokoonat { get; set; }
           public byte mogheiyatJographiya { get; set; }
           public bool isDarbZedSerghat { get; set; }
           public byte kafpoosh { get; set; }
           public byte cooler { get; set; }
           public byte garmayeshi { get; set; }
           public bool isIphoneTasviry { get; set; }
           public bool isAntenMarkazi { get; set; }
           public byte divar { get; set; }
           public byte saghf { get; set; }
           public bool isKomodDivari { get; set; }
           public bool isPangerehDojedareh { get; set; }
           public byte typeDastebandi { get; set; }
          // public int price = Convert.ToInt32(HttpContext.Current.Request.Form["price"]);
           public byte melkType { get; set; }
           public byte sanad { get; set; }
           public bool sanadType { get; set; }
           public bool typeEskelet { get; set; }
           public int zirbana { get; set; }
           public int tedadTabaghat { get; set; }
           public int tedadVahedDarTabaghe { get; set; }
           public int tedadOtaghkhab { get; set; }
           public int buildDateId { get; set; }
           public int cityId { get; set; }
           public int categoriId { get; set; }
           public bool isVip { get; set; }
           public long gheymateForoosh { get; set; }
           public long gheymateRahn { get; set; }
           public long gheymateEjareh { get; set; }
           public bool ghabeleTabdilBarayeRahn { get; set; }
           public bool ghabeleTabdilBarayeEjareh { get; set; }
           public byte userType { get; set; }
           public int subCityId { get; set; }
           public int subCategoriId { get; set; }
           public int childSubCategoriId { get; set; }
           public int userId { get; set; }
           public string apiToken { get; set; }
           public  string fullName { get; set; }

    }
}