

namespace Nonsense.Application.Users.Requests {

    public class EditUserRequest {

        public string Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public string Password { get; }

        public EditUserRequest(string id, string userName, string email, string password) {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}
