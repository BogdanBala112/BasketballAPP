package org.example;

import ControllerEntities.MatchController;
import ControllerEntities.TicketController;
import ControllerEntities.UserController;
import Entities.Match;
import Entities.Ticket;
import Entities.User;
import javafx.collections.transformation.FilteredList;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import server_connection.SimpleClientConnection;

public class MatchGUI {
    public MatchController matchController = new MatchController();
    public TicketController ticketController = new TicketController();
    public UserController userController = new UserController();

    public FilteredList<Match> filteredMatches = new FilteredList<>(matchController.getMatchList(), p -> true);
    public ListView<Match> matchList = new ListView<>(filteredMatches);
    public ListView<Ticket> ticketList = new ListView<>(ticketController.getTicketList());
    public ListView<User> userList = new ListView<>(userController.getUsersList());

    public UserGUI userGUI;
    public SimpleClientConnection connection;

    public VBox prepareMatchVBox() {
        TextField searchBar = new TextField();
        searchBar.setPromptText("Search for matches...");

        searchBar.textProperty().addListener((observable, oldValue, newValue) -> {
            filteredMatches.setPredicate(match ->
                    match.getMatchName().toLowerCase().contains(newValue.toLowerCase()));
        });

        matchList.setCellFactory(listView -> new ListCell<>() {
            @Override
            protected void updateItem(Match match, boolean empty) {
                super.updateItem(match, empty);
                if (empty || match == null) {
                    setText(null);
                    setStyle("");
                } else {
                    setText(match.toString());
                    setStyle(match.getNr_seats() == 0 ? "-fx-text-fill: red;" : "");
                }
            }
        });

        ComboBox<String> userComboBox = new ComboBox<>();
        userComboBox.getItems().addAll(userController.getUsernames());
        userComboBox.setPromptText("Select a user");

        TextField number_of_seats = new TextField();
        number_of_seats.setPromptText("Number of Seats Desired");

        Button sellTickets = new Button("Sell Tickets");

        sellTickets.setOnAction(e -> {
            String customer = userComboBox.getValue();
            int seats;

            try {
                seats = Integer.parseInt(number_of_seats.getText());
            } catch (NumberFormatException ex) {
                showAlert("Invalid input", "Please enter a valid number of seats.");
                return;
            }

            if (filteredMatches.isEmpty()) {
                showAlert("No matches", "Cannot find any matches!");
                return;
            }

            if (!userController.findUser(customer)) {
                showAlert("User not found", "Cannot find user!");
                return;
            }

            Match selectedMatch = filteredMatches.get(0);
            int matchId = selectedMatch.getMatchId();

            boolean success = matchController.buyTickets(matchId, seats);
            if (!success) {
                showAlert("Not enough seats", "Not enough available seats for buying!");
                return;
            }

            matchController.refreshMatches();
            filteredMatches.setPredicate(filteredMatches.getPredicate());

            if (connection != null) {
                connection.notifyServer();
            }

            searchBar.clear();
            userComboBox.setValue(null);
            number_of_seats.clear();
        });

        Button logout = new Button("Logout");
        logout.setOnAction(e -> {
            userGUI.start_GUI();
            ((Stage) logout.getScene().getWindow()).close();
        });

        HBox logoutBox = new HBox(logout);
        logoutBox.setAlignment(Pos.CENTER_RIGHT);

        return new VBox(10, searchBar, userComboBox, number_of_seats, sellTickets, matchList, logoutBox);
    }

    public void refreshMatchesFromServer() {
        matchController.refreshMatches();
        filteredMatches.setPredicate(filteredMatches.getPredicate());
    }

    private void showAlert(String title, String content) {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(content);
        alert.showAndWait();
    }

    public void openMatchWindow(String username) {
        if (connection == null) {
            connection = new SimpleClientConnection(this, username);
        }

        Stage stage = new Stage();
        VBox matchLayout = prepareMatchVBox();
        Scene matchScene = new Scene(matchLayout, 700, 500);
        stage.setTitle("Matches Window");
        stage.setScene(matchScene);
        stage.show();
    }

}
