using GabeEyes.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace GabeEyes.Web.Data
{
	public class GabeEyesContext : DbContext
    {
		public GabeEyesContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Quote> Quotes { get; set; }
    }
}