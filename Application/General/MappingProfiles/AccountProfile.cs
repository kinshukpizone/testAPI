using AutoMapper;
using Domain.Entities.Account;
using Domain.Entities.Admin;
using Domain.Entities.Location;
using Presentation.DataTransferObject;
using Presentation.DataTransferObject.Admin;

namespace Application.General.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterUserModel, ApplicationUser>().ReverseMap();

            CreateMap<StateModel, State>().ReverseMap();

            CreateMap<CityModel, City>().ReverseMap();
            CreateMap<BannerModel, Banner>().ReverseMap();
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<PageModel, Pages>().ReverseMap();
            CreateMap<PermissionModel, Permission>().ReverseMap();

            CreateMap<CityModel, City>()
                .ForMember(dest => dest.StateNavigation, opt => opt.MapFrom(src => src.State))
                .ReverseMap();

        }

    }
}
