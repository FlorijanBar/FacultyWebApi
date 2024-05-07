using FacultetApi.Data;
using FacultetApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] Study study)
        {
            var studys = db.Studys.FirstOrDefault(c => c.Id == id);
            if (studys == null) return NotFound();
            db.Remove(studys);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var study = db.Studys.FirstOrDefault(c => c.Id == id);
            if (study == null)
            {
                return NotFound();
            }
            return Ok(study);
        }

        [HttpGet("GetAllStudys")]
        public IActionResult GetAllStudys()
        {
            return Ok(db.Studys);
        }

        [HttpGet("GetAllGetAllTogetherStudys")]
        public IActionResult GetAllTogetherStudys()
        {

            var together = from study in db.Studys
                           join student in db.Studys on study equals student
                           select new { st = student.Name, stt = student.Student };

            return Ok(together);
        }

        [HttpGet("GetStudyStudent")]
        public IActionResult GetStudyStudent()
        {
            var query = from sty in db.Studys
                        join std in db.Students
                        on sty.Id equals std.StudyId
                        select sty;

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
        public IActionResult Post([FromBody] Study study)
        {
            var result = db.Studys.Add(study);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Study study)
        {
            var studys = db.Studys.FirstOrDefault(c => c.Id == id);
            if (studys == null) return NotFound();
            studys.Name = study.Name;
            
            db.SaveChanges();
            return Ok("Succesfuly updated!");
        }
        

       
        

    }
}
