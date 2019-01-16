using Nonsense.Application.Users.Dto;
using Nonsense.Common;
using System.Collections.Generic;

namespace Nonsense.Application.Gateways.Repositories {

    public class GetUserByIdResponse : OperationResult {

        public User User { get; }

        public GetUserByIdResponse(bool success, IEnumerable<string> errors, User user)
            : base(success, errors) {

            User = user;
        }
    }
}
