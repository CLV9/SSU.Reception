using System.ComponentModel.DataAnnotations;

namespace SSU.Reception.Models
{
    public class Direction
    {
        [Key, ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required, Display(Name = "Название направления")]
        public string Name { get; set; }

        [Display(Name = "Количество бюджетных мест"), Range(0, 99999)]
        public int BudgetPlaces { get; set; }

        [Display(Name = "Количество льготных мест"), Range(0, 99999)]
        public int PrivilegedPlaces { get; set; }

        [Display(Name = "Количество целевых мест"), Range(0, 99999)]
        public int TargetPlaces { get; set; }

        [Display(Name = "Количество мест БВИ"), Range(0, 99999)]
        public int WithoutExamsPlaces { get; set; }

        [Display(Name = "Количество мест первой волны"), Range(0, 99999)]
        public int FirstWavePlaces { get; set; }

        [Display(Name = "Количество мест второй волны"), Range(0, 99999)]
        public int SecondWavePlaces { get; set; }
    }
}