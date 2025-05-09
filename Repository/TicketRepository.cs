using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DatabaseConnection;
using Microsoft.Extensions.Logging;
using Domain;

namespace projectC.Repository
{
    public class TicketRepository : GenericInterface<Ticket, int>
    {
        private SQLiteConnection connection;
        private static readonly ILogger<TicketRepository> logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger<TicketRepository>();

        public TicketRepository()
        {
            this.connection = DBRepo.Connect();
        }

        public void Add(Ticket ticket)
        {
            string sql = "INSERT INTO tickets (ticketId, ticketPrice, nr_seats, matchId_fk) VALUES (@ticketId, @ticketPrice, @nrSeats, @matchId)";

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ticketId", ticket.ticketId);
                    cmd.Parameters.AddWithValue("@ticketPrice", ticket.ticketPrice);
                    cmd.Parameters.AddWithValue("@nrSeats", ticket.nr_seats);
                    cmd.Parameters.AddWithValue("@matchId", ticket.matchId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        logger.LogInformation("✅ Ticket added successfully.");
                    else
                        logger.LogWarning("⚠️ Ticket was not added.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("❌ Error inserting ticket: {0}", ex.Message);
            }
        }

        public void Remove(int ticketId)
        {
            string sql = "DELETE FROM tickets WHERE ticketId = @ticketId";

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ticketId", ticketId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        logger.LogInformation("✅ Ticket removed successfully.");
                    else
                        logger.LogWarning("⚠️ Ticket was not removed.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("❌ Error removing ticket: {0}", ex.Message);
            }
        }

        public Ticket GetById(int ticketId)
        {
            string sql = "SELECT * FROM tickets WHERE ticketId = @ticketId";

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ticketId", ticketId);
                    using (SQLiteDataReader resultSet = cmd.ExecuteReader())
                    {
                        if (resultSet.Read())
                        {
                            return new Ticket(
                                resultSet.GetInt32(0), // ticketId
                                resultSet.GetDouble(1), // ticketPrice
                                resultSet.GetInt32(2), // nr_seats
                                resultSet.GetInt32(3)  // matchId_fk
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("❌ Error getting ticket: {0}", ex.Message);
            }
            return null;
        }

        public void Update(int ticketId, Ticket ticket)
        {
            string sql = "UPDATE tickets SET ticketPrice = @ticketPrice, nr_seats = @nrSeats, matchId_fk = @matchId WHERE ticketId = @ticketId";

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ticketPrice", ticket.ticketPrice);
                    cmd.Parameters.AddWithValue("@nrSeats", ticket.nr_seats);
                    cmd.Parameters.AddWithValue("@matchId", ticket.matchId);
                    cmd.Parameters.AddWithValue("@ticketId", ticket.ticketId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        logger.LogInformation("✅ Ticket updated successfully.");
                    else
                        logger.LogWarning("⚠️ Ticket was not updated.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("❌ Error updating ticket: {0}", ex.Message);
            }
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> tickets = new List<Ticket>();
            string sql = "SELECT * FROM tickets";

            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader resultSet = cmd.ExecuteReader())
                    {
                        while (resultSet.Read())
                        {
                            Ticket ticket = new Ticket(
                                resultSet.GetInt32(0),
                                resultSet.GetDouble(1),
                                resultSet.GetInt32(2),
                                resultSet.GetInt32(3)
                            );
                            tickets.Add(ticket);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("❌ Error retrieving tickets: {0}", ex.Message);
            }
            return tickets;
        }
    }
}
