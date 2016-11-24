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
    public class BehavioursController : ApiController
    {
        private ZooContext db = new ZooContext();

        // GET: api/Behaviours
        [HttpGet]
        public IQueryable<Behaviours> GetBehaviours()
        {
            return db.Behaviours;
        }

        // GET: api/Behaviours/5
        [HttpGet]
        [ResponseType(typeof(Behaviours))]
        public IHttpActionResult GetBehaviours(int id)
        {
            Behaviours behaviours = db.Behaviours.Find(id);
            if (behaviours == null)
            {
                return NotFound();
            }

            return Ok(behaviours);
        }

        // PUT: api/Behaviours/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBehaviours(int id, Behaviours behaviours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != behaviours.ID)
            {
                return BadRequest();
            }

            db.Entry(behaviours).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BehavioursExists(id))
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

        // POST: api/Behaviours
        [HttpPost]
        [ResponseType(typeof(Behaviours))]
        public IHttpActionResult PostBehaviours(Behaviours behaviours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Behaviours.Add(behaviours);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = behaviours.ID }, behaviours);
        }

        // DELETE: api/Behaviours/5
        [HttpDelete]
        [ResponseType(typeof(Behaviours))]
        public IHttpActionResult DeleteBehaviours(int id)
        {
            Behaviours behaviours = db.Behaviours.Find(id);
            if (behaviours == null)
            {
                return NotFound();
            }

            db.Behaviours.Remove(behaviours);
            db.SaveChanges();

            return Ok(behaviours);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BehavioursExists(int id)
        {
            return db.Behaviours.Count(e => e.ID == id) > 0;
        }
    }
}