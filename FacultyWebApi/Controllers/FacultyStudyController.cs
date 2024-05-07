using FacultetApi.Data;
using FacultetApi.Models;
using FacultyWebApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyStudyController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();
        public FacultyStudyController(FacultyDbContext _db) { db = _db; }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] FacultyStudy facultyStudy)
        {
            var facstudy = db.FacultyStudys.FirstOrDefault(c => c.Id == id);
            if (facstudy == null) return NotFound();
            db.Remove(facstudy);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");
        }
        [HttpGet("ExtensionGetFacultyStudyByStudyName")]
        public IActionResult GetFacultyStudyByStudyName(string name)
        {
            var faculty = db.FacultyStudys.GetFacultyStudyByStudyName(name);
            return Ok(faculty);
        }
        [HttpGet("ExtensionGetFacultyStudyByFacultyName")]
        public IActionResult GetFacultyStudyByFacultyName(string name)
        {
            var faculty = db.FacultyStudys.GetFacultyStudyByFacultyName(name);
            return Ok(faculty);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var facultyStudy = db.FacultyStudys.FirstOrDefault(c => c.Id == id);
            if (facultyStudy == null)
            {
                return NotFound();
            }
            return Ok(facultyStudy);
        }
        [HttpGet("GetAllFacultyStudy")]
        public IActionResult GetAllFacultyStudy()
        {
            return Ok(db.FacultyStudys);
        }

        [HttpGet("OrderByName")]
        public IActionResult OrderByName()
        {
            var county = db.Countys.OrderBy(c => c.Name);
            if (county.Count() == 0) return NotFound($"dont exist");
            return Ok(county);


        }
        [HttpPost]
        public IActionResult Post([FromBody] FacultyStudy facultyStudy)
        {
            var result = db.FacultyStudys.Add(facultyStudy);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FacultyStudy facultyStudy) 
        {

            var facstudy = db.FacultyStudys.FirstOrDefault(c => c.Id == id);
            if (facstudy == null) return NotFound();
            facstudy.FacultyId = facultyStudy.FacultyId;
            facstudy.StudyId = facultyStudy.StudyId;
            db.SaveChanges();
            return Ok("Succesfuly updated!");

        }

        //[HttpGet("ExtensionStudyId")]
        //public IActionResult ExtensionStudyId(int id)
        //{
        //    var study=db.FacultyStudys.FirstOrDefault(i=>i.Id == id);
        //    if(study == null) return NotFound("Dont exists");
        //   var result = db.FacultyStudys.GetByStudyId(id);
        //    return Ok(result);
        //}

        //[HttpGet("ExtensionFacultyId")]
        //public IActionResult ExtensionFacultyId(int id)
        //{
        //    var facultys = db.FacultyStudys.FirstOrDefault(i => i.Id == id);
        //    if (facultys == null) return NotFound("dont exist");
        //    var result=db.FacultyStudys.GetByFacultyId(id).ToList();
        //    return Ok(result);
        //}
        //[HttpGet("ExtensionGetById")]
        //public IActionResult ExtensionGetById(int id)
        //{
        //    var first = db.FacultyStudys.FirstOrDefault(i => i.Id == id);
        //    if (first == null) return NotFound();
        //    var result = db.FacultyStudys.GetById(id);
        //    return Ok(result);
        //}



    }
}
