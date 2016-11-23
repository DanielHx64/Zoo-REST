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
    public class BehaviourLinksController : ApiController
    {
        private ZooContext db = new ZooContext();

        // GET: api/BehaviourLinks
        public IQueryable<BehaviourLink> GetBehaviourLinks()
        {
            return db.BehaviourLinks;
        }

        // GET: api/BehaviourLinks/5
        [ResponseType(typeof(BehaviourLink))]
        public IHttpActionResult GetBehaviourLink(int id)
        {
            BehaviourLink behaviourLink = db.BehaviourLinks.Find(id);
            if (behaviourLink == null)
            {
                return NotFound();
            }

            return Ok(behaviourLink);
        }

        // PUT: api/BehaviourLinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBehaviourLink(int id, BehaviourLink behaviourLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != behaviourLink.ID)
            {
                return BadRequest();
            }

            db.Entry(behaviourLink).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BehaviourLinkExists(id))
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

        // POST: api/BehaviourLinks
        [ResponseType(typeof(BehaviourLink))]
        public IHttpActionResult PostBehaviourLink(BehaviourLink behaviourLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BehaviourLinks.Add(behaviourLink);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = behaviourLink.ID }, behaviourLink);
        }

        // DELETE: api/BehaviourLinks/5
        [ResponseType(typeof(BehaviourLink))]
        public IHttpActionResult DeleteBehaviourLink(int id)
        {
            BehaviourLink behaviourLink = db.BehaviourLinks.Find(id);
            if (behaviourLink == null)
            {
                return NotFound();
            }

            db.BehaviourLinks.Remove(behaviourLink);
            db.SaveChanges();

            return Ok(behaviourLink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BehaviourLinkExists(int id)
        {
            return db.BehaviourLinks.Count(e => e.ID == id) > 0;
        }
    }
}