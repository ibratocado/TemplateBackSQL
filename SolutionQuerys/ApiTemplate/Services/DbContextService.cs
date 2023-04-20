using ApiTemplate.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ApiTemplate.Services
{
    public class DbContextService : IDbContextService
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conexion;

        public DbContextService(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(
                "appsettings.json", optional: false);

            _configuration = builder.Build();
            string connectionString = _configuration.GetConnectionString("DbConection").ToString();

            conexion = new SqlConnection(connectionString);
            _configuration = configuration;
        }
        public SqlConnection CloseConection()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }

        public SqlConnection OpenConection()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }
    }
}
