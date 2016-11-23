using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Models
{
    public class Behaviours
    {
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
    }
}