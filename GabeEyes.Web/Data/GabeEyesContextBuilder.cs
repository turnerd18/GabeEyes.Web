using Microsoft.EntityFrameworkCore;

namespace GabeEyes.Web.Data
{
	public class GabeEyesContextFactory
    {
		public static GabeEyesContext Build(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder();
			optionsBuilder.UseSqlServer(connectionString);

			var context = new GabeEyesContext(optionsBuilder.Options);
			context.Database.EnsureCreated();

			return context;
		}
    }
}
