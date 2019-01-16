using Nonsense.Common;
using System.Collections.Generic;

namespace Nonsense.Application.Users.Responses {

    public class CreateUserResponse : OperationResult {

        public string Id { get; }

        public CreateUserResponse(bool success, IEnumerable<string> errors, string id)
            : base(success, errors) {

            Id = Id;
        }
    }
}
