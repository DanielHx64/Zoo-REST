using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Zoo.Controller
{
    public class TestController : ApiController
    {
        Test[] Tests = new Test[]
        {
            new Test { ID = 1, Name = "Tomato Soup", Stuff = "Groceries"},
            new Test { ID = 2, Name = "Yo-yo", Stuff = "Toys"},
            new Test { ID = 3, Name = "Hammer", Stuff = "Hardware"}
        };


        public IEnumerable<Test> GetAllTests()
        {
            return Tests;
        }


        public IHttpActionResult GetTest(int id)
        {
            var test = Tests.FirstOrDefault((p) => p.ID == id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }
    }
}

