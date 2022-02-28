using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Images.Controllers
{
    public class CategoryController : Controller
    {
        private Entities1 db = new Entities1();
        // GET: Category

        [HttpPost]
        public ViewResult search(string searchString)
        {
            /*   ViewBag.BrandSortParm = String.IsNullOrEmpty(sortOrder) ? "Brand_desc" : "";/*/

            var pic = from p in db.Pictures
                      select p;

            if (!String.IsNullOrEmpty(searchString))
            {

                pic = pic.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper())
                                   || p.Type.ToUpper().Contains(searchString.ToUpper()));
            }

            /*  switch (sortOrder)
              {
                  case "Brand_desc":
                      phone = phone.OrderByDescending(p => p.brand);
                      break;
                  default:
                      phone = phone.OrderBy(p => p.product_id);
                      break;
              }*/
            return View(pic);
        }
        public ActionResult Art()
        {
            return View(db.Pictures.ToList().Where(picture => picture.Type == "ศิลปะ"));
        }
        public ActionResult Nature()
        {
            return View(db.Pictures.ToList().Where(picture => picture.Type == "ธรรมชาติ"));
        }
        public ActionResult Animal()
        {
            return View(db.Pictures.ToList().Where(picture => picture.Type == "สัตว์"));
        }
        public ActionResult Science()
        {
            return View(db.Pictures.ToList().Where(picture => picture.Type == "วิทยาศาสตร์"));
        }
        public ActionResult Tech()
        {
            return View(db.Pictures.ToList().Where(picture => picture.Type == "เทคโนโลยี"));
        }
    }
}