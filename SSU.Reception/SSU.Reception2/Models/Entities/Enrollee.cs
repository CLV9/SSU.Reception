using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSU.Reception.Models
{
	public class Enrollee
	{
		[ScaffoldColumn(false)]
		[Key]
		public int Id { get; set; }

		[Display(Name = "Фамилия")]
		[Required]
		public string Surname { get; set; }

		[Display(Name = "Имя")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Отчество")]
		[Required]
		public string Patronymic { get; set; }

		[Required]
		public int SchoolId { get; set; }

		[Display(Name = "Школа")]
		[ForeignKey("SchoolId")]
		public School School { get; set; }

		[Display(Name = "Год сдачи ЕГЭ")]
		[Required]
		public int ExamYear { get; set; }

		[Phone]
		[Display(Name = "Личный телефон")]
		[Required]
		public string PersonalPhone { get; set; }

		[Display(Name = "Дополнительные номера телефонов")]
		[UIHint("MultilineText")]
		public string AdditionalPhones { get; set; }

		[Display(Name = "Математика")]
		[Required]
		[Range(0,100)]
		public int MathScore { get; set; }

		[Display(Name = "Русский язык")]
		[Required]
		[Range(0, 100)]
		public int RussianScore { get; set; }

		[Display(Name = "Информатика")]
		[Range(0, 100)]
		public int? CSScore { get; set; }

		[Display(Name = "Обществознание")]
		[Range(0, 100)]
		public int? SSScore { get; set; }

		[UIHint("Boolean")]
		[Display(Name = "Наличие медали")]
		public bool HasMedal { get; set; }

		[Display(Name = "Баллы за олимпиаду первого уровня")]
		[DefaultValue(0)]
		public int FirstLevelOlympiad { get; set; }

		[Display(Name = "Баллы за дополнительные олимпиады")]
		[DefaultValue(0)]
		public int OtherOlympiads { get; set; }

		[Display(Name = "Статус поступления")]
		public ReceiptStatus ReceiptStatus { get; set; }


		[Display(Name = "Сумма баллов")]
		[NotMapped]
		public int TotalPoints {
			get
			{
				var score = MathScore + RussianScore;
				if (CSScore != null)
					score += CSScore.Value;
				return score;
			}
		}

		[UIHint("Boolean")]
		[Display(Name = "Оригинал аттестата")]
		public bool OriginalCertificate { get; set; }

		[Required]
		public int FirstPriorityId { get; set; }
		public int? SecondPriorityId { get; set; }
		public int? ThirdPriorityId { get; set; }


		[Display(Name = "Первый приоритет")]
		[ForeignKey("FirstPriorityId")]
		public Direction FirstPriority { get; set; }

		[Display(Name = "Второй приоритет")]
		[ForeignKey("SecondPriorityId")]
		public Direction SecondPriority { get; set; }

		[Display(Name = "Третий приоритет")]
		[ForeignKey("ThirdPriorityId")]
		public Direction ThirdPriority { get; set; }

		[Display(Name = "Номер дела для первого приоритета")]
		[Required]
		public int FirstPriorityNumber { get; set; }

		[Display(Name = "Номер дела для второго приоритета")]
		public int? SecondPriorityNumber { get; set; }

		[Display(Name = "Номер дела для третьего приоритета")]
		public int? ThirdPriorityNumber { get; set; }

		[Display(Name = "Комментарий")]
		[UIHint("MultilineText")]
		public string Comment { get; set; }

		[Display(Name = "Статус зачисления")]
		[UIHint("Boolean")]
		public bool EnrollmentStatus { get; set; }

		[Display(Name = "Статус активности")]
		[UIHint("Boolean")]
		[DefaultValue(true)]
		public bool ActivityStatus { get; set; }

		[Display(Name = "История переходов")]
		[UIHint("MultilineText")]
		public string ConversionHistory { get; set; }

		[NotMapped]
		public string GetReceiptStatus
		{
			get
			{
				switch (ReceiptStatus) {
					case ReceiptStatus.NoEntranceExams:
						return "БВИ";
					case ReceiptStatus.Privilege:
						return "Льгота";
					case ReceiptStatus.Target:
						return "Целевое";
					case ReceiptStatus.GeneralCompetition:
						return "Общий конкурс";
				}
				return "Не определено";
			}
		}

		[NotMapped]
		public int ExtraPoints
        {
            get
            {
                return FirstLevelOlympiad + OtherOlympiads;
            }
        }
	}

	public enum ReceiptStatus
	{
		[Display(Name = "БВИ")]
		NoEntranceExams = 4,
		[Display(Name = "Льгота")]
		Privilege = 3,
		[Display(Name = "Целевое")]
		Target = 2,
		[Display(Name = "ОК")]
		GeneralCompetition = 1
	}
}
