

namespace Nonsense.Domain.Entities {

    public sealed class ProfileImage : BaseEntity {

        public byte[] Image { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
