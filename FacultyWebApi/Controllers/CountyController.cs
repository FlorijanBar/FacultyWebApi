using FacultetApi.Data;
using FacultetApi.Models;
using FacultyWebApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : ControllerBase
    {
        FacultyDbContext db=new FacultyDbContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] County county)
        {
            var countys = db.Countys.FirstOrDefault(c => c.Id == id);
            if (countys == null) return NotFound();
            db.Remove(countys);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");

        }
        [HttpGet("ExtensionGetCountyNameById")]
        public IActionResult ExtensionGetCountyNameById(int id)
        {
            var county = db.Countys.GetCountyName(id);
            if (county == null || county.Count() == 0) return NotFound("dont exists");
            return Ok(county);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var county = db.Countys.FirstOrDefault(c => c.Id == id);
            if (county == null)
            {
                return NotFound();
            }
            return Ok(county);
        }

        [HttpGet("GetAllCounty")]
        public IActionResult GetAllCounty()
        {
            return Ok(db.Countys);
        }
        [HttpGet("GetCountyByName")]
        public IActionResult GetCountyByName(string name)
        {
            var county = db.Countys.Where(c => c.Name == name);
            if (county.Count() == 0) return NotFound($"dont exist with this {name} name");
            return Ok(county);

        }
        [HttpGet("GetCountyPlacesById")]
        public IActionResult GetCountyPlacesById(int? id)
        {
            if (id.HasValue)
            {
                var county = db.Countys.Include(c => c.Place)
                    .Where(c => c.Id == id).Select(c => new
                    {
                        CountyName = c.Name,
                        CountyId = c.Id,
                        Places = c.Place
                    });

                if (county.Count() == 0) return NotFound($"dont exist with this {id} id");
                return Ok(county);
            }
            return BadRequest();
        }
        [HttpGet("OrderByName")]
        public IActionResult OrderByName()
        {
            var county = db.Countys.OrderBy(c => c.Name);
            if (county.Count() == 0) return NotFound($"dont exist");
            return Ok(county);
        }

        [HttpPost]
        public IActionResult Post([FromBody] County county)
        {
            var result = db.Countys.Add(county);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] County county)
        {
            var countys=db.Countys.FirstOrDefault(c=>c.Id == id);
            if (countys == null)return NotFound();
            countys.Name = county.Name;
            db.SaveChanges();
            return Ok("Succesfuly updated!");

        }
   
        [HttpGet("SearchByName")]
        public IActionResult SearchByName(string name)
        {
            var county = db.Countys.Where(c => c.Name.Contains(name));
            if (county.Count() == 0) return NotFound($"dont exist with this {name} name");
            return Ok(county);
        }

      
    }
}
