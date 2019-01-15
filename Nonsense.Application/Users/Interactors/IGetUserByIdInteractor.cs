using Nonsense.Application.Interfaces;
using Nonsense.Application.Users.Requests;
using Nonsense.Application.Users.Responses;

namespace Nonsense.Application.Users.Interactors {

    public interface IGetUserByIdInteractor : IInteractor<GetUserByIdRequest, GetUserByIdResponse> {

    }
}
