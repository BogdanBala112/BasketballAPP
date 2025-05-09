package Repository;

import Entities.Match;
import Entities.Ticket;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import javax.xml.transform.Result;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class TicketRepository implements GenericInterface<Ticket, Integer>{
    private static final Logger logger = LogManager.getLogger(MatchRepository.class);

    public Connection connection;

    public TicketRepository() {
        this.connection = DBAccess.connect();
    }

    @Override
    public void add(Ticket t) {
        String sql = "INSERT INTO tickets (ticketId,nr_seats, matchId_fk) VALUES (?, ?, ?)";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setInt(1,t.getticketId());
            stmt.setInt(2,t.getnr_seats());
            stmt.setInt(3,t.getmatchId());

            int rowsAffected = stmt.executeUpdate();

            if (rowsAffected > 0) {
                logger.info("Ticket added successfully");
            }
            else{
                logger.warn("Ticket not added");
            }
        }
        catch(SQLException e){
            logger.error("Error inserting ticket" + e.getMessage());
        }
    }

    @Override
    public void remove(Integer ticketId){
        String sql = "DELETE FROM tickets WHERE ticketId = ?";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setInt(1,ticketId);

            int rowsAffected = stmt.executeUpdate();

            if (rowsAffected > 0) {
                logger.info("Ticket removed successfully");
            }
            else{
                logger.warn("Ticket not removed");
            }
        }
        catch(SQLException e){
            logger.error("Error removing ticket" + e.getMessage());
        }
    }

    @Override
    public Ticket getById(Integer ticketId){
        String sql = "SELECT * FROM ticket WHERE ticketId = ?";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setInt(1,ticketId);
            ResultSet resultSet = stmt.executeQuery();

            if (resultSet.next()){
                return new Ticket(
                        resultSet.getInt("ticketId"),
                        resultSet.getInt("nr_seats"),
                        resultSet.getInt("matchId_fk")
                );
            }
        }
        catch(SQLException e){
            logger.error("Error getting ticket" + e.getMessage());
        }
        return null;
    }

    @Override
    public void update(Integer ticketId, Ticket t){
        String sql = "UPDATE tickets SET nr_seats = ?,matchId_fk = ? WHERE ticketId = ?";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setInt(1,t.getnr_seats());
            stmt.setInt(2,t.getmatchId());
            stmt.setInt(3,t.getticketId());

            int rowsAffected = stmt.executeUpdate();

            if (rowsAffected > 0) {
                logger.info("Ticket updated successfully");
            }
            else{
                logger.warn("Ticket not updated");
            }
        }
        catch(SQLException e){
            logger.error("Error inserting ticket" + e.getMessage());
        }
    }

    public List<Ticket> getAll(){
        String sql = "SELECT * FROM tickets";
        List<Ticket> tickets = new ArrayList<>();

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            ResultSet resultSet = stmt.executeQuery();

            while (resultSet.next()){
                tickets.add(new Ticket(
                        resultSet.getInt("ticketId"),
                        resultSet.getInt("nr_seats"),
                        resultSet.getInt("matchId_fk")
                ));
            }
        }
        catch(SQLException e){
            logger.error("Error getting tickets" + e.getMessage());
        }
        return tickets;
    }

}
