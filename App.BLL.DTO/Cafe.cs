using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Identity;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Cafe : IDomainEntityId
{
    public Guid Id { get; set; }
    [MaxLength (50)]
    public string? CafeName { get; set; }
    [MaxLength(250)]
    public string? CafeAddress { get; set; }
    [MaxLength(250)]
    public string? CafeEmail { get; set; }
    [MaxLength(250)]
    public string? CafeTelephone { get; set; }
    [MaxLength(250)]
    public string? CafeWebsiteLink { get; set; }
    
    public int CafeAverageRating { get; set; }
    public string? PhotoLink { get; set; }
    
    //FK
    public City? City { get; set; }
    public Guid CityId { get; set; }
    
    //FK
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<Menu>? Menus { get; set; }
    public ICollection<CafeCategory>? CafeCategories { get; set; }
    public ICollection<CafeType>? CafeTypes { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<CafeOccasion>? CafeOccasions { get; set; }
    public ICollection<Favourite>? Favourites { get; set; }
}