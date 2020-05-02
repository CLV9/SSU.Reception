using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SSU.Reception.Models
{
	public class DirectionContext : DbContext
	{
		public DirectionContext() : base("DbConnection") { }

		public DbSet<Direction> Directions { get; set; }
	}
}