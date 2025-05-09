using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DatabaseConnection;
using Domain;
using Microsoft.Extensions.Logging;

namespace projectC.Repository
{
    public class MatchRepository
    {
        private readonly SQLiteConnection connection;
        private static readonly ILogger<MatchRepository> logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger<MatchRepository>();

        public MatchRepository()
        {
            connection = DBRepo.Connect();
        }

        public void Add(Match match)
        {
            const string sql = "INSERT INTO matches (matchId, matchName, nr_seats, ticketPrice) VALUES (@id, @name, @seats, @price)";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", match.matchId);
                cmd.Parameters.AddWithValue("@name", match.matchName);
                cmd.Parameters.AddWithValue("@seats", match.nr_seats);
                cmd.Parameters.AddWithValue("@price", match.ticketPrice);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) logger.LogInformation("Match added successfully");
                else logger.LogWarning("Match not added");
            }
            catch (Exception ex)
            {
                logger.LogError("Error adding match: {Message}", ex.Message);
            }
        }

        public void Remove(int matchId)
        {
            const string deleteTicketsSQL = "DELETE FROM tickets WHERE matchId_fk = @id";
            const string deleteMatchSQL = "DELETE FROM matches WHERE matchId = @id";
            try
            {
                using var cmd1 = new SQLiteCommand(deleteTicketsSQL, connection);
                cmd1.Parameters.AddWithValue("@id", matchId);
                cmd1.ExecuteNonQuery();

                using var cmd2 = new SQLiteCommand(deleteMatchSQL, connection);
                cmd2.Parameters.AddWithValue("@id", matchId);
                int rows = cmd2.ExecuteNonQuery();
                if (rows > 0) logger.LogInformation("Match and tickets deleted");
                else logger.LogWarning("Match not deleted");
            }
            catch (Exception ex)
            {
                logger.LogError("Error deleting match: {Message}", ex.Message);
            }
        }

        public Match GetById(int matchId)
        {
            const string sql = "SELECT * FROM matches WHERE matchId = @id";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", matchId);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Match(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetDouble(3));
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting match: {Message}", ex.Message);
            }
            return null;
        }

        public void Update(int matchId, Match match)
        {
            const string sql = "UPDATE matches SET matchName = @name, nr_seats = @seats, ticketPrice = @price WHERE matchId = @id";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", match.matchName);
                cmd.Parameters.AddWithValue("@seats", match.nr_seats);
                cmd.Parameters.AddWithValue("@price", match.ticketPrice);
                cmd.Parameters.AddWithValue("@id", matchId);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) logger.LogInformation("Match updated");
                else logger.LogWarning("Match not updated");
            }
            catch (Exception ex)
            {
                logger.LogError("Error updating match: {Message}", ex.Message);
            }
        }

        public List<Match> GetAll()
        {
            var matches = new List<Match>();
            const string sql = "SELECT * FROM matches ORDER BY ticketPrice DESC";
            try
            {
                using var cmd = new SQLiteCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    matches.Add(new Match(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetDouble(3)));
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error retrieving matches: {Message}", ex.Message);
            }
            return matches;
        }

        public bool SellTicketBySeats(int matchId, int number)
        {
            const string selectSql = "SELECT nr_seats FROM matches WHERE matchId = @id";
            const string updateSql = "UPDATE matches SET nr_seats = nr_seats - @count WHERE matchId = @id";

            try
            {
                using var selectCmd = new SQLiteCommand(selectSql, connection);
                selectCmd.Parameters.AddWithValue("@id", matchId);
                using var reader = selectCmd.ExecuteReader();
                if (reader.Read())
                {
                    int available = reader.GetInt32(0);
                    if (available >= number)
                    {
                        using var updateCmd = new SQLiteCommand(updateSql, connection);
                        updateCmd.Parameters.AddWithValue("@count", number);
                        updateCmd.Parameters.AddWithValue("@id", matchId);
                        int affected = updateCmd.ExecuteNonQuery();
                        if (affected > 0)
                        {
                            logger.LogInformation("Sold {number} tickets for match {matchId}");
                            return true;
                        }
                    }
                    else
                    {
                        logger.LogWarning("Not enough seats for match {matchId}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error selling tickets: {Message}", ex.Message);
            }
            return false;
        }
    }
}