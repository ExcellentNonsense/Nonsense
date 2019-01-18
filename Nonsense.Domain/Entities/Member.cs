using Nonsense.Common.Utilities;
using System;

namespace Nonsense.Domain.Entities {

    class Member : BaseEntity {

        public string IdentityGuid { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public Member() {
            // Required by EF.
        }

        public Member(string identityGuid) : this() {
            Guard.NotNull(identityGuid, nameof(identityGuid));

            IdentityGuid = identityGuid;
        }
    }
}
