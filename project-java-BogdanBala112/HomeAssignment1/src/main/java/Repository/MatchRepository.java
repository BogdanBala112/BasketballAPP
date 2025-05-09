package Repository;

import Entities.Match;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class MatchRepository implements GenericInterface<Match, Integer>{
    private static final Logger logger = LogManager.getLogger(MatchRepository.class);

    public Connection connection;

    public MatchRepository() {
        this.connection = DBAccess.connect();
    }

    @Override
    public void add(Match match){
        String sql = "INSERT INTO matches (matchId, matchName, nr_seats, ticketPrice) VALUES (?, ?, ?, ?)";

        try (PreparedStatement stmt = connection.prepareStatement(sql)) {
            stmt.setInt(1, match.getMatchId());
            stmt.setString(2, match.getMatchName());
            stmt.setInt(3, match.getNr_seats());
            stmt.setDouble(4, match.getticketPrice());

            int rowsInserted = stmt.executeUpdate();
            if (rowsInserted > 0)
                logger.info("Match added successfully");
            else
                logger.warn("Match was not added");

        } catch (SQLException e) {
            logger.error("Error adding match: " + e.getMessage());
        }
    }

    @Override
    public void remove(Integer matchId){
        String sql = "DELETE FROM matches WHERE matchId = ?";
        String deleteTicketsSQL = "DELETE FROM tickets WHERE matchId_fk = ?";

        try(PreparedStatement stmt = connection.prepareStatement(sql) ;
        PreparedStatement stmt2 = connection.prepareStatement(deleteTicketsSQL)) {
            stmt2.setInt(1, matchId);
            stmt2.executeUpdate();

            stmt.setInt(1,matchId);
            int rowsDeleted = stmt.executeUpdate();
            if (rowsDeleted > 0)
                logger.info("Match and associated tickets deleted successfully.");
            else
                logger.warn("Match was not removed");
        }
        catch (SQLException e) {
            logger.error("Error deleting match: " + e.getMessage());
        }
    }

    @Override
    public Match getById(Integer matchId) {
        String sql = "SELECT * FROM matches WHERE matchId = ?";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setInt(1,matchId);
            ResultSet resultSet = stmt.executeQuery();

            if (resultSet.next()) {
                return new Match(
                        resultSet.getInt("matchId"),
                        resultSet.getString("matchName"),
                        resultSet.getInt("nr_seats"),
                        resultSet.getDouble("ticketPrice")
                );
            }
        }
        catch (SQLException e) {
            logger.error("Error retrieving the desired match: " + e.getMessage());
        }
        return null;
    }

    @Override
    public void update(Integer matchId, Match match){
        String sql = "UPDATE matches SET matchName = ?, nr_seats = ?, ticketPrice = ? WHERE matchId = ?";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setString(1,match.getMatchName());
            stmt.setInt(2,match.getNr_seats());
            stmt.setDouble(3,match.getticketPrice());
            stmt.setInt(4,match.getMatchId());

            int rowsUpdated = stmt.executeUpdate();

            if (rowsUpdated > 0)
                logger.info("Match updated successfully");
            else
                logger.warn("Match was not updated");
        }
        catch(SQLException e){
            logger.error("Error updating match: " + e.getMessage());
        }
    }

    @Override
    public List<Match> getAll(){
        List<Match> matches = new ArrayList<>();
        String sql = "SELECT * FROM matches ORDER BY ticketPrice DESC";

        try(PreparedStatement stmt = connection.prepareStatement(sql)){
                ResultSet resultSet = stmt.executeQuery();
                while (resultSet.next()){
                    Match match = new Match(
                            resultSet.getInt("matchId"),
                            resultSet.getString("matchName"),
                            resultSet.getInt("nr_seats"),
                            resultSet.getInt("ticketPrice")
                    );
                    matches.add(match);
                }
        }
        catch (SQLException e) {
            logger.error("Error retrieving all matches: " + e.getMessage());
        }
        return matches;
    }

    public boolean sellTicketBySeats(int matchId, int number){
        String selectSql = "SELECT nr_seats FROM matches WHERE matchId = ?";
        String updateSql = "UPDATE matches SET nr_seats = nr_seats - ? WHERE matchId = ?";

        try (PreparedStatement selectStmt = connection.prepareStatement(selectSql); PreparedStatement updateStmt = connection.prepareStatement(updateSql)){
            selectStmt.setInt(1, matchId);
            ResultSet rs = selectStmt.executeQuery();

            if (rs.next()){
                int availableSeats = rs.getInt("nr_seats");
                if (availableSeats >= number){
                    updateStmt.setInt(1, number);
                    updateStmt.setInt(2, matchId);
                    int rowsAffected = updateStmt.executeUpdate();

                    if (rowsAffected > 0){
                        logger.info("Sold: " + number  + " tickets for match " + matchId);
                        return true;
                    }
                }
                else {
                    logger.warn("Not enough tickets to sell!");
                    return false;
                }
            }

        }
        catch (SQLException e){
            logger.error("Error updating match: " + e.getMessage());
        }

        return false;

    }

}
