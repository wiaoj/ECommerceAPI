using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence;

public static class Configuration {
	public static String GetPostgreSQLConnectionString {
		get {
			ConfigurationManager configurationManager = new();
			configurationManager.AddJsonFile("appsettings.json");

			return configurationManager.GetConnectionString("PostgreSQL");
		}
	}
}