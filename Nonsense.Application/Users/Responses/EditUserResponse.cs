using Nonsense.Common;
using System.Collections.Generic;

namespace Nonsense.Application.Users.Responses {

    public class EditUserResponse : OperationResult {

        public string Id { get; set; }

        public EditUserResponse(bool success, IEnumerable<string> errors, string id)
            : base(success, errors) {

            Id = Id;
        }
    }
}
