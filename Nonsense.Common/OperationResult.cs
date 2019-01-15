using System;
using System.Collections.Generic;
using System.Linq;

namespace Nonsense.Common {

    public class OperationResult {

        public bool Success { get; set; } = true;
        public IList<string> ErrorsList { get; } = new List<string>();

        public void AddError(string error) {
            if (!String.IsNullOrWhiteSpace(error)) {
                ErrorsList.Add(error);
            }
        }

        public OperationResult() {

        }

        public OperationResult(bool success, IEnumerable<string> errors) {
            Success = success;

            foreach (var error in errors ?? Enumerable.Empty<string>()) {
                AddError(error);
            }
        }
    }
}
