using AutoMapper;
using Nonsense.Application.Users.Dto;
using Nonsense.MvcApp.Areas.Admin.Features.Accounts;

namespace Nonsense.MvcApp {

    public class MappingProfile : Profile {

        public MappingProfile() {

            CreateMap<Account, DisplayViewModel>();
            CreateMap<CreateViewModel, Account>();
            CreateMap<Account, EditViewModel>();
            CreateMap<EditViewModel, Account>();
        }
    }
}
