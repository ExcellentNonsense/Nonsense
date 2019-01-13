

namespace Nonsense.Application.Interfaces {

    public interface IOutputPort<TInteractorResponse> {

        void Handle(TInteractorResponse response);
    }
}
