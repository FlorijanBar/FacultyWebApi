using System.ComponentModel.DataAnnotations;

namespace FacultetApi.Models
{
    public class County
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
        public ICollection<Place> Place { get; set; }
    }
}
