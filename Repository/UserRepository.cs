using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DatabaseConnection;
using Domain;
using Microsoft.Extensions.Logging;

namespace projectC.Repository
{
    public class UserRepository
    {
        private readonly SQLiteConnection connection;
        private static readonly ILogger<UserRepository> logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger<UserRepository>();

        public UserRepository()
        {
            connection = DBRepo.Connect();
        }

        public void Add(User user)
        {
            string sql = "INSERT INTO users (username, password) VALUES (@username, @password)";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    logger.LogInformation("User added successfully");
                else
                    logger.LogWarning("User was not added");
            }
            catch (Exception e)
            {
                logger.LogError("Error adding user: {Message}", e.Message);
            }
        }

        public bool FindUser(string username)
        {
            string sql = "SELECT 1 FROM users WHERE username = @username";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@username", username);
                using var reader = cmd.ExecuteReader();
                return reader.Read();
            }
            catch (Exception e)
            {
                logger.LogError("Error finding user: {Message}", e.Message);
                return false;
            }
        }

        public User GetById(string username)
        {
            string sql = "SELECT * FROM users WHERE username = @username";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@username", username);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User(reader.GetString(0), reader.GetString(1));
                }
            }
            catch (Exception e)
            {
                logger.LogError("Error getting user: {Message}", e.Message);
            }
            return null;
        }

        public List<User> GetAll()
        {
            var users = new List<User>();
            string sql = "SELECT * FROM users";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User(reader.GetString(0), reader.GetString(1)));
                }
            }
            catch (Exception e)
            {
                logger.LogError("Error retrieving users: {Message}", e.Message);
            }
            return users;
        }
    }
}