using FacultetApi.Data;
using FacultetApi.Models;
using FacultyWebApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();



        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] College college)
        {
            var colleges = db.Colleges.FirstOrDefault(c => c.Id == id);
            if (colleges == null) return NotFound();
            db.Remove(colleges);
            db.SaveChanges();
            return Ok("succesfuly deleted!");
        }


        [HttpGet("ExtensionGetNameById")]
        public IActionResult ExtensionGetNameById(int id)
        {
            var college = db.Colleges.GetCollegeName(id);
            if (college == null || college.Count() == 0) return NotFound("dont exists");
            return Ok(college);

        }

        [HttpGet("ExtensionGetCollegeByEmployerName")]
        public IActionResult ExtensionGetCollegeByEmployerName(string name)
        {
            var college = db.Colleges.GetCollegeByEmployerName(name);
            return Ok(college);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var college = db.Colleges.FirstOrDefault(c => c.Id == id);
            if (college == null)
            {
                return NotFound();
            }
            return Ok(college);
        }

        [HttpGet("GetAllCollege")]
        public IActionResult GetAllCollege()
        {
            return Ok(db.Colleges);
        }

        [HttpGet("GetCollegeEmployersByCollegeId")]
        public IActionResult GetCollegeEmployersByCollegeId(int id)
        {
            var college = db.Colleges.Include(s => s.StudentCollege)
                    .Where(s => s.StudentCollege.Any(k => k.CollegeId == id))
                    .Select(k => new
                    {
                        CollegeName = k.Name,
                        CollegeId = k.Id,
                        EmployerName = k.Employer.Name,
                        EmployerSurname = k.Employer.Surname,
                        EmployerId = k.Employer.Id,
                        StudentCollegy = k.StudentCollege
                    });
            if (college.Count() == 0) return NotFound($"dont exist with this {id} id");
            return Ok(college);
        }

        [HttpGet("GetCollegeEmployersByEmployerId")]
        public IActionResult GetCollegeEmployersByEmployerId(int id)
        {
            var college = db.Colleges.Where(c => c.EmployerId == id).Select(c => new
            {
                CollegeName = c.Name,
                EmployerName = c.Employer.Name,
                EmployerSurname = c.Employer.Surname,
                EmployerId = c.Employer.Id
            });
            if (college.Count() == 0) return NotFound($"dont exist with this {id} id");
            return Ok(college);
        }

        [HttpGet("GetCollegeByName")]
        public IActionResult GetCollegeByName(string name)
        {
            var college = db.Colleges.Where(c => c.Name == name);
            if (college.Count() == 0) return NotFound($"dont exist with this {name} name");
            return Ok(college);

        }


        [HttpGet("OrderByName")]
        public IActionResult OrderByName()
        {
            var county = db.Countys.OrderBy(c => c.Name);
            if (county.Count() == 0) return NotFound($"dont exist");
            return Ok(county);
        }

        [HttpPost]
        public IActionResult Post([FromBody] College college)
        {
            var result = db.Colleges.Add(college);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] College college)
        {
            var colleges = db.Colleges.FirstOrDefault(c => c.Id == id);
            if (colleges == null) return NotFound();
            colleges.Name = college.Name;
            colleges.EmployerId = college.EmployerId;
            db.SaveChanges();
            return Ok("succesfuly updated!");
        }


        [HttpGet("SearchCollegeByName")]
        public IActionResult SearchCollegeByName(string name)
        {
            var college = db.Colleges.Where(c => c.Name.Contains(name));
            if (college.Count() == 0) return NotFound($"dont exist with this {name} name");
            return Ok(college);

        }


        [HttpGet("SearchCollegeByCollegeId")]
        public IActionResult SearchCollegeByCollegeId(int? id)
        {
            if (id.HasValue)
            {
                var college = db.Colleges.Where(c => c.Id == id);

                if (college.Count() == 0) return NotFound($"dont exist with this {id} name");
                return Ok(college);

            }

            return BadRequest("must type number value!");

        }





    }
    }
    

