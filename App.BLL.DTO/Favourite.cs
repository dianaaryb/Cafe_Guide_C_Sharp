using App.Domain.Identity;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Favourite: IDomainEntityId
{
    public Guid Id { get; set; }
    
    //FK
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }
}