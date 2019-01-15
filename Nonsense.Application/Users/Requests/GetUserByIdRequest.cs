

namespace Nonsense.Application.Users.Requests {

    public class GetUserByIdRequest {

        public string Id { get; }

        public GetUserByIdRequest(string id) {
            Id = id;
        }
    }
}
