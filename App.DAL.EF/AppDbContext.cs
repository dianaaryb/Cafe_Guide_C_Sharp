using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, 
    AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Cafe> Cafes { get; set; } = default!;
    public DbSet<CafeCategory> CafeCategories { get; set; } = default!;
    public DbSet<CafeOccasion> CafeOccasions { get; set; } = default!;
    public DbSet<CafeType> CafeTypes { get; set; } = default!;
    public DbSet<CategoryOfCafe> CategoryOfCafes { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;
    public DbSet<Favourite> Favourites { get; set; } = default!;
    public DbSet<Menu> Menus { get; set; } = default!;
    public DbSet<MenuItem> MenuItems { get; set; } = default!;
    public DbSet<MenuItemCategory> MenuItemCategories { get; set; } = default!;
    public DbSet<OccasionOfCafe> OccasionOfCafes { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<ReviewPhoto> ReviewPhotos { get; set; } = default!;
    public DbSet<TypeOfCafe> TypeOfCafes { get; set; } = default!;
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;

    public AppDbContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure cascade delete for the CafeCategory to CategoryOfCafe relationship
        builder.Entity<CafeCategory>()
            .HasOne(cc => cc.CategoryOfCafe)
            .WithMany()
            .HasForeignKey(cc => cc.CategoryOfCafeId)
            .OnDelete(DeleteBehavior.Cascade); // Enable cascade delete

        // Optionally, configure cascade delete for the CafeCategory to Cafe relationship if needed
        builder.Entity<CafeCategory>()
            .HasOne(cc => cc.Cafe)
            .WithMany()
            .HasForeignKey(cc => cc.CafeId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        
        builder.Entity<CafeType>()
            .HasOne(cc => cc.TypeOfCafe)
            .WithMany()
            .HasForeignKey(cc => cc.TypeOfCafeId)
            .OnDelete(DeleteBehavior.Cascade); // Enable cascade delete

        // Optionally, configure cascade delete for the CafeCategory to Cafe relationship if needed
        builder.Entity<CafeType>()
            .HasOne(cc => cc.Cafe)
            .WithMany()
            .HasForeignKey(cc => cc.CafeId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        builder.Entity<CafeOccasion>()
            .HasOne(cc => cc.OccasionOfCafe)
            .WithMany()
            .HasForeignKey(cc => cc.OccasionOfCafeId)
            .OnDelete(DeleteBehavior.Cascade); // Enable cascade delete

        // Optionally, configure cascade delete for the CafeCategory to Cafe relationship if needed
        builder.Entity<CafeOccasion>()
            .HasOne(cc => cc.Cafe)
            .WithMany()
            .HasForeignKey(cc => cc.CafeId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        
        builder.Entity<Review>()
            .HasMany(r => r.ReviewPhotos)
            .WithOne(rp => rp.Review)
            .HasForeignKey(rp => rp.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Cafe>()
            .HasMany(c => c.Reviews)
            .WithOne(r => r.Cafe)
            .HasForeignKey(r => r.CafeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //disable cascade delete
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            if (relationship.DeleteBehavior != DeleteBehavior.Cascade)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        // foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
        // {
        //     relationship.DeleteBehavior = DeleteBehavior.Restrict;
        // }
        if (this.Database.ProviderName!.Contains("InMemory"))
        {
            builder.Entity<Cafe>()
                .OwnsOne(e => e.CafeName, b => { b.ToJson();});
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime)))
            {
                Console.WriteLine(prop);
                prop.CurrentValue = ((DateTime) prop.CurrentValue!).ToUniversalTime();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}
