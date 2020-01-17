using FinalMelkBin.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.Services.Description;
using Models;
using RestSharp;
using System.Web.Hosting;
using FinalMelkBin.Uility;

namespace FinalMelkBin.Controllers.api
{
    public class HomeController : ApiController
    {
        TheFinalMelkobinEntities Context = new TheFinalMelkobinEntities();


        [HttpGet]
        [Route("api/Home/DateData")]
        public object DateData()
        {
            var result = Context.buildDates.Select(p => new { p.id, p.pDate }).ToList();
            return result;
        }


        [HttpGet]
        [Route("api/Home/LoadCityData")]

        public object LoadCityData()
        {
            var result = Context.cities.Select(x => new { x.id, x.cityName }).ToList();
            return result;
        }
        [HttpGet]
        [Route("api/Home/LoadSubCityData/{id}")]

        public object LoadSubCityData(int id)
        {
            var result = Context.subCities.Where(p => p.cityId == id).Select(x => new { x.id, x.subCityname }).ToList();
            return result;
        }
        [HttpGet]
        [Route("api/Home/LoadCategoryData")]

        public object LoadCategoryData()
        {
            var result = Context.Categoris.Select(x => new { x.id, x.categoriName }).ToList();
            return result;
        }
        [HttpGet]
        [Route("api/Home/LoadSubCategoryData/{id}")]

        public object LoadSubCategory(int id)
        {
            var result = Context.SubCategoris.Where(p => p.categoriId == id).Select(x => new { x.id, x.subCategoriName }).ToList();
            return result;
        }


        //بارگزای اطلاعات زیردسته بندی دوم اساس ای دی زیر دسته بندی

        [HttpGet]
        [Route("api/Home/LoadChildSubCategoryData/{id}")]

        public object LoadChildSubCategoryData(int id)
        {
            var result = Context.ChildSubCategoris.Where(p => p.subCategoriId == id).Select(x => new { x.id, x.childSubCategoriName }).ToList();
            return result;
        }




        [HttpGet]
        [Route("api/Home/LoadMelks/{page}")]

        public object LoadMelks(int page)
        {
			var thisyear = DateTime.Now;
			

            var result = Context.melks.Where(s=>s.expireDate >= thisyear&& s.statusId==1.ToString()).Select(p => new
            {
                p.id,
                p.userType,
                p.film,
                p.user.apiToken,
                p.user.fullName,
                p.lat,
                p.lon,
                p.ghabeleTabdilBarayeEjareh,
                p.ghabeleTabdilBarayeRahn,
                p.subCity.subCityname,
                p.imageUrl,
                p.SubCategori.subCategoriName,
                p.title,
                p.telephone,
                p.mobile,
                p.address,
                p.tozihatTakmily,
                p.parking,
                p.isNegahban,
                p.isSystemGarmayeshiMarkazi,
                p.isKabinet,
                p.isHayat,
                p.isSalonEjtema,
                p.isLaby,
                p.isEstakhrVaSona,
                p.isSalonVarzeshi,
                p.isPentHouse,
                p.isFazayeSabz,
                p.IsDastshooyiFarangi,
                p.IsMaster,
                p.vaziyatSokoonat,
                p.mogheiyatJographiya,
                p.isDarbZedSerghat,
                p.kafpoosh,
                p.cooler,
                p.garmayeshi,
                p.isIphoneTasviry,
                p.isAntenMarkazi,
                p.divar,
                p.saghf,
                p.isKomodDivari,
                p.isPangerehDojedareh,
                p.typeDastebandi,
                p.gheymateForoosh,
                p.gheymateRahn,
                p.gheymateEjareh,
                p.melkType,
                p.sanad,
                p.sanadType,
                p.typeEskelet,
                p.zirbana,
                p.tedadTabaghat,
                p.tedadVahedDarTabaghe,
                p.tedadOtaghkhab,
                p.buildDate.pDate,
                p.city.cityName,
                p.Categori.categoriName,
                p.ChildSubCategori.childSubCategoriName
				
                //x.id,
                //x.title,
                //x.telephone,
                //x.lon,
                //x.lat,
                //x.gheymateEjareh,
                //x.gheymateForoosh,
                //x.gheymateRahn,
                //x.address,
                //x.zirbana,
                //x.typeDastebandi,
                //x.imageUrl,
            }).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = result.Count() };
        }
        [HttpGet]
        [Route("api/Home/LoadOneMelk/{id}")]

        public object LoadOneMelk(int id)
        {
            var result = Context.melks.Where(p => p.id == id).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).FirstOrDefault(x => x.id == id);

            var images = Context.pictures.Where(p => p.melkId == id).Select(p => new { p.url }).ToList();


