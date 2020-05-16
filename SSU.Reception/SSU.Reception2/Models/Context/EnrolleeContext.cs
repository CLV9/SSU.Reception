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
		public IQueryable<Enrollee> FilterEnrolles(bool originalCertificateOnly, string search, bool activeOnly)
		{
            var all = Enrolles
               .Include(x => x.FirstPriority)
               .Include(x => x.SecondPriority)
               .Include(x => x.ThirdPriority)
               .Include(x => x.School)
               .OrderBy(x => x.Surname);

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
	}
}