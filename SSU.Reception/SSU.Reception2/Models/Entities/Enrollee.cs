using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSU.Reception.Models
{
    public class Enrollee
    {
        #region Const
        public const int ScoreForMedal = 5;
        #endregion

        [ScaffoldColumn(false), Key]
        public int Id { get; set; }

        #region ФИО
        [Display(Name = "Фамилия"), Required]
        public string Surname { get; set; }

        [Display(Name = "Имя"), Required]
        public string Name { get; set; }

        [Display(Name = "Отчество"), Required]
        public string Patronymic { get; set; }
        #endregion

        #region Школа
        [Required]
        public int SchoolId { get; set; }

        [Display(Name = "Школа"), ForeignKey("SchoolId")]
        public School School { get; set; }
        #endregion

        [Display(Name = "Год сдачи ЕГЭ"), Required]
        public int ExamYear { get; set; }

        [Display(Name = "Телефон"), Phone, Required]
        public string PersonalPhone { get; set; }

        [Display(Name = "Доп. телефоны"), UIHint("MultilineText")]
        public string AdditionalPhones { get; set; }

        #region Баллы
        [Display(Name = "М"), Required, Range(0, 100), DefaultValue(0)]
        public int MathScore { get; set; }

        [Display(Name = "Р"), Required, Range(0, 100), DefaultValue(0)]
        public int RussianScore { get; set; }

        [Display(Name = "И"), Range(0, 100), DefaultValue(0)]
        public int? CSScore { get; set; }

        [Display(Name = "О"), Range(0, 100), DefaultValue(0)]
        public int? SSScore { get; set; }

        [Display(Name = "Медаль"), UIHint("Boolean")]
        public bool HasMedal { get; set; }

        [DefaultValue(0), Display(Name = "Олимп. 1 уровня")]
        public int FirstLevelOlympiad { get; set; }

        [DefaultValue(0), Display(Name = "Доп. олимп.")]
        public int OtherOlympiads { get; set; }

        /// <summary>
        /// Возвращает количество дополнительных баллов (Олимпиады + медаль)
        /// </summary>
        /// <returns></returns>
        public int ExtraPoints
        {
            get
            {
                var score = FirstLevelOlympiad + OtherOlympiads;
                if (HasMedal)
                {
                    score += ScoreForMedal;
                }
                return score;
            }
        }

        [Display(Name = "Сум. бал.")]
        public int TotalPoints
        {
            get
            {
                var score = MathScore + RussianScore;

                if (CSScore != null)
                {
                    score += CSScore.Value;
                }

                //if (SSScore != null)
                //{
                //    score += SSScore.Value;
                //}

                score += ExtraPoints;

                return score;
            }
        }

        #endregion

        [Display(Name = "Ст. пост.")]
        public ReceiptStatus ReceiptStatus { get; set; }

        [Display(Name = "Ст. ориг."), UIHint("Boolean")]
        public bool OriginalCertificate { get; set; }

        #region Приоритеты
        [Required]
        public int FirstPriorityId { get; set; }
        public int? SecondPriorityId { get; set; }
        public int? ThirdPriorityId { get; set; }

        [ForeignKey("FirstPriorityId"), Display(Name = "1-й пр")]
        public Direction FirstPriority { get; set; }

        [ForeignKey("SecondPriorityId"), Display(Name = "2-й пр")]
        public Direction SecondPriority { get; set; }

        [ForeignKey("ThirdPriorityId"), Display(Name = "3-й пр")]
        public Direction ThirdPriority { get; set; }

        [Display(Name = "Номер дела для первого приоритета"), Required, DefaultValue(0)]
        public int FirstPriorityNumber { get; set; }

        [Display(Name = "Номер дела для второго приоритета")]
        public int? SecondPriorityNumber { get; set; }

        [Display(Name = "Номер дела для третьего приоритета")]
        public int? ThirdPriorityNumber { get; set; }
        #endregion

        [Display(Name = "Ст. зачис."), UIHint("Boolean")]
        public bool EnrollmentStatus { get; set; }

        [Display(Name = "Ст. акт."), UIHint("Boolean"), DefaultValue(true)]
        public bool ActivityStatus { get; set; }

        [Display(Name = "История переходов"), UIHint("MultilineText")]
        public string ConversionHistory { get; set; }

        [Display(Name = "Комментарий"), UIHint("MultilineText")]
        public string Comment { get; set; }

        public string ReceiptStatusToString
        {
            get
            {
                switch (ReceiptStatus)
                {
                    case ReceiptStatus.NoEntranceExams:
                        return "БВИ";
                    case ReceiptStatus.Privilege:
                        return "Льгота";
                    case ReceiptStatus.Target:
                        return "Целевое";
                    case ReceiptStatus.GeneralCompetition:
                        return "ОК";
                }
                return "Не определено";
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
