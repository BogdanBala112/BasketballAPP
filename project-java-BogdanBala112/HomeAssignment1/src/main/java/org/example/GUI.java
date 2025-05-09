package org.example;

import ControllerEntities.TicketController;
import ControllerEntities.UserController;
import Entities.Match;
import ControllerEntities.MatchController;
import Entities.Ticket;
import Entities.User;
import javafx.application.Application;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class GUI extends Application {
    public UserGUI userGUI = new UserGUI();
    public MatchGUI matchGUI = new MatchGUI();

    public static void CheckServerSignal(int port){

    }

    @Override
    public void start(Stage primaryStage){
        userGUI.matchGUI = matchGUI;
        matchGUI.userGUI = userGUI;

        userGUI.start_GUI();
    }

    public static void main(String[] args){
        launch(args);
    }

}