            return new { result, images };
        }
        [HttpGet]
        [Route("api/Home/DeleteMelk/{id}")]

        public object DeleteMelk(int id)
        {
            melk melk = Context.melks.Find(id);

            try
            {
                System.IO.File.Delete("~/Upload/" + melk.imageUrl);
            }
            catch (Exception rer) { }
            var pictures = Context.pictures.Where(x => x.melkId == id).ToList();
            foreach (var item in pictures)
            {
                try
                {
                    System.IO.File.Delete("~/Upload/" + item.url);
                }
                catch (Exception ex) { }
            }
            Context.melks.Remove(melk);
            Context.SaveChanges();
            return new { Message = 0 };
        }

        [HttpPost]
        [Route("api/Home/CreateMelk")]

        public object CreateMelk()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            RegisterMelk melka = jalbum.ToObject<RegisterMelk>();

            //bool? enable = Convert.ToBoolean(HttpContext.Current.Request.Form["enable"]);

            //string image = "";
            //string film = "";

            //var Puser = Context.users.FirstOrDefault(x => x.apiToken == apiToken);
            user Puser = new user();
            if (melka.apiToken == null)
            {
                string mobileUser = melka.mobile;

                var SearhUser = Context.users.FirstOrDefault(p => p.mobile == melka.mobile);
                if (SearhUser != null)
                {

                    melka.apiToken = SearhUser.apiToken;

                }
                else
                {
                    Random rand = new Random();
                    RandomGenerator generator = new RandomGenerator();
                    Puser.fullName = melka.fullName;
                    Puser.mobile = mobileUser;
                    Puser.userType = melka.userType;
                    Puser.rolId = 1;
                    Puser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                    Puser.password = rand.Next(10000, 99999).ToString();
                    Puser.userName = generator.RandomString(5, false);
                    Context.users.Add(Puser);

                }
            }

            var melk = new melk();
            //var Puserr = Context.users.FirstOrDefault(x => x.apiToken == melka.apiToken);

            melk.title = melka.title;
            melk.telephone = melka.telephone;
            melk.mobile = melka.mobile;
            melk.address = melka.address;
            melk.lat = melka.lat;
            melk.lon = melka.lon;
            melk.tozihatTakmily = melka.tozihatTakmily;
            melk.parking = melka.parking;
            melk.isNegahban = melka.isNegahban;
            melk.isSystemGarmayeshiMarkazi = melka.isSystemGarmayeshiMarkazi;
            melk.isKabinet = melka.isKabinet;
            melk.isHayat = melka.isHayat;
            melk.isSalonEjtema = melka.isSalonEjtema;
            melk.isLaby = melka.isLaby;
            melk.isEstakhrVaSona = melka.isEstakhrVaSona;
            melk.isSalonVarzeshi = melka.isSalonVarzeshi;
            melk.isPentHouse = melka.isPentHouse;
            melk.isFazayeSabz = melka.isFazayeSabz;
            melk.IsDastshooyiFarangi = melka.IsDastshooyiFarangi;
            melk.IsMaster = melka.IsMaster;
            melk.vaziyatSokoonat = melka.vaziyatSokoonat;
            melk.mogheiyatJographiya = melka.mogheiyatJographiya;
            melk.isDarbZedSerghat = melka.isDarbZedSerghat;
            melk.kafpoosh = melka.kafpoosh;
            melk.cooler = melka.cooler;
            melk.garmayeshi = melka.garmayeshi;
            melk.isIphoneTasviry = melka.isIphoneTasviry;
            melk.isAntenMarkazi = melka.isAntenMarkazi;
            melk.divar = melka.divar;
            melk.saghf = melka.saghf;
            melk.isKomodDivari = melka.isKomodDivari;
            melk.isPangerehDojedareh = melka.isPangerehDojedareh;
            melk.typeDastebandi = melka.typeDastebandi;
            melk.gheymateForoosh = melka.gheymateForoosh;
            melk.gheymateEjareh = melka.gheymateEjareh;
            melk.gheymateRahn = melka.gheymateRahn;
            melk.ghabeleTabdilBarayeEjareh = melka.ghabeleTabdilBarayeEjareh;
            melk.ghabeleTabdilBarayeRahn = melka.ghabeleTabdilBarayeRahn;
            melk.melkType = melka.melkType;
            melk.sanad = melka.sanad;
            melk.sanadType = melka.sanadType;
            melk.typeEskelet = melka.typeEskelet;
            melk.zirbana = melka.zirbana;
            melk.tedadTabaghat = melka.tedadTabaghat;
            melk.tedadVahedDarTabaghe = melka.tedadVahedDarTabaghe;
            melk.tedadOtaghkhab = melka.tedadOtaghkhab;
            melk.childSubCategoriId = melka.childSubCategoriId;
            melk.isVip = melka.isVip;
            melk.statusId = Convert.ToString(2);
            melk.buildDateId = melka.buildDateId;
            melk.expireDate = DateTime.Now.AddMonths(1);
            melk.pExpireDate = ExtensionFunction.ToPersian((DateTime.Now.AddMonths(1)));
            melk.userType = melka.userType;
            var mobile = "";
            if (melka.apiToken != null)
            {
                var user = Context.users.FirstOrDefault(x => x.apiToken == melka.apiToken);
                melk.userId = user.id;
                mobile = user.mobile;
            }
            else
            {
                melk.userId = Puser.id;
                mobile = Puser.mobile;
            }
            melk.cityId = melka.cityId;
            melk.subCityId = melka.subCityId;
            melk.categoriId = melka.categoriId;
            melk.subCityId = melka.subCityId;
            melk.subCategoriId = melka.subCategoriId;
            string imageUrl = "";
            string film = "";

            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["imageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            imageUrl = "/Upload/" + namefile;
                        }
                        else if (postedFile == HttpContext.Current.Request.Files["film"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            film = "/Upload/" + namefile;
                        }
                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            Context.pictures.Add(new picture
                            {
                                melkId = melk.id,
                                url = "/Upload/" + namefile
                            });
                        }
                        postedFile.SaveAs(filePath);

                    }
                }
            }
            melk.film = film;

            melk.imageUrl = imageUrl;

            Context.melks.Add(melk);

            try
            {
                Context.SaveChanges();
                

                if(Puser.userName !=null && Puser.password !=null)
                {

                    var userandpass = "نام کاربری و رمز عبور در صورت نیاز" + Puser.password + " " + Puser.userName + "";


                    var msg = "ملک شما با عنوان: " + melk.title + " با موفقیت ثبت گردید.";
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \","+userandpass+"\"," +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);



                }

                else
                {




                    var msg = "ملک شما با عنوان: " + melk.title + " با موفقیت ثبت گردید.";
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }


            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return new
            {
                Message = 0,
                FullName = melk.user.fullName


            };
        }
        [HttpPost]
        [Route("api/Home/EditMelk")]

        public object EditMelk()
        {

            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            EditRegisterMelk melka = jalbum.ToObject<EditRegisterMelk>();


            int id = melka.Id;
            string imageUrl = "";
            string film = "";
            var melk = Context.melks.FirstOrDefault(x => x.id == id);
            if (HttpContext.Current.Request.Form.AllKeys.Contains("imageUrl"))
            {
                try
                {
                    System.IO.File.Delete(melk.imageUrl);
                }
                catch (Exception ex)
                { }

            }
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  
                    //var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //var extension = ext.ToLower();

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["imageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            imageUrl = "/Upload/" + namefile;
                            melk.imageUrl = imageUrl;
                            //imageUrl = namefile;
                        }
                        else if (postedFile == HttpContext.Current.Request.Files["film"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            film = "/Upload/" + namefile;
                            melk.film = film;
                        }
                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            Context.pictures.Add(new picture
                            {
                                melkId = melk.id,
                                url = "/Upload/" + namefile
                            });
                        }

                        postedFile.SaveAs(filePath);
                    }
                }
            }
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("title"))
            //{
            //    string title = HttpContext.Current.Request.Form["title"];
            //    melk.title = title;
            //}
            melk.title = melka.title;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("telephone"))
            //{
            //    string telephone = HttpContext.Current.Request.Form["telephone"];
            //    melk.telephone = telephone;
            //}
            melk.telephone = melka.telephone;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("mobile"))
            //{
            //    string mobile = HttpContext.Current.Request.Form["mobile"];
            //    melk.mobile = mobile;
            //}
            //if (melka.mobile != null)
            //{

            //melk.mobile = melka.mobile;
            //}
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("address"))
            //{
            //    string address = HttpContext.Current.Request.Form["address"];
            //    melk.address = address;
            //}
            melk.address = melka.address;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("lon"))
            //{
            //    long lon = Convert.ToInt64(HttpContext.Current.Request.Form["lon"]);
            //    melk.lon = lon;
            //}
            melk.lon = melka.lon;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("lat"))
            //{
            //    long lat = Convert.ToInt64(HttpContext.Current.Request.Form["lat"]);
            //    melk.lat = lat;

            //}
            melk.lat = melka.lat;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("tozihatTakmily"))
            //{
            //    string tozihatTakmily = HttpContext.Current.Request.Form["tozihatTakmily"];
            //    melk.tozihatTakmily = tozihatTakmily;
            //}
            melk.tozihatTakmily = melka.tozihatTakmily;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("parking"))
            //{
            //    byte parking = Convert.ToByte(HttpContext.Current.Request.Form["parking"]);

            //    melk.parking = parking;
            //}
            melk.parking = melka.parking;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isNegahban"))
            //{
            //    bool isNegahban = Convert.ToBoolean(HttpContext.Current.Request.Form["isNegahban"]);

            //    melk.isNegahban = isNegahban;
            //}
            melk.isNegahban = melka.isNegahban;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isSystemGarmayeshiMarkazi"))
            //{
            //    bool isSystemGarmayeshiMarkazi = Convert.ToBoolean(HttpContext.Current.Request.Form["isSystemGarmayeshiMarkazi"]);


            //    melk.isSystemGarmayeshiMarkazi = isSystemGarmayeshiMarkazi;
            //}
            melk.isSystemGarmayeshiMarkazi = melka.isSystemGarmayeshiMarkazi;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isKabinet"))
            //{
            //    bool isKabinet = Convert.ToBoolean(HttpContext.Current.Request.Form["isKabinet"]);

            //    melk.isKabinet = isKabinet;
            //}
            melk.isKabinet = melka.isKabinet;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isHayat"))
            //{
            //    bool isHayat = Convert.ToBoolean(HttpContext.Current.Request.Form["isHayat"]);

            //    melk.isHayat = isHayat;
            //}
            melk.isHayat = melka.isHayat;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isSalonEjtema"))
            //{
            //    bool isSalonEjtema = Convert.ToBoolean(HttpContext.Current.Request.Form["isSalonEjtema"]);

            //    melk.isSalonEjtema = isSalonEjtema;
            //}
            melk.isSalonEjtema = melka.isSalonEjtema;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isLaby"))
            //{
            //    bool isLaby = Convert.ToBoolean(HttpContext.Current.Request.Form["isLaby"]);


            //    melk.isLaby = isLaby;
            //}
            melk.isLaby = melka.isLaby;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isEstakhrVaSona"))
            //{
            //    bool isEstakhrVaSona = Convert.ToBoolean(HttpContext.Current.Request.Form["isEstakhrVaSona"]);

            //    melk.isEstakhrVaSona = isEstakhrVaSona;
            //}
            melk.isEstakhrVaSona = melka.isEstakhrVaSona;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isSalonVarzeshi"))
            //{
            //    bool isSalonVarzeshi = Convert.ToBoolean(HttpContext.Current.Request.Form["isSalonVarzeshi"]);

            //    melk.isSalonVarzeshi = isSalonVarzeshi;
            //}
            melk.isSalonVarzeshi = melka.isSalonVarzeshi;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isPentHouse"))
            //{
            //    bool isPentHouse = Convert.ToBoolean(HttpContext.Current.Request.Form["isPentHouse"]);

            //    melk.isPentHouse = isPentHouse;
            //}
            melk.isPentHouse = melka.isPentHouse;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isFazayeSabz"))
            //{
            //    bool isFazayeSabz = Convert.ToBoolean(HttpContext.Current.Request.Form["isFazayeSabz"]);


            //    melk.isFazayeSabz = isFazayeSabz;
            //}
            melk.isFazayeSabz = melka.isFazayeSabz;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("IsDastshooyiFarangi"))
            //{
            //    bool IsDastshooyiFarangi = Convert.ToBoolean(HttpContext.Current.Request.Form["IsDastshooyiFarangi"]);

            //    melk.IsDastshooyiFarangi = IsDastshooyiFarangi;
            //}
            melk.IsDastshooyiFarangi = melka.IsDastshooyiFarangi;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("IsMaster"))
            //{
            //    bool IsMaster = Convert.ToBoolean(HttpContext.Current.Request.Form["IsMaster"]);

            //    melk.IsMaster = IsMaster;
            //}
            melk.IsMaster = melka.IsMaster;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("vaziyatSokoonat"))
            //{
            //    bool vaziyatSokoonat = Convert.ToBoolean(HttpContext.Current.Request.Form["vaziyatSokoonat"]);


            //    melk.vaziyatSokoonat = vaziyatSokoonat;
            //}
            melk.vaziyatSokoonat = melka.vaziyatSokoonat;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("mogheiyatJographiya"))
            //{
            //    byte mogheiyatJographiya = Convert.ToByte(HttpContext.Current.Request.Form["mogheiyatJographiya"]);

            //    melk.mogheiyatJographiya = mogheiyatJographiya;
            //}
            melk.mogheiyatJographiya = melka.mogheiyatJographiya;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isDarbZedSerghat"))
            //{
            //    bool isDarbZedSerghat = Convert.ToBoolean(HttpContext.Current.Request.Form["isDarbZedSerghat"]);

            //    melk.isDarbZedSerghat = isDarbZedSerghat;
            //}
            melk.isDarbZedSerghat = melka.isDarbZedSerghat;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("kafpoosh"))
            //{
            //    byte kafpoosh = Convert.ToByte(HttpContext.Current.Request.Form["kafpoosh"]);

            //    melk.kafpoosh = kafpoosh;
            //}
            melk.kafpoosh = melka.kafpoosh;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("cooler"))
            //{
            //    byte cooler = Convert.ToByte(HttpContext.Current.Request.Form["cooler"]);


            //    melk.cooler = cooler;
            //}
            melk.cooler = melka.cooler;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("garmayeshi"))
            //{
            //    byte garmayeshi = Convert.ToByte(HttpContext.Current.Request.Form["garmayeshi"]);

            //    melk.garmayeshi = garmayeshi;
            //}
            melk.garmayeshi = melka.garmayeshi;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isIphoneTasviry"))
            //{
            //    bool isIphoneTasviry = Convert.ToBoolean(HttpContext.Current.Request.Form["isIphoneTasviry"]);

            //    melk.isIphoneTasviry = isIphoneTasviry;
            //}
            melk.isIphoneTasviry = melka.isIphoneTasviry;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isAntenMarkazi"))
            //{
            //    bool isAntenMarkazi = Convert.ToBoolean(HttpContext.Current.Request.Form["isAntenMarkazi"]);
            //    melk.isAntenMarkazi = isAntenMarkazi;
            //}
            melk.isAntenMarkazi = melka.isAntenMarkazi;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("divar"))
            //{
            //    byte divar = Convert.ToByte(HttpContext.Current.Request.Form["divar"]);

            //    melk.divar = divar;
            //}
            melk.divar = melka.divar;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("saghf"))
            //{
            //    byte saghf = Convert.ToByte(HttpContext.Current.Request.Form["saghf"]);


            //    melk.saghf = saghf;
            //}
            melk.saghf = melka.saghf;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isKomodDivari"))
            //{
            //    bool isKomodDivari = Convert.ToBoolean(HttpContext.Current.Request.Form["isKomodDivari"]);

            //    melk.isKomodDivari = isKomodDivari;
            //}
            melk.isKomodDivari = melka.isKomodDivari;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isPangerehDojedareh"))
            //{
            //    bool isPangerehDojedareh = Convert.ToBoolean(HttpContext.Current.Request.Form["isPangerehDojedareh"]);


            //    melk.isPangerehDojedareh = isPangerehDojedareh;
            //}
            melk.isPangerehDojedareh = melka.isPangerehDojedareh;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("typeDastebandi"))
            //{
            //    byte typeDastebandi = Convert.ToByte(HttpContext.Current.Request.Form["typeDastebandi"]);

            //    melk.typeDastebandi = typeDastebandi;
            //}
            melk.typeDastebandi = melka.typeDastebandi;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("gheymateForoosh"))
            //{
            //    long gheymateForoosh = Convert.ToInt32(HttpContext.Current.Request.Form["gheymateForoosh"]);


            //    melk.gheymateForoosh = gheymateForoosh;
            //}
            melk.gheymateForoosh = melka.gheymateForoosh;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("gheymateRahn"))
            //{
            //    long gheymateRahn = Convert.ToInt32(HttpContext.Current.Request.Form["gheymateRahn"]);


            //    melk.gheymateRahn = gheymateRahn;
            //}
            melk.gheymateRahn = melka.gheymateRahn;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("gheymateEjareh"))
            //{
            //    long gheymateEjareh = Convert.ToInt32(HttpContext.Current.Request.Form["gheymateEjareh"]);


            //    melk.gheymateEjareh = gheymateEjareh;
            //}
            melk.gheymateEjareh = melka.gheymateEjareh;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("ghabeleTabdilBarayeEjareh"))
            //{
            //    bool ghabeleTabdilBarayeEjareh = Convert.ToBoolean(HttpContext.Current.Request.Form["ghabeleTabdilBarayeEjareh"]);


            //    melk.ghabeleTabdilBarayeEjareh = ghabeleTabdilBarayeEjareh;
            //}
            melk.ghabeleTabdilBarayeEjareh = melka.ghabeleTabdilBarayeEjareh;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("ghabeleTabdilBarayeRahn"))
            //{
            //    bool ghabeleTabdilBarayeRahn = Convert.ToBoolean(HttpContext.Current.Request.Form["ghabeleTabdilBarayeRahn"]);


            //    melk.ghabeleTabdilBarayeRahn = ghabeleTabdilBarayeRahn;
            //}
            melk.ghabeleTabdilBarayeRahn = melka.ghabeleTabdilBarayeRahn;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("melkType"))
            //{
            //    byte melkType = Convert.ToByte(HttpContext.Current.Request.Form["melkType"]);

            //    melk.melkType = melkType;
            //}
            melk.melkType = melka.melkType;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("sanad"))
            //{
            //    byte sanad = Convert.ToByte(HttpContext.Current.Request.Form["sanad"]);


            //    melk.sanad = sanad;
            //}
            melk.sanad = melka.sanad;

            //if (HttpContext.Current.Request.Form.AllKeys.Contains("sanadType"))
            //{
            //    bool sanadType = Convert.ToBoolean(HttpContext.Current.Request.Form["sanadType"]);


            //    melk.sanadType = sanadType;
            //}
            melk.sanadType = melka.sanadType;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("typeEskelet"))
            //{
            //    bool typeEskelet = Convert.ToBoolean(HttpContext.Current.Request.Form["typeEskelet"]);
            //    melk.typeEskelet = typeEskelet;
            //}
            melk.typeEskelet = melka.typeEskelet;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("zirbana"))
            //{
            //    int zirbana = Convert.ToInt32(HttpContext.Current.Request.Form["zirbana"]);

            //    melk.zirbana = zirbana;
            //}
            melk.zirbana = melka.zirbana;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("tedadTabaghat"))
            //{
            //    int tedadTabaghat = Convert.ToInt32(HttpContext.Current.Request.Form["tedadTabaghat"]);

            //    melk.tedadTabaghat = tedadTabaghat;
            //}
            melk.tedadTabaghat = melka.tedadTabaghat;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("buildDateId"))
            //{
            //    int buildDateId = Convert.ToInt32(HttpContext.Current.Request.Form["buildDateId"]);

            //    melk.buildDateId = buildDateId;
            //}
            melk.buildDateId = melka.buildDateId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("cityId"))
            //{
            //    int cityId = Convert.ToInt32(HttpContext.Current.Request.Form["cityId"]);


            //    melk.cityId = cityId;
            //}
            melk.cityId = melka.cityId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("categoriId"))
            //{
            //    int categoriId = Convert.ToInt32(HttpContext.Current.Request.Form["categoriId"]);
            //    melk.categoriId = categoriId;
            //}
            melk.categoriId = melka.categoriId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("isVip"))
            //{

            //    bool isVip = Convert.ToBoolean(HttpContext.Current.Request.Form["isVip"]);

            //    melk.isVip = isVip;
            //}
            melk.isVip = melka.isVip;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("pExpireDate"))
            //{
            //    string pExpireDate = HttpContext.Current.Request.Form["pExpireDate"];
            //    melk.pExpireDate = pExpireDate;
            //}
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("expireDate"))
            //{
            //    byte expireDate = Convert.ToByte(HttpContext.Current.Request.Form["expireDate"]);

            //    melk.expireDate = expireDate;
            //}
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("subCityId"))
            //{
            //    int subCityId = Convert.ToInt32(HttpContext.Current.Request.Form["subCityId"]);

            //    melk.subCityId = subCityId;
            //}
            melk.subCityId = melka.subCityId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("subCategoriId"))
            //{
            //    int subCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["subCategoriId"]);


            //    melk.subCategoriId = subCategoriId;
            //}

            melk.subCategoriId = melka.subCategoriId;
            melk.childSubCategoriId = melka.childSubCategoriId;

            //if (HttpContext.Current.Request.Form.AllKeys.Contains("enable"))
            //{
            //    bool? enable = Convert.ToBoolean(HttpContext.Current.Request.Form["enable"]);
            //    product.enable = enable;
            //}
       
            melk.statusId = Convert.ToString(2);
            Context.SaveChanges();
            return new { Message = 0 };
        }


        [HttpGet]
        [Route("api/Home/SearchMelksBySubCatId/{subCatId}/{page}")]

        public object SearchMelks(int subCatId, int page)
        {
            var result = Context.melks.Where(x => x.subCategoriId == subCatId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).Take(10).ToList(), totalCount = result.Count() };
        }
        [HttpGet]
        [Route("api/Home/SearchMelksByCategoriId/{CategoriId}/{page}")]

        public object SearchMelksByCategoriId(int CategoriId, int page)
        {
            var result = Context.melks.Where(x => x.categoriId == CategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).Take(10).ToList(), totalCount = result.Count() };
        }
        [HttpGet]
        [Route("api/Home/SearchMelksByCityId/{CityId}/{page}")]

        public object SearchMelksByCityId(int CityId, int page)
        {
            var result = Context.melks.Where(x => x.cityId == CityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).Take(10).ToList(), totalCount = result.Count() };
        }
        [HttpGet]
        [Route("api/Home/SearchMelksBySubCityId/{SubCityId}/{page}")]

        public object SearchMelksBySubCityId(int SubCityId, int page)
        {
            var result = Context.melks.Where(x => x.subCityId == SubCityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).Take(10).ToList(), totalCount = result.Count() };
        }
        [HttpGet]
        [Route("api/Home/SearchSubCategoti/{CategoriId}/{page}")]

        public object SearchSubCategoti(int CategoriId, int page)
        {
            var result = Context.SubCategoris.Where(x => x.categoriId == CategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(x => new { x.id, x.subCategoriName }).Take(10).ToList(), totalCount = result.Count() };
        }
        [HttpGet]
        [Route("api/Home/SearchSubCity/{CityId}/{page}")]

        public object SearchSubCity(int CityId, int page)
        {
            var result = Context.subCities.Where(x => x.cityId == CityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(x => new { x.id, x.subCityname }).Take(10).ToList(), totalCount = result.Count() };
        }

        [HttpGet]
        [Route("api/Home/SearchMelksByChildSubCatrgoriId/{ChildSubCategoriId}/{page}")]

        public object SearchMelksByChildSubCatrgoriId(int ChildSubCategoriId, int page)
        {
            var result = Context.melks.Where(x => x.childSubCategoriId == ChildSubCategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).Take(10).ToList(), totalCount = result.Count() };
        }


        [HttpPost]
        [Route("api/Home/SearchMelkNearest")]
        public object SearchMelkNearest()
        {
            double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            int? subCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["subCategoriId"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.Form["page"]);
            if (page == 0)
                page = 1;

            var today = DateTime.Now;
            double constValue = 57.2957795130823D;
            double constValue2 = 3958.75586574D;
            if (lon != 0)
                constValue = lon;
            if (lat != 0)
                constValue2 = lat;

            int pageSize = 10;
            page = Convert.ToInt32(page);

            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            var Pmelk = (from l in Context.melks
                         let temp = SqlFunctions.Sin(l.lat / constValue)
                         * SqlFunctions.Sin(lat / constValue)
                         + SqlFunctions.Cos(l.lat / constValue)
                         * SqlFunctions.Cos(lat / constValue)
                         * SqlFunctions.Cos(lon / constValue)
                         - (l.lon / constValue)
                         let calMiles = (constValue2 * SqlFunctions.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                         where (l.lat > 0 && l.lon > 0)
                         orderby calMiles descending
                         select new { l.subCategoriId, l.id, l.title, l.lon, l.lat, l.subCityId, l.childSubCategoriId,l.categoriId, l.address, l.imageUrl,l.gheymateEjareh,l.gheymateForoosh,l.gheymateRahn,l.zirbana });
            if (subCategoriId != 0)
                Pmelk = Pmelk.Where(x => x.subCategoriId == subCategoriId);
            return new { Data = Pmelk.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).Select(x => new { x.subCategoriId, x.childSubCategoriId,x.id, x.title,x.lon, x.lat, x.subCityId, x.categoriId, x.address, x.imageUrl,x.zirbana,x.gheymateRahn,x.gheymateForoosh,x.gheymateEjareh }), totalCount = Pmelk.Count() };
        }

        //جستجوی همه  املاک روی نقشه 

        [HttpPost]
        [Route("api/Home/SearchAllMelksNearest")]
        public object SearchAllMelksNearest()
        {
            double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            //int? equipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentSubCategoriId"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.Form["page"]);

            if (page == 0)
                page = 1;

            var today = DateTime.Now;
            double constValue = 57.2957795130823D;
            double constValue2 = 3958.75586574D;
            if (lon != 0)
                constValue = lon;
            if (lat != 0)
                constValue2 = lat;
            int pageSize = 10;
            page = Convert.ToInt32(page);

            page = page > 0 ? page : 1;

            pageSize = pageSize > 0 ? pageSize : 10;
            var PMelk = (from l in Context.melks
                        let temp = SqlFunctions.Sin(l.lat / constValue)
                        * SqlFunctions.Sin(lat / constValue)
                        + SqlFunctions.Cos(l.lat / constValue)
                        * SqlFunctions.Cos(lat / constValue)
                        * SqlFunctions.Cos(lon / constValue)
                        - (l.lon / constValue)
                        let calMiles = (constValue2 * SqlFunctions.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                        where (l.lat > 0 && l.lon > 0)
                        orderby calMiles descending
                        select new { l.categoriId, l.childSubCategoriId,l.id, l.title, l.lon, l.lat, l.subCityId, l.subCategoriId, l.imageUrl });
            //if (equipmentSubCategoriId != 0)
            //PEquipment = PEquipment.Select(p=>p.id).ToList();
            return new { Data = PMelk.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).Select(x => new { x.subCategoriId, x.childSubCategoriId,x.id, x.title, x.lon, x.lat, x.subCityId, x.categoriId, x.imageUrl }), totalCount = PMelk.Count() };
        }

        [HttpPost]
        [Route("api/Home/SearchMelkByTitle/{page}")]

        public object SearchMelkByTitle(int page)
        {


            var description = Convert.ToString(HttpContext.Current.Request.Form["description"]);
            var result = Context.melks.Where(x => x.title.Contains(description)).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).Take(10).ToList(), totalCount = result.Count() };

        }



        [HttpPost]
        [Route("api/Home/Register")]

        public object Register()
        {


            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            UserModel usera = jalbum.ToObject<UserModel>();


            //string mobile = HttpContext.Current.Request.Form["mobile"];
            string mobile = usera.mobile;

            var findUser = Context.users.FirstOrDefault(p => p.mobile == mobile);
            user pUser = new user();
            if (findUser != null)
            {
                //if (HttpContext.Current.Request.Form.AllKeys.Contains("fullName"))
                if (usera.fullName != null)

                {
                    //string UserfullName = HttpContext.Current.Request.Form["fullName"];
                    findUser.fullName = usera.fullName;

                }
                if (usera.email != null)

                {
                    //string UserfullName = HttpContext.Current.Request.Form["fullName"];
                    findUser.Email = usera.email;

                }
                //if (HttpContext.Current.Request.Form.AllKeys.Contains("telephone"))
                if (usera.telephone != null)

                {
                    //string Usertelephone = HttpContext.Current.Request.Form["telephone"];

                    //findUser.telephone = Usertelephone;
                    findUser.telephone = usera.telephone;

                }
                //if (HttpContext.Current.Request.Form.AllKeys.Contains("userName"))
                if (usera.userName != null)

                {
                    //string UseruserName = HttpContext.Current.Request.Form["userName"];

                    //findUser.userName = UseruserName;
                    findUser.userName = usera.userName;

                }
                //if (HttpContext.Current.Request.Form.AllKeys.Contains("password"))
                if (usera.password != null)

                {
                    //string Userpassword = HttpContext.Current.Request.Form["password"];

                    //findUser.password = Userpassword;
                    findUser.password = usera.password;

                }
                //if (HttpContext.Current.Request.Form.AllKeys.Contains("abutMe"))
                if (usera.abutMe != null)

                {
                    //string UserabutMe = HttpContext.Current.Request.Form["abutMe"];

                    //findUser.abutMe = UserabutMe;
                    findUser.abutMe = usera.abutMe;

                }

                Context.SaveChanges();
                return new
                {
                    Token = findUser.apiToken,


                    Message = 0
                };

            }

            else
            {

                //string telephone = HttpContext.Current.Request.Form["telephone"];
                //string fullName = HttpContext.Current.Request.Form["fullName"];
                //string userName = HttpContext.Current.Request.Form["userName"];
                //string password = HttpContext.Current.Request.Form["password"];
                //string abutMe = HttpContext.Current.Request.Form["abutMe"];
                //bool userType = Convert.ToBoolean(HttpContext.Current.Request.Form["userType"]);
                string telephone = usera.telephone;
                string fullName = usera.fullName;
                string userName = usera.userName;
                string password = usera.password;
                string email = usera.email;
                string abutMe = usera.abutMe;
                byte userType = usera.userType;

                string userImageUrl = HttpContext.Current.Request.Form["userImageUrl"];


                var isUserName = Context.users.Any(p => p.userName == userName);
                if (isUserName)
                {
                    return new
                    {
                        Message = 3
                    };
                }


                //else
                //{
                //newuser.state = 0;
                //کاربر
                var Puser = new user();
                Random _g = new Random();

                Puser.fullName = fullName;
                Puser.telephone = telephone;
                Puser.mobile = mobile;
                Puser.userName = userName;
                Puser.password = password;
                Puser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                Puser.abutMe = abutMe;
                Puser.userType = userType;
                Puser.Email = email;
                var httpRequest = HttpContext.Current.Request;
                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                        if (postedFile.ContentLength > MaxContentLength)
                        {
                            return new
                            {
                                Message = 2
                            };
                        }
                        else
                        {
                            string fileNameExternal = Path.GetFileName(postedFile.FileName);
                            var namefile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fileNameExternal).ToLower();
                            var filePath = "";

                            if (postedFile == HttpContext.Current.Request.Files["userImageUrl"])
                            {
                                filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                                userImageUrl = "/Upload/" + namefile;
                            }
                            postedFile.SaveAs(filePath);
                        }

                    }
                }
                Puser.userImageUrl = userImageUrl;
                Context.users.Add(Puser);


                Context.SaveChanges();

                //Random random = new Random();
                //string guidcode = random.Next(10000, 99999).ToString();


                return new
                {

                    Token = Puser.apiToken,

                    Message = 0,
                    //guid = newuser.guid,
                    //code = guidcode
                };

            }
        }

        [HttpPost]
        [Route("api/Home/EditUser")]
        public object EditUser()
        {

            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            EditMelkUserModel Melkusera = jalbum.ToObject<EditMelkUserModel>();



            string apiToken = Melkusera.apiToken;
            var FindUser = Context.users.FirstOrDefault(p => p.apiToken == apiToken);
            string userImageUrl = "";

            if (HttpContext.Current.Request.Form.AllKeys.Contains("userImageUrl"))
            {
                try
                {
                    System.IO.File.Delete(FindUser.userImageUrl);
                }
                catch (Exception ex)
                { }

            }
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  
                    //var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //var extension = ext.ToLower();

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["userImageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            userImageUrl = "/Upload/" + namefile;
                        }

                        postedFile.SaveAs(filePath);
                    }
                }
            }
            if (Melkusera.fullName != null)
            {
                string fullName = Melkusera.fullName;
                FindUser.fullName = fullName;
            }
            if (Melkusera.telephone != null)
            {
                string telephone = Melkusera.telephone;
                FindUser.telephone = telephone;
            }
            if (Melkusera.mobile != null)
            {
                string mobile = Melkusera.mobile;
                FindUser.mobile = mobile;
            }

            if (Melkusera.password != null)
            {
                string password = Melkusera.password;
                FindUser.password = password;
            }
            if (Melkusera.abutMe != null)
            {
                string abutMe = Melkusera.abutMe;
                FindUser.abutMe = abutMe;
            }
            if (Melkusera.email != null)
            {
                string email = Melkusera.email;
                FindUser.Email = email;
            }


            if (userImageUrl != null)
            {
                FindUser.userImageUrl = userImageUrl;

            }

            Context.SaveChanges();
            return new
            {
                Message = 0
            };




        }






        [HttpPost]
        [Route("api/Home/UserInformations")]
        public object UserInformations()
        {
            string apiToken = HttpContext.Current.Request.Form["apiToken"];


            var UserInformation = Context.users.Where(p => p.apiToken == apiToken).Select(p => new { p.fullName, p.telephone, p.mobile, p.Email,p.abutMe,p.userType, p.userImageUrl,p.userName }).ToList().FirstOrDefault();

            if (UserInformation != null)
            {
                return UserInformation;
            }
            else
            {
                return new
                {
                    Message = 2
                };
            }


        }
        //املاک ثبت شده توسط کاربر
        [HttpPost]
        [Route("api/Home/MelksUserSaved/{page}")]
        public object MelksUserSaved(int page)
        {

            string apiToken = HttpContext.Current.Request.Form["apiToken"];

            var ListMelks = Context.melks.Where(p => p.user.apiToken == apiToken).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName,p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).AsQueryable();

            //var total = Context.melks.Where(p => p.user.apiToken == apiToken).ToList();

            //    var countmelk = 0;
            //foreach(var item in total)
            //{
            //    countmelk++;

            //}

            return new { Data = ListMelks.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = ListMelks.Count() };

        }



        //[HttpPost]
        //[Route("api/Home/ChangePass")]

        //public object ChangePass(RegisterModel register)
        //{

        //    Users user = db.Users.Where(r => r.guid == register.guid).FirstOrDefault();
        //    if (register.regpass.ToLower() != user.password.ToLower())
        //    {
        //        return new
        //        {
        //            Message = 1
        //        };
        //    }
        //    else if (register.newpass != register.rnewpass)
        //    {
        //        return new
        //        {
        //            Message = 2
        //        };
        //    }
        //    else
        //    {
        //        user.password = register.newpass;
        //        db.SaveChanges();
        //        return new
        //        {
        //            Message = 0,

        //        };
        //    }
        //}

        [HttpPost]
        [Route("api/Home/Login")]
        public object Login()
        {
            var userName = HttpContext.Current.Request.Form["userName"];
            var password = HttpContext.Current.Request.Form["password"];
            if (Context.users.Any(p => p.userName == userName && p.password == password))
            {
                var pUser = Context.users.FirstOrDefault(x => x.password == password && x.userName == userName);
                return new
                {
                    pUser.apiToken,
                };
            }
            else
            {
                return new
                {
                    Message = 1,
                };
            }

            //اگر هیچ کاربری وجود نداشت یک کاربر مدیر به وجود بیاید

            //Users user = db.Users.Where(r => r.username.ToLower() == register.username.ToLower()).FirstOrDefault();
            //if (user != null)
            //{
            //    if (user.password.ToLower() != register.password.ToLower())
            //    {
            //        return new { Message = 1 };
            //    }

            //if Its Not Activated
            //else if (user.state == 0 /*&& user.userType != 1*/)
            //{
            //    //ViewBag.Error = "جهت استفاده از امکانات صفحه اختصاصی خود نیاز به پرداخت حق عضویت دارید. <br/> امکانات : ساخت اسلاید، نمایش لوگو،درباره ما،محصولات، تماس با ما، ایجاد ساب دامین اختصاصی<br/>  برای فعالسازی حساب کاربری و پرداخت <button type=\"submit\" class=\"btn btn-success\" name=\"btnpay\" style=\"margin: 10px;\" value=\"pay\">اینجا</button> کلیک کنید <br/>(مبلغ قابل پرداخت برای استفاده نا محدود از امکانات فوق 69 هزار تومان می باشد)";
            //    //Session["useridpay"] = user.id;
            //    //Session["login"] = user.userType;
            //    //Session["userid"] = user.id;
            //    ////آخرین زمان لاگین
            //    //user.lastlogin = DateTime.Now;
            //    //db.SaveChanges();
            //    //if (Session["url"] != null)
            //    //{
            //    //    string url = Session["url"].ToString();
            //    //    Response.Redirect(url, false);
            //    //}
            //    //else
            //    //{
            //    //    Response.Redirect("/login" , false);
            //    //}


            //    //کاربر

            //    Random random = new Random();
            //    string guidcode = random.Next(10000, 99999).ToString();



            //}
            //        else
            //        {
            //            //آخرین زمان لاگین
            //            db.SaveChanges();
            //            return new
            //            {
            //                Message = 0,
            //                apiToken = user.apiToken
            //            };
            //        }
            //    }
            //    else
            //    {
            //        return new { Message = 3 };

            //    }
            //}


        }

         [HttpPost]
        [Route("api/Home/ForgetPassword")]

        public object ForgetPassword()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            RegisterModel register = jalbum.ToObject<RegisterModel>();





            if (String.IsNullOrWhiteSpace(register.userName) == false || String.IsNullOrWhiteSpace(register.mobile) == false)
            {
            }
            else
            {
                return new { Message = 1 };
            }
            user user = new user();
            user = null;
            if (String.IsNullOrWhiteSpace(register.userName) == false)
            {
                user = Context.users.Where(r => r.userName == register.userName).FirstOrDefault();
            }
            else if (String.IsNullOrWhiteSpace(register.mobile) == false)
            {
                user = Context.users.Where(r => r.mobile == register.mobile).FirstOrDefault();
            }


            if (user == null)
            {
                return new { Message = 2 };

            }
            else
            {
                try
                {
                    var msg = "نام کاربری : " + user.userName + " \\n رمز عبور : " + user.password + "";
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + user.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    //ServiceReference1.smsserverPortTypeClient newservice = new ServiceReference1.smsserverPortTypeClient();

                    //var result = newservice.SendSMS("+9850001676666", user.mobile, "نام کاربری : " + user.username + "\n رمز عبور : " + user.password + "\n barcodet.com", "", "mapjo", "09199129063");


                    return new { Message = 0 };

                }
                catch
                {
                    return new { Message = 3 };

                }
            }

        }
        [HttpPost]
        [Route("api/Home/MelkUserDelete")]
        public object MelkUserDelete()
        {
            int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);
            var melks = Context.melks.Where(p => p.userId == id).ToList();
            var SearchMelkUser = Context.users.Find(id);
            if (SearchMelkUser != null)
            {
                foreach (var item in melks)
                {
                    var idMelk = item.id;
                    var findMelk = Context.melks.Find(idMelk);
                    Context.melks.Remove(findMelk);

                }

                Context.users.Remove(SearchMelkUser);
                Context.SaveChanges();
            }
            else
            {
                return new
                {
                    Message = 2
                };
            }


            return new
            {
                Message = 1
            };



        }

        [HttpPost]
        [Route("api/Home/SearchAdvancedMelk/{page}")]

        public object SearchAdvancedMelk(int page)
        {
            var description = Convert.ToString(HttpContext.Current.Request.Form["description"]);
            var priceDescription = Convert.ToInt64(HttpContext.Current.Request.Form["priceDescription"]);
        
            var ListMelks = Context.melks.Where(p => p.Categori.categoriName.Contains(description) || p.SubCategori.subCategoriName.Contains(description) ||p.ChildSubCategori.childSubCategoriName.Contains(description) ||p.city.cityName.Contains(description) || p.subCity.subCityname.Contains(description) || p.title.Contains(description) || p.gheymateForoosh ==priceDescription || p.gheymateRahn == priceDescription || p.gheymateEjareh == priceDescription).Select(p => new { p.id, p.ChildSubCategori.childSubCategoriName, p.userType, p.film, p.user.apiToken, p.user.fullName, p.lat, p.lon, p.ghabeleTabdilBarayeEjareh, p.ghabeleTabdilBarayeRahn, p.subCity.subCityname, p.imageUrl, p.SubCategori.subCategoriName, p.title, p.telephone, p.mobile, p.address, p.tozihatTakmily, p.parking, p.isNegahban, p.isSystemGarmayeshiMarkazi, p.isKabinet, p.isHayat, p.isSalonEjtema, p.isLaby, p.isEstakhrVaSona, p.isSalonVarzeshi, p.isPentHouse, p.isFazayeSabz, p.IsDastshooyiFarangi, p.IsMaster, p.vaziyatSokoonat, p.mogheiyatJographiya, p.isDarbZedSerghat, p.kafpoosh, p.cooler, p.garmayeshi, p.isIphoneTasviry, p.isAntenMarkazi, p.divar, p.saghf, p.isKomodDivari, p.isPangerehDojedareh, p.typeDastebandi, p.gheymateForoosh, p.gheymateRahn, p.gheymateEjareh, p.melkType, p.sanad, p.sanadType, p.typeEskelet, p.zirbana, p.tedadTabaghat, p.tedadVahedDarTabaghe, p.tedadOtaghkhab, p.buildDate.pDate, p.city.cityName, p.Categori.categoriName }).AsQueryable();


            return new { Data = ListMelks.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = ListMelks.Count() };
        }

        //از اینجا به بعد  کلیه امور مربوط به مشاغل است

        //مشاغل
        [HttpGet]
        [Route("api/Home/LoadJobCategori")]

        public object LoadJobCategori()
        {
            var result = Context.jobCategoris.Select(x => new { x.id, x.namee }).ToList();
            return result;
        }
        [HttpGet]
        [Route("api/Home/LoadJobSubCategori/{id}")]
        public object LoadJobSubCategori(int? id)
        {
            var result = Context.jobSubCategoris.Where(p => p.jobCategori.id == id).Select(x => new { x.id, x.namee }).ToList();
            return result;
        }



        //واکشی اطلاعات زیردسته بندی دوم
        [HttpGet]
        [Route("api/Home/LoadChildJobSubCategori/{idJobSubCategori}")]
        public object LoadChildJobSubCategori(int? idJobSubCategori)
        {
            var result = Context.childjobSubCategoris.Where(p => p.jobSubCategoriId == idJobSubCategori).Select(x => new { x.id, x.childJobSubCategoriName }).ToList();
            return result;
        }


        [HttpPost]
        [Route("api/Home/AddJob")]
        public object AddJob()
        {
            string data = HttpContext.Current.Request.Form["data"];

            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            RegisterJob Joba = jalbum.ToObject<RegisterJob>();


            string imageUrl = "";
            string film = "";


            job _job = new job();

            jobUser _JobUser = new jobUser();

            if (Joba.apiToken == null)
            {
                var SearchUser = Context.jobUsers.FirstOrDefault(p => p.mobile == Joba.mobile);
                if (SearchUser != null)
                {
                    Joba.apiToken = SearchUser.apiToken;
                }
                else
                {
                    RandomGenerator generator = new RandomGenerator();
                    Random random = new Random();
                    _JobUser.fullName = Joba.fullName;
                    _JobUser.mobile = Joba.mobile;
                    _JobUser.userName = generator.RandomString(5, false);
                    _JobUser.password  = random.Next(10000, 99999).ToString();
                    _JobUser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                    _JobUser.userType = Joba.userType;
                    _JobUser.email = Joba.email;
                    Context.jobUsers.Add(_JobUser);
                }

            }
            var finduserr = Context.jobUsers.FirstOrDefault(p => p.apiToken == Joba.apiToken);

            _job.title = Joba.title;
            _job.lon = Joba.lon;
            _job.lat = Joba.lat;
            _job.fullName = Joba.fullName;
            _job.mobile = Joba.mobile;
            _job.email = Joba.email;
            _job.cityId = Joba.cityId;
            _job.subCityId = Joba.subCityId;
            _job.tahsilat = Joba.tahsilat;
            _job.jobCategoriId = Joba.jobCategoriId;
            _job.jobSubCategoriId = Joba.jobSubCategoriId;
            _job.sharheVazayef = Joba.sharheVazayef;
            _job.vazayefRoozaneVaHaftegiVaMahane = Joba.vazayefRoozaneVaHaftegiVaMahane;
            _job.JobAddress = Joba.JobAddress;
            _job.sabeghe = Joba.sabeghe;
            _job.otherMaharat = Joba.otherMaharat;
            _job.childjobSubCategoriId = Joba.childjobSubCategoriId;
            _job.pExpire = ExtensionFunction.ToPersian((DateTime.Now.AddMonths(1)));
			//imageUrl خط اخر
			//_job.isHasPardakhtBime = Joba.isHasPardakhtBime;
			//_job.modatePardakhtBime = Joba.modatePardakhtBime;
			//_job.shomareBime = Joba.shomareBime;
			_job.ExpireDate = DateTime.Now.AddMonths(1);

			_job.Mablagh = Joba.Mablagh;
            _job.userType = Joba.userType;
            _job.advertisementType = Joba.advertisementType;
            //apiToken خط اخر
            if (finduserr != null)
            {
                _job.userId = finduserr.id;
                _job.apiToken = finduserr.apiToken;

            }
            else
            {
                _job.userId = _JobUser.id;
                _job.apiToken = _JobUser.apiToken;
            }


            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["imageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            imageUrl = "/Upload/" + namefile;
                        }
                        else if (postedFile == HttpContext.Current.Request.Files["film"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            film = "/Upload/" + namefile;
                        }

                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            Context.jobPictures.Add(new jobPicture
                            {
                                jobId = _job.id,
                                url = "/Upload/" + namefile
                            });
                        }
                        postedFile.SaveAs(filePath);

                    }
                }
            }

            _job.imageUrl = imageUrl;
            _job.film = film;

            _job.status = Convert.ToString(2);

            Context.jobs.Add(_job);

            try
            {
                Context.SaveChanges();


                if(_JobUser.password !=null && _JobUser.userName !=null)
                {
                    var msg = "آگهی شغلی شما با عنوان: " + Joba.title + " با موفقیت ثبت گردید.";
                    var userandpass = "نام کاربری و رمز عبور در صورت نیاز" + _JobUser.password + " " + _JobUser.userName + "";

                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"," + userandpass + "\"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + Joba.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);


                }
                else
                {
                    var msg = "آگهی شغلی شما با عنوان: " + Joba.title + " با موفقیت ثبت گردید.";
                 

                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"," +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + Joba.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);


                }




            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return new
            {
                Message = 0
            };


        }
        //اطلاعات مختصر یک شغل
        [HttpGet]
        [Route("api/Home/LoadJobs/{page}")]

        public object LoadJobs(int page)
        {
			var thisyear = DateTime.Now;

			var result = Context.jobs.Where(s=>s.status==1.ToString() &&s.ExpireDate >=thisyear ).Select(x => new
            {
                x.id,
                x.title,
                x.mobile,
                x.jobCategoriId,
                x.lon,
                x.lat,
                x.jobUser.fullName,
                x.jobUser.email,
                x.city.cityName,
                x.jobUser.userType,
                JobCategori = x.jobCategori.namee,
                x.imageUrl,
                x.Mablagh,
                x.tahsilat,
                x.JobAddress,
                x.advertisementType,
                x.film,
                x.childjobSubCategori.childJobSubCategoriName
            }).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = result.Count() };
        }
        //اطلاعات کلی شغل
        [HttpGet]
        [Route("api/Home/LoadOnejob/{id}")]
        public object LoadOnejob(int id)
        {
            var LoadOneJob = Context.jobs.Where(p => p.id == id).Select(p => new { p.id, p.childjobSubCategori.childJobSubCategoriName,p.advertisementType, p.film,p.JobAddress,p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, JobCategori=p.jobCategori.namee,p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane, p.fullName, p.Mablagh,p.apiToken }).FirstOrDefault();

            var images = Context.jobPictures.Where(p => p.jobId == id).Select(p => p.url).ToList();

            return new { LoadOneJob, images };
        }
        //حذف یک شغل
        [HttpPost]
        [Route("api/Home/DeleteJob/{id}")]

        public object DeleteJob(int id)
        {
            job _job = Context.jobs.Find(id);
            try
            {
                System.IO.File.Delete("~/Upload/" + _job.imageUrl);


            }
            catch (Exception rer) { }
            var JobPics = Context.jobPictures.Where(x => x.jobId == id).ToList();


            foreach (var item in JobPics)
            {
                try
                {

                    System.IO.File.Delete("~/Upload/" + item.url);

                }
                catch (Exception ex) { }
            }
            Context.jobs.Remove(_job);
            Context.SaveChanges();
            return new { Message = 0};
        }
        //ویرایش یک شغل
        [HttpPost]
        [Route("api/Home/EditJob")]

        public object EditJob()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            EditJobModel jobA = jalbum.ToObject<EditJobModel>();


            int id = jobA.Id;

            string film = "";
            //int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);
            string imageUrl = "";
            var Job = Context.jobs.FirstOrDefault(x => x.id == id);
            if (HttpContext.Current.Request.Form.AllKeys.Contains("imageUrl"))
            {
                try
                {
                    System.IO.File.Delete(Job.imageUrl);
                }
                catch (Exception ex)
                { }

            }
            else if (HttpContext.Current.Request.Form.AllKeys.Contains("film"))
            {
                try
                {
                    System.IO.File.Delete(Job.film);
                }
                catch (Exception ex)
                { }


            }

            var JobPics = Context.jobPictures.Where(x => x.jobId == id).ToList();


            foreach (var item in JobPics)
            {
                try
                {

                    System.IO.File.Delete("~/Upload/" + item.url);

                }
                catch (Exception ex) { }
            }
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  
                    //var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //var extension = ext.ToLower();

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(fileNameExternal).ToLower();

                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["imageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            imageUrl = "/Upload/" + namefile;

                            Job.imageUrl = imageUrl;
                        }
                        else if (postedFile == HttpContext.Current.Request.Files["film"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            film = "/Upload/" + namefile;
                            Job.film = film;
                        }

                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            Context.jobPictures.Add(new jobPicture
                            {
                                jobId = Job.id,
                                url = "/Upload/" + namefile
                            });
                        }

                        postedFile.SaveAs(filePath);
                    }
                }
            }



            if (jobA.title != null)
            {
                string title = jobA.title;
                Job.title = title;
            }
            if(jobA.JobAddress != null)
            {
                string JobAddress = jobA.JobAddress;
                Job.JobAddress = jobA.JobAddress;
            }
            //if (jobA.lon !=null)
            //{
            double lon = jobA.lon;
            Job.lon = lon;
            //}
            //if (jobA.lat !=null)
            //{
            double lat = jobA.lat;
            Job.lat = lat;
            //}
            if (jobA.fullName != null)
            {
                string fullName = jobA.fullName;
                Job.fullName = fullName;
            }
            //if (jobA.mobile != null)
            //{
            //    string mobile = jobA.mobile;
            //    Job.mobile = mobile;
            //}
            if (jobA.email != null)
            {
                string email = jobA.email;
                Job.email = email;
            }
          
                int childjobSubCategoriId = jobA.childjobSubCategoriId;
                Job.childjobSubCategoriId = childjobSubCategoriId;
            
            //if (jobA.cityId != null)
            //{
            int cityId = jobA.cityId;


            Job.cityId = cityId;
            //}
            //if (jobA.subCityId != null)
            //{
            int subCityId = jobA.subCityId;


            Job.subCityId = subCityId;
            //}
            //if (jobA.tahsilat !=null)
            //{
            byte tahsilat = jobA.tahsilat;

            Job.tahsilat = tahsilat;
            //}
            //if (jobA.jobCategoriId !=null)
            //{
            int jobCategoriId = jobA.jobCategoriId;


            Job.jobCategoriId = jobCategoriId;
            //}
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("jobSubCategoriId"))
            //{
            int jobSubCategoriId = jobA.jobSubCategoriId;


            Job.jobSubCategoriId = jobSubCategoriId;
            //}
            if (jobA.sharheVazayef != null)
            {
                string sharheVazayef = jobA.sharheVazayef;
                Job.sharheVazayef = sharheVazayef;
            }
            if (jobA.vazayefRoozaneVaHaftegiVaMahane != null)
            {
                string vazayefRoozaneVaHaftegiVaMahane = jobA.vazayefRoozaneVaHaftegiVaMahane;
                Job.vazayefRoozaneVaHaftegiVaMahane = vazayefRoozaneVaHaftegiVaMahane;
            }
            Job.otherMaharat = jobA.otherMaharat;
            Job.sabeghe = jobA.sabeghe;
            //if (jobA.isHasPardakhtBime )
            //{
            //bool isHasPardakhtBime = jobA.isHasPardakhtBime;


            //Job.isHasPardakhtBime = isHasPardakhtBime;
            //}
            //if (jobA.modatePardakhtBime != null)
            //{
            //    string modatePardakhtBime = jobA.modatePardakhtBime;
            //    Job.modatePardakhtBime = modatePardakhtBime;
            ////}
            //if (jobA.shomareBime != null)
            //{
            //    string shomareBime = jobA.shomareBime;
            //    Job.shomareBime = shomareBime;
            //}
            if (jobA.Mablagh != null)
            {
                string Mablagh = jobA.Mablagh;
                Job.Mablagh = Mablagh;
            }

        
           
            Job.status = Convert.ToString(2);
            Context.SaveChanges();
            return new { Message =0};
        }
        //جستجوی شغل بر اساس شهر
        [HttpGet]
        [Route("api/Home/SearchJobByCityId/{CityId}/{page}")]

        public object SearchJobByCityId(int CityId, int page)
        {
            var result = Context.jobs.Where(x => x.cityId == CityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childjobSubCategori.childJobSubCategoriName,p.film, p.JobAddress,p.advertisementType, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane, p.fullName, p.Mablagh }).Take(10).ToList(), totalCount = result.Count() };
        }
        //جستجوی شغل با شهرستان
        [HttpGet]
        [Route("api/Home/SearchJobBySubCityId/{SubCityId}/{page}")]

        public object SearchJobBySubCityId(int SubCityId, int page)
        {
            var result = Context.jobs.Where(x => x.subCityId == SubCityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childjobSubCategori.childJobSubCategoriName,p.film, p.JobAddress,p.advertisementType, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane,  p.fullName, p.Mablagh }).Take(10).ToList(), totalCount = result.Count() };
        }
        //جستجوی شغل با دسته بندی
        [HttpGet]
        [Route("api/Home/SearchJobByCategoriId/{CategoriId}/{page}")]

        public object SearchJobByCategoriId(int CategoriId, int page)
        {
            var result = Context.jobs.Where(x => x.jobCategoriId == CategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childjobSubCategori.childJobSubCategoriName,p.film, p.JobAddress, p.advertisementType, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane,  p.fullName, p.Mablagh }).Take(10).ToList(), totalCount = result.Count() };
        }
        //جستجوی شغل با زیردسته بندی
        [HttpGet]
        [Route("api/Home/SearchJobBySubCategoriId/{jobSubCategoriId}/{page}")]

        public object SearchJobBySubCategoriId(int jobSubCategoriId, int page)
        {
            var result = Context.jobs.Where(x => x.jobSubCategoriId == jobSubCategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.film, p.childjobSubCategori.childJobSubCategoriName,p.JobAddress, p.advertisementType, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane,p.fullName, p.Mablagh }).Take(10).ToList(), totalCount = result.Count() };
        }

        //جستجوی شغل با زیردسته بندی دوم
        [HttpGet]
        [Route("api/Home/SearchJobByChildSubCategoriId/{ChildjobSubCategoriId}/{page}")]

        public object SearchJobByChildSubCategoriId(int ChildjobSubCategoriId, int page)
        {
            var result = Context.jobs.Where(x => x.childjobSubCategoriId == ChildjobSubCategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.film, p.childjobSubCategori.childJobSubCategoriName,p.JobAddress, p.advertisementType, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane, p.fullName, p.Mablagh }).Take(10).ToList(), totalCount = result.Count() };
        }



        //جستجوی شغل بر اساس نزدیک ترین ها
        [HttpPost]
        [Route("api/Home/SearchJobNearest")]
        public object SearchJobNearest()
        {

            double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            int? jobSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["jobSubCategoriId"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.Form["page"]);

            if (page == 0)
                page = 1;

            var today = DateTime.Now;
            double constValue = 57.2957795130823D;
            double constValue2 = 3958.75586574D;
            if (lon != 0)
                constValue = lon;
            if (lat != 0)
                constValue2 = lat;
            int pageSize = 10;
            page = Convert.ToInt32(page);
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            var Pjob = (from l in Context.jobs
                        let temp = SqlFunctions.Sin(l.lat / constValue)
                        * SqlFunctions.Sin(lat / constValue)
                        + SqlFunctions.Cos(l.lat / constValue)
                        * SqlFunctions.Cos(lat / constValue)
                        * SqlFunctions.Cos(lon / constValue)
                        - (l.lon / constValue)
                        let calMiles = (constValue2 * SqlFunctions.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                        where (l.lat > 0 && l.lon > 0)
                        orderby calMiles descending
                        select new { l.jobSubCategoriId, l.id, l.childjobSubCategoriId,l.title, l.lon, l.lat, l.subCityId, l.jobCategoriId, l.imageUrl,l.JobAddress });
            if (jobSubCategoriId != 0)
                Pjob = Pjob.Where(x => x.jobSubCategoriId == jobSubCategoriId);
            return new { Data = Pjob.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).Select(x => new { x.jobSubCategoriId, x.childjobSubCategoriId,x.id, x.title, x.lon, x.lat, x.subCityId, x.jobCategoriId, x.imageUrl ,x.JobAddress}), totalCount = Pjob.Count() };
        }



        //جستجوی همه مشاغل ها روی نقشه 

        [HttpPost]
        [Route("api/Home/SearchAllJobsNearest")]
        public object SearchAllJobsNearest()
        {
            double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            //int? equipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentSubCategoriId"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.Form["page"]);

            if (page == 0)
                page = 1;

            var today = DateTime.Now;
            double constValue = 57.2957795130823D;
            double constValue2 = 3958.75586574D;
            if (lon != 0)
                constValue = lon;
            if (lat != 0)
                constValue2 = lat;
            int pageSize = 10;
            page = Convert.ToInt32(page);

            page = page > 0 ? page : 1;

            pageSize = pageSize > 0 ? pageSize : 10;
            var PJob = (from l in Context.jobs
                              let temp = SqlFunctions.Sin(l.lat / constValue)
                              * SqlFunctions.Sin(lat / constValue)
                              + SqlFunctions.Cos(l.lat / constValue)
                              * SqlFunctions.Cos(lat / constValue)
                              * SqlFunctions.Cos(lon / constValue)
                              - (l.lon / constValue)
                              let calMiles = (constValue2 * SqlFunctions.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                              where (l.lat > 0 && l.lon > 0)
                              orderby calMiles descending
                              select new { l.jobSubCategoriId, l.childjobSubCategoriId,l.id, l.title, l.lon, l.lat, l.subCityId, l.jobCategoriId, l.imageUrl ,l.JobAddress});
            //if (equipmentSubCategoriId != 0)
            //PEquipment = PEquipment.Select(p=>p.id).ToList();
            return new { Data = PJob.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).Select(x => new { x.jobSubCategoriId, x.id, x.title, x.childjobSubCategoriId,x.lon, x.lat, x.subCityId, x.jobCategoriId, x.imageUrl,x.JobAddress }), totalCount = PJob.Count() };
        }
        [HttpPost]
        [Route("api/Home/SearchJobByTitle/{page}")]

        public object SearchJobByTitle(int page)
        {


            var description = Convert.ToString(HttpContext.Current.Request.Form["description"]);
            var result = Context.jobs.Where(x => x.title.Contains(description)).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childjobSubCategori.childJobSubCategoriName,p.advertisementType, p.JobAddress, p.film, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, p.tahsilat, p.jobCategori.namee, jobSubCategori = p.jobSubCategori.namee, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane, p.fullName, p.Mablagh }).Take(10).ToList(), totalCount = result.Count() };

        }
        //ایجادیک حساب کاربری
        [HttpPost]
        [Route("api/Home/jobCreatAccount")]

        public object jobCreatAccount()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;


            JobUserModel jobUserA = jalbum.ToObject<JobUserModel>();



            string mobile = jobUserA.mobile;
            var findUser = Context.jobUsers.FirstOrDefault(p => p.mobile == mobile);
            user pUser = new user();
            if (findUser != null)
            {
                if (jobUserA.fullName != null)
                {
                    string UserfullName = jobUserA.fullName;
                    findUser.fullName = UserfullName;

                }
                if (jobUserA.email != null)
                {
                    string email = jobUserA.email;
                    findUser.email = email;

                }
                if (jobUserA.telephone != null)
                {
                    string Usertelephone = jobUserA.telephone;

                    findUser.telephone = Usertelephone;

                }
                if (jobUserA.userName != null)
                {
                    string UseruserName = jobUserA.userName;

                    findUser.userName = UseruserName;

                }
                if (jobUserA.password != null)
                {
                    string Userpassword = jobUserA.password;

                    findUser.password = Userpassword;

                }
                if (jobUserA.abutMe != null)
                {
                    string UserabutMe = jobUserA.abutMe;

                    findUser.abutMe = UserabutMe;
                }

                Context.SaveChanges();
                return new
                {
                    Token = findUser.apiToken,


                    Message = 0
                };

            }

            else
            {

                //string telephone = HttpContext.Current.Request.Form["telephone"];
                //string email = HttpContext.Current.Request.Form["email"];

                //string fullName = HttpContext.Current.Request.Form["fullName"];
                //string userName = HttpContext.Current.Request.Form["userName"];
                //string password = HttpContext.Current.Request.Form["password"];
                //string abutMe = HttpContext.Current.Request.Form["abutMe"];
                //byte userType = Convert.ToByte(HttpContext.Current.Request.Form["userType"]);

                string userImageUrl = HttpContext.Current.Request.Form["userImageUrl"];


                var isUserName = Context.jobUsers.Any(p => p.userName == jobUserA.userName);
                if (isUserName)
                {
                    return new
                    {
                        Message =3
                    };
                }


                //else
                //{
                //newuser.state = 0;
                //کاربر
                var Puser = new jobUser();
                Random _g = new Random();

                Puser.fullName = jobUserA.fullName;
                Puser.telephone = jobUserA.telephone;
                Puser.mobile = jobUserA.mobile;
                Puser.email = jobUserA.email;
                Puser.userName = jobUserA.userName;
                Puser.password = jobUserA.password;
                Puser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                Puser.abutMe = jobUserA.abutMe;
                Puser.userType = jobUserA.userType;

                var httpRequest = HttpContext.Current.Request;
                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                        if (postedFile.ContentLength > MaxContentLength)
                        {
                            return new
                            {
                                Message = 2
                            };
                        }
                        else
                        {
                            string fileNameExternal = Path.GetFileName(postedFile.FileName);
                            var namefile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fileNameExternal).ToLower();
                            var filePath = "";

                            if (postedFile == HttpContext.Current.Request.Files["userImageUrl"])
                            {
                                filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                                userImageUrl = "/Upload/" + namefile;
                            }

                            postedFile.SaveAs(filePath);
                        }

                    }
                }
                Puser.userImageUrl = userImageUrl;
                Context.jobUsers.Add(Puser);


                Context.SaveChanges();


                //Random random = new Random();
                //string guidcode = random.Next(10000, 99999).ToString();


                return new
                {

                    Token = Puser.apiToken,

                    Message = 0,

                };

            }
        }

        //ویرایش حساب کاربری مشاغل
        [HttpPost]
        [Route("api/Home/jobEditAccount")]
        public object jobEditAccount()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            jobuserAccountEdit EditUserA = jalbum.ToObject<jobuserAccountEdit>();
            var apiToken = EditUserA.apiToken;
            string userImageUrl = "";
            var FindUser = Context.jobUsers.FirstOrDefault(x => x.apiToken == apiToken);
            if (HttpContext.Current.Request.Form.AllKeys.Contains("userImageUrl"))
            {
                try
                {
                    System.IO.File.Delete(FindUser.userImageUrl);
                }
                catch (Exception ex)
                { }

            }
            if (EditUserA.fullName != null)
            {
                FindUser.fullName = EditUserA.fullName;
            }
            if (EditUserA.mobile != null)
            {
                FindUser.mobile = EditUserA.mobile;
            }
            if (EditUserA.telephone != null)
            {
                FindUser.telephone = EditUserA.telephone;
            }
            if (EditUserA.email != null)
            {
                FindUser.email = EditUserA.email;
            }
            if (EditUserA.password != null)
            {
                FindUser.password = EditUserA.password;
            }
            if (EditUserA.abutMe != null)
            {
                FindUser.abutMe = EditUserA.abutMe;
            }
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["userImageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            userImageUrl = "/Upload/" + namefile;
                        }


                        postedFile.SaveAs(filePath);

                    }
                }
            }

            if (userImageUrl != null)
            {

                FindUser.userImageUrl = userImageUrl;
            }


            Context.SaveChanges();
            return new
            {
                Message = 0,

            };




        }


        //ورود به حساب کاربری مشاغل
        [HttpPost]
        [Route("api/Home/JobUserLogin")]
        public object JobUserLogin()
        {
            var userName = HttpContext.Current.Request.Form["userName"];
            var password = HttpContext.Current.Request.Form["password"];
            if (Context.jobUsers.Any(p => p.userName == userName && p.password == password))
            {
                var pUser = Context.jobUsers.FirstOrDefault(x => x.password == password && x.userName == userName);
                return new
                {
                    pUser.apiToken,
                };
            }
            else
            {
                return new
                {
                    Message = 1,
                };
            }




        }
        //حذف حساب کاربری مشاغل
        [HttpPost]
        [Route("api/Home/JobUserDelete")]
        public object JobUserDelete()
        {
            int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);
            var Jobs = Context.jobs.Where(p => p.userId == id).ToList();
            var SearchJobUser = Context.jobUsers.Find(id);
            if (SearchJobUser != null)
            {
                foreach (var item in Jobs)
                {
                    var idjob = item.id;
                    var findjob = Context.jobs.Find(idjob);
                    Context.jobs.Remove(findjob);

                }

                Context.jobUsers.Remove(SearchJobUser);
                Context.SaveChanges();
            }
            else
            {
                return new
                {
                    Message = 2
                };
            }


            return new
            {
                Message = 0
            };



        }
        // اطلاعات حساب کاربری
        [HttpPost]
        [Route("api/Home/JobUserInformations")]
        public object JobUserInformations()
        {
            string apiToken = HttpContext.Current.Request.Form["apiToken"];
            int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);


            var UserInformation = Context.jobUsers.Where(p => p.apiToken == apiToken || p.id == id).Select(p => new { p.fullName, p.telephone, p.mobile, p.email,p.userType, p.abutMe, p.userImageUrl ,p.userName}).ToList().FirstOrDefault();

            if (UserInformation != null)
            {
                return UserInformation;
            }
            else
            {
                return new
                {
                    Message =2
                };
            }


        }
        //شغل های ذخیره شده توسط کاربر
        [HttpPost]
        [Route("api/Home/jobsUserSaved/{page}")]
        public object jobsUserSaved(int page)
        {

            string apiToken = HttpContext.Current.Request.Form["apiToken"];
         


            var ListJobs = Context.jobs.Where(p => p.jobUser.apiToken == apiToken ).Select(p => new { p.id,p.childjobSubCategori.childJobSubCategoriName, p.film, p.title, p.city.cityName, p.jobCategori.namee, JobSubCategori = p.jobSubCategori.namee, p.email, p.imageUrl, p.sharheVazayef, p.vazayefRoozaneVaHaftegiVaMahane, p.status, p.lon, p.lat, p.Mablagh,p.JobAddress }).AsQueryable();

            return new { Data = ListJobs.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = ListJobs.Count() };

        }


        [HttpPost]
        [Route("api/Home/JobForgetPassword")]

        public object JobForgetPassword()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            JobUserModel _jobUserModel = jalbum.ToObject<JobUserModel>();



            if (String.IsNullOrWhiteSpace(_jobUserModel.userName) == false || String.IsNullOrWhiteSpace(_jobUserModel.mobile) == false)
            {
            }
            else
            {
                return new { Message = 1 };
            }
            jobUser _user = new jobUser();
            _user = null;
            if (String.IsNullOrWhiteSpace(_jobUserModel.userName) == false)
            {
                _user = Context.jobUsers.Where(r => r.userName == _jobUserModel.userName).FirstOrDefault();
            }
            else if (String.IsNullOrWhiteSpace(_jobUserModel.mobile) == false)
            {
                _user = Context.jobUsers.Where(r => r.mobile == _jobUserModel.mobile).FirstOrDefault();
            }


            if (_user == null)
            {
                return new { Message = 2 };

            }
            else
            {
                try
                {
                    var msg = "نام کاربری : " + _user.userName + " \\n رمز عبور : " + _user.password + "";
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + _user.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    //ServiceReference1.smsserverPortTypeClient newservice = new ServiceReference1.smsserverPortTypeClient();

                    //var result = newservice.SendSMS("+9850001676666", user.mobile, "نام کاربری : " + user.username + "\n رمز عبور : " + user.password + "\n barcodet.com", "", "mapjo", "09199129063");


                    return new { Message = 0 };

                }
                catch
                {
                    return new { Message = 3 };

                }
            }

        }




        //از اینجا به بعد مربوط به لوازم دسته دوم است
        //فاز سوم


        //اطلاعات گروه لوازم
        [HttpGet]
        [Route("api/Home/LoadEquipmentCategori")]

        public object LoadEquipmentCategori()
        {
            var result = Context.equipmentCategoris.Select(x => new { x.id, x.namee }).ToList();
            return result;
        }
        //اطلاعات زیرگروه لوازم بر اساس ای دی گروه
        [HttpGet]
        [Route("api/Home/LoadEquipmentSubCategori/{id}")]
        public object LoadEquipmentSubCategori(int? id)
        {
            var result = Context.equipmentSubCategoris.Where(p => p.equipmentCategoriId == id).Select(x => new { x.id, x.namee }).ToList();
            return result;
        }



        //اطلاعات زیرگروه دوم  لوازم بر اساس ای دی گروه
        [HttpGet]
        [Route("api/Home/LoadChildEquipmentSubCategori/{idEquipmentSubCategori}")]
        public object LoadChildEquipmentSubCategori(int? idEquipmentSubCategori)
        {
            var result = Context.childSubEquipmentCategoris.Where(p => p.equipmentSubCategoriId== idEquipmentSubCategori).Select(x => new { x.id, x.childSubEquipmentCategoriName }).ToList();
            return result;
        }
        //ثبت وسیله
        [HttpPost]
        [Route("api/Home/AddEquipment")]
        public object AddEquipment()
        {

            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            AddEquipmentModel EquipmentA = jalbum.ToObject<AddEquipmentModel>();





            //string title = HttpContext.Current.Request.Form["title"];
            //string fullName = HttpContext.Current.Request.Form["fullName"];

            //string description = HttpContext.Current.Request.Form["description"];
            //double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            //double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            //string mobile = HttpContext.Current.Request.Form["mobile"];
            //string email = HttpContext.Current.Request.Form["email"];
            string imageUrl = "";
            string film = "";
            //int price = Convert.ToInt32(HttpContext.Current.Request.Form["price"]);

            //int cityId = Convert.ToInt32(HttpContext.Current.Request.Form["cityId"]);
            //int subCityId = Convert.ToInt32(HttpContext.Current.Request.Form["subCityId"]);
            //int equipmentCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentCategoriId"]);
            //int equipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentSubCategoriId"]);
            //string apiToken = HttpContext.Current.Request.Form["apiToken"];
            //byte userType = Convert.ToByte(HttpContext.Current.Request.Form["userType"]);

            equipment _equipment = new equipment();

            equipmetUser _equipmetUser = new equipmetUser();

            if (EquipmentA.apiToken == null)
            {
                var SearchUser = Context.equipmetUsers.FirstOrDefault(p => p.mobile == EquipmentA. mobile);
                if (SearchUser != null)
                {
                    EquipmentA. apiToken = SearchUser.apiToken;
                }
                else
                {
                    Random rand = new Random();
                    RandomGenerator generator = new RandomGenerator();
                    _equipmetUser.fullName = EquipmentA. fullName;
                    _equipmetUser.mobile = EquipmentA. mobile;
                    _equipmetUser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                    _equipmetUser.userName =generator.RandomString(5,false);

                    _equipmetUser.password = rand.Next(10000, 99999).ToString();
                    _equipmetUser.userType = EquipmentA. userType;
                    _equipmetUser.email = EquipmentA. email;
                    Context.equipmetUsers.Add(_equipmetUser);

                }

            }
            var finduserr = Context.equipmetUsers.FirstOrDefault(p => p.apiToken == EquipmentA. apiToken);




            _equipment.title = EquipmentA. title;
            _equipment.lon = EquipmentA. lon;
            _equipment.lat = EquipmentA.lat;
            //_equipment.fullName = fullName;
            _equipment.mobile = EquipmentA. mobile;
            _equipment.email = EquipmentA. email;
            _equipment.cityId = EquipmentA. cityId;
            _equipment.subCityId = EquipmentA. subCityId;
            _equipment.description = EquipmentA. description;
            _equipment.equipmentCategoriId = EquipmentA. equipmentCategoriId;
            _equipment.equipmentSubCategoriId = EquipmentA. equipmentSubCategoriId;
            _equipment.price = EquipmentA. price;
            _equipment.EquipmentAddress = EquipmentA.EquipmentAddress;
            _equipment.childEquipmentSubCategoriId = EquipmentA.childEquipmentSubCategoriId;
            _equipment.pexpireDate = ExtensionFunction.ToPersian((DateTime.Now.AddMonths(1)));
			_equipment.ExpireDate = DateTime.Now.AddMonths(1);

			//imageUrl خط اخر

			_equipment.userType = EquipmentA. userType;
            //apiToken خط اخر
            if (finduserr != null)
            {
                _equipment.userId = finduserr.id;
                _equipment.apiToken = finduserr.apiToken;

            }
            else
            {
                _equipment.userId = _equipmetUser.id;
                _equipment.apiToken = _equipmetUser.apiToken;
            }


            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["imageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            imageUrl = "/Upload/" + namefile;
                        }
                        else if (postedFile == HttpContext.Current.Request.Files["film"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            film = "/Upload/" + namefile;
                        }

                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            Context.equipmentPictures.Add(new equipmentPicture
                            {
                                equipmentId = _equipment.id,
                                url = "/Upload/" + namefile
                            });
                        }
                        postedFile.SaveAs(filePath);

                    }
                }
            }
            _equipment.imageUrl = imageUrl;
            _equipment.film = film;

            _equipment.status = Convert.ToString(2);

            Context.equipments.Add(_equipment);

            try
            {
                Context.SaveChanges();

                if (_equipmetUser.password !=null && _equipmetUser.userName != null)
                {




                    var msg = "آگهی مصالح شما با عنوان: " + EquipmentA.title + " با موفقیت ثبت گردید.";
                    var userandpass = "نام کاربری و رمز عبور در صورت نیاز" + _equipmetUser.password + " " + _equipmetUser.userName + "";
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"," + userandpass + "\"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + EquipmentA.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                }
                else
                {
                    var msg = "آگهی مصالح شما با عنوان: " + EquipmentA.title + " با موفقیت ثبت گردید.";
                
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + EquipmentA.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                }





            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return new
            {
                Message = 0,

            };


        }

        //اطلاعات مختصر یک وسلیه
        [HttpGet]
        [Route("api/Home/LoadEquipments/{page}")]

        public object LoadEquipments(int page)
        {
			var thisYear = DateTime.Now;
            var result = Context.equipments.Where(s=>s.status ==1.ToString() && s.ExpireDate >= thisYear).Select(x => new
            {
                x.id,
                x.title,
                x.mobile,
                x.description,

                x.lon,
                x.lat,
                x.price,
                x.email,
                x.imageUrl,
                x.city.cityName,
                x.subCity.subCityname,
                x.equipmentCategori.namee,
                SubCategori = x.equipmentSubCategori.namee,
                x.apiToken,
                x.equipmetUser.fullName,

                x.pexpireDate,
                x.userType,
                x.status,
                x.EquipmentAddress,
                x.film,
                x.childSubEquipmentCategori.childSubEquipmentCategoriName

            }).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = result.Count() };
        }

        //اطلاعات کلی وسیله
        [HttpGet]
        [Route("api/Home/LoadOneequipment/{id}")]
        public object LoadOneequipment(int id)
        {
            //var LoadOneEquipment = Context.equipments.Include("subCity").Include("city").Include("equipmentCategori").Include("equipmentSubCategori").Where(p => p.id == id).Select(p => new { p.id, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname,Categori=p.equipmentCategori.namee,SubCategori=p.equipmentSubCategori.namee ,p.price,p.description,p.pexpireDate,p.userId}).ToList();
            var LoadOneEquipment = Context.equipments.Where(p => p.id == id).Select(p => new { p.id,p.equipmetUser.fullName,p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress,p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId,p.apiToken }).FirstOrDefault();

            var pic = Context.equipmentPictures.Where(p => p.equipmentId == id).Select(p => new { p.url, p.id }).ToList();

            return new { Data = LoadOneEquipment,  pic };
            //return  LoadOneEquipment;

        }
        //حذف یک وسیله
        [HttpPost]
        [Route("api/Home/DeleteEquipment/{id}")]

        public object DeleteEquipment(int id)
        {
            equipment _equipment = Context.equipments.Find(id);
            try
            {
                System.IO.File.Delete("~/Upload/" + _equipment.imageUrl);


            }
            catch (Exception rer) { }
            var EquipmentPics = Context.equipmentPictures.Where(x => x.equipmentId == id).ToList();


            foreach (var item in EquipmentPics)
            {
                try
                {

                    System.IO.File.Delete("~/Upload/" + item.url);

                }
                catch (Exception ex) { }
            }
            Context.equipments.Remove(_equipment);
            Context.SaveChanges();
            return new { Message = 0 };
        }

        //ویرایش یک وسیله
        [HttpPost]
        [Route("api/Home/EditEquipment")]

        public object EditEquipment()
        {

            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            EditEquipmentModel EquipmentA = jalbum.ToObject<EditEquipmentModel>();








            int id = EquipmentA.Id;
            string imageUrl = "";
            string film = "";
            var equipment = Context.equipments.FirstOrDefault(x => x.id == id);
            if (HttpContext.Current.Request.Form.AllKeys.Contains("imageUrl"))
            {
                try
                {
                    System.IO.File.Delete(equipment.imageUrl);
                }
                catch (Exception ex)
                { }

            }
            var equipmentPics = Context.equipmentPictures.Where(x => x.equipmentId == id).ToList();


            foreach (var item in equipmentPics)
            {
                try
                {

                    System.IO.File.Delete("~/Upload/" + item.url);

                }
                catch (Exception ex) {
                }
            }
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  
                    //var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //var extension = ext.ToLower();

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(fileNameExternal).ToLower();

                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["imageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            imageUrl = "/Upload/" + namefile;
                            equipment.imageUrl = imageUrl;
                            ;
                        }

                        else if (postedFile == HttpContext.Current.Request.Files["film"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            film = "/Upload/" + namefile;
                            equipment.film = film;
                        }

                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);

                            Context.equipmentPictures.Add(new equipmentPicture
                            {
                                equipmentId = equipment.id,
                                url = "/Upload/" + namefile
                            });
                        }



                        postedFile.SaveAs(filePath);
                    }
                }
            }
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("title"))
            //{
            //    string title = HttpContext.Current.Request.Form["title"];
            //    equipment.title = title;
            //}
            equipment.title = EquipmentA.title;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("EquipmentAddress"))
            //{
            //    string EquipmentAddress = HttpContext.Current.Request.Form["EquipmentAddress"];
            //    equipment.EquipmentAddress = EquipmentAddress;
            //}
            equipment.EquipmentAddress = EquipmentA.EquipmentAddress;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("lon"))
            //{
            //    double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            //    equipment.lon = lon;
            //}
            equipment.lon = EquipmentA.lon;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("lat"))
            //{
            //    double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            //    equipment.lat = lat;
            //}
            equipment.lat = EquipmentA.lat;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("mobile"))
            //{
            //    string mobile = HttpContext.Current.Request.Form["mobile"];
            //    equipment.mobile = mobile;
            //}

            //if (HttpContext.Current.Request.Form.AllKeys.Contains("email"))
            //{
            //    string email = HttpContext.Current.Request.Form["email"];
            //    equipment.email = email;
            //}
            equipment.email = EquipmentA.email;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("cityId"))
            //{
            //    int cityId = Convert.ToInt32(HttpContext.Current.Request.Form["cityId"]);


            //    equipment.cityId = cityId;
            //}
            equipment.cityId = EquipmentA.cityId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("childEquipmentSubCategoriId"))
            //{
            //    int childEquipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["childEquipmentSubCategoriId"]);


            //    equipment.childEquipmentSubCategoriId = childEquipmentSubCategoriId;
            //}
            equipment.childEquipmentSubCategoriId = EquipmentA.childEquipmentSubCategoriId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("subCityId"))
            //{
            //    int subCityId = Convert.ToInt32(HttpContext.Current.Request.Form["subCityId"]);


            //    equipment.subCityId = subCityId;
            //}
            equipment.subCityId = EquipmentA.subCityId;

            //if (HttpContext.Current.Request.Form.AllKeys.Contains("equipmentCategoriId"))
            //{
            //    int equipmentCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentCategoriId"]);


            //    equipment.equipmentCategoriId = equipmentCategoriId;
            //}
            equipment.equipmentCategoriId = EquipmentA.equipmentCategoriId;
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("equipmentSubCategoriId"))
            //{
            //    int equipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentSubCategoriId"]);


            //    equipment.equipmentSubCategoriId = equipmentSubCategoriId;
            //}
            equipment.equipmentSubCategoriId = EquipmentA.equipmentSubCategoriId;

            //if (HttpContext.Current.Request.Form.AllKeys.Contains("price"))
            //{
            //    int price = Convert.ToInt32(HttpContext.Current.Request.Form["price"]);


            //    equipment.price = price;
            //}
            equipment.price = EquipmentA.price;



          
              
            
            equipment.status = Convert.ToString(2);
            Context.SaveChanges();
            return new { Message = 0 };
        }

        //جستجوی وسیله بر اساس شهر
        [HttpGet]
        [Route("api/Home/SearchEquipmentByCityId/{CityId}/{page}")]

        public object SearchEquipmentByCityId(int CityId, int page)
        {
            var result = Context.equipments.Where(x => x.cityId == CityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress,p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId }).Take(10).ToList(), totalCount = result.Count() };
        }
        //جستجوی وسیله با شهرستان
        [HttpGet]
        [Route("api/Home/SearchEquipmentBySubCityId/{SubCityId}/{page}")]

        public object SearchEquipmentBySubCityId(int SubCityId, int page)
        {
            var result = Context.equipments.Where(x => x.subCityId == SubCityId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId }).Take(10).ToList(), totalCount = result.Count() };
        }
        //جستجوی وسیله با دسته بندی
        [HttpGet]
        [Route("api/Home/SearchEquipmentByCategoriId/{CategoriId}/{page}")]

        public object SearchEquipmentByCategoriId(int CategoriId, int page)
        {
            var result = Context.equipments.Where(x => x.equipmentCategoriId == CategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId }).Take(10).ToList(), totalCount = result.Count() };
        }
        //جستجوی وسیله با زیردسته بندی
        [HttpGet]
        [Route("api/Home/SearchEquipmentBySubCategoriId/{jobSubCategoriId}/{page}")]

        public object SearchEquipmentBySubCategoriId(int jobSubCategoriId, int page)
        {
            var result = Context.equipments.Where(x => x.equipmentSubCategoriId == jobSubCategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId }).Take(10).ToList(), totalCount = result.Count() };
        }


        //جستجوی وسیله با زیردسته بندی دوم 
        [HttpGet]
        [Route("api/Home/SearchEquipmentByChildSubCategoriId/{ChildEquipentSubCategoriId}/{page}")]

        public object SearchEquipmentByChildSubCategoriId(int ChildEquipentSubCategoriId, int page)
        {
            var result = Context.equipments.Where(x => x.childEquipmentSubCategoriId == ChildEquipentSubCategoriId).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.film, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.EquipmentAddress, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId }).Take(10).ToList(), totalCount = result.Count() };
        }





        //جستجوی وسیله بر اساس نزدیک ترین ها
        [HttpPost]
        [Route("api/Home/SearchEquipmentNearest")]
        public object SearchEquipmentNearest()
        {
            double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            int? equipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentSubCategoriId"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.Form["page"]);

            if(page == 0 )
             page = 1;

            var today = DateTime.Now;
            double constValue = 57.2957795130823D;
            double constValue2 = 3958.75586574D;
            if (lon != 0)
                constValue = lon;
            if (lat != 0)
                constValue2 = lat;
            int pageSize = 10;
            page = Convert.ToInt32(page);

            page = page > 0 ? page : 1;

            pageSize = pageSize > 0 ? pageSize : 10;
            var Pjob = (from l in Context.equipments
                        let temp = SqlFunctions.Sin(l.lat / constValue)
                        * SqlFunctions.Sin(lat / constValue)
                        + SqlFunctions.Cos(l.lat / constValue)
                        * SqlFunctions.Cos(lat / constValue)
                        * SqlFunctions.Cos(lon / constValue)
                        - (l.lon / constValue)
                        let calMiles = (constValue2 * SqlFunctions.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                        where (l.lat > 0 && l.lon > 0)
                        orderby calMiles descending
                        select new { l.equipmentCategoriId, l.childEquipmentSubCategoriId,l.id, l.title, l.lon, l.lat, l.subCityId, l.equipmentSubCategoriId, l.imageUrl,l.EquipmentAddress });
            if (equipmentSubCategoriId != 0)
                Pjob = Pjob.Where(x => x.equipmentSubCategoriId == equipmentSubCategoriId);
            return new { Data = Pjob.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).Select(x => new { x.equipmentSubCategoriId, x.childEquipmentSubCategoriId,x.EquipmentAddress,x.id, x.title, x.lon, x.lat, x.subCityId, x.equipmentCategoriId, x.imageUrl }), totalCount = Pjob.Count() };
        }


        //جستجوی همه وسیله ها روی نقشه 

        [HttpPost]
        [Route("api/Home/SearchAllEquipmentNearest")]
        public object SearchAllEquipmentNearest()
        {
            double lon = Convert.ToDouble(HttpContext.Current.Request.Form["lon"]);
            double lat = Convert.ToDouble(HttpContext.Current.Request.Form["lat"]);
            //int? equipmentSubCategoriId = Convert.ToInt32(HttpContext.Current.Request.Form["equipmentSubCategoriId"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.Form["page"]);

            if (page == 0)
                page = 1;

            var today = DateTime.Now;
            double constValue = 57.2957795130823D;
            double constValue2 = 3958.75586574D;
            if (lon != 0)
                constValue = lon;
            if (lat != 0)
                constValue2 = lat;
            int pageSize = 10;
            page = Convert.ToInt32(page);

            page = page > 0 ? page : 1;

            pageSize = pageSize > 0 ? pageSize : 10;
            var PEquipment = (from l in Context.equipments
                              let temp = SqlFunctions.Sin(l.lat / constValue)
                              * SqlFunctions.Sin(lat / constValue)
                              + SqlFunctions.Cos(l.lat / constValue)
                              * SqlFunctions.Cos(lat / constValue)
                              * SqlFunctions.Cos(lon / constValue)
                              - (l.lon / constValue)
                              let calMiles = (constValue2 * SqlFunctions.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                              where (l.lat > 0 && l.lon > 0)
                              orderby calMiles descending
                              select new { l.equipmentCategoriId, l.childEquipmentSubCategoriId,l.id, l.title, l.lon, l.lat, l.subCityId, l.equipmentSubCategoriId, l.imageUrl,l.EquipmentAddress });
            //if (equipmentSubCategoriId != 0)
            //PEquipment = PEquipment.Select(p=>p.id).ToList();
            return new { Data = PEquipment.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).Select(x => new { x.equipmentSubCategoriId, x.childEquipmentSubCategoriId,x.EquipmentAddress,x.id, x.title, x.lon, x.lat, x.subCityId, x.equipmentCategoriId, x.imageUrl }), totalCount = PEquipment.Count() };
        }





        [HttpPost]
        [Route("api/Home/SearchEquipmentByTitle/{page}")]

        public object SearchEquipmentByTitle(int page)
        {


            var description = Convert.ToString(HttpContext.Current.Request.Form["description"]);
            var result = Context.equipments.Where(x => x.title.Contains(description)).AsQueryable();
            return new { Data = result.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Select(p => new { p.id, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate, p.userId }).Take(10).ToList(), totalCount = result.Count() };

        }


        //ایجادیک حساب کاربری
        [HttpPost]
        [Route("api/Home/EquipmentCreatAccount")]

        public object EquipmentCreatAccount()
        {


            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            AddEquipmentUserModel EquipmentUserA = jalbum.ToObject<AddEquipmentUserModel>();




            string mobile = EquipmentUserA.mobile;
            var findUser = Context.equipmetUsers.FirstOrDefault(p => p.mobile == mobile);
            user pUser = new user();
            if (findUser != null)
            {
                if (EquipmentUserA.fullName !=null)
                {
                    string UserfullName = EquipmentUserA.fullName;
                    findUser.fullName = UserfullName;

                }
                if (EquipmentUserA.email!=null)
                {
                    string email = EquipmentUserA.email;
                    findUser.email = email;

                }
                if (EquipmentUserA.telephone !=null)
                {
                    string Usertelephone = EquipmentUserA.telephone;
                    findUser.telephone = Usertelephone;

                }
                if (EquipmentUserA.userName !=null)
                {
                    string UseruserName = EquipmentUserA.userName;

                    findUser.userName = UseruserName;

                }
                if (EquipmentUserA.password !=null)
                {
                    string Userpassword = EquipmentUserA.password;

                    findUser.password = Userpassword;

                }
                if (EquipmentUserA.abutMe !=null)
                {
                    string UserabutMe = EquipmentUserA.abutMe;

                    findUser.abutMe = UserabutMe;
                }

                Context.SaveChanges();
                return new
                {
                    Token = findUser.apiToken,


                    Message = 0
                };

            }

            else
            {

                //string telephone = HttpContext.Current.Request.Form["telephone"];
                //string email = HttpContext.Current.Request.Form["email"];

                //string fullName = HttpContext.Current.Request.Form["fullName"];
                //string userName = HttpContext.Current.Request.Form["userName"];
                //string password = HttpContext.Current.Request.Form["password"];
                //string abutMe = HttpContext.Current.Request.Form["abutMe"];
                //byte userType = Convert.ToByte(HttpContext.Current.Request.Form["userType"]);

                string userImageUrl = HttpContext.Current.Request.Form["userImageUrl"];


                var isUserName = Context.equipmetUsers.Any(p => p.userName == EquipmentUserA.userName);
                if (isUserName)
                {
                    return new
                    {
                        Message = 2
                    };
                }


                //else
                //{
                //newuser.state = 0;
                //کاربر
                var Puser = new equipmetUser();
                Random _g = new Random();

                Puser.fullName = EquipmentUserA.fullName;
                Puser.telephone = EquipmentUserA.telephone;
                Puser.mobile = mobile;
                Puser.email = EquipmentUserA. email;
                Puser.userName = EquipmentUserA. userName;
                Puser.password = EquipmentUserA. password;
                Puser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                Puser.abutMe = EquipmentUserA. abutMe;
                Puser.userType = EquipmentUserA. userType;

                var httpRequest = HttpContext.Current.Request;
                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                        if (postedFile.ContentLength > MaxContentLength)
                        {
                            return new
                            {
                                Message = 2
                            };
                        }
                        else
                        {
                            string fileNameExternal = Path.GetFileName(postedFile.FileName);
                            var namefile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fileNameExternal).ToLower();
                            var filePath = "";

                            if (postedFile == HttpContext.Current.Request.Files["userImageUrl"])
                            {
                                filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);


                                userImageUrl = "/Upload/" + namefile;
                            }
                            postedFile.SaveAs(filePath);
                        }

                    }
                }
                Puser.userImageUrl = userImageUrl;
                Context.equipmetUsers.Add(Puser);


                Context.SaveChanges();

                //Random random = new Random();
                //string guidcode = random.Next(10000, 99999).ToString();


                return new
                {

                    Token = Puser.apiToken,

                    Message = 0,

                };

            }
        }

        //ویرایش حساب کاربری لوازم دسته دوم
        [HttpPost]
        [Route("api/Home/EquipmentEditAccount")]
        public object EquipmentEditAccount()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            EditEquipmentUserModel EditUserA = jalbum.ToObject<EditEquipmentUserModel>();
            var apiToken = EditUserA.aptToken;
            string userImageUrl = "";
            var FindUser = Context.equipmetUsers.FirstOrDefault(x => x.apiToken == apiToken);
            //if (HttpContext.Current.Request.Form.AllKeys.Contains("userImageUrl"))
            //{
            //    try
            //    {
            //        System.IO.File.Delete(FindUser.userImageUrl);
            //    }
            //    catch (Exception ex)
            //    { }

            //}
            if (EditUserA.fullName != null)
            {
                FindUser.fullName = EditUserA.fullName;
            }
            if (EditUserA.mobile != null)
            {
                FindUser.mobile = EditUserA.mobile;
            }
            if (EditUserA.telephone != null)
            {
                FindUser.telephone = EditUserA.telephone;
            }
            if (EditUserA.email != null)
            {
                FindUser.email = EditUserA.email;
            }
            if (EditUserA.password != null)
            {
                FindUser.password = EditUserA.password;
            }
            if (EditUserA.abutMe != null)
            {
                FindUser.abutMe = EditUserA.abutMe;
            }
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  

                    if (postedFile.ContentLength > MaxContentLength)
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                    else
                    {
                        string fileNameExternal = Path.GetFileName(postedFile.FileName);
                        var namefile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fileNameExternal).ToLower();
                        var filePath = "";
                        if (postedFile == HttpContext.Current.Request.Files["userImageUrl"])
                        {
                            filePath = HttpContext.Current.Server.MapPath("~/Upload/" + namefile);
                            userImageUrl = "/Upload/" + namefile;
                        }


                        postedFile.SaveAs(filePath);

                    }
                }
            }
            //if (userImageUrl != null)
            //{

                FindUser.userImageUrl = userImageUrl;
            //}


            Context.SaveChanges();
            return new
            {
                Message = 0,

            };




        }


        //ورود به حساب کاربری وسایل
        [HttpPost]
        [Route("api/Home/EquipmentUserLogin")]
        public object EquipmentUserLogin()
        {
            var userName = HttpContext.Current.Request.Form["userName"];
            var password = HttpContext.Current.Request.Form["password"];
            if (Context.equipmetUsers.Any(p => p.userName == userName && p.password == password))
            {
                var pUser = Context.equipmetUsers.FirstOrDefault(x => x.password == password && x.userName == userName);
                return new
                {
                    pUser.apiToken,
                };
            }
            else
            {
                return new
                {
                    Message = 2,
                };
            }




        }


        //حذف حساب کاربری وسایل
        [HttpPost]
        [Route("api/Home/EquipmentUserDelete")]
        public object EquipmentUserDelete()
        {
            int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);
            var Equipments = Context.equipments.Where(p => p.userId == id).ToList();
            var SearchEquipmentUser = Context.equipmetUsers.Find(id);
            if (SearchEquipmentUser != null)
            {
                foreach (var item in Equipments)
                {
                    var idEquipment = item.id;
                    var findEquipment = Context.equipments.Find(idEquipment);
                    Context.equipments.Remove(findEquipment);

                }

                Context.equipmetUsers.Remove(SearchEquipmentUser);
                Context.SaveChanges();
            }
            else
            {
                return new
                {
                    Message =2
                };
            }


            return new
            {
                Message = 0
            };



        }
        // اطلاعات حساب کاربری
        [HttpPost]
        [Route("api/Home/EquipmentUserInformations")]
        public object EquipmentUserInformations()
        {
            string apiToken = HttpContext.Current.Request.Form["apiToken"];
            int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);


            var UserInformation = Context.equipmetUsers.Where(p => p.apiToken == apiToken || p.id == id).Select(p => new { p.fullName, p.telephone, p.mobile, p.userType, p.abutMe, p.email,p.userImageUrl,p.userName }).ToList().FirstOrDefault();

            if (UserInformation != null)
            {
                return UserInformation;
            }
            else
            {
                return new
                {
                    Message = 2
                };
            }


        }
        //وسایل ذخیره شده توسط کاربر
        [HttpPost]
        [Route("api/Home/EquipmentUserSaved/{page}")]
        public object EquipmentUserSaved(int page)
        {

            string apiToken = HttpContext.Current.Request.Form["apiToken"];
          


            var ListEquipments = Context.equipments.Where(p => p.equipmetUser.apiToken == apiToken ).Select(p=>new { p.id, p.childSubEquipmentCategori.childSubEquipmentCategoriName,p.film, p.EquipmentAddress, p.userType, p.lon, p.lat, p.imageUrl, p.title, p.mobile, p.email, p.city.cityName, p.subCity.subCityname, Categori = p.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, p.price, p.description, p.pexpireDate }).AsQueryable();

            return new { Data = ListEquipments.OrderByDescending(x => x.id).Skip(10 * (page - 1)).Take(10).ToList(), totalCount = ListEquipments.Count() };

        }

        [HttpPost]
        [Route("api/Home/EquipmentForgetPassword")]

        public object EquipmentForgetPassword()
        {

            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            EquipmentUserModel _EquipmentUserModel = jalbum.ToObject<EquipmentUserModel>();




            if (String.IsNullOrWhiteSpace(_EquipmentUserModel.userName) == false || String.IsNullOrWhiteSpace(_EquipmentUserModel.mobile) == false)
            {
            }
            else
            {
                return new { Message = 1 };
            }
            equipmetUser _user = new equipmetUser();
            _user = null;
            if (String.IsNullOrWhiteSpace(_EquipmentUserModel.userName) == false)
            {
                _user = Context.equipmetUsers.Where(r => r.userName == _EquipmentUserModel.userName).FirstOrDefault();
            }
            else if (String.IsNullOrWhiteSpace(_EquipmentUserModel.mobile) == false)
            {
                _user = Context.equipmetUsers.Where(r => r.mobile == _EquipmentUserModel.mobile).FirstOrDefault();
            }


            if (_user == null)
            {
                return new { Message = 2 };

            }
            else
            {
                try
                {
                    var msg = "نام کاربری : " + _user.userName + " \\n رمز عبور : " + _user.password + "";
                    var client = new RestClient("http://37.130.202.188/api/select");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("undefined", "{\"op\" : \"send\"" +
                        ",\"uname\" : \"Barcodet\"" +
                        ",\"pass\":  \"09130126411\"" +
                        ",\"message\" : \"" + msg + " \"" +
                        ",\"from\": \"simcard\"" +
                        ",\"to\" : [\"" + _user.mobile + "\"]}"
                        , ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    //ServiceReference1.smsserverPortTypeClient newservice = new ServiceReference1.smsserverPortTypeClient();

                    //var result = newservice.SendSMS("+9850001676666", user.mobile, "نام کاربری : " + user.username + "\n رمز عبور : " + user.password + "\n barcodet.com", "", "mapjo", "09199129063");


                    return new { Message = 0 };

                }
                catch
                {
                    return new { Message = 3 };

                }
            }

        }

        //شناسایی هویت
        [HttpPost]
        [Route("api/Home/MadeCodeForUserRegister")]
        public object MadeCodeForUserRegister()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            GenerateCodeModel Code = jalbum.ToObject<GenerateCodeModel>();

            //var findUser = Context.users.Any(p => p.mobile == mobile);
            Random rand = new Random();
           
            string guidcode = rand.Next(10000, 99999).ToString();
            var message = "کد ورودشما:" + guidcode;

            var client = new RestClient("http://37.130.202.188/api/select");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\"op\" : \"send\"" +
                ",\"uname\" : \"Barcodet\"" +
                ",\"pass\":  \"09130126411\"" +
                ",\"message\" : \"" + message + " \"" +
                ",\"from\": \"simcard\"" +
                ",\"to\" : [\"" +Code. mobile + "\"]}"
                , ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return new
            {
                guidcode
            };
        }



        //کد تایید برای ثبت آگهی املاک
        [HttpPost]
        [Route("api/Home/MadeCodeForAddMelk")]
        public object GetCodeForUserRegister()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            GenerateCodeModel Code = jalbum.ToObject<GenerateCodeModel>();

            string guidcode = "";
            var findUser = Context.users.Any(p=>p.mobile==Code.mobile);
            if (findUser)
            {
                return new
                {
                    guidcode = ""
                };

            }
            else
            {


                Random rand = new Random();

                guidcode = rand.Next(10000, 99999).ToString();
                var message = "کد ورودشما:" + guidcode;

                var client = new RestClient("http://37.130.202.188/api/select");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\"op\" : \"send\"" +
                    ",\"uname\" : \"Barcodet\"" +
                    ",\"pass\":  \"09130126411\"" +
                    ",\"message\" : \"" + message + " \"" +
                    ",\"from\": \"simcard\"" +
                    ",\"to\" : [\"" + Code.mobile + "\"]}"
                    , ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                return new
                {
                    guidcode
                };
            }

        }



        //کدتایید برای ثبت آگهی مشاغل
        [HttpPost]
        [Route("api/Home/MadeCodeForAddJob")]
        public object MadeCodeForAddJob()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            GenerateCodeModel Code = jalbum.ToObject<GenerateCodeModel>();

            string guidcode = "";
            var findUser = Context.jobUsers.Any(p => p.mobile == Code.mobile);
            if (findUser)
            {
                return new
                {
                    guidcode = ""
                };

            }
            else
            {


                Random rand = new Random();

                guidcode = rand.Next(10000, 99999).ToString();
                var message = "کد ورودشما:" + guidcode;

                var client = new RestClient("http://37.130.202.188/api/select");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\"op\" : \"send\"" +
                    ",\"uname\" : \"Barcodet\"" +
                    ",\"pass\":  \"09130126411\"" +
                    ",\"message\" : \"" + message + " \"" +
                    ",\"from\": \"simcard\"" +
                    ",\"to\" : [\"" + Code.mobile + "\"]}"
                    , ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                return new
                {
                    guidcode
                };
            }

        }



        //کدتایید برای ثبت آگهی لوازم دسته دوم
        [HttpPost]
        [Route("api/Home/MadeCodeForAddEquipment")]
        public object MadeCodeForAddEquipment()
        {
            string data = HttpContext.Current.Request.Form["data"];


            JObject json = JObject.Parse(data);
            JObject jalbum = json as JObject;

            // Copy to a static Album instance
            GenerateCodeModel Code = jalbum.ToObject<GenerateCodeModel>();

            string guidcode = "";
            var findUser = Context.equipmetUsers.Any(p => p.mobile == Code.mobile);
            if (findUser)
            {
                return new
                {
                    guidcode = ""
                };

            }
            else
            {


                Random rand = new Random();

                guidcode = rand.Next(10000, 99999).ToString();
                var message = "کد ورودشما:" + guidcode;

                var client = new RestClient("http://37.130.202.188/api/select");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\"op\" : \"send\"" +
                    ",\"uname\" : \"Barcodet\"" +
                    ",\"pass\":  \"09130126411\"" +
                    ",\"message\" : \"" + message + " \"" +
                    ",\"from\": \"simcard\"" +
                    ",\"to\" : [\"" + Code.mobile + "\"]}"
                    , ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                return new
                {
                    guidcode
                };
            }

        }


        //حدف عکس های لوازم دسته دوم
        [HttpPost]
        [Route("api/Home/RemovePictureForEquipment")]
        public object RemovePictureForEquipment()
        {  
            //ای دی آگهی
            int Id = Convert.ToInt32(HttpContext.Current.Request.Form["Id"]);
            //url عکس
            string fileUrl = Convert.ToString(HttpContext.Current.Request.Form["fileUrl"]);
            //آگهی مورد نظر
            var Item = Context.equipments.Where(s => s.id == Id).FirstOrDefault();
            //دیگر عکس  های این آگهی
            var OtherPictures = Context.equipmentPictures.Where(c => c.equipmentId == Id).ToList();
            //string rootFolder = "~/Upload/";

            string authorsFile = fileUrl;
            try
            {
                //اگر یوارال فرستاده شده با یوارال اصلی اگهی برابر بود، داخل این شرط میشود و پاک میکند
                if (Item.imageUrl == fileUrl)
                {
                    Item.imageUrl = "";
          
                    try
                    {

                    System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                    }
                    catch
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                   
           
                }

                //اگر یوارال فرستاده شده با یوارال فیلم اگهی برابر بود، داخل این شرط میشود و پاک میکند
                else if (Item.film == fileUrl)
                {
                    Item.film = "";
                    try
                    {

                        System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                    }
                    catch
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                }
                //در غیر این صورت در دیگر عکس های آگهی جستجو میکند تا یوارال مورد نظر را پیدا کند
                else 
                {
                    foreach (var pic in OtherPictures)
                    {
                        if (pic.url == fileUrl)
                        {
                            Context.equipmentPictures.Remove(pic);
                            try
                            {

                                System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                            }
                            catch
                            {
                                return new
                                {
                                    Message = 2
                                };
                            }
                        }

                    }
                }
                Context.SaveChanges();
                return new
                {
                    Message = 0
                };
            }
            catch
            {
                return new
                {
                    Message = 1
                };
            }

        }

        //حذف عکس های مشاغل
        [HttpPost]
        [Route("api/Home/RemovePictureForJobs")]
        public object RemovePictureForJobs()
        {
            //ای دی آگهی
            int Id = Convert.ToInt32(HttpContext.Current.Request.Form["Id"]);
            //url عکس
            string fileUrl = Convert.ToString(HttpContext.Current.Request.Form["fileUrl"]);
            //آگهی مورد نظر
            var Item = Context.jobs.Where(s => s.id == Id).FirstOrDefault();
            //دیگر عکس  های این آگهی
            var OtherPictures = Context.jobPictures.Where(c => c.jobId == Id).ToList();
            //string rootFolder = "~/Upload/";

            string authorsFile = fileUrl;
            try
            {
                //اگر یوارال فرستاده شده با یوارال اصلی اگهی برابر بود، داخل این شرط میشود و پاک میکند
                if (Item.imageUrl == fileUrl)
                {
                    Item.imageUrl = "";

                    try
                    {

                        System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                    }
                    catch
                    {
                        return new
                        {
                            Message = 2
                        };
                    }


                }

                //اگر یوارال فرستاده شده با یوارال فیلم اگهی برابر بود، داخل این شرط میشود و پاک میکند
                else if (Item.film == fileUrl)
                {
                    Item.film = "";
                    try
                    {

                        System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                    }
                    catch
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                }
                //در غیر این صورت در دیگر عکس های آگهی جستجو میکند تا یوارال مورد نظر را پیدا کند
                else
                {
                    foreach (var pic in OtherPictures)
                    {
                        if (pic.url == fileUrl)
                        {
                            Context.jobPictures.Remove(pic);
                            try
                            {

                                System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                            }
                            catch
                            {
                                return new
                                {
                                    Message = 2
                                };
                            }
                        }

                    }
                }
                Context.SaveChanges();
                return new
                {
                    Message = 0
                };
            }
            catch
            {
                return new
                {
                    Message = 1
                };
            }

        }
        //حدف عکس های املاک
        [HttpPost]
        [Route("api/Home/RemovePictureForMelks")]
        public object RemovePictureForMelks()
        {
            //ای دی آگهی
            int Id = Convert.ToInt32(HttpContext.Current.Request.Form["Id"]);
            //url عکس
            string fileUrl = Convert.ToString(HttpContext.Current.Request.Form["fileUrl"]);
            //آگهی مورد نظر
            var Item = Context.melks.Where(s => s.id == Id).FirstOrDefault();
            //دیگر عکس  های این آگهی
            var OtherPictures = Context.pictures.Where(c => c.melkId == Id).ToList();
            //string rootFolder = "~/Upload/";

            string authorsFile = fileUrl;
            try
            {
                //اگر یوارال فرستاده شده با یوارال اصلی اگهی برابر بود، داخل این شرط میشود و پاک میکند
                if (Item.imageUrl == fileUrl)
                {
                    Item.imageUrl = "";

                    try
                    {

                        System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                    }
                    catch
                    {
                        return new
                        {
                            Message = 2
                        };
                    }


                }

                //اگر یوارال فرستاده شده با یوارال فیلم اگهی برابر بود، داخل این شرط میشود و پاک میکند
                else if (Item.film == fileUrl)
                {
                    Item.film = "";
                    try
                    {

                        System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                    }
                    catch
                    {
                        return new
                        {
                            Message = 2
                        };
                    }
                }
                //در غیر این صورت در دیگر عکس های آگهی جستجو میکند تا یوارال مورد نظر را پیدا کند
                else
                {
                    foreach (var pic in OtherPictures)
                    {
                        if (pic.url == fileUrl)
                        {
                            Context.pictures.Remove(pic);
                            try
                            {

                                System.IO.File.Delete(HostingEnvironment.MapPath(fileUrl));
                            }
                            catch
                            {
                                return new
                                {
                                    Message = 2
                                };
                            }
                        }

                    }
                }
                Context.SaveChanges();
                return new
                {
                    Message = 0
                };
            }
            catch
            {
                return new
                {
                    Message = 1
                };
            }

        }




        //پایان
    }
}
