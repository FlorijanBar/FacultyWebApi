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
    public class EmployerController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();


        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] Employer employer)
        {
            var employers = db.Employers.FirstOrDefault(c => c.Id == id);
            if (employers == null) return NotFound();
            db.Remove(employers);
            db.SaveChanges();
            return Ok("Succesfuly delleted!");
        }
        [HttpGet("ExtensionfacultyId")]
        public IActionResult ExtensionfacultyId(int facultyId)
        {
            var employer = db.Employers.GetEmployersByFacultyId(facultyId);
            if (employer == null || employer.Count == 0)
            {
                return NotFound();
            }
            return Ok(employer);
        }
        [HttpGet("ExtensionGetEmployereByPlaceName")]
        public IActionResult ExtensionGetEmployereByPlaceName(string name)
        {
            var employer = db.Employers.GetEmployereByFacultyName(name);
            return Ok(employer);
        }
        [HttpGet("ExtensionGetEmployereByFacultyName")]
        public IActionResult ExtensionGetEmployereByFacultyName(string name)
        {
            var employer = db.Employers.GetEmployereByFacultyName(name);
            return Ok(employer);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employer = db.Employers.FirstOrDefault(c => c.Id == id);
            if (employer == null)
            {
                return NotFound();
            }
            return Ok(employer);
        }

        [HttpGet("GetAllEmployer")]
        public IActionResult GetAllEmployer()
        {
            return Ok(db.Employers);
        }
        [HttpGet("GetAllEmployerInfo")]
        public IActionResult GetAllEmployers()
        {
            var together = from employer in db.Employers
                           join place in db.Places on employer.PlaceId equals place.Id
                           join faculty in db.Facultys on employer.FacultyId equals faculty.Id
                           select new
                           {
                               Name = employer.Name,
                               Surname = employer.Surname,
                               Years = employer.Years,
                               Gender = employer.Gender,
                               PlaceId = employer.PlaceId,
                               place = place.Name,
                               zip = place.ZipCode,
                               facultyId = employer.FacultyId,
                               faculty = faculty.Name,
                               //facultyPlace = faculty.Place,
                               //college = employer.College,
                               //facultyemployer = faculty.Employer

                           };
            return Ok(together);

        }
        [HttpGet("GetEmployerByCollege/{collegeId}")]
        public IActionResult GetEmployerByCollege(int? collegeId)
        {
            if (collegeId.HasValue)
            {
                var student = db.Employers.Include(s => s.College)
                .Where(c => c.College.Any(c => c.EmployerId == collegeId));
                if (student.Count() == 0) return NotFound($"dont exist with this{collegeId} collegeId");
                return Ok(student);
            }
            return BadRequest();
        }
        [HttpGet("GetEmployerByCollegeName/{collegeName}")]
        public IActionResult GetEmployerByCollegeName(string collegeName)
        {

            var student = db.Employers.Include(s => s.College)
                .Where(c => c.College.Any(c => c.Name == collegeName));
            if (student.Count() == 0) return NotFound($"dont exist with this{collegeName} collegename");
            return Ok(student);
        }

        [HttpGet("GetEmployersByFaculty/{facultyId}")]

        public IActionResult GetEmployersByFaculty(int? facultyId)
        {
            if (facultyId.HasValue)
            {
                var student = db.Employers.Where(s => s.FacultyId == facultyId);
                if (student.Count() == 0) return NotFound($"dont exist with this{facultyId} facultyid");
                return Ok(student);
            }
            return BadRequest();
        }

        [HttpGet("GetEmployersByFacultyName/{facultyName}")]

        public IActionResult GetEmployersByFaculty(string facultyName)
        {
            var student = db.Employers.Where(c => c.Faculty.Name == facultyName).Include(s => s.Faculty).Include(s => s.College);
            if (student.Count() == 0) return NotFound($"ne postoji s takvim{facultyName}");
            return Ok(student);
        }

        [HttpGet("GetEmployersByPlaceId/{placeId}")]

        public IActionResult GetEmployersByPlaceId(int? placeId)
        {
            if (placeId.HasValue)
            {
                var employer = db.Employers.Where(s => s.PlaceId == placeId).Select(c => new
                {
                    EmployerName = c.Name,
                    EmployerSurname = c.Surname,
                    EmployerYears = c.Years,
                    EmployerGender = c.Gender,
                    CollegeName = c.College.Select(c => c.Name),
                    FalcutyName = c.Faculty.Name,
                    PlaceName = c.Place.Name,
                    PlaceId = c.Place.Id,
                    PlaceZipCode = c.Place.ZipCode,

                });
                if (employer.Count() == 0) return NotFound($"dont exist with this{placeId} placeId");
                return Ok(employer);
            }
            return BadRequest();
        }
        [HttpGet("GetOneColleges")]
        public IActionResult GetOneColleges()
        {

            var StudentsCollegy = db.Employers
               .Join(db.Colleges,
                   s => s.Id,
                   u => u.EmployerId,
                (s, u) => s).Distinct();

            var query = from emplo in db.Employers
                        join coll in db.Colleges
                        on emplo.Id equals coll.EmployerId
                        select emplo;

            if (query.Count() == 0) return NotFound("nema nijednog djelatnika");
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
        public IActionResult Post([FromBody] Employer employer)
        {
            var result = db.Employers.Add(employer);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employer employer)
        {
            var employers=db.Employers.FirstOrDefault(c=>c.Id == id);
            if(employers==null)return NotFound();
            employers.Name = employer.Name;
            employers.Surname = employer.Surname;
            employers.Years = employers.Years;
            employers.Gender = employers.Gender;
            employers.PlaceId = employers.Id;
            employers.FacultyId = employers.FacultyId;
            db.SaveChanges ();
            return Ok("Succesfuly updated!");

        }

        
        [HttpGet("SearchSurname")]
        public IActionResult SearchBySurname(string surname)
        {
            var students = db.Employers.Where(x=>x.Surname.Contains(surname));
            if (students.Count() == 0) return NotFound($"not found with this{surname} surname");
            return Ok(students);
        }
        [HttpGet("SearchGender")]
        public IActionResult SearchGender(string gender)
        {
            var employer = db.Employers.Where(x => x.Gender.Contains(gender));
            if (employer.Count() == 0) return NotFound($"not found with this{gender} gender");
            return Ok(employer);
        }
        [HttpGet("SearchName")]
        public IActionResult SearchName(string name)
        {
            var employer = db.Employers.Where(x => x.Surname.Contains(name));
            if (employer.Count() == 0) return NotFound($"not found with this{name} surname");
            return Ok(employer);
        }

        [HttpGet("SearchYears")]
        public IActionResult SearchYears(int? years)
        {
            if (years.HasValue)
            {
                var employer = db.Employers.Where(c => c.Years == years.Value);
                if (employer.Count() == 0)
                { return NotFound($"employer don't exists {years} years"); }
                return Ok(employer);
            }
            return BadRequest("put number value!");

        }


       

    }
}
