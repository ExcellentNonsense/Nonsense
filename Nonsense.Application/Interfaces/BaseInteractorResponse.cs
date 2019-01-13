using System.Collections.Generic;

namespace Nonsense.Application.Interfaces {

    public abstract class BaseInteractorResponse {

        public bool Success { get; }
        public IEnumerable<string> Errors { get; }

        protected BaseInteractorResponse(bool success, IEnumerable<string> errors) {
            Success = success;
            Errors = errors;
        }
    }
}
