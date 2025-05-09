package server_connection;

import org.example.MatchGUI;

import java.io.*;
import java.net.Socket;

public class SimpleClientConnection {
    private Socket socket;
    private PrintWriter out;
    private BufferedReader in;
    private MatchGUI matchGUI;
    private String username;

    public SimpleClientConnection(MatchGUI matchGUI, String username) {
        this.matchGUI = matchGUI;
        this.username = username;

        try {
            socket = new Socket("localhost", 1234);
            out = new PrintWriter(socket.getOutputStream(), true);
            in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            out.println(username);

            new Thread(() -> {
                try {
                    String line;
                    while ((line = in.readLine()) != null) {
                        if (line.equals("REFRESH_MATCHES")) {
                            javafx.application.Platform.runLater(matchGUI::refreshMatchesFromServer);
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Disconnected from server.");
                }
            }).start();

        } catch (IOException e) {
            System.err.println("Could not connect to server: " + e.getMessage());
        }
    }

    public void notifyServer() {
        if (out != null) {
            out.println("REFRESH_MATCHES");
        }
    }
}