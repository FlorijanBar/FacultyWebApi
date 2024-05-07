using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class Study
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
        public ICollection<FacultyStudy>FacultyStudy { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
