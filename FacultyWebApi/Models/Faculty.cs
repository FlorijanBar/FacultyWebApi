using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "PlaceId can't be empty")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        [Required(ErrorMessage = "FacultyName can't be empty")]
        public string Name { get; set; }
        public ICollection<Employer> Employer { get; set; }
        public ICollection<FacultyStudy> FacultyStudy { get; set; }
    }
}
