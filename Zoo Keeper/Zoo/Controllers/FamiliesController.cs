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
using Zoo.Models;

namespace Zoo.Controllers
{
    public class FamiliesController : ApiController
    {
        private ZooContext db = new ZooContext();

        // GET: api/Families
        public IQueryable<Family> GetFamilies()
        {
            return db.Families;
        }

        // GET: api/Families/5
        [ResponseType(typeof(Family))]
        public IHttpActionResult GetFamily(int id)
        {
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return NotFound();
            }

            return Ok(family);
        }

        // PUT: api/Families/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFamily(int id, Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != family.ID)
            {
                return BadRequest();
            }

            db.Entry(family).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(id))
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

        // POST: api/Families
        [ResponseType(typeof(Family))]
        public IHttpActionResult PostFamily(Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Families.Add(family);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = family.ID }, family);
        }

        // DELETE: api/Families/5
        [ResponseType(typeof(Family))]
        public IHttpActionResult DeleteFamily(int id)
        {
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return NotFound();
            }

            db.Families.Remove(family);
            db.SaveChanges();

            return Ok(family);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FamilyExists(int id)
        {
            return db.Families.Count(e => e.ID == id) > 0;
        }
    }
}