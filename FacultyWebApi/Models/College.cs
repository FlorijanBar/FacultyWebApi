using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class College
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "EmployerId can't be empty")]
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public ICollection<StudentCollege> StudentCollege { get; set; }
    }
}
