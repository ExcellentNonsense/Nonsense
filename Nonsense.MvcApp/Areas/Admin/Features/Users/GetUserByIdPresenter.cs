using Nonsense.Application.Users.Responses;
using Nonsense.Domain.Entities;
using Nonsense.MvcApp.Interfaces;

namespace Nonsense.MvcApp.Areas.Admin.Features.Users {

    public class GetUserByIdPresenter : BasePresenter<GetUserByIdResponse> {

        public User User { get; set; }

        public override void Handle(GetUserByIdResponse response) {
            base.Handle(response);

            User = response.User;
        }
    }
}
