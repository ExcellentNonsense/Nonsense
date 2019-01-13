using Nonsense.Application.RandomImages.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nonsense.Application.Interfaces {
    public interface IInteractor<TInteractorResponse> : IInputPort {
        Task<bool> Execute(IOutputPort<TInteractorResponse> outputPort);
    }
}
