using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class FacultyStudy
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FacultyId can't be empty")]
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "StudyId can't be empty")]
        public int StudyId { get; set; }
        public Faculty Faculty { get; set; }
        public Study Study { get; set; }
    }
}
