using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Interfaces;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.Application.Users.Interactors {

    public sealed class GetAllUsersInteractor : IGetAllUsersInteractor {

        private readonly IUserRepository _userRepository;

        public GetAllUsersInteractor(IUserRepository userRepository) {
            Guard.NotNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        public async Task<bool> Execute(IOutputPort<Responses.GetAllUsersResponse> outputPort) {
            Guard.NotNull(outputPort, nameof(outputPort));

            var response = await _userRepository.GetAllUsers();

            outputPort.Handle(
                new Responses.GetAllUsersResponse(response.Success, response.ErrorsList, response.Users));

            return response.Success;
        }
    }
}
