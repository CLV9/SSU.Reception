using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        [Display(Name = "Приоритетный предмет")]
        [DefaultValue(PrioritySubject.ComputerScience)]
        public PrioritySubject PrioritySubject { get; set; }

        public IOrderedEnumerable<Enrollee> SortEnrolleesByPoints(IQueryable<Enrollee> enrollees)
        {
            var sorted = enrollees.OrderByDescending(GetEnrolleeExtraPoints);

            switch (PrioritySubject)
            {
                case PrioritySubject.ComputerScience:
                    sorted = sorted.OrderByDescending(x => x.CSScore);
                    break;
                case PrioritySubject.SocialStudies:
                    sorted = sorted.OrderByDescending(x => x.SSScore);
                    break;
                default:
                    break;
            }

            return sorted.OrderByDescending(x => x.MathScore)
                         .OrderByDescending(GetEnrolleeTotalPoints)
                         .OrderByDescending(x => (int)x.ReceiptStatus);
        }

        private static int GetEnrolleeExtraPoints(Enrollee x)
        {
            return x.ExtraPoints;
        }

        private static int GetEnrolleeTotalPoints(Enrollee x)
        {
            return x.TotalPoints;
        }

        public string PrioritySublectToString
        {
            get
            {
                switch (PrioritySubject)
                {
                    case PrioritySubject.ComputerScience:
                        return "Информатика";
                    case PrioritySubject.SocialStudies:
                        return "Обществознание";
                    default:
                        return "Не определено";
                }
            }
        }
    }

    public enum PrioritySubject
    {
        [Display(Name = "Информатика")] ComputerScience = 0,
        [Display(Name = "Обществознание")] SocialStudies = 1
    }
}