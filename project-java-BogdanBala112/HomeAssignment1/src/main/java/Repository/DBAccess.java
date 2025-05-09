package Repository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import java.io.IOException;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Properties;

public class DBAccess {
    private static final Logger logger = LogManager.getLogger(DBAccess.class);
    private static Connection connection;

    private DBAccess() {}

    public static Connection connect() {
        if (connection == null) {
            try (InputStream input = DBAccess.class.getClassLoader().getResourceAsStream("config.properties")) {
                if (input == null) {
                    throw new IOException("config.properties not found in classpath!");
                }
                Properties properties = new Properties();
                properties.load(input);
                String url = properties.getProperty("database.url");

                connection = DriverManager.getConnection(url);
                Statement stmt = connection.createStatement();
                stmt.execute("PRAGMA foreign_keys = ON");
                logger.info("Database connection established successfully.");
            } catch (IOException | SQLException e) {
                logger.error("Failed to connect to the database", e);
            }
        }
        return connection;
    }

    public static void closeConnection() {
        if (connection != null) {
            try {
                connection.close();
                connection = null;
                logger.info("Database connection closed successfully.");
            } catch (SQLException e) {
                logger.error("Error closing the database connection", e);
            }
        }
    }
}
