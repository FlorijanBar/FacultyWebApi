using FacultetApi.Data;
using FacultetApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] Place place)
        {
            var places = db.Places.FirstOrDefault(c => c.Id == id);
            if (places == null) return NotFound();
            db.Remove(places);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var place = db.Places.FirstOrDefault(c => c.Id == id);
            if (place == null)
            {
                return NotFound();
            }
            return Ok(place);
        }

        [HttpGet("GetAllPlaces")]
        public IActionResult GetAllPlaces()
        {
            return Ok(db.Places);
        }
        [HttpGet("GetFacultyPlace")]
        public IActionResult GetFacultyPlace()
        {
            var query = from pla in db.Places
                        join fac in db.Facultys
                        on pla.Id equals fac.PlaceId
                        select pla;

            if (query.Count() == 0) return NotFound("place dont exist");
            return Ok(query);
        }
        [HttpGet("OrderByName")]
        public IActionResult OrderByName()
        {
            var county = db.Countys.OrderBy(c => c.Name);
            if (county.Count() == 0) return NotFound($"dont exist");
            return Ok(county);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Place place)
        {
            var result = db.Places.Add(place);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Place place)
        {
            var places = db.Places.FirstOrDefault(c => c.Id == id);
            if (places == null) return NotFound();
            places.Name = place.Name;
            places.ZipCode = place.ZipCode;
            db.SaveChanges();
            return Ok("Succesfuly updated!");
        }

        [HttpGet("ReflectionPlaceNoValues")]
        public IActionResult ReflectionPlaceGet()
        {
            var fakulteti = db.Places
                                    .OrderByDescending(f => f.Name)
                                    .Select(f => new { f.Name, f.ZipCode })
                                    .ToList();

            var properties = typeof(Place).GetProperties()
                                             .Where(p => !p.PropertyType.IsGenericType)
                                             .Select(p => p.Name);


            return Ok(properties);
        }

        [HttpGet("ReflectionPlaceValues")]
        public IActionResult ReflectionPlaceValues(int id)
        {
            var fakultet = db.Places.FirstOrDefault(f => f.Id == id);
            if (fakultet == null)
            {
                return NotFound();
            }

            var properties = typeof(Place).GetProperties();
            var result = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(fakultet, null));

            return Ok(result);
        }



    }
}
