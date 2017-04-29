using System;
using System.Collections.Generic;
using System.Linq;
using PhonebookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhonebookAPI.Controllers
{
    [Route("api/[controller]")]
    public class DefaultController : Controller
    {
        private readonly IRepository repo;
        public DefaultController(IRepository repo)
        {
            this.repo = repo;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Entry> GetAll()
        {
            return repo.GetAll();
        }
        // GET api/values/5
        [HttpGet("{id}", Name = "GetPhoneNumber")]
        public IActionResult GetById(long id)
        {
            var item = repo.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Entry entry)
        {
            if (entry == null)
                return BadRequest();
            repo.Add(entry);
            return CreatedAtRoute("GetPhoneNumber", new { id = entry.Key }, entry);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]Entry update)
        {
            if (update == null || update.Key != id)
                return BadRequest();
            var entry = repo.Find(id);
            if (entry == null)
                return NotFound();
            entry.PhoneNumber = update.PhoneNumber;
            entry.Name = update.Name;
            repo.Update(entry);
            return new NoContentResult();

        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entry = repo.Find(id);
            if (entry == null)
                return NotFound();
            repo.Remove(id);
            return new NoContentResult();
        }
    }
}
