using AutoMapper;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.Cafe, App.BLL.DTO.Cafe>().ReverseMap();
        CreateMap<App.DAL.DTO.CafeCategory, App.BLL.DTO.CafeCategory>().ReverseMap();
        CreateMap<App.DAL.DTO.CafeOccasion, App.BLL.DTO.CafeOccasion>().ReverseMap();
        CreateMap<App.DAL.DTO.CafeType, App.BLL.DTO.CafeType>().ReverseMap();
        CreateMap<App.DAL.DTO.CategoryOfCafe, App.BLL.DTO.CategoryOfCafe>().ReverseMap();
        CreateMap<App.DAL.DTO.City, App.BLL.DTO.City>().ReverseMap();
        CreateMap<App.DAL.DTO.Favourite, App.BLL.DTO.Favourite>().ReverseMap();
        CreateMap<App.DAL.DTO.MenuItemCategory, App.BLL.DTO.MenuItemCategory>().ReverseMap();
        CreateMap<App.DAL.DTO.MenuItem, App.BLL.DTO.MenuItem>().ReverseMap();
        CreateMap<App.DAL.DTO.Menu, App.BLL.DTO.Menu>().ReverseMap();
        CreateMap<App.DAL.DTO.OccasionOfCafe, App.BLL.DTO.OccasionOfCafe>().ReverseMap();
        CreateMap<App.DAL.DTO.ReviewPhoto, App.BLL.DTO.ReviewPhoto>().ReverseMap();
        CreateMap<App.DAL.DTO.Review, App.BLL.DTO.Review>().ReverseMap();
        CreateMap<App.DAL.DTO.TypeOfCafe, App.BLL.DTO.TypeOfCafe>().ReverseMap();
    }
}