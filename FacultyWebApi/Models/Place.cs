using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class Place
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ZipCode can't be empty")]
        public int ZipCode { get; set; }
        public ICollection<Student> Student { get; set; }
        public ICollection<Employer> Employer { get; set; }
        public ICollection<Faculty> Faculty { get; set;}

    }
}
