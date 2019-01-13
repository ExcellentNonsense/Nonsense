using System.Threading.Tasks;

namespace Nonsense.Application.Interfaces {

    public interface IInteractor<TInteractorResponse> : IInputPort {

        Task<bool> Execute(IOutputPort<TInteractorResponse> outputPort);
    }
}
