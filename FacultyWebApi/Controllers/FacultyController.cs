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
    public class FacultyController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();

        [HttpDelete]
        public IActionResult Delete(int id, [FromBody] Faculty faculty)
        {
            var facultys = db.Facultys.FirstOrDefault(c => c.Id == id);
            if (facultys == null) return NotFound();
            db.Remove(facultys);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");
        }

        [HttpGet("ExtensionBycity")]
        public IActionResult ExtensionBycity(string grad)
        {
            var faculties = db.Facultys.GetFakultetiByGrad(grad);

            if (faculties == null)
            {
                return NotFound("dont exist!");
            }

            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var faculty = db.Facultys.FirstOrDefault(c => c.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }
            return Ok(faculty);
        }
        [HttpGet("GetAllFaculty")]
        public IActionResult GetAllFaculty()
        {
            return Ok(db.Facultys);
        }
        [HttpGet("GetAllFacultyEmployers/{employerId}")]
        public IActionResult GetAllFacultyEmployers(int? employerId)
        {
            if (employerId.HasValue)
            {
                var faculty = db.Facultys.Include(f => f.Employer)
                    .Where(e => e.Employer.All(e => e.FacultyId == employerId));
                if (faculty.Count() == 0) return NotFound($"don't exists employer with this{employerId} employerid");
                return Ok(faculty);

            }
            return BadRequest("put number value!");
        }
       
        [HttpGet("GetAllFacultyStudys/{facultyId}")]
        public IActionResult GetAllFacultyStudys(int? facultyId)
        {
            if (facultyId.HasValue)
            {
                var result = db.Facultys.Include(s => s.FacultyStudy)
                .Where(k => k.FacultyStudy.Any(k => k.FacultyId == facultyId));
                if (result.Count() == 0) return NotFound($"don't exists with this {facultyId} facultyId");
                return Ok(result);
            }
            return BadRequest("put number value!");

        }
        [HttpGet("GetAllFacultyStudyss/{facultyId}")]
        public IActionResult GetAllFacultyStudyss(int? facultyId)
        {
            if (facultyId.HasValue)
            {
                var faculty = db.Facultys.Include(f => f.FacultyStudy)
                    .Where(e => e.FacultyStudy.Any(e => e.FacultyId == facultyId)).Select(c => new
                    {
                        FacultyName = c.Name,
                        FacultyId = c.Id,
                        FacultyPlace = c.Place.Name,
                        StudyId = c.FacultyStudy.Select(c => c.StudyId),
                        StudyName = c.FacultyStudy.Select(c => c.Study.Name),
                        EmployerName = c.Employer.Select(c => c.Name),

                    });
                if (faculty.Count() == 0) return NotFound($"don't exists faculstystudy with this{facultyId} facultyid");
                return Ok(faculty);

            }
            return BadRequest("put number value!");
        }

        [HttpGet("GetAllFacultyPlaces/{placeId}")]
        public IActionResult GetAllFacultyPlaces(int? placeId)
        {
            if (placeId.HasValue)
            {
                var faculty = db.Facultys.Include(f => f.Employer)
                    .Where(e => e.Employer.Any(e => e.PlaceId == placeId));
                if (faculty.Count() == 0) return NotFound($"don't exists employer with this{placeId} employerid");
                return Ok(faculty);

            }
            return BadRequest("put number value!");
        }
        [HttpGet("OrderByName")]
        public IActionResult OrderByName()
        {
            var county = db.Countys.OrderBy(c => c.Name);
            if (county.Count() == 0) return NotFound($"dont exist");
            return Ok(county);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Faculty faculty)
        {
            var result = db.Facultys.Add(faculty);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut]
        public IActionResult Put(int id, [FromBody] Faculty faculty)
        {var facultys=db.Facultys.FirstOrDefault(c=>c.Id == id);
            if(facultys == null)return NotFound();
            facultys.Name = faculty.Name;
            facultys.PlaceId = faculty.PlaceId;
            db.SaveChanges();
            return Ok("Succesfuly updated!");
        
        }
        [HttpGet("Action")]
        public IActionResult Paging(int pageNumber, int pageSize)
        {
            var all = db.Students;
            return Ok(db.Students.Skip((pageNumber-1) * pageSize).Take(pageSize));

        }
        //[HttpGet("ExtensionByName")]
        //public IActionResult ExtensionByName(string name)
        //{
        //    var faculties = db.Facultys.GetByName(name);

        //    if (faculties == null)
        //    {
        //        return NotFound("dont exist!");
        //    }

        //    return Ok(faculties);
        //}


        //[HttpGet("ExtensionQuery")]
        //public IActionResult ExtensionQuery()
        //{
        //    var faculties = db.Facultys.GetStudyFaculty();
            

        //    if (faculties == null)
        //    {
        //        return NotFound("dont exist!");
        //    }
            
        //    return Ok(faculties);
            
        //}








    }
}
