using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class StudentCollege
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "StudentId can't be empty")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "CollegeId can't be empty")]
        public int CollegeId { get; set; }
        public Student Student { get; set; }
        public College College { get; set; }
    }
}
