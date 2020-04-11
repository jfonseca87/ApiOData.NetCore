using System.Linq;
using ApiODATACore.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiODATACore.Controllers
{
    public class PeopleController : ControllerBase
    {
        private readonly TestContext db;

        public PeopleController(TestContext db)
        {
            this.db = db;
        }

        [EnableQuery()]
        [HttpGet]
        public IActionResult GetPeople() => Ok(db.People.Include(x => x.Comments).ToList());

        public IActionResult Post([FromBody] People entity)
        {
            db.People.Add(entity);
            db.SaveChanges();

            return Created("", entity);
        }

        public IActionResult Put([FromODataUri] int key, [FromBody] People entity)
        {
            var person = db.People.FirstOrDefault(x => x.Id == key);

            if (person == null)
            {
                return NotFound();
            }

            person.Names = entity.Names;
            person.LastNames = entity.LastNames;
            person.Email = entity.Email;
            person.Age = entity.Age;
            person.Active = entity.Active;

            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();

            return Ok();
        }

        public IActionResult Delete([FromODataUri] int key)
        {
            var person = db.People.FirstOrDefault(x => x.Id == key);

            if (person == null)
            {
                return NotFound();
            }

            db.Entry(person).State = EntityState.Deleted;
            db.SaveChanges();

            return Ok();
        }

    }
}
