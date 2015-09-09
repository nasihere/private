using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DieChart.Models;
using DieChart.Entity;
using System.Web.Http.Cors;
namespace DieChart.Controllers
{
    public class ServerController : ApiController
    {
        private DataAccessLayer Adonet = new DataAccessLayer();
        // GET api/<controller>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<DieEntity> Get()
        {
            return Adonet.GetDieEntityADONET();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}