namespace STAReportsAPI.STADataAccess
{
	public class DatabaseHandlerFactory
	{
		private IConfiguration _configuration;
		string connectionString = "";

		public DatabaseHandlerFactory(IConfiguration configuration)
		{
			_configuration = configuration;
			// connectionString = _configuration.GetConnectionString("DefaultConnection");
		}
		public DatabaseHandlerFactory(string connectionStringName)
		{
			//connectionString= _configuration?.GetSection("ConnectionStrings")["DefaultConnection"].ToString();
			//   connectionString = _configuration.GetConnectionString("DefaultConnection");
			//connectionString = "Data Source = 146.56.55.230; UserID = root; Password = Flexi@123; Database = recon_bikebazaar; Port = 3306; SslMode = None";
			connectionString = connectionStringName;
			//connectionString = "Data Source = 146.56.55.230; UserID = root; Password = Flexi@123; Database = recon_flexi; Port = 3306; SslMode = None";
		}

		public IDatabaseHandler CreateDatabase()
		{
			IDatabaseHandler database = null;

			database = new MySqlDataAccess(connectionString);

			return database;
		}

		public string GetProviderName()
		{
			return "MySql.Data.MySqlClient";
		}
	}
}
