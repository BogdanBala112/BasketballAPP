package org.example;

import ControllerEntities.MatchController;
import ControllerEntities.TicketController;
import ControllerEntities.UserController;
import Entities.Match;
import Entities.Ticket;
import Entities.User;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class UserGUI {
    public MatchController matchController = new MatchController();
    public TicketController ticketController = new TicketController();
    public UserController userController = new UserController();
    public ListView<Match> matchList = new ListView<>(matchController.getMatchList());
    public ListView<Ticket> ticketList = new ListView<>(ticketController.getTicketList());
    public ListView<User> userList = new ListView<>(userController.getUsersList());
    public MatchGUI matchGUI = new MatchGUI();
    public TicketGUI ticketGUI = new TicketGUI();

    public VBox createUserVBox(){
        VBox layout = new VBox(10);
        layout.setPadding(new Insets(20));
        layout.setAlignment(Pos.CENTER);

        Label loginInfo = new Label("SIGN IN");
        loginInfo.setStyle("-fx-font-weight: bold; -fx-text-fill: black; -fx-font-size: 20px;");

        TextField Username = new TextField();
        Username.setPromptText("Username");

        PasswordField passwordField = new PasswordField();
        passwordField.setPromptText("Password");

        TextField visiblePasswordField = new TextField();
        visiblePasswordField.setPromptText("Password");
        visiblePasswordField.setManaged(false);
        visiblePasswordField.setVisible(false);

        passwordField.textProperty().bindBidirectional(visiblePasswordField.textProperty());

        PasswordField PasswordAgain = new PasswordField();
        PasswordAgain.setPromptText("Re-Introduce Password");

        TextField visiblePasswordAgain = new TextField();
        visiblePasswordAgain.setPromptText("Re-Introduce Password");
        visiblePasswordAgain.setManaged(false);
        visiblePasswordAgain.setVisible(false);

        PasswordAgain.textProperty().bindBidirectional(visiblePasswordAgain.textProperty());

        Button toggleButton = new Button("Show Password");
        toggleButton.setOnAction(e -> {
            boolean showing = visiblePasswordField.isVisible();

            visiblePasswordField.setVisible(!showing);
            visiblePasswordField.setManaged(!showing);
            passwordField.setVisible(showing);
            passwordField.setManaged(showing);

            visiblePasswordAgain.setVisible(!showing);
            visiblePasswordAgain.setManaged(!showing);
            PasswordAgain.setVisible(showing);
            PasswordAgain.setManaged(showing);

            toggleButton.setText(showing ? "Show Password" : "Hide Password");
        });

        Button CreateUser = new Button("Create Account");

        CreateUser.setOnAction(e -> {
            String username = Username.getText();
            String password = passwordField.getText();
            String passwordAgain = PasswordAgain.getText();

            if ((username == null || password == null || passwordAgain == null) || (!password.equals(passwordAgain))){
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Sign In Failed");
                alert.setHeaderText(null);
                alert.setContentText("Cannot create user! Password not correct!");
                alert.showAndWait();
            }
            else{
                userController.addUser(new User(username, password));
                matchGUI.openMatchWindow(username);
                ((Stage) CreateUser.getScene().getWindow()).close();
            }

            Username.clear();
            passwordField.clear();
            visiblePasswordField.clear();
            PasswordAgain.clear();
            visiblePasswordAgain.clear();
        });

        VBox.setMargin(loginInfo, new Insets(0,10,50,0));
        layout.getChildren().addAll(
                loginInfo, Username,
                passwordField, visiblePasswordField,
                PasswordAgain, visiblePasswordAgain,
                toggleButton,
                CreateUser
        );

        return layout;
    }


    public VBox prepareUserVBox() {
        VBox layout = new VBox(10);
        layout.setPadding(new Insets(20));
        layout.setAlignment(Pos.CENTER);

        Label loginInfo = new Label("LOG IN");
        loginInfo.setStyle("-fx-font-weight: bold; -fx-text-fill: black; -fx-font-size: 20px;");

        TextField Username = new TextField();
        Username.setPromptText("Username");

        PasswordField passwordField = new PasswordField();
        passwordField.setPromptText("Password");

        TextField visiblePasswordField = new TextField();
        visiblePasswordField.setPromptText("Password");
        visiblePasswordField.setManaged(false);
        visiblePasswordField.setVisible(false);

        passwordField.textProperty().bindBidirectional(visiblePasswordField.textProperty());

        Button toggleButton = new Button("Show Password");
        toggleButton.setOnAction(e -> {
            boolean showing = visiblePasswordField.isVisible();
            visiblePasswordField.setVisible(!showing);
            visiblePasswordField.setManaged(!showing);
            passwordField.setVisible(showing);
            passwordField.setManaged(showing);
            toggleButton.setText(showing ? "Show Password" : "Hide Password");
        });

        Button authenticateBtn = new Button("Log In");

        authenticateBtn.setOnAction(e -> {
            String username = Username.getText();
            String password = passwordField.getText();

            if (userController.authenticate(username, password)) {
                matchGUI.openMatchWindow(username);
                ((Stage) authenticateBtn.getScene().getWindow()).close();
            }
            else{
                Alert alert = new Alert(Alert.AlertType.ERROR);
                alert.setTitle("Authentication Failed");
                alert.setHeaderText(null);
                alert.setContentText("Cannot find user or password!");
                alert.showAndWait();
            }
            Username.clear();
            passwordField.clear();
            visiblePasswordField.clear();
        });

        Button CreateUser = new Button("Sign In");

        CreateUser.setOnAction(e -> {
            ((Stage) authenticateBtn.getScene().getWindow()).close();

            Stage Createstage = new Stage();
            VBox CreateUserLayout = createUserVBox();
            Scene userScene = new Scene(CreateUserLayout, 500, 400);
            Createstage.setTitle("Sign In Window");
            Createstage.setScene(userScene);
            Createstage.show();
        });

        Button CloseApp = new Button("Close Application");

        CloseApp.setOnAction(e -> {
            System.exit(0);
        });

        HBox closeLayout = new HBox();
        closeLayout.setAlignment(Pos.CENTER_RIGHT);
        closeLayout.getChildren().add(CloseApp);

        VBox.setMargin(loginInfo, new Insets(0,10,50,0));
        layout.getChildren().addAll(
                loginInfo, Username,
                passwordField, visiblePasswordField, toggleButton,
                authenticateBtn, CreateUser, closeLayout
        );

        return layout;
    }

    public void start_GUI() {
        Stage primaryStage = new Stage();
        VBox userLayout = prepareUserVBox();
        Scene userScene = new Scene(userLayout, 500, 400);
        primaryStage.setTitle("Authentication Window");
        primaryStage.setScene(userScene);
        primaryStage.show();
    }

}
