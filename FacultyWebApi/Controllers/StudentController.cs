using FacultetApi.Data;
using FacultetApi.Models;
using FacultyWebApi.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml;

namespace FacultyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        FacultyDbContext db = new FacultyDbContext();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] Student student)
        {
            var students = db.Students.FirstOrDefault(c => c.Id == id);
            if (students == null) return NotFound();
            db.Remove(students);
            db.SaveChanges();
            return Ok("Succesfuly deleted!");
        }
        [HttpGet("DynamicValues")]

        public IActionResult DynamicValues(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }


            dynamic studentDynamic = new ExpandoObject();
            studentDynamic.Id = student.Id;
            studentDynamic.FirstName = student.Name;
            studentDynamic.LastName = student.Surname;
            studentDynamic.Years = student.Years;
            studentDynamic.Place = student.PlaceId;
            studentDynamic.Study = student.StudyId;

            db.Dispose();
            return Ok(studentDynamic);
        }

        [HttpGet("DynamicNoValues")]

        public IActionResult DynamicNoValues()
        {
            var assemblyName = typeof(Student).Assembly.GetName().Name;
            var studentType = typeof(Student);
            var properties = studentType.GetProperties();
            PropertyInfo nameProperty = studentType.GetProperty("Name");

            return Ok(assemblyName);
        }

        [HttpGet("ExtensionStudentNameById")]
        public IActionResult ExtensionStudentNameById(int id)
        {
            var student = db.Students.GetStudentName(id);
            return Ok(student);
        }

        [HttpGet("ExtensionStudentYearById")]
        public IActionResult ExtensionStudentYearById(int age)
        {
            var student = db.Students.GetStudentYear(age);
            return Ok(student);
        }

        [HttpGet("ExtensionstudyId")]
        public IActionResult ExtensionstudyId(int studyId)
        {
            var student = db.Students.GetStudentsByStudyId(studyId);
            if (student == null || student.Count == 0)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet("ExtensionCount")]
        public IActionResult ExtensionCount()
        {
            var student = db.Students.GetStudentsCount();

            return Ok(student);
        }
        [HttpGet("ExtensionGetStudyFaculty")]
        public IActionResult ExtensionGetStudyFaculty(int id)
        {
            var first = db.Students.FirstOrDefault(i => i.Id == id);
            if (first == null) return NotFound();
            var student = db.Students.GetStudentsByStudyId(id);
            return Ok(student);
        }


        [HttpGet("ExtensionGetStudentBystudyName")]
        public IActionResult ExtensionGetStudentBystudyName(string name)
        {
            var student = db.Students.GetStudentBystudyName(name);
            return Ok(student);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var result = db.Students.FirstOrDefault(x => x.Id == id);
            if (result == null) return NotFound();
            return Ok(result);

        }
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            return Ok(db.Students);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllTogether()
        {
            var together = from students in db.Students
                           join place in db.Places on students.PlaceId equals place.Id
                           join study in db.Studys on students.StudyId equals study.Id
                           select new
                           {
                               Name = students.Name,
                               Surname = students.Surname,
                               Years = students.Years,
                               PlaceId = students.PlaceId,
                               place = place.Name,
                               StudyId = students.StudyId,
                               study = study.Name
                           };

            return Ok(together);

        }

        [HttpGet("GetOneCollege")]
        public IActionResult GetOneCollege()
        {

            var StudentsCollegy = db.Students
               .Join(db.StudentColleges,
                   s => s.Id,
                   u => u.StudentId,
                (s, u) => s).Distinct();

            if (StudentsCollegy.Count() == 0) return NotFound("nema nijednog studenta");
            return Ok(StudentsCollegy);
        }

        [HttpGet("GetStudentByStudentCollege/{studentCollegeId}")]
        public IActionResult GetStudentByCollege(int? studentCollegeId)
        {
            if (studentCollegeId.HasValue)
            {
                var student = db.Students.Include(s => s.StudentCollege)
                    .Where(s => s.StudentCollege.Any(k => k.CollegeId == studentCollegeId));
                if (student.Count() == 0) return NotFound($"don't exists with this {studentCollegeId}studentCollegeId");
                return Ok(student);
            }

            return BadRequest("put number value");
        }

        [HttpGet("GetStudentiByPlace/{placeId}")]
        public IActionResult GetStudentiByPlace(int? placeId)
        {
            if (placeId.HasValue)
            {
                var student = db.Students.Where(s => s.PlaceId == placeId);
                if (student.Count() == 0) return NotFound($"don't exists with this {placeId}placeId");
                return Ok(student);
            }
            return BadRequest("put number value!");
        }
        [HttpGet("GetStudentiByStudij/{studyId}")]
        public IActionResult GetStudentiByStudij(int? studyId)
        {
            if (studyId.HasValue)
            {
                var student = db.Students.Where(s => s.StudyId == studyId);
                if (student.Count() == 0) return NotFound($"don't exists with this{studyId} studyId");
                return Ok(student);
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

        [HttpGet("OldestStudents")]
        public IActionResult OldestStudents()
        {
            var oldest = db.Students.Max(x => x.Years);
            var max = db.Students.Where(x => x.Years == oldest);
            return Ok(max);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            var result = db.Students.Add(student);
            db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            var students = db.Students.FirstOrDefault(c => c.Id == id);
            if (students == null) return NotFound();
            students.Name = student.Name;
            students.Surname = student.Surname;
            students.Years = student.Years;
            student.PlaceId = student.PlaceId;
            student.StudyId = student.StudyId;

            db.SaveChanges();
            return Ok("Succesfuly updated!");
        }
        [HttpGet("ReflectionValue")]
        public IActionResult GetReflection(int id, string propertyName)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            var property = typeof(Student).GetProperty(propertyName);
            if (property == null)
            {
                return BadRequest($"Property with name {propertyName} does not exist.");
            }

            var value = property.GetValue(student);
            return Ok(value);
        }

        [HttpGet("SearchName")]
        public IActionResult SearchByName(string name)
        {
            if (String.IsNullOrEmpty(name)) return BadRequest("must type name");
            var Name = db.Students.Where(x => x.Name.StartsWith(name));
            if (name == null) return NotFound();
            return Ok(Name);
        }
        [HttpGet("SearchSurname")]
        public IActionResult SearchBySurname(string surname)
        {
            if (String.IsNullOrEmpty(surname)) return BadRequest("must type surname");
            var Surname = db.Students.Where(x => x.Surname.Contains(surname));
            if (surname == null) return NotFound();
            return Ok(Surname);
        }

        [HttpGet("SearchStudents")]
        public IActionResult SearchByNameSurname(string name, string surname)
        {

            var students = db.Students.Where(x => x.Name.Contains(name) && x.Surname.Contains(surname));
            if (students.Count() == 0) return NotFound($"not found with this{name} name and {surname} surname");
            return Ok(students);
        }

        [HttpGet("SearchByYears")]
        public IActionResult SearchByYears(int? years)
        {
            if (years.HasValue)
            {
                var students = db.Students.Where(c => c.Years == years.Value);
                if (students.Count() == 0)
                { return NotFound($"student don't exists {years} years"); }
                return Ok(students);
            }
            return BadRequest("put number value!");

        }

        [HttpGet("YoungStudents")]
        public IActionResult YoungStudents()
        {
            var younger = db.Students.Min(x => x.Years);
            var min = db.Students.Where(x => x.Years == younger);
            return Ok(min);

        }


       
       

     

      

        //[HttpGet("ExtensionGetStudentByPlaceName")]
        //public IActionResult ExtensionGetStudentByPlaceName(string name)
        //{
        //    var student = db.Students.GetStudentByPlaceName(name);
        //    return Ok(student);
        //}


    }
   
 
}
