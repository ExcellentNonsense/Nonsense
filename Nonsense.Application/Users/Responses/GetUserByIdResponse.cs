using Nonsense.Common;
using Nonsense.Domain.Entities;
using System.Collections.Generic;

namespace Nonsense.Application.Users.Responses {

    public class GetUserByIdResponse : OperationResult {

        public User User { get; }

        public GetUserByIdResponse(bool success, IEnumerable<string> errors, User user)
            : base(success, errors) {

            User = user;
        }
    }
}
