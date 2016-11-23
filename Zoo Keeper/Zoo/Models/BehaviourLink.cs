using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Models
{
    public class BehaviourLink
    {
        public int ID { get; set; }
        [Required]

        //Foreign Key
        public int speciesID { get; set; }
        public int behaviorID { get; set; }
        // Navigation property 
        public Species species { get; set; }
        public Behaviours behaviour { get; set; }

    }
}