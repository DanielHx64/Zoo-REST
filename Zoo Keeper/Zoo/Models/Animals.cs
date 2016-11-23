using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Models
{
    public class Animals
    {
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string DOB { get; set; }
        public string gendder { get; set; }

        //Foreign Key
        public int speciesID { get; set; }
        // Navigation property 
        public Species species { get; set; } 

    }
}