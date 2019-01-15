using Nonsense.Common;
using System.Collections.Generic;

namespace Nonsense.Application.Gateways.Repositories {

    public class CreateUserResponse : OperationResult {

        public string Id { get; set; }

        public CreateUserResponse(bool success, IEnumerable<string> errors, string id)
            : base(success, errors) {

            Id = Id;
        }
    }
}
