using System.Data.Entity;

namespace SSU.Reception.Models
{
	public class EnrolleeContext : DbContext
	{
		public EnrolleeContext(): base("DbConnection")
		{
			
		}

		public DbSet<Enrollee> Enrolles { get; set; }
	}
}