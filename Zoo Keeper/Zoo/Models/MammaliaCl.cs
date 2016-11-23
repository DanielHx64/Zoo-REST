using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Models 
{
    public class MammaliaCl : AnimalsCl
    {
        private bool predator;
        private AnimalsCl animal;

        public MammaliaCl(bool isPredator, int nID, String nName, String nDOB) : base(nID, nName, nDOB)
        {
            predator = isPredator;
        }
    }
}