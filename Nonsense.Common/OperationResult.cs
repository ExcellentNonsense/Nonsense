using System;
using System.Collections.Generic;

namespace Nonsense.Common {

    public class OperationResult {

        public bool Success { get; set; } = true;
        public List<string> Messages { get; } = new List<string>();

        public void AddMessage(string message) {
            if (!String.IsNullOrWhiteSpace(message)) {
                Messages.Add(message);
            }
        }
    }
}
