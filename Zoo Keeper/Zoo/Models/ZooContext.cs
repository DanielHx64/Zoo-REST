﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zoo.Models
{
    public class ZooContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ZooContext() : base("name=ZooContext")
        {
        }

        public System.Data.Entity.DbSet<Zoo.Models.Species> Species { get; set; }

        public System.Data.Entity.DbSet<Zoo.Models.Animals> Animals { get; set; }

        public System.Data.Entity.DbSet<Zoo.Models.Family> Families { get; set; }

        public System.Data.Entity.DbSet<Zoo.Models.Behaviours> Behaviours { get; set; }

        public System.Data.Entity.DbSet<Zoo.Models.BehaviourLink> BehaviourLinks { get; set; }
    }
}
