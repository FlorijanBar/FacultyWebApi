using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class Student
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
        [Required(ErrorMessage = "StutyId can't be empty")]
        public int StudyId { get; set; }
        public Study Study { get; set; }
        public ICollection<StudentCollege> StudentCollege { get; set; }
    }
}
