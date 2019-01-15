using Nonsense.Application.Interfaces;
using Nonsense.Common;
using Nonsense.Common.Utilities;
using System.Collections.Generic;

namespace Nonsense.MvcApp.Interfaces {

    public abstract class BasePresenter<TInteractorResponse> : IOutputPort<TInteractorResponse> where TInteractorResponse : OperationResult {

        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public virtual void Handle(TInteractorResponse response) {
            Guard.NotNull(response, nameof(response));

            Success = response.Success;
            Errors = response.ErrorsList;
        }
    }
}
