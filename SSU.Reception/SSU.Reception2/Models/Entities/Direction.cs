using System.ComponentModel.DataAnnotations;

namespace SSU.Reception.Models
{
	public class Direction
	{

		[ScaffoldColumn(false)]
		[Key]
		public int Id { get; set; }

		[Display(Name = "Название направления")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Количество бюджетных мест")]
		[Range(0, 99999)]
		public int BudgetPlaces { get; set; }
	}
}