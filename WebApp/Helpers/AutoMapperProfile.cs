using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.Cafe, App.DTO.v1_0.Cafe>().ReverseMap();
        CreateMap<App.BLL.DTO.CafeCategory, App.DTO.v1_0.CafeCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.CafeOccasion, App.DTO.v1_0.CafeOccasion>().ReverseMap();
        CreateMap<App.BLL.DTO.CafeType, App.DTO.v1_0.CafeType>().ReverseMap();
        CreateMap<App.BLL.DTO.CategoryOfCafe, App.DTO.v1_0.CategoryOfCafe>().ReverseMap();
        CreateMap<App.BLL.DTO.City, App.DTO.v1_0.City>().ReverseMap();
        CreateMap<App.BLL.DTO.Favourite, App.DTO.v1_0.Favourite>().ReverseMap();
        CreateMap<App.BLL.DTO.MenuItemCategory, App.DTO.v1_0.MenuItemCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.MenuItem, App.DTO.v1_0.MenuItem>().ReverseMap();
        CreateMap<App.BLL.DTO.Menu, App.DTO.v1_0.Menu>().ReverseMap();
        CreateMap<App.BLL.DTO.OccasionOfCafe, App.DTO.v1_0.OccasionOfCafe>().ReverseMap();
        CreateMap<App.BLL.DTO.ReviewPhoto, App.DTO.v1_0.ReviewPhoto>().ReverseMap();
        CreateMap<App.BLL.DTO.Review, App.DTO.v1_0.Review>().ReverseMap();
        CreateMap<App.BLL.DTO.TypeOfCafe, App.DTO.v1_0.TypeOfCafe>().ReverseMap();
    }
}