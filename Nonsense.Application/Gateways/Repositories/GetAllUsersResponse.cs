using Nonsense.Common;
using Nonsense.Domain.Entities;
using System.Collections.Generic;

namespace Nonsense.Application.Gateways.Repositories {

    public class GetAllUsersResponse : OperationResult {

        public IEnumerable<User> Users { get; }

        public GetAllUsersResponse(bool success, IEnumerable<string> errors, IEnumerable<User> users)
            : base(success, errors) {

            Users = users;
        }
    }
}
