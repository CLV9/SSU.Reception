using System.ComponentModel.DataAnnotations;

namespace SSU.Reception.Models
{
    public class School
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

    }
}