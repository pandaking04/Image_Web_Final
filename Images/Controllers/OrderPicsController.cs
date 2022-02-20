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
    public class OrderPicsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: OrderPics
        public ActionResult Index()
        {
            return View(db.OrderPics.ToList().Where(orders => orders.User_email == User.Identity.Name));
        }

        [HttpPost]
        public ActionResult AddOrder(FormCollection fc)
        {
            Picture product = new Picture();
            int Pic_ID = int.Parse(fc["Pic_ID"]);
            
            product = db.Pictures.SingleOrDefault(m => m.Pic_ID == Pic_ID);

            OrderPic my_order = new OrderPic();
            my_order.User_email = User.Identity.Name;
            my_order.Pic_ID = Pic_ID;
            my_order.Name = product.Name;
            my_order.total = product.Price;


            db.OrderPics.Add(my_order);
            db.SaveChanges();

            return View("Index", db.OrderPics.ToList());
        }

        // GET: OrderPics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPic orderPic = db.OrderPics.Find(id);
            if (orderPic == null)
            {
                return HttpNotFound();
            }
            return View(orderPic);
        }

        // GET: OrderPics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderPics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Pic,User_email,Pic_ID,total")] OrderPic orderPic)
        {
            if (ModelState.IsValid)
            {
                db.OrderPics.Add(orderPic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderPic);
        }

        // GET: OrderPics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPic orderPic = db.OrderPics.Find(id);
            if (orderPic == null)
            {
                return HttpNotFound();
            }
            return View(orderPic);
        }

        // POST: OrderPics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Pic,User_email,Pic_ID,total")] OrderPic orderPic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderPic);
        }

        // GET: OrderPics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPic orderPic = db.OrderPics.Find(id);
            if (orderPic == null)
            {
                return HttpNotFound();
            }
            return View(orderPic);
        }

        // POST: OrderPics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPic orderPic = db.OrderPics.Find(id);
            db.OrderPics.Remove(orderPic);
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
