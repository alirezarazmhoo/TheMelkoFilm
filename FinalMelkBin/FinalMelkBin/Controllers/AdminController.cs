
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalMelkBin.Models;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Infrastructure;

namespace FinalMelkBin.Controllers
{
    public class AdminController : Controller
    {

        TheFinalMelkobinEntities Context = new TheFinalMelkobinEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult logOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult logOn(user puser)
        {
            string ReturnUrl = Request["ReturnUrl"];

            bool IsRemember = Request["IsRemember"] != null;

            if (Context.AdminUsers.Any(p => p.userName == puser.userName && p.password == puser.password))
            {
                FormsAuthentication.SetAuthCookie(puser.userName, IsRemember);
                if (ReturnUrl != "")
                {
                    return new RedirectResult(ReturnUrl);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(puser.userName, IsRemember);
                    return new RedirectResult(FormsAuthentication.DefaultUrl);
                }
            }
            else
            {
                ViewBag.Error = "کلمه کاربری یا رمز عبور اشتباه است";
                return View();
            }

        }
        [HttpGet]
        public ActionResult logOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("logOn", "Admin");
        }
        [HttpGet]
        public ActionResult AddCity()
        {
          


            return View();
        }
        [HttpPost]
        //[AjaxErrorHandler]
        //[MyAuthorize(Roles =("r1"))]
        public ActionResult CityRegister(city pCity)
        {
           if( User.IsInRole("AddCity") || User.IsInRole("SuperAdmin"))
                {

                var _cityName = pCity.cityName;
                var findCity = Context.cities.Any(p => p.cityName == _cityName);
                if (findCity)
                {
                    //ViewBag.ErrorCity = "اطلاعات تکراری است";

                    //TempData["ErorrCity"] = "اطلاعات تکراری است";

                    return Json("fail");

                }
                else
                {


                    if (pCity.id == 0)
                        Context.cities.Add(pCity);
                    else
                    {
                        Context.Entry(pCity).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.SaveChanges();

                }
                return RedirectToAction("AddCity");
                
            }
            return RedirectToAction("AddCity");

        }
        public ActionResult GetCityName(string description)
        {
            var ListCities = Context.cities.Where(p=>p.cityName.Contains(description)).Select(p => new { Name = p.cityName, ID = p.id }).OrderByDescending(p=>p.ID).ToList();
            return Json(ListCities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
   
        public ActionResult RemoveCity(int id)
        {
            if (User.IsInRole("RemoveCity") || User.IsInRole("SuperAdmin"))
            {


                var city = Context.cities.Find(id);

                try
                {


                    Context.cities.Remove(city);
                    Context.SaveChanges();

                }
                catch (DbUpdateException)
                {

                    return Json("Fail");

                }


                return RedirectToAction("AddCity");
            }
            return RedirectToAction("AddCity");

        }
        public ActionResult CheckCity(string Data)
        {
            var City = Context.cities.Any(p => p.cityName == Data);
            return Json(City, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddSubCity()
        {
            return View();
        }
        public ActionResult subCityRegister(subCity PSubcity)
        {

            if (User.IsInRole("AddSubCity") || User.IsInRole("SuperAdmin"))
            {
                var _SubcityName = PSubcity.subCityname;
                var findSubCity = Context.subCities.Any(p => p.subCityname == _SubcityName);
                if (findSubCity)
                {
                    //ViewBag.ErrorCity = "اطلاعات تکراری است";

                    //TempData["ErorrCity"] = "اطلاعات تکراری است";
                    return Json("fail");

                }
                else
                {

                    if (PSubcity.id == 0)
                        Context.subCities.Add(PSubcity);
                    else
                    {
                        Context.Entry(PSubcity).State = System.Data.Entity.EntityState.Modified;
                    }

                    Context.SaveChanges();
                }
                    return RedirectToAction("GetSubCityInfo");
            }
         
            return RedirectToAction("GetSubCityInfo");
         
        }
        public ActionResult GetSubCityInfo(string description)
        {
            var ListTowShip = Context.subCities.Where(p=>p.subCityname.Contains(description) || p.city.cityName.Contains(description)).Select(p => new { ID = p.id, Name = p.subCityname, CityName = p.city.cityName, CityID = p.city.id }).OrderByDescending(p => p.ID).ToList();
            return Json(ListTowShip, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveSubCity(int id)
        {
            if (User.IsInRole("RemoveSubCity") || User.IsInRole("SuperAdmin"))
            {

                var TownShip = Context.subCities.Find(id);
                try
                {
                    Context.subCities.Remove(TownShip);
                    Context.SaveChanges();
                    return RedirectToAction("AddSubCity");
                }
                catch (DbUpdateException)
                {

                    return Json("Fail");

                }
            }
            else
            {

            return RedirectToAction("AddSubCity");
            }




        }

        public ActionResult CheckSubCity(string Data)
        {
            var City = Context.subCities.Any(p => p.subCityname == Data);
            return Json(City, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddCategori()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult CategoriRegister(Categori pCategori)
        {
            if (User.IsInRole("AddMelkCategori") || User.IsInRole("SuperAdmin"))
            {

                var _CategoriName = pCategori.categoriName;
                var findCategori = Context.Categoris.Any(p => p.categoriName == _CategoriName);
                if (findCategori)
                {
                    //ViewBag.ErrorCity = "اطلاعات تکراری است";

                    //TempData["ErorrCity"] = "اطلاعات تکراری است";
                    return Json("fail");

                }

                else
                {



                    if (pCategori.id == 0)
                        Context.Categoris.Add(pCategori);
                    else
                    {
                        Context.Entry(pCategori).State = System.Data.Entity.EntityState.Modified;
                    }

                    Context.SaveChanges();
                    return RedirectToAction("AddCategori");

                }
            }
            return RedirectToAction("AddCategori");

        }
        [HttpGet]
        public ActionResult GetCategoriName(string description)
        {
            var Categori = Context.Categoris.Where(p=>p.categoriName.Contains(description)).Select(p => new { ID = p.id, Name = p.categoriName }).OrderByDescending(p => p.ID).ToList();
            return Json(Categori, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult RemoveCategori(int id)
        {
            if (User.IsInRole("RemoveMelkCategori") || User.IsInRole("SuperAdmin"))
            {

                var Categori = Context.Categoris.Find(id);
                try
                {
                    Context.Categoris.Remove(Categori);
                    Context.SaveChanges();

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                          "Try again, and if the problem persists, " +
                          "see your system administrator.");
                    return Json("Fail");

                }

                return RedirectToAction("AddCategori");
            }
            return RedirectToAction("AddCategori");

        }

        public ActionResult CheckCategori(string Data)
        {
            var Categori = Context.Categoris.Any(p => p.categoriName == Data);
            return Json(Categori, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddSubCategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubCategoriRegister(SubCategori PdownCategori)
        {
            if (User.IsInRole("AddMelkSubCategori") || User.IsInRole("SuperAdmin"))
            {



                var _SubCategoriName = PdownCategori.subCategoriName;
                var findSubCategori = Context.SubCategoris.Any(p => p.subCategoriName == _SubCategoriName);
                if (findSubCategori)
                {
                    //ViewBag.ErrorCity = "اطلاعات تکراری است";

                    //TempData["ErorrCity"] = "اطلاعات تکراری است";
                    return Json("fail");

                }


                else
                {



                    if (PdownCategori.id == 0)
                        Context.SubCategoris.Add(PdownCategori);
                    else
                    {
                        Context.Entry(PdownCategori).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.SaveChanges();
                    return RedirectToAction("AddSubCategori");
                }
            }
            return RedirectToAction("AddSubCategori");

        }
        [HttpGet]
        public ActionResult GetSubCategoriInfo(string description)
        {
            var list = Context.SubCategoris.Where(p=>p.subCategoriName.Contains(description) || p.Categori.categoriName.Contains(description)).Select(p => new { ID = p.id, Name = p.subCategoriName , CategoriName = p.Categori.categoriName, CategoriID = p.Categori.id }).OrderByDescending(p => p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DownCatDel(int id)
        {

            if (User.IsInRole("RemoveMelkSubCategori") || User.IsInRole("SuperAdmin"))
            {
                

                var DownCat = Context.SubCategoris.Find(id);

                try
                {
                    Context.SubCategoris.Remove(DownCat);
                    Context.SaveChanges();
                
                }

                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                          "Try again, and if the problem persists, " +
                          "see your system administrator.");
                    return Json("Fail");

                }
                return RedirectToAction("GetSubCategoriInfo");

            }
            return RedirectToAction("GetSubCategoriInfo");



        }
        public ActionResult CheckSubCategori(string Data)
        {
            var Categori = Context.SubCategoris.Any(p => p.subCategoriName == Data);
            return Json(Categori, JsonRequestBehavior.AllowGet);

        }

        //زیر دسته دوم
        public ActionResult AddChildSubCategori()
        {
            return View();
        }
        //ثبت زیردسته دوم
        public ActionResult ChildSubCategoriRegister(ChildSubCategori _childsubcategori)
        {
            if (User.IsInRole("AddMelkChildSubCategori") || User.IsInRole("SuperAdmin"))
            {
                var _ChildSubCategoriName = _childsubcategori.childSubCategoriName;
                var findChildSubCategori = Context.ChildSubCategoris.Any(p => p.childSubCategoriName == _ChildSubCategoriName);
                if (findChildSubCategori)
                {

                    return Json("fail");

                }

                else
                {
                    if (_childsubcategori.id == 0)
                        Context.ChildSubCategoris.Add(_childsubcategori);
                    else
                    {
                        Context.Entry(_childsubcategori).State = System.Data.Entity.EntityState.Modified;
                    }

                    Context.SaveChanges();
                    return RedirectToAction("AddChildSubCategori");

                }
            }
            return RedirectToAction("AddChildSubCategori");


        }
        //واکشی اطلاعات زیردسته دوم
        public ActionResult GetChildSubCategoriInfo(string description)
        {
            var list = Context.ChildSubCategoris.Where(p=>p.childSubCategoriName.Contains(description)||p.SubCategori.subCategoriName.Contains(description) || p.SubCategori.Categori.categoriName.Contains(description)).Select(p => new { ID = p.id, CategoriName = p.SubCategori.Categori.categoriName, SubCategori=p.SubCategori.subCategoriName, ChildSubCategori = p.childSubCategoriName , subCategoriId =p.SubCategori.id}).OrderByDescending(p=>p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //حذف اطلاعات زیردسته بندی دوم
        public ActionResult ChildSubCatDel(int id)
        {
            if(User.IsInRole("RemoveMelkChildSubCategori") || User.IsInRole("SuperAdmin"))
            {
                try
            {
                var item = Context.ChildSubCategoris.Find(id);
                Context.ChildSubCategoris.Remove(item);
                Context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                      "Try again, and if the problem persists, " +
                      "see your system administrator.");
                return Json("Fail");

            }
            return RedirectToAction("AddChildSubCategori");
                }
            return RedirectToAction("AddChildSubCategori");

        }

        //برسی اطلاعات زیردسته دوم
        public ActionResult CheckChildSubCategori(string Data)
        {
            var item = Context.ChildSubCategoris.Any(p => p.childSubCategoriName == Data);
            return Json(item, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public ActionResult listMelk()
        {
  

            return View();
        }
       

        [HttpGet]
        public ActionResult GetMelkInfo(string StatusCode, string description)
        {
            var ListMelk = Context.melks.Where(p=>(p.title.Contains(description)|| p.mobile.Contains(description)  ||p.city.cityName.Contains(description))&& p.statusId.Contains(StatusCode) ).Select(p => new { ID = p.id, Name = p.title, Price = p.gheymateForoosh,p.gheymateRahn,p.gheymateEjareh, Mobile = p.mobile ,CityName=p.city.cityName,StatusID=p.statusId,ExpireDate=p.pExpireDate}).OrderByDescending(p => p.ID).ToList();
            return Json(ListMelk,JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        public ActionResult StatusRegister(string statusvalue,int id,string adminComment)
        {
            if (User.IsInRole("RegisterMelkAuthorisation") || User.IsInRole("SuperAdmin"))
            {
                var melk = Context.melks.Find(id);
                //melk.statusId = statusvalue;
                melk.statusId = statusvalue;
                melk.adminComment = adminComment;
                Context.SaveChanges();
                return RedirectToAction("GetMelkInfo");
            }
            else
            {
                return RedirectToAction("GetMelkInfo");

            }
        }
        [HttpPost]
        public ActionResult RemoveMelk(int id)
        {
            if(User.IsInRole("RemoveMelk") || User.IsInRole("SuperAdmin"))
           {
                var melk = Context.melks.Find(id);
                Context.melks.Remove(melk);
                Context.SaveChanges();
                return RedirectToAction("GetMelkInfo");
            }
            else
            {
                return RedirectToAction("GetMelkInfo");

            }

        }

        [HttpPost]
        public ActionResult UserActivity(int id,string Puser,string statusvalue)
        {
            melkAcceptUser PmelkAccreptUser = new melkAcceptUser();
            PmelkAccreptUser.melkId = id;
            PmelkAccreptUser.userName = Puser;
            PmelkAccreptUser.statusSelected = statusvalue;
            PmelkAccreptUser.RegisterDate = ExtensionFunction.ToPersian(DateTime.Now);
            Context.melkAcceptUsers.Add(PmelkAccreptUser);
            Context.SaveChanges();



            return RedirectToAction("GetMelkInfo");
        }
        [HttpPost]
        public ActionResult RemoveActivity(int id)
        {
            var t = Context.melkAcceptUsers.Find(id);
            Context.melkAcceptUsers.Remove(t);
            Context.SaveChanges();
            return RedirectToAction("GetAdminInfo");
        }
       


        [HttpGet]
        public ActionResult listAdmin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAdminInfo(string StatusCode,string Description)
        {
            var ListAdmins = Context.melkAcceptUsers.Where(p=>(p.statusSelected.Contains(StatusCode) ||p.melk.statusId.Contains(StatusCode))&&p.userName.Contains(Description)).Select(p => new { ID = p.id,Edate= p.RegisterDate,StatusSelected = p.statusSelected, StatusMelk=p.melk.statusId,Melk = p.melk.title, AdminName = p.userName }).OrderByDescending(p => p.ID).ToList();
            return Json(ListAdmins, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowMelkInformation(int id)
        {

            ViewBag.IdMelk = id;
            return View();
        }
        public ActionResult ListDate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DateRegister(buildDate _buildDate)
        {
            if (User.IsInRole("AddMelkDate") || User.IsInRole("SuperAdmin"))
            {
                var _pDate = _buildDate.pDate;
                var findDate = Context.buildDates.Any(p => p.pDate == _pDate);
                if (findDate)
                {
                    //ViewBag.ErrorCity = "اطلاعات تکراری است";

                    //TempData["ErorrCity"] = "اطلاعات تکراری است";
                    return Json("ListDate");

                }

                else
                {

                    //_buildDate.date = ExtensionFunction.shamsitomiladi(Convert.ToDateTime(PDatee));
                    Context.buildDates.Add(_buildDate);
                    Context.SaveChanges();
                    return RedirectToAction("ListDate");

                }
            }
            return RedirectToAction("ListDate");

        }
        [HttpGet]
        public ActionResult GetDate(string description)
        {
            var ListDate = Context.buildDates.Where(p=>p.pDate.Contains(description)).Select(p => new { ID = p.id, Date = p.pDate }).OrderByDescending(p => p.ID).ToList();
            return Json(ListDate, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveDate(int id)
        {
            if (User.IsInRole("RemoveMelkDate") || User.IsInRole("SuperAdmin"))
            {

                var _Date = Context.buildDates.Find(id);
                try
                {
                    Context.buildDates.Remove(_Date);
                    Context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Json("failed");
                }
            }

            return RedirectToAction("ListDate");



        }
        [HttpGet]
        public ActionResult CheckDate(string Data)
        {
            var Date = Context.buildDates.Any(p => p.pDate ==Data);
            return Json(Date, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult RemoveAllRecordsForMelkAdminActivity(ListTableTrModel selectedRole)
        {
            if (selectedRole.listRoles != null)
            {


                foreach (var item in selectedRole.listRoles)
                {
                    var findid = Context.melkAcceptUsers.Find(item.ID);
                    Context.melkAcceptUsers.Remove(findid);


                }
                Context.SaveChanges();
                return RedirectToAction("GetAdminInfo");
            }
            return RedirectToAction("GetAdminInfo");


        }
        //پایان امور مربوط به املاک


        //از اینجا به بعد مربوط به مشاغل است 

        [HttpGet]
        public ActionResult JobCategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult JobCategoriRegister(jobCategori _JobCategori)
        {
            if(User.IsInRole("AddJobCategori") || User.IsInRole("SuperAdmin"))
                {
                var _JobCategoriName = _JobCategori.namee;
            var findJobCategori = Context.jobCategoris.Any(p => p.namee == _JobCategoriName);
            if (findJobCategori)
            {
            
                return Json("fail");

            }


            else
            {



                if (_JobCategori.id == 0)
                    Context.jobCategoris.Add(_JobCategori);
                else
                {
                    Context.Entry(_JobCategori).State = System.Data.Entity.EntityState.Modified;
                }
                Context.SaveChanges();
                return RedirectToAction("JobCategori");
            }
           }
            return RedirectToAction("JobCategori");

        }
        [HttpGet]
        public ActionResult GetJobCategoriName(string description)
        {
            var list = Context.jobCategoris.Where(p=>p.namee.Contains(description)).Select(p => new {ID= p.id, Name = p.namee }).OrderByDescending(p => p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveJobCategori(int id)
        {
            if (User.IsInRole("RemoveJobCategori") || User.IsInRole("SuperAdmin"))
            {
                var jobCat = Context.jobCategoris.Find(id);
                try
                {
                    Context.jobCategoris.Remove(jobCat);
                    Context.SaveChanges();

                }
                catch (DbUpdateException)
                {
                    return Json("failed");
                }

            return RedirectToAction("JobCategori");
            }
            else
            {
                return RedirectToAction("JobCategori");

            }

        }
     
        public ActionResult CheckJobCategori(string Data)
        {
            var JobCat = Context.jobCategoris.Any(p => p.namee == Data);
            return Json(JobCat, JsonRequestBehavior.AllowGet);

        }
        public ActionResult JobSubCategori()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult JobSubCategoriRegister(jobSubCategori _jobSubCategori)
        {
            if (User.IsInRole("AddJobSubCategori") || User.IsInRole("SuperAdmin"))
            {
                var _JobSubCategoriName = _jobSubCategori.namee;
                var findJobSubCategori = Context.jobSubCategoris.Any(p => p.namee == _JobSubCategoriName);
                if (findJobSubCategori)
                {

                    return Json("fail");

                }
                else
                {

                    if (_jobSubCategori.id == 0)
                        Context.jobSubCategoris.Add(_jobSubCategori);
                    else
                    {
                        Context.Entry(_jobSubCategori).State = System.Data.Entity.EntityState.Modified;
                    }

                    Context.SaveChanges();
                    return RedirectToAction("JobSubCategori");


                }
            }
            else
            {
                return RedirectToAction("JobSubCategori");

            }


        }
        [HttpGet]
        public ActionResult GetJobSubCategori(string description)
        {
            var listJobSub = Context.jobSubCategoris.Where(p=>p.namee.Contains(description) || p.jobCategori.namee.Contains(description)).Select(p => new { ID=p.id,Name= p.namee, NameJoBbCategori = p.jobCategori.namee ,JobCategoriId=p.jobCategori.id}).OrderByDescending(p => p.ID).ToList();

            return Json(listJobSub, JsonRequestBehavior.AllowGet);
               
        }
        [HttpPost]
        public ActionResult jobSubCategoriRemove(int id)
        {
            if (User.IsInRole("RemoveJobSubCategori") || User.IsInRole("SuperAdmin"))
            {
                var JobSub = Context.jobSubCategoris.Find(id);
                try
                {
                    Context.jobSubCategoris.Remove(JobSub);
                    Context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Json("fail");
                }

                return RedirectToAction("JobSubCategori");
            }
            else
            {
                return RedirectToAction("JobSubCategori");


            }
        }
        [HttpGet]
        public ActionResult listJobs()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetJobs(string StatusCode, string description)
        {
            var ListJobs = Context.jobs.Where(p =>(p.title.Contains(description)|| p.city.cityName.Contains(description)||p.fullName.Contains(description)) && p.status.Contains(StatusCode)).Select(p => new { ID = p.id, StatusID = p.status, FullName = p.fullName,Title=p.title,City=p.city.cityName,ExpireDate=p.pExpire }).OrderByDescending(p=>p.ID).ToList();

            return Json(ListJobs,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveJob(int id)
        {
            if (User.IsInRole("RemoveJob") || User.IsInRole("SuperAdmin"))
            {
                var job = Context.jobs.Find(id);
                Context.jobs.Remove(job);
                Context.SaveChanges();
                return RedirectToAction("listJobs");
            }
            else
            {
                return RedirectToAction("listJobs");

            }
        }
        [HttpGet]
        public ActionResult ShowJobInformation(int id)
        {
            ViewBag.IdJob = id;
            return View();
        }
        [HttpPost]
        public ActionResult JobStatusRegister(string statusvalue, int id,string adminComment)
        {
            if (User.IsInRole("RegisterJobAuthorisation") || User.IsInRole("SuperAdmin"))
            {
                var Job = Context.jobs.Find(id);
                //melk.statusId = statusvalue;
                Job.status = statusvalue;
                Job.adminComment = adminComment;
                Context.SaveChanges();
                return RedirectToAction("listJobs");
            }
            else
            {

            return RedirectToAction("listJobs");
            }

        }
        [HttpPost]
        public ActionResult JobUserActivity(int id, string Puser, string statusvalue)
        {
            jobAcceptUser PJobAccreptUser = new jobAcceptUser();
        
            PJobAccreptUser.jobId = id;
            PJobAccreptUser.userName = Puser;
            PJobAccreptUser.statusSelected = statusvalue;
            PJobAccreptUser.registerDate = ExtensionFunction.ToPersian(DateTime.Now);
            Context.jobAcceptUsers.Add(PJobAccreptUser);
            Context.SaveChanges();



            return RedirectToAction("listJobs");
        }


        public ActionResult LstJobActivityAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetJobAdminInfo(string StatusCode, string Description)
        {
            var ListAdmins = Context.jobAcceptUsers.Where(p => (p.statusSelected.Contains(StatusCode) || p.job.status.Contains(StatusCode)) && p.userName.Contains(Description)).Select(p => new { ID = p.id, Edate = p.registerDate, StatusSelected = p.statusSelected, StatusJob = p.job.status, job = p.job.title, AdminName = p.userName }).OrderByDescending(p => p.ID).ToList();
            return Json(ListAdmins, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveJobActivity(int id)
        {
            var t = Context.jobAcceptUsers.Find(id);
            Context.jobAcceptUsers.Remove(t);
            Context.SaveChanges();
            return RedirectToAction("GetJobAdminInfo");
        }

        //زیردسته دوم مشاغل ساختمانی
        public ActionResult JobChildSubCategori()
        {
            return View();
        }
        //ثبت زیردسته دوم
        public ActionResult JobChildSubCategoriRegister(childjobSubCategori _childjobSubCategori)
        {
            if (User.IsInRole("AddJobChildSubCategori") || User.IsInRole("SuperAdmin"))
            {
                var _JobChildSubCategoriName = _childjobSubCategori.childJobSubCategoriName;
                var findJobSubCategori = Context.childjobSubCategoris.Any(p => p.childJobSubCategoriName == _JobChildSubCategoriName);
                if (findJobSubCategori)
                {
                    return Json("fail");
                }
                else
                {
                    if (_childjobSubCategori.id == 0)
                        Context.childjobSubCategoris.Add(_childjobSubCategori);
                    else
                    {
                        Context.Entry(_childjobSubCategori).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.SaveChanges();
                }
            }
           

            return RedirectToAction("JobChildSubCategori");
            
        }
        public ActionResult CheckJobChildSubCategori(string Data)
        {
            var JobCat = Context.childjobSubCategoris.Any(p => p.childJobSubCategoriName == Data);
            return Json(JobCat, JsonRequestBehavior.AllowGet);

        }
        //واکشی اطلاعات زیردسته دوم
        public ActionResult GetJobChildSubCategoriInfo(string description)
        {
            var list = Context.childjobSubCategoris.Where(p => p.childJobSubCategoriName.Contains(description) || p.jobSubCategori.namee.Contains(description) || p.jobSubCategori.jobCategori.namee.Contains(description)).Select(p => new { ID = p.id, CategoriName = p.jobSubCategori.jobCategori.namee, SubCategori = p.jobSubCategori.namee, ChildSubCategori = p.childJobSubCategoriName, subCategoriId = p.jobSubCategori.id }).OrderByDescending(p => p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        //حذف زیردسته دوم
        public ActionResult JobChildSubCatDel(int id)
        {
            if (User.IsInRole("RemoveJobChildSubCategori") || User.IsInRole("SuperAdmin"))
            {
                var item = Context.childjobSubCategoris.Find(id);
                try
                {
                    Context.childjobSubCategoris.Remove(item);
                    Context.SaveChanges();

                }
                catch (DbUpdateException)
                {
                    return Json("fail");
                }

            }
            return RedirectToAction("JobChildSubCategori");

        }

        [HttpPost]
        public ActionResult RemoveAllRecordsForJobAdminActivity(ListTableTrModel selectedRole)
        {
            if (selectedRole.listRoles != null)
            {


                foreach (var item in selectedRole.listRoles)
                {
                    var findid = Context.jobAcceptUsers.Find(item.ID);
                    Context.jobAcceptUsers.Remove(findid);


                }
                Context.SaveChanges();
                return RedirectToAction("GetJobAdminInfo");

            }
            return RedirectToAction("GetJobAdminInfo");



        }





        //از اینجا به بعد مربوط به فاز سوم است
        //لوازم دسته دوم
        public ActionResult equipmentCategori()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddEquipmentCategori(equipmentCategori _equipmentCategori)
        {
            if (User.IsInRole("AddEquipmentCategori") || User.IsInRole("SuperAdmin"))
            {
                var _EuipmentCategoriName = _equipmentCategori.namee;
                var findEquipmentCategori = Context.equipmentCategoris.Any(p => p.namee == _EuipmentCategoriName);
                if (findEquipmentCategori)
                {
                    return Json("fail");

                }


                else
                {



                    if (_equipmentCategori.id == 0)
                        Context.equipmentCategoris.Add(_equipmentCategori);
                    else
                    {
                        Context.Entry(_equipmentCategori).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.SaveChanges();
                    return RedirectToAction("equipmentCategori");
                }
            }
            return RedirectToAction("equipmentCategori");


        }

        [HttpGet]
        public ActionResult GetEquipmentCategoriName(string description)
        {
            var list = Context.equipmentCategoris.Where(p=>p.namee.Contains(description)).Select(p => new { ID = p.id, Name = p.namee }).OrderByDescending(p => p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveEquipmentCategori(int id)
        {
            if (User.IsInRole("RemoveEquipmentCategori") || User.IsInRole("SuperAdmin"))
            {
                var cat = Context.equipmentCategoris.Find(id);
                try
                {
                    Context.equipmentCategoris.Remove(cat);
                    Context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Json("fail");
                }

                return RedirectToAction("equipmentCategori");
            }
            return RedirectToAction("equipmentCategori");

        }
        public ActionResult CheckEquipmentCategori(string Data)
        {
            var Cat = Context.equipmentCategoris.Any(p => p.namee == Data);
            return Json(Cat, JsonRequestBehavior.AllowGet);

        }


    public ActionResult equipmentSubCategori()
        {
            return View();
        }

        public ActionResult EquipmentSubCategoriRegister(equipmentSubCategori _equipmentSubCategori)
        {
            if (User.IsInRole("AddSubEquipmentCategori") || User.IsInRole("SuperAdmin"))
            {
                var _EuipmentSubCategoriName = _equipmentSubCategori.namee;
                var findEquipmentSubCategori = Context.equipmentSubCategoris.Any(p => p.namee == _EuipmentSubCategoriName);
                if (findEquipmentSubCategori)
                {

                    return Json("fail");

                }


                else
                {



                    if (_equipmentSubCategori.id == 0)
                        Context.equipmentSubCategoris.Add(_equipmentSubCategori);
                    else
                    {
                        Context.Entry(_equipmentSubCategori).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.SaveChanges();
                    return RedirectToAction("equipmentSubCategori");
                }
            }
            return RedirectToAction("equipmentSubCategori");

        }

        public ActionResult GetEquipmentSubCategori(string description)
        {
            var list = Context.equipmentSubCategoris.Where(p => p.namee.Contains(description) || p.equipmentCategori.namee.Contains(description)).Select(p => new { ID = p.id, NameEquipmentCategori = p.equipmentCategori.namee, Name = p.namee , p.equipmentCategoriId}).OrderByDescending(p => p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EquipmentSubCategoriRemove(int id)
        {
            if (User.IsInRole("RemovSubEquipmentCategori") || User.IsInRole("SuperAdmin"))
            {
                var item = Context.equipmentSubCategoris.Find(id);
                try
                {
                    Context.equipmentSubCategoris.Remove(item);
                    Context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Json("fail");
                }

                return RedirectToAction("equipmentSubCategori");
            }
            return RedirectToAction("equipmentSubCategori");

        }
        public ActionResult CheckEquipmentSubCategori(string Data)
        {
            var Cat = Context.equipmentSubCategoris.Any(p => p.namee == Data);
            return Json(Cat, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult listEquipments()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEquipments(string StatusCode, string description)
        {
            var ListEquipments = Context.equipments.Where(p => (p.title.Contains(description) || p.city.cityName.Contains(description))  && p.status.Contains(StatusCode)).Select(p => new { ID = p.id, StatusID = p.status, pexpired = p.pexpireDate, Title = p.title, City = p.city.cityName }).OrderByDescending(p => p.ID).ToList();

            return Json(ListEquipments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveEquipment(int id)
        {
            if (User.IsInRole("RemoveEquipment") || User.IsInRole("SuperAdmin"))
            {
                var Equipment = Context.equipments.Find(id);
                Context.equipments.Remove(Equipment);
                Context.SaveChanges();
            }
            return RedirectToAction("listEquipments");
        }
        [HttpPost]
        public ActionResult EquipmentStatusRegister(string statusvalue, int id,string adminComment)
        {

            if (User.IsInRole("RegisterEquipmentAuthorisation") || User.IsInRole("SuperAdmin"))
            {
                var Equipments = Context.equipments.Find(id);
                //melk.statusId = statusvalue;
                Equipments.status = statusvalue;
                Equipments.adminComment = adminComment;
                Context.SaveChanges();
            }
            return RedirectToAction("listEquipments");
        }

        [HttpPost]
        public ActionResult EquipmentUserActivity(int id, string Puser, string statusvalue)
        {
            equipmetAcceptUser PEquipmentAccreptUser = new equipmetAcceptUser();

            PEquipmentAccreptUser.equipmentId = id;
            PEquipmentAccreptUser.userName = Puser;
            PEquipmentAccreptUser.statusSelected = statusvalue;
            PEquipmentAccreptUser.registerDate = ExtensionFunction.ToPersian(DateTime.Now);
            Context.equipmetAcceptUsers.Add(PEquipmentAccreptUser);
            Context.SaveChanges();
            
            return RedirectToAction("listEquipments");
        }

        public  ActionResult ShowEquipmentInformation(int id)
        {
            ViewBag.IdEquipment = id;
            return View();

        }
        public ActionResult LstEquipmentActivityAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEquipmentAdminInfo(string StatusCode, string Description)
        {
            var ListAdmins = Context.equipmetAcceptUsers.Where(p => (p.statusSelected.Contains(StatusCode) || p.equipment.status.Contains(StatusCode)) && p.userName.Contains(Description)).Select(p => new { ID = p.id, Edate = p.registerDate, StatusSelected = p.statusSelected, StatusJob = p.equipment.status, Equipment = p.equipment.title, AdminName = p.userName }).OrderByDescending(p => p.ID).ToList();
            return Json(ListAdmins, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveEquipmentActivity(int id)
        {
            var t = Context.equipmetAcceptUsers.Find(id);
            Context.equipmetAcceptUsers.Remove(t);
            Context.SaveChanges();
            return RedirectToAction("LstEquipmentActivityAdmin");
        }

        //زیردسته دوم مصالح ساختمانی
        public ActionResult equipmentChildSubCategori()
        {
            return View();
        }
        //ثبت زیردسته دوم مصالح ساختمانی
        public ActionResult EquipmentChildSubCategoriRegister(childSubEquipmentCategori _childSubEquipmentCategori)
        {
            if (User.IsInRole("AddEquipentChildSubCategori") || User.IsInRole("SuperAdmin"))
            {

                var _EuipmentChildSubCategoriName = _childSubEquipmentCategori.childSubEquipmentCategoriName;
                var findEquipmenChildtSubCategori = Context.childSubEquipmentCategoris.Any(p => p.childSubEquipmentCategoriName == _EuipmentChildSubCategoriName);
                if (findEquipmenChildtSubCategori)
                {

                    return Json("fail");

                }
                else
                {
                    if (_childSubEquipmentCategori.id == 0)
                        Context.childSubEquipmentCategoris.Add(_childSubEquipmentCategori);
                    else
                    {
                        Context.Entry(_childSubEquipmentCategori).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.SaveChanges();
                    return RedirectToAction("equipmentChildSubCategori");

                }
            }
            return RedirectToAction("equipmentChildSubCategori");



        }
        public ActionResult CheckEquipmentChildSubCategori(string Data)
        {
            var item = Context.childSubEquipmentCategoris.Any(p => p.childSubEquipmentCategoriName == Data);
            return Json(item, JsonRequestBehavior.AllowGet);

        }
        //واکشی اطلاعات زیردسته دوم مصالح ساختمانی
        public ActionResult GetEquipmentChildSubCategori(string description)
        {
            var list = Context.childSubEquipmentCategoris.Where(p => p.childSubEquipmentCategoriName.Contains(description) || p.equipmentSubCategori.namee.Contains(description) || p.equipmentSubCategori.equipmentCategori.namee.Contains(description)).Select(p => new { ID = p.id, CategoriName = p.equipmentSubCategori.equipmentCategori.namee, SubCategori = p.equipmentSubCategori.namee, ChildSubCategori = p.childSubEquipmentCategoriName, subCategoriId = p.equipmentSubCategori.id }).OrderByDescending(p => p.ID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //حذف اطلاعات زیردسته بندی دوم مصالح ساختمانی
        public ActionResult EquipmentChildSubCatDel(int id)
        {
            if (User.IsInRole("RemoveEquipmentChildSubCategori") || User.IsInRole("SuperAdmin"))
            {
                var item = Context.childSubEquipmentCategoris.Find(id);
                try
                {
                    Context.childSubEquipmentCategoris.Remove(item);
                    Context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return Json("fail");

                }
                return RedirectToAction("equipmentChildSubCategori");

            }
            return RedirectToAction("equipmentChildSubCategori");

        }



        [HttpPost]
        public ActionResult RemoveAllRecordsForEquipmentAdminActivity(ListTableTrModel selectedRole)
        {
            if (selectedRole.listRoles != null)
            {


                foreach (var item in selectedRole.listRoles)
                {
                    var findid = Context.equipmetAcceptUsers.Find(item.ID);
                    Context.equipmetAcceptUsers.Remove(findid);


                }
                Context.SaveChanges();
            
                return RedirectToAction("LstEquipmentActivityAdmin");

            }
            return RedirectToAction("LstEquipmentActivityAdmin");



        }
        

        //مدیریت و نقش دهی مدیران
        public ActionResult AdminUser()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult AdminUserRegister(AdminUser _adminUser)
        {

            //var _UserName = _adminUser.userName;
            //var findAdminUser = Context.AdminUsers.Any(p => p.userName == _UserName);
            //if (findAdminUser)
            //{


            //    return RedirectToAction("AdminUser");

            //}
   

                _adminUser.apiToken = Guid.NewGuid().ToString().Replace('-', '0');
                if (_adminUser.id == 0)
                {

                    Context.AdminUsers.Add(_adminUser);
                }
                else
                {
                    Context.Entry(_adminUser).State = System.Data.Entity.EntityState.Modified;
                }
                Context.SaveChanges();
                return RedirectToAction("AdminUser");
            }
        

        //واکشی اطلاعات مدیران
        public ActionResult GetAdminUserInfo()
        {
            var list = Context.AdminUsers.Select(p => new { ID = p.id, fullName = p.fullName, Mobile = p.mobile, Telephone = p.telephone, UserName = p.userName, Password = p.password }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveAdminUser(int id)
        {
            var item = Context.AdminUsers.Find(id);
            Context.AdminUsers.Remove(item);
            Context.SaveChanges();
            return RedirectToAction("AdminUser");

        }

        [HttpPost]
        public ActionResult AdminUserRolesRegister(SelectedRoles selectedRoles)
        {
            var idUser = selectedRoles.AdminID;
            var FindedUserAdmin = Context.AdminUsers.FirstOrDefault(p => p.id == idUser);
            FindedUserAdmin.RollAdmins.Clear();
           foreach(var item in selectedRoles.listRoles)
            {
                var theid = item.ID;

                FindedUserAdmin.RollAdmins.Add(Context.RollAdmins.Find(theid));
            }
            Context.SaveChanges();
            return RedirectToAction("AdminUser");


        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }




    }

}