using DalilakProject.Database;
using DalilakProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DalilakProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Admin()
        {

            return View();
        }
        //public ActionResult Index(string cat, string area)
        //{
        //    var model = new LocationModel();

        //    model.Areas = Helpers.LoadAreas().Select(m => new SelectListItem() { Text = m.AreaName, Value = m.Id.ToString() }).ToList();
        //    using (var db = new DalilakDatabaseEntities())
        //    {
        //        if (!string.IsNullOrEmpty(cat))
        //        {
        //            var area_id = !string.IsNullOrEmpty(area) ? long.Parse(area) : 0;
        //            model.Locations = db.Locations.Where(m => m.Category.CategoryName == cat && (area_id == 0 || m.FK_AreaId == area_id)).ToList();

        //        }
        //        else
        //        {
        //            var first_category = db.Categories.Where(m => m.MenuOrder == 0).FirstOrDefault();
        //            model.Locations = db.Locations.Where(m => m.Category.Id == first_category.Id).ToList();
        //        }
        //    }
        //    model.current_category = cat;
        //    return View(model);
        //}

        public ActionResult cats(long areaId)
        {
            ViewBag.area = areaId;
            return View();
        }

        public ActionResult Search(string Keyword)
        {
            var category = "Restaurants";
            //LocationModel model = new LocationModel();
            string lower_search = Keyword.ToLower();
            using (var db = new DalilakDatabaseEntities1())
            {

                //if (category == "Restaurants")
                //{
                //    var locatoin_list = db.Restaurants.Where(m =>
                //        (string.IsNullOrEmpty(lower_search)) ||
                //        (m.LocationName.ToLower().Contains(lower_search))).ToList();
                //    model.Locations = locatoin_list;
                //}

                if (category == "Companies")
                {
                    var model = db.Companies.Where(m =>
                        (string.IsNullOrEmpty(lower_search)) ||
                        (m.CompanyName.ToLower().Contains(lower_search))).ToList();

                    return View("Company", model);
                }
                else if (category == "Drug Addiction Recovery")
                {
                    var model = db.DrugAddictionRecoveryVillas.Where(m =>
                        (string.IsNullOrEmpty(lower_search)) ||
                        (m.DrugAddictionRecoveryVillaName.ToLower().Contains(lower_search))).ToList();

                    return View("DrugAddictionRecoveryVilla", model);
                }
                else if (category == "Emergency Phone Number")
                {
                    var model = db.EmergencyPhoneNumbers.Where(m =>
                       (string.IsNullOrEmpty(lower_search)) ||
                       (m.EmergencyPhoneNumberName.ToLower().Contains(lower_search))).ToList();

                    return View("EmergencyPhoneNumber", model);
                }
                else if (category == "Football Curts")
                {
                    var model = db.FootballCourts.Where(m =>
                      (string.IsNullOrEmpty(lower_search)) ||
                      (m.FootballCourtName.ToLower().Contains(lower_search))).ToList();

                    return View("FootballCurt", model);
                }
                else if (category == "Gas Stations & Services ")
                {
                    var model = db.GasStations.Where(m =>
                      (string.IsNullOrEmpty(lower_search)) ||
                      (m.GasStationName.ToLower().Contains(lower_search))).ToList();

                    return View("GasStations", model);
                }
                else if (category == "Hospitals")
                {
                    var model = db.Hospitals.Where(m =>
                     (string.IsNullOrEmpty(lower_search)) ||
                     (m.HospitalName.ToLower().Contains(lower_search))).ToList();

                    return View("Hospitals", model);
                }
                else if (category == "Pharmacies")
                {
                    var model = db.Pharmacies.Where(m =>
                    (string.IsNullOrEmpty(lower_search)) ||
                    (m.PharmacyName.ToLower().Contains(lower_search))).ToList();

                    return View("Pharmacies", model);
                }
                else if (category == "Restaurants")
                {
                    var model = db.Restaurants.Where(m =>
                  (string.IsNullOrEmpty(lower_search)) ||
                  (m.RestaurantsName.ToLower().Contains(lower_search))).ToList();

                    return View("Restaurants", model);
                }




            }
            return View("loadlocations");
        }
        //public ActionResult LoadCategories(long areaId)
        //{

        //    return View();
        //}


        public ActionResult loadlocations(long catId, long areaId)
        {
            LocationModel model = new LocationModel();
            using (var db = new DalilakDatabaseEntities1())
            {
                var cat = db.Categories.Where(m => m.Id == catId).FirstOrDefault();
                if (cat.CategoryName == "Companies")
                {
                    return View("Company", db.Companies.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Drug Addiction Recovery")
                {
                    return View("DrugAddictionRecoveryVilla", db.DrugAddictionRecoveryVillas.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Emergency Phone Number")
                {
                    return View("EmergencyPhoneNumber", db.EmergencyPhoneNumbers.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Football Curts")
                {
                    return View("FootballCurt", db.FootballCourts.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Gas Stations & Services ")
                {
                    return View("GasStations", db.GasStations.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Hospitals")
                {
                    return View("Hospitals", db.Hospitals.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Pharmacies")
                {
                    return View("Pharmacies", db.Pharmacies.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                else if (cat.CategoryName == "Restaurants")
                {
                    return View("Restaurants", db.Restaurants.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList());
                }
                //model.Locations = db.Locations.Where(m => m.FK_Category == catId && m.FK_AreaId == areaId).ToList();
            }
            return View(model);
        }



        public ActionResult Restaurants(List<Restaurant> model)
        {
            return View(model);
        }


        public ActionResult Pharmacies(List<Pharmacy> model)
        {
            return View(model);
        }

        public ActionResult Hospitals(List<Hospital> model)
        {
            return View(model);
        }

        public ActionResult GasStations(List<GasStation> model)
        {
            return View(model);
        }

        public ActionResult FootballCurt(List<FootballCourt> model)
        {
            return View(model);
        }
        public ActionResult EmergencyPhoneNumber(List<EmergencyPhoneNumber> model)
        {
            return View(model);
        }
        public ActionResult DrugAddictionRecoveryVilla(List<DrugAddictionRecoveryVilla> model)
        {
            return View(model);
        }

        public ActionResult Company(List<Company> model)
        {
            return View(model);
        }

        public ActionResult Map(long loc)
        {
            LocationModel model = new LocationModel();
            using (var db = new DalilakDatabaseEntities1())
            {
                model.location = db.Locations.Where(m => m.Id == loc).FirstOrDefault();
            }

            return View(model);
        }

        public ActionResult AddLocation(LocationModel model)
        {
            using (var db = new DalilakDatabaseEntities1())
            {
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}