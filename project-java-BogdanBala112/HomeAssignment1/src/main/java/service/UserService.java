package service;

import Entities.User;
import Repository.UserRepository;

import java.util.ArrayList;
import java.util.List;

public class UserService {
    public UserRepository userRepository;

    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public List<User> getUsers(){
        return userRepository.getAll();
    }

    public void addUser(User user){
        userRepository.add(user);
    }

    public void removeUser(String username){
        userRepository.remove(username);
    }

    public void updateUser(String username, User user){
        userRepository.update(username, user);
    }

    public boolean authenticateUser(String username, String password){
        for (User user : userRepository.getAll()){
            if (user.getUsername().equals(username) && user.getPassword().equals(password))
                return true;
        }
        return false;
    }

    public boolean findUser(String username){
        return userRepository.findUserByUsername(username);
    }

    public List<String> getAllUsernames(){
        List<String> usernames = new ArrayList<>();

        for (User user : userRepository.getAll()){
            usernames.add(user.getUsername());
        }

        return usernames;
    }

}
