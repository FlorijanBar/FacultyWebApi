using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class Employer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname can't be empty")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Years can't be empty")]
        public int Years { get; set; }
        [Required(ErrorMessage = "PlaceId can't be empty")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public string Gender { get; set; }
       
        [Required(ErrorMessage = "FacultyId can't be empty")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public ICollection<College> College { get; set; }
    }
}
