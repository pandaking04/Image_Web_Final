using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace Images.Controllers
{
    public class HomeController : Controller
    {
        private Entities1 db = new Entities1();
        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }

        [HttpPost]
        public ViewResult search(string searchString)
        {
            /*   ViewBag.BrandSortParm = String.IsNullOrEmpty(sortOrder) ? "Brand_desc" : "";/*/

               var pic = from p in db.Pictures
                           select p;

               if (!String.IsNullOrEmpty(searchString))
               {

                   pic = pic.Where(p =>  p.Name.ToUpper().Contains(searchString.ToUpper())
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
        public ActionResult Details(int? id, string email)
        {


            var username = (from x in db.AspNetUsers where x.Email == email select x).FirstOrDefault();

            string name;
            string pic;



            name = username.FirstName.ToString();
            pic = username.LastName.ToString();


            ViewBag.unam = name;
            ViewBag.pic = pic;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);


        }
        public ActionResult QRCODE(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Picture picture = db.Pictures.Find(id);
                if (picture == null)
                {
                    return HttpNotFound();
                }
                return View(picture);
            }
            else
            {
                return RedirectToAction("LoginT", "Account");
            }
        }


        public ActionResult AddOrder(int? idx, decimal? total)
        {
            if (idx == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(idx);
            if (picture == null)
            {
                return HttpNotFound();
            }

            OrderPic order = new OrderPic();
            order.User_email = User.Identity.Name;
            order.Pic_ID = idx;
            order.total = total;
            db.OrderPics.Add(order);
            db.SaveChanges();


            return RedirectToAction("Index");
            //return View(picture);
        }




    }
}