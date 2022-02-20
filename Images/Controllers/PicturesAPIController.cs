using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Images;

namespace Images.Controllers
{
    public class PicturesAPIController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/PicturesAPI
        public IQueryable<Picture> GetPictures()
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string urlx = host.GetLeftPart(UriPartial.Authority);

            var prod = db.Pictures.ToList();
            foreach (var item in prod)
            {
                item.url = urlx + "/Content/images/" + item.url;
            }
            return db.Pictures;
        }

        // GET: api/PicturesAPI/5
        [ResponseType(typeof(Picture))]
        public IHttpActionResult GetPicture(int id)
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string urlx = host.GetLeftPart(UriPartial.Authority);

            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }
            picture.url = urlx + "/Content/images/" + picture.url;

            return Ok(picture);
        }

        // PUT: api/PicturesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPicture(int id, Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != picture.Pic_ID)
            {
                return BadRequest();
            }

            db.Entry(picture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PicturesAPI
        [ResponseType(typeof(Picture))]
        public IHttpActionResult PostPicture(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pictures.Add(picture);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = picture.Pic_ID }, picture);
        }

        // DELETE: api/PicturesAPI/5
        [ResponseType(typeof(Picture))]
        public IHttpActionResult DeletePicture(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }

            db.Pictures.Remove(picture);
            db.SaveChanges();

            return Ok(picture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PictureExists(int id)
        {
            return db.Pictures.Count(e => e.Pic_ID == id) > 0;
        }
    }
}