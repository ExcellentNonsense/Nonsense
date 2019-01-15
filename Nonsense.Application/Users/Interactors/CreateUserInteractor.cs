using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Interfaces;
using Nonsense.Application.Users.Requests;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.Application.Users.Interactors {

    public sealed class CreateUserInteractor : ICreateUserInteractor {

        private readonly IUserRepository _userRepository;

        public CreateUserInteractor(IUserRepository userRepository) {
            Guard.NotNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        public async Task<bool> Execute(CreateUserRequest request, IOutputPort<Responses.CreateUserResponse> outputPort) {
            Guard.NotNull(request, nameof(request));
            Guard.NotNull(outputPort, nameof(outputPort));

            var response = await _userRepository.Create(request.UserName, request.Email, request.Password);

            outputPort.Handle(new Responses.CreateUserResponse(response.Success, response.ErrorsList, response.Id));

            return response.Success;
        }
    }
}
