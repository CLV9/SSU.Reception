using System.Data.Entity;
using System.Linq;

namespace SSU.Reception.Models
{
	public class EnrolleeContext : DbContext
	{
		public EnrolleeContext(): base("DbConnection")
		{
			
		}

		public DbSet<Enrollee> Enrolles { get; set; }

        /// <summary>
        /// Получает список всех абитуриентов и фильтрует их по параметрам
        /// </summary>
        /// <param name="originalCertificateOnly">Только абитуриенты с оригиналом аттестата</param>
        /// <param name="search">Поиск по ФИО</param>
        /// <param name="activeOnly">Только активные абитуриенты</param>
        /// <returns></returns>
		public IQueryable<Enrollee> GetFilterEnrollesAndSortedBySurname(bool originalCertificateOnly, string search, bool activeOnly)
		{
            var all = GetEnrolleesSortedBySurname();

            var filtred = from x in all
                          where x.Surname.Contains(search) ||
                                x.Name.Contains(search) ||
                                x.Patronymic.Contains(search)
                          select x;

            if (activeOnly)
            {
                filtred = filtred.Where(x => x.ActivityStatus == true);
            }

            if (originalCertificateOnly)
            {
                filtred = filtred.Where(x => x.OriginalCertificate == originalCertificateOnly);
            }

            return filtred;
        }

        public IOrderedQueryable<Enrollee> GetEnrolleesSortedBySurname()
        {
            return GetEnrolleesWithIncludes().OrderBy(x => x.Surname);
        }

        public IQueryable<Enrollee> GetEnrolleesWithIncludes()
        {
            var all = Enrolles
               .Include(x => x.FirstPriority)
               .Include(x => x.SecondPriority)
               .Include(x => x.ThirdPriority)
               .Include(x => x.School);
            return all;
        }
	}

    public static class EnrolleeExtinsions
    {
        public static IOrderedEnumerable<Enrollee> SortEnrolleesByPoints(this IQueryable<Enrollee> enrollees)
        {
            return enrollees.OrderByDescending(GetEnrolleeExtraPoints)
                            .OrderByDescending(x => x.CSScore)
                            .OrderByDescending(x => x.MathScore)
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
    }

}