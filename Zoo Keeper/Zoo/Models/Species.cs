using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Models
{
    public class Species
    {
        public int ID { get; set; }
        [Required]
        public string name { get; set; }

        //Foreign Key
        public int familyID { get; set; }
        // Navigation property 
        public Family family{ get; set; }
    }
}