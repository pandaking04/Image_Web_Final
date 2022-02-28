using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Images.Controllers
{
    public class AdminController : Controller
    {
        private Entities1 db = new Entities1();
        // GET: Admin

        public ActionResult Managment()
        {
            if (User.Identity.Name == "admin@admin.com")
            {

                return View(db.AspNetUsers.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            if (AspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUsers);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            if (AspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUsers);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUser AspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(AspNetUsers);
            db.SaveChanges();
            return RedirectToAction("Managment");
        }

   

    }
}