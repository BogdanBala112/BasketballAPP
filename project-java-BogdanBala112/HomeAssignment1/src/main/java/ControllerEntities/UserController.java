package ControllerEntities;

import Entities.User;
import Repository.UserRepository;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import service.UserService;

import java.util.List;
import java.util.Objects;

public class UserController {
    public final UserService userService;
    public final ObservableList<User> userList;
    private User currentUser;

    public UserController() {
        UserRepository repository = new UserRepository();
        this.userService = new UserService(repository);
        this.userList = FXCollections.observableArrayList(userService.getUsers());
    }

    public void setCurrentUser(User user){
        currentUser = user;
    }

    public String getCurrentUser(){
        return currentUser.getUsername();
    }

    public ObservableList<User> getUsersList() {
        return userList;
    }

    public boolean findUser(String username){
        return userService.findUser(username);
    }

    public List<User> getUsers(){
        return userService.getUsers();
    }

    public void addUser(User user) {
        userService.addUser(user);
        userList.add(user);
    }

    public void deleteUser(String username) {
        userService.removeUser(username);
        userList.removeIf(m -> Objects.equals(m.getUsername(), username));
    }

    public void updateUser(String username, User user) {
        userService.updateUser(username, user);
        userList.clear();
        userList.addAll(userService.getUsers());
    }

    public boolean authenticate(String username, String password) {
        return userService.authenticateUser(username, password);
    }

    public List<String> getUsernames(){
        return userService.getAllUsernames();
    }

}
