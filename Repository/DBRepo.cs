using System;
using System.Data.SQLite;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DatabaseConnection
{
    public class DBRepo
    {
        private static SQLiteConnection _connection;
        private static readonly ILogger<DBRepo> _logger;

        static DBRepo()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            _logger = loggerFactory.CreateLogger<DBRepo>();
        }

        private DBRepo() { }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
        
        public static SQLiteConnection Connect()
        {
            if (_connection == null)
            {
                try
                {
                    var config = LoadConfiguration();
                    string connectionString = config.GetConnectionString("DefaultConnection");

                    _connection = new SQLiteConnection(connectionString);
                    _connection.Open();
                    _logger.LogInformation("Database connection established successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to connect to the database");
                }
            }
            return _connection;
        }

        public static void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
                _logger.LogInformation("Database connection closed successfully.");
            }
        }
    }
}