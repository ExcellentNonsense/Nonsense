using System;
using System.Collections.Generic;
using System.Text;

namespace Nonsense.Application.Interfaces {
    public interface IOutputPort<TInteractorResponse> {
        void Handle(TInteractorResponse response);
    }
}
