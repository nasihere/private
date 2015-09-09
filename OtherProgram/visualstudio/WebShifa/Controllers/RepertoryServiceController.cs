using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebsiteDatabase;
using WebsiteShifa.Models;

namespace WebsiteShifa.Controllers
{
    public class RepertoryServiceController : ApiController
    {
        private ShifaDBConnection db = new ShifaDBConnection();
        private RepertoryModel KentRep = new RepertoryModel();
        private int Limit = 50;
        private string Book = "Kent";
        // GET api/repertoryservice
        public IEnumerable<Repertory> Get()
        {
            
            KentRep.Repertory = db.Repertories.Where(x => x.Book.Contains(Book)).Where(x=>x.level.Contains("0")).OrderBy(x=>x.Name).Take(Limit).ToList();
            KentRep.fill();
            return KentRep.Repertory;
        }

        // GET api/repertoryservice/5
        /*public IEnumerable<Repertory> Get(int id)
        {
            
            if (id == 1) Book = "Boeric";
            KentRep.Repertory = db.Repertories.Where(x => x.Book.Contains(Book)).Take(Limit).ToList();
            KentRep.fill();
            return KentRep.Repertory;
        }
        */

        // POST api/repertoryservice
        public IEnumerable<Repertory> Post([FromBody]string SearchTerm)
        {
            if (SearchTerm == null) return Get();
            SearchTerm = SearchTerm.Replace('_', '|').Replace(", ", "|");
            KentRep.Repertory = db.Repertories.Where(x => x.title.Contains(SearchTerm)).OrderBy(x=>x.title).Take(Limit).ToList();
            KentRep.fill();
            return KentRep.Repertory;
        }
        public IEnumerable<Repertory> Rubric([FromBody]string SearchTerm)
        {
            if (SearchTerm == null) return Get();
            SearchTerm = SearchTerm.Replace('_', '|').Replace(", ", "|");
            SearchTerm = SearchTerm.TrimEnd('|');
            KentRep.Repertory = db.Repertories.Where(x => x.categoy.Equals(SearchTerm)).OrderBy(x => x.Name).Take(Limit).ToList();
            KentRep.fill();
            return KentRep.Repertory;
        }
        
        // PUT api/repertoryservice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/repertoryservice/5
        public void Delete(int id)
        {
        }
    }
}
