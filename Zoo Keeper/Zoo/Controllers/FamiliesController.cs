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


        //[Route("getFamily/{familyID}")] //tried and faild attempt of creating a new route
        [HttpGet]
        [ActionName("GetFamilyDetails")]
        //[ResponseType(typeof(Species))]
        public async Task<IHttpActionResult> GetFamilyDetails(int id)
        {

            //im keeping all these other ones becuase this is taking ages and i dont know which one is correct 

            //this works just not what i want
            //var species = db.Species.Where(s => s.familyID == id).ToList();

            //if (species == null)
            //{
            //    return NotFound();
            //}

            //return Ok(species);

            //lets get this started basically what im trying to do here is this 
            //Add an additional end-point that allows users to view all "birds" (- families) with an indication of their "family" (- species) whether they "can fly" (- behaviour).

            //var species = await db.BehaviourLinks
            //    .Include(b => b.behaviour)
            //    .Include(s => s.species)
            //    .Include(f => f.Family)
            //    .Select(b =>
            //    new BehaviourLink()
            //    {
            //        ID = b.ID,
            //        name = b.name,
            //        familyID = b.familyID,
            //        family = b.family
            //        Behaviour = b.behaviour
            //    }).SingleOrDefaultAsync(b => b.Id == id);


            //var query =
            //   from behaviorLink in db.BehaviourLinks
            //   join behaviour in db.Behaviours on behaviorLink.behaviorID equals behaviour.ID
            //   join species in db.Species on behaviorLink.speciesID equals species.ID
            //   join family in db.Families on species.familyID equals family.ID
            //   where family.ID == id
            //   select new Species {
            //       ID = species.ID,
            //       name = species.name,
            //       familyID = species.familyID,
            //       family = species.family
            //   };

            //var query2 = 
            //    db.BehaviourLinks    // your starting point - table in the "from" statement
            //   .Join(db.Behaviours, // the source table of the inner join
            //        behaviorLink => behaviorLink.behaviorID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
            //        behaviour => behaviour.ID,   // Select the foreign key (the second part of the "on" clause)
            //        (behaviorLink, behaviour) => new { BehaviourLink = behaviorLink, Behaviours = behaviour}) // selection
            //   .Join(db.Species,
            //        behaviorLink => behaviorLink.speciesID,
            //        species => species.ID,
            //        (behaviorLink, species) => new { BehaviourLink = behaviorLink, Species = species })
            //   .Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement


            //ok lets go throught this
            var query3 = //var which will stor the result of the query... i think
                db.BehaviourLinks.Join(db.Behaviours,//we are telling it to do a inner join between behaviourLinks and Behaviours
                   bh => bh.behaviorID, //bh is the local variable for BehaviourLinks table 
                   b => b.ID,  //here is the inner join, getting the two id's and matching them up
                   (bh, b) => new //this is the select so what do we want back form this innerjoin
                   {
                       BehaviourLink = bh, //we are returning BehavourLinks table we need to do another innerjoin on it
                       behaviours = b, //we want the behaviours
                       
                   })
             .Join(db.Species,  //do a inner join on the speces
                   bh => bh.BehaviourLink.speciesID,    //same ordeal grab the species name
                   s => s.ID,
                   (bh, s) => new
                   {
                       Species = s,     //we want to species here becuase we need to match it up to the family
                       behaviours = bh.behaviours

                   })
              .Join(db.Families,    //join on the families 
                   s => s.Species.familyID,
                   f => f.ID,
                   (s, f) => new
                   {
                       Species = s,
                       Family = f,
                       familyName = f.name,  //we just want the family name
                       SpeciesName = s.Species.name,    //also we want its name... supose this isint necessory since we are already returning all of the species 
                       behaviours = s.behaviours //so we jsut keep on passing this down
                   })
              .Join(db.Animals,
                    s => s.Species.Species.ID, // alright this is getting out of hand with the .Species im doing somthing really wrong here i can tell
                    a => a.ID,
                    (s, a) => new 
                    {
                        animals = a.name,
                        FamilyName = s.Family.name, 
                        SpeceisName = s.Species.Species.name,
                        BehaviourName = s.behaviours.name,
                        FamilyID = s.Family.ID //how do i get the families down here?
                    })
              .Where(f => f.FamilyID == id);    //so here we are with the where statment we are searching by familyID

            //alright that didnt work so well

            //so this is what i want to do
            //SELECT a.name, b.name, s.name
		        //FROM BehaviourLinks bh
                //INNER JOIN Behavour b ON bh.behaviourID = b.ID
                //INNER JOIN Species s ON bh.bspeciesID = s.ID
                //INNER JOIN Families f ON s.familyID = f.ID
                //INNER JOIN Animals a ON s.ID = a.speciesID
                //WHERE f.ID = id

            //actually it would be better to write it like this
            //SELECT a.name, b.name, s.name, f.name
                //FROM Animals a 
                //INNER JOIN Species s ON a.speceisID = s.ID
                //INNER JOIN Families f ON s.familyID = f.ID
                //INNER JOIN BehaviourLinks bh ON s.ID = bh.speciesID
                //INNER JOIN Behaviour ON bh.behaviourID = b.ID
                //WHERE f.ID = id
      
            if (query3 == null)
            {
                return NotFound();
            }

            return Ok(query3);
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