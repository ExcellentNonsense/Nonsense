using Nonsense.Common;
using System.Collections.Generic;
using System.Linq;

namespace Nonsense.Application {

    public class BoundaryResponse<TData> : OperationResult {

        public TData Data { get; set; }

        public BoundaryResponse(bool success, IEnumerable<string> errors, TData data)
            : base(success, errors) {

            Data = data;
        }
    }
}
