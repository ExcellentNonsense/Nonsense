using Nonsense.Application.Users.Responses;
using Nonsense.Domain.Entities;
using Nonsense.MvcApp.Interfaces;
using System.Collections.Generic;

namespace Nonsense.MvcApp.Areas.Admin.Features.Users {

    public class GetAllUsersPresenter : BasePresenter<GetAllUsersResponse> {

        public IEnumerable<User> Users { get; set; }

        public override void Handle(GetAllUsersResponse response) {
            base.Handle(response);

            Users = response.Users;
        }
    }
}
