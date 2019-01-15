using System.Threading.Tasks;

namespace Nonsense.Application.Interfaces {

    public interface IInteractor<TInteractorResponse> : IInputPort {

        Task<bool> Execute(IOutputPort<TInteractorResponse> outputPort);
    }

    public interface IInteractor<TInteractorRequest, TInteractorResponse> : IInputPort<TInteractorRequest> {

        Task<bool> Execute(TInteractorRequest request, IOutputPort<TInteractorResponse> outputPort);
    }
}
