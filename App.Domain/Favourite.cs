using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class Favourite: BaseEntityId, IDomainAppUser<AppUser>
{
    //FK
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    //FK
    public Cafe? Cafe { get; set; }
    public Guid CafeId { get; set; }

}