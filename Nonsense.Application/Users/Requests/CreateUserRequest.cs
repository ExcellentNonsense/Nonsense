

namespace Nonsense.Application.Users.Requests {

    public class CreateUserRequest {

        public string UserName { get; }
        public string Email { get; }
        public string Password { get; }

        public CreateUserRequest(string userName, string email, string password) {
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}
