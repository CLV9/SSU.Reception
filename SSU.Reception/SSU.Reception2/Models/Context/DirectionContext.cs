using System.Data.Entity;

namespace SSU.Reception.Models
{
	public class DirectionContext : DbContext
	{
		public DirectionContext() : base("DbConnection") { }

		public DbSet<Direction> Directions { get; set; }
	}
}