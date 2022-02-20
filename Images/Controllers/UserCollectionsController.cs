using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Images
{
    public class UserCollectionsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: UserCollections
        public ActionResult Index()
        {
            return View(db.UserCollections.ToList().Where(user => user.user_email == User.Identity.Name));
            //return View(db.UserCollections.ToList());
        }
        public ActionResult AddToMyLib(int idx, decimal? price, string url, string type, string name, string size, string desc)
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

            UserCollection user = new UserCollection();

            user.user_email = User.Identity.Name;
            //int maxid = db.UserCollections.Max(u => u.Id_userimage);
            //user.Id_userimage = maxid+1;
            user.Price = price;
            user.url = url;
            user.Type = type;
            user.Name = name;
            user.Size = size;
            user.Desc = desc;
            user.picid = idx;

            db.UserCollections.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(picture);
        }

            // GET: UserCollections/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollections.Find(id);
            if (userCollection == null)
            {
                return HttpNotFound();
            }
            return View(userCollection);
        }

        // GET: UserCollections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_userimage,user_email,Name,Size,Type,Desc,Price,url")] UserCollection userCollection)
        {
            if (ModelState.IsValid)
            {
                db.UserCollections.Add(userCollection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userCollection);
        }

        // GET: UserCollections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollections.Find(id);
            if (userCollection == null)
            {
                return HttpNotFound();
            }
            return View(userCollection);
        }

        // POST: UserCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_userimage,user_email,Name,Size,Type,Desc,Price,url")] UserCollection userCollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userCollection);
        }

        // GET: UserCollections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollections.Find(id);
            if (userCollection == null)
            {
                return HttpNotFound();
            }
            return View(userCollection);
        }

        // POST: UserCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCollection userCollection = db.UserCollections.Find(id);
            db.UserCollections.Remove(userCollection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
