

namespace Nonsense.Application.Users.Dto {

    public sealed class User {

        public string Id { get; }
        public string UserName { get; }
        public string Email { get; }

        public User(string id, string userName, string email) {
            Id = id;
            UserName = userName;
            Email = email;
        }
    }
}
