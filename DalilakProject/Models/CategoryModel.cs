using DalilakProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DalilakProject.Models
{
    public class CategoryModel
    {
        public Category category { get; set; }
        public HttpPostedFileBase img { get; set; }
    }
}