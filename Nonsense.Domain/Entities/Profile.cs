using Nonsense.Common.Utilities;
using System;

namespace Nonsense.Domain.Entities {

    public sealed class Profile : BaseEntity {

        public string IdentityGuid { get; private set; }
        public string SpendTimeOn { get; set; }
        public DateTime SpentOnNonsenseToday { get; set; }
        public DateTime SpentOnNonsensePerMonth { get; set; }
        public DateTime SpentOnNonsensePerYear { get; set; }
        public DateTime SpentOnNonsenseTotal { get; set; }

        public ProfileImage ProfileImage { get; set; }

        public Profile() {
            // Required by EF.
        }

        public Profile(string identityGuid) : this() {
            Guard.NotNull(identityGuid, nameof(identityGuid));

            IdentityGuid = identityGuid;
        }
    }
}
