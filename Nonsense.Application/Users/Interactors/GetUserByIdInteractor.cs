using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Interfaces;
using Nonsense.Application.Users.Requests;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.Application.Users.Interactors {

    class GetUserByIdInteractor : IGetUserByIdInteractor {

        private readonly IUserRepository _userRepository;

        public GetUserByIdInteractor(IUserRepository userRepository) {
            Guard.NotNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        public async Task<bool> Execute(GetUserByIdRequest request, IOutputPort<Responses.GetUserByIdResponse> outputPort) {
            Guard.NotNull(outputPort, nameof(outputPort));

            var response = await _userRepository.GetUserById(request.Id);

            outputPort.Handle(
                new Responses.GetUserByIdResponse(response.Success, response.ErrorsList, response.User));

            return response.Success;
        }
    }
}
