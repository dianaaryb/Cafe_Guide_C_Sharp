using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile: Profile //teeb profiili domain Cafest DAL.DTO.Cafe
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Cafe, App.DAL.DTO.Cafe>().ReverseMap();
        CreateMap<App.Domain.CafeCategory, App.DAL.DTO.CafeCategory>().ReverseMap();
        CreateMap<App.Domain.CafeOccasion, App.DAL.DTO.CafeOccasion>().ReverseMap();
        CreateMap<App.Domain.CafeType, App.DAL.DTO.CafeType>().ReverseMap();
        CreateMap<App.Domain.CategoryOfCafe, App.DAL.DTO.CategoryOfCafe>().ReverseMap();
        CreateMap<App.Domain.City, App.DAL.DTO.City>().ReverseMap();
        CreateMap<App.Domain.Favourite, App.DAL.DTO.Favourite>().ReverseMap();
        CreateMap<App.Domain.MenuItemCategory, App.DAL.DTO.MenuItemCategory>().ReverseMap();
        CreateMap<App.Domain.MenuItem, App.DAL.DTO.MenuItem>().ReverseMap();
        CreateMap<App.Domain.Menu, App.DAL.DTO.Menu>().ReverseMap();
        CreateMap<App.Domain.OccasionOfCafe, App.DAL.DTO.OccasionOfCafe>().ReverseMap();
        CreateMap<App.Domain.ReviewPhoto, App.DAL.DTO.ReviewPhoto>().ReverseMap();
        CreateMap<App.Domain.Review, App.DAL.DTO.Review>().ReverseMap();
        CreateMap<App.Domain.TypeOfCafe, App.DAL.DTO.TypeOfCafe>().ReverseMap();
    }
}