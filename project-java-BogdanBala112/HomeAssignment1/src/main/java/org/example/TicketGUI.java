package org.example;

import ControllerEntities.MatchController;
import ControllerEntities.TicketController;
import ControllerEntities.UserController;
import Entities.Match;
import Entities.Ticket;
import Entities.User;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ListView;
import javafx.scene.control.TextField;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class TicketGUI {
    public MatchController matchController = new MatchController();
    public TicketController ticketController = new TicketController();
    public UserController userController = new UserController();
    public ListView<Match> matchList = new ListView<>(matchController.getMatchList());
    public ListView<Ticket> ticketList = new ListView<>(ticketController.getTicketList());
    public ListView<User> userList = new ListView<>(userController.getUsersList());

    public VBox prepareTicketVBox() {
        TextField idTicket = new TextField();
        idTicket.setPromptText("ID Ticket");

        TextField NrSeats = new TextField();
        NrSeats.setPromptText("Number of Seats");

        TextField MatchKey = new TextField();
        MatchKey.setPromptText("Match Key");

        Button addTicket = new Button("Add Ticket");

        addTicket.setOnAction(e -> {
            int id = Integer.parseInt(idTicket.getText());
            int seats = Integer.parseInt(NrSeats.getText());
            int matchKey = Integer.parseInt(MatchKey.getText());

            Ticket t1 = new Ticket(id,seats,matchKey);
            int size = ticketController.getTicketList().size();
            ticketController.addTicket(t1);
            if (ticketController.getTicketList().size() != size + 1) {
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Insertion Failed!");
                alert.setHeaderText(null);
                alert.setContentText("Cannot add a ticket if a match doesn't exist!");
                alert.showAndWait();
            }

            idTicket.clear();
            NrSeats.clear();
            MatchKey.clear();
        });

        Button deleteTicket = new Button("Delete Ticket");

        deleteTicket.setOnAction(e -> {
            int id = Integer.parseInt(idTicket.getText());
            ticketController.deleteTicket(id);
            idTicket.clear();
        });

        return new VBox(10,idTicket, NrSeats, MatchKey, addTicket, deleteTicket, ticketList);
    }

    public void openTicketWindow(){
        Stage stage = new Stage();
        Stage ticketStage = new Stage();
        VBox ticketLayout = prepareTicketVBox();
        Scene ticketScene = new Scene(ticketLayout, 500, 400);
        ticketStage.setTitle("Ticket Window");
        ticketStage.setScene(ticketScene);
        ticketStage.show();
    }
}
