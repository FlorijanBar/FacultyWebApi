using FacultetApi.Data;
using FacultetApi.Models;
using FacultyWebApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCollegeController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] StudentCollege studentCollege)
        {
            var stcollege = db.StudentColleges.FirstOrDefault(c => c.Id == id);
            if (stcollege == null) return NotFound();
            db.Remove(stcollege);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var studentCollege = db.StudentColleges.FirstOrDefault(c => c.Id == id);
            if (studentCollege == null)
            {
                return NotFound();
            }
            return Ok(studentCollege);
        }

        [HttpGet("GetAllStudentColleges")]
        public IActionResult GetAllStudentColleges()
        {
            return Ok(db.StudentColleges);
        }

        [HttpGet("ExtensionGetStudentCollegeByStudentName")]
        public IActionResult ExtensionGetStudentCollegeByStudentName(string name)
        {
            var student = db.StudentColleges.GetStudentCollegeByStudentName(name);
            return Ok(student);
        }
        [HttpGet("ExtensionGetStudentCollegeByCollegeName")]
        public IActionResult GetStudentCollegeByCollegeName(string name)
        {
            var student = db.StudentColleges.GetStudentCollegeByCollegeName(name);
            return Ok(student);
        }

        [HttpGet("ExtensionGetStudentCollegeByCollegeEmployerName")]
        public IActionResult ExtensionGetStudentCollegeByCollegeEmployerName(string name)
        {
            var student = db.StudentColleges.GetStudentCollegeByCollegeEmployerName(name);
            return Ok(student);
        }

        [HttpGet("OrderByName")]
        public IActionResult OrderByName()
        {
            var county = db.Countys.OrderBy(c => c.Name);
            if (county.Count() == 0) return NotFound($"dont exist");
            return Ok(county);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StudentCollege studentCollege)
        {
            var result = db.StudentColleges.Add(studentCollege);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentCollege studentCollege)
        {
            var stcollege = db.StudentColleges.FirstOrDefault(c => c.Id == id);
            if (stcollege == null) return NotFound();
            stcollege.StudentId = studentCollege.StudentId;
            stcollege.CollegeId = studentCollege.CollegeId;
            db.SaveChanges();
            return Ok("Succesfuly updated!");
        }

   
    }
}
