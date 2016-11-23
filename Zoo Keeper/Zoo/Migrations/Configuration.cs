namespace Zoo.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Zoo.Models.ZooContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zoo.Models.ZooContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Families.AddOrUpdate(x => x.ID,
                new Family() { ID = 1, name = "Mammalia" },
                new Family() { ID = 2, name = "Aves" }
            );

            context.Species.AddOrUpdate(x => x.ID,
                new Species() { ID = 1, name = "Lion", familyID = 1},
                new Species() { ID = 2, name = "Elephant", familyID = 1},
                new Species() { ID = 3, name = "Ostrich", familyID = 2 },
                new Species() { ID = 4, name = "Parrot", familyID = 2 }
            );

            context.Animals.AddOrUpdate(x => x.ID,
                new Animals() { ID = 1, name = "George", DOB = "10/03/1995", gendder = "M", speciesID = 1 },
                new Animals() { ID = 2, name = "Jannet", DOB = "13/12/2001", gendder = "F", speciesID = 1 },

                new Animals() { ID = 3, name = "Allen", DOB = "08/06/2002", gendder = "M", speciesID = 2 },
                new Animals() { ID = 4, name = "Grace", DOB = "21/07/2003", gendder = "F", speciesID = 2 },

                new Animals() { ID = 5, name = "James", DOB = "27/02/2005", gendder = "M", speciesID = 3 },
                new Animals() { ID = 6, name = "Cathy", DOB = "12/11/2005", gendder = "F", speciesID = 3 },

                new Animals() { ID = 7, name = "Fred", DOB = "11/05/2015", gendder = "M", speciesID = 4 },
                new Animals() { ID = 8, name = "Velma", DOB = "03/08/2011", gendder = "F", speciesID = 4 }
            );

            context.Behaviours.AddOrUpdate(x => x.ID,
                new Behaviours() { ID = 1, name = "Predator" },
                new Behaviours() { ID = 2, name = "Fly" }
            );

            context.BehaviourLinks.AddOrUpdate(x => x.ID,
                new BehaviourLink() { ID = 1, speciesID = 1, behaviorID = 1 },
                new BehaviourLink() { ID = 1, speciesID = 4, behaviorID = 2 }
            );
        }
    }
}
