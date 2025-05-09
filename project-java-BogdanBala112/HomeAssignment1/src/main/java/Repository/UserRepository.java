package Repository;

import Entities.User;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class UserRepository implements GenericInterface<User, String>{
    private static final Logger logger = LogManager.getLogger(MatchRepository.class);

    public Connection connection;

    public UserRepository() {
        this.connection = DBAccess.connect();
    }

    public void add(User user) {
        String sql = "INSERT INTO users VALUES (?,?)";

        try (PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setString(1,user.getUsername());
            stmt.setString(2,user.getPassword());

            int rowsAffected = stmt.executeUpdate();

            if (rowsAffected > 0) {
                logger.info("User added successfully");
            }
            else{
                logger.warn("Error adding user");
            }
        }
        catch(SQLException e){
            logger.error("Error adding user");
        }
    }

    public void remove(String username){
        String sql = "DELETE FROM users WHERE username = ?";

        try (PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setString(1,username);

            int rowsAffected = stmt.executeUpdate();

            if (rowsAffected > 0) {
                logger.info("User removed successfully");
            }
            else{
                logger.warn("Error removing user");
            }
        }
        catch(SQLException e){
            logger.error("Error removing user");
        }
    }

    public void update(String username, User user){
        String sql = "UPDATE users SET username = ?, password = ? WHERE username = ?";

        try (PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setString(1,user.getUsername());
            stmt.setString(2,user.getPassword());
            stmt.setString(3,username);

            int rowsAffected = stmt.executeUpdate();

            if (rowsAffected > 0) {
                logger.info("User updated successfully");
            }
            else{
                logger.warn("Error updating user");
            }
        }
        catch(SQLException e){
            logger.error("Error updating user");
        }
    }

    public User getById(String username){
        String sql = "SELECT * FROM users WHERE username = ?";

        try (PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setString(1,username);
            ResultSet resultSet = stmt.executeQuery();

            if (resultSet.next()){
                return new User(
                        resultSet.getString("username"),
                        resultSet.getString("password")
                );
            }
        }
        catch(SQLException e){
            logger.error("Error getting user");
        }
        return null;
    }

    public List<User> getAll(){
        String sql = "SELECT * FROM users";
        List<User> users = new ArrayList<>();

        try (PreparedStatement stmt = connection.prepareStatement(sql)){
            ResultSet resultSet = stmt.executeQuery();

            while (resultSet.next()){
                users.add(new User(
                   resultSet.getString("username"),
                   resultSet.getString("password")
                ));
            }
        }
        catch(SQLException e){
            logger.error("Error getting users");
        }
        return users;
    }

    public boolean findUserByUsername(String username){
        String sql = "SELECT * FROM users WHERE username = ?";
        boolean found = false;

        try (PreparedStatement stmt = connection.prepareStatement(sql)){
            stmt.setString(1, username);
            ResultSet resultSet = stmt.executeQuery();

            if (resultSet.next()){
                found = true;
            }
        }
        catch (SQLException e){
            logger.error("Error getting user");
        }

        return found;
    }
}
