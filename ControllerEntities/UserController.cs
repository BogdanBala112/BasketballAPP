using System.Collections.Generic;
using Domain;
using projectC.Repository;
using projectC.Service;

namespace projectC.ControllerEntities
{
    public class UserController
    {
        private readonly UserService userService;

        public UserController()
        {
            var repo = new UserRepository();
            userService = new UserService(repo);
        }

        public void AddUser(User user) => userService.AddUser(user);

        public bool Authenticate(string username, string password) => userService.AuthenticateUser(username, password);

        public bool FindUser(string username) => userService.FindUser(username);

        public List<string> GetUsernames() => userService.GetAllUsernames();
    }
}