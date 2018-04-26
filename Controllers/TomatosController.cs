using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TomatosAPI.Db;
using TomatosAPI.Models;

namespace TomatosAPI.Controllers
{
    [Route("api/[controller]")]
    public class TomatosController : Controller
    {
        // GET api/tomatos
        [HttpGet]
        public IEnumerable<Tomato> Get()
        {
            using (TomatoDb db = new TomatoDb())
            {
                return db.Tomatos.ToArrayAsync().Result;
            }
        }

        // GET api/tomatos/:id
        [HttpGet("{id}")]
        public Tomato Get(int id)
        {
            using (TomatoDb db = new TomatoDb())
            {
                return db.Tomatos.Find(id);
            }
        }

        // POST api/tomatos
        [HttpPost]
        public void Post([FromBody]JObject value)
        {
            Tomato posted = value.ToObject<Tomato>();
            using (TomatoDb db = new TomatoDb())
            {
                db.Tomatos.Add(posted);
                db.SaveChanges();
            }
        }

        // PUT api/tomatos/:id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]JObject value)
        {
            Tomato posted = value.ToObject<Tomato>();
            posted.Id = id;
            using (TomatoDb db = new TomatoDb())
            {
                db.Tomatos.Update(posted);
                db.SaveChanges();
            }
        }

        // PATCH api/tomatos/:id
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody]JObject value)
        {
            Tomato posted = value.ToObject<Tomato>();
            posted.Id = id;
            using (TomatoDb db = new TomatoDb())
            {
                var tomato = db.Tomatos.Find(id);
                if (tomato == null)
                {
                    return; // 404
                }

                if (posted.Name != null)
                {
                    tomato.Name = posted.Name;
                }

                if (posted.OriginPostCode != null)
                {
                    tomato.OriginPostCode = posted.OriginPostCode;
                }

                if (posted.Tastes != null)
                {
                    tomato.Tastes = posted.Tastes;
                }

                db.Tomatos.Update(tomato);
                db.SaveChanges();
            }
        }

        // DELETE api/Tomatos/:id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (TomatoDb db = new TomatoDb())
            {
                var tomato = db.Tomatos.Find(id);
                if (tomato == null)
                {
                    return HttpNotFound(); // 404
                }

                db.Tomatos.Remove(tomato);
                db.SaveChanges();
            }
        }
    }
}
