using System.Collections.Generic;
using Domain;
using projectC.Repository;

namespace projectC.Service
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository repository)
        {
            userRepository = repository;
        }

        public List<User> GetUsers() => userRepository.GetAll();

        public void AddUser(User user) => userRepository.Add(user);

        public bool AuthenticateUser(string username, string password)
        {
            var user = userRepository.GetById(username);
            return user != null && user.password == password;
        }

        public bool FindUser(string username) => userRepository.FindUser(username);

        public List<string> GetAllUsernames()
        {
            var result = new List<string>();
            foreach (var user in userRepository.GetAll())
            {
                result.Add(user.username);
            }
            return result;
        }
    }
}