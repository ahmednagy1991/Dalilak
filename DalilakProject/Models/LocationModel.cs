using DalilakProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DalilakProject.Models
{
    public class LocationModel
    {
        public Location location { get; set; }
        public Category category { get; set; }
        public List<Location> Locations { get; set; }
        public List<SelectListItem> Areas { get; set; }
        public string current_category { get; set; }
        public HttpPostedFileBase img { get; set; }
    }
}