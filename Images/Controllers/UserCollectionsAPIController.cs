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
    public class UserCollectionsAPIController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/UserCollectionsAPI
        public IQueryable<UserCollection> GetUserCollections()
        {
            //Uri host = new Uri(Request.RequestUri.ToString());
            //string urlx = host.GetLeftPart(UriPartial.Authority);

            //var prod = db.UserCollections.ToList();
            //foreach (var item in prod)
            //{
            //    item.url = urlx + "/Content/images/" + item.url;
            //}
            return db.UserCollections;
        }

        // GET: api/UserCollectionsAPI/5
        [ResponseType(typeof(UserCollection))]
        public IHttpActionResult GetUserCollection(string username )
        {
            
            var userCollection = db.UserCollections.Where(u => u.user_email == username);
            if (userCollection == null)
            {
                return NotFound();
            }

            return Ok(userCollection);
        }

        // PUT: api/UserCollectionsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserCollection(int id, UserCollection userCollection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userCollection.Id_userimage)
            {
                return BadRequest();
            }

            db.Entry(userCollection).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCollectionExists(id))
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

        // POST: api/UserCollectionsAPI
        [ResponseType(typeof(UserCollection))]
        public IHttpActionResult PostUserCollection(UserCollection userCollection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userCollection.url = userCollection.url.ToString().Replace("http://10.0.2.2:62599/Content/images/", "");
            db.UserCollections.Add(userCollection);
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserCollectionExists(userCollection.Id_userimage))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userCollection.Id_userimage }, userCollection);
        }

        // DELETE: api/UserCollectionsAPI/5
        [ResponseType(typeof(UserCollection))]
        public IHttpActionResult DeleteUserCollection(int id)
        {
            UserCollection userCollection = db.UserCollections.Find(id);
            if (userCollection == null)
            {
                return NotFound();
            }

            db.UserCollections.Remove(userCollection);
            db.SaveChanges();

            return Ok(userCollection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserCollectionExists(int id)
        {
            return db.UserCollections.Count(e => e.Id_userimage == id) > 0;
        }
    }
}