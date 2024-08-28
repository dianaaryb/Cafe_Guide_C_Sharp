using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser: IdentityUser<Guid>, IDomainEntityId
{
    [MinLength(1)]
    [MaxLength(64)]
    public string? FirstName { get; set; }
    [MinLength(1)]
    [MaxLength(64)]
    public string? LastName { get; set; }
    public ICollection<Favourite>? Favourites { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Cafe>? Cafes { get; set; }

    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
}