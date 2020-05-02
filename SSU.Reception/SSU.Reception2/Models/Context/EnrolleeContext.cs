using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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