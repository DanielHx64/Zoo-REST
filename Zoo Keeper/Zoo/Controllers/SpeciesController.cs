using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Zoo.Models;

namespace Zoo.Controllers
{
    [RoutePrefix("api/Species")]
    public class SpeciesController : ApiController
    {
        private ZooContext db = new ZooContext();

        // GET: api/Species
        [HttpGet]
        public IQueryable<Species> GetSpecies()
        {
            return db.Species;
        }

        // GET: api/Species/5
        [HttpGet]
        [ResponseType(typeof(Species))]
        public IHttpActionResult GetSpecies(int id)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }

            return Ok(species);
        }

        // PUT: api/Species/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpecies(int id, Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != species.ID)
            {
                return BadRequest();
            }

            db.Entry(species).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
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

        // POST: api/Species
        [HttpPost]
        [ResponseType(typeof(Species))]
        public IHttpActionResult PostSpecies(Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Species.Add(species);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = species.ID }, species);
        }

        // DELETE: api/Species/5
        [HttpDelete]
        [ResponseType(typeof(Species))]
        public IHttpActionResult DeleteSpecies(int id)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }

            db.Species.Remove(species);
            db.SaveChanges();

            return Ok(species);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpeciesExists(int id)
        {
            return db.Species.Count(e => e.ID == id) > 0;
        }
    }
}