﻿// <auto-generated />
using System;
using App.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.DAL.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240503130435_AddUserFIeldsLen")]
    partial class AddUserFIeldsLen
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("App.Domain.Cafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CafeAddress")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<int>("CafeAverageRating")
                        .HasColumnType("integer");

                    b.Property<string>("CafeEmail")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("CafeName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("CafeTelephone")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("CafeWebsiteLink")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CityId");

                    b.ToTable("Cafes");
                });

            modelBuilder.Entity("App.Domain.CafeCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryOfCafeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("CategoryOfCafeId");

                    b.ToTable("CafeCategories");
                });

            modelBuilder.Entity("App.Domain.CafeOccasion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OccasionOfCafeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("OccasionOfCafeId");

                    b.ToTable("CafeOccasions");
                });

            modelBuilder.Entity("App.Domain.CafePhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.ToTable("CafePhotos");
                });

            modelBuilder.Entity("App.Domain.CafeType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeOfCafeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("TypeOfCafeId");

                    b.ToTable("CafeTypes");
                });

            modelBuilder.Entity("App.Domain.CategoryOfCafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryOfCafeDescription")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("CategoryOfCafeName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("CategoryOfCafes");
                });

            modelBuilder.Entity("App.Domain.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CityName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("App.Domain.Favourite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CafeId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("App.Domain.Identity.AppRefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDT")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("PreviousExpirationDT")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PreviousRefreshToken")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("App.Domain.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("App.Domain.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("App.Domain.Identity.AppUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("App.Domain.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("App.Domain.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MenuItemCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("MenuItemDescription")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("MenuItemName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("MenuItemPrice")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("MenuItemCategoryId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("App.Domain.MenuItemCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MenuItemCategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("MenuItemCategories");
                });

            modelBuilder.Entity("App.Domain.MenuItemPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MenuItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("MenuItemPhotoLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuItemPhotos");
                });

            modelBuilder.Entity("App.Domain.OccasionOfCafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("OccasionOfCafeDescription")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("OccasionOfCafeName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("OccasionOfCafes");
                });

            modelBuilder.Entity("App.Domain.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CafeId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("App.Domain.ReviewPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReviewPhotoLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReviewPhotos");
                });

            modelBuilder.Entity("App.Domain.TypeOfCafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("TypeOfCafeDescription")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("TypeOfCafeName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfCafes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("App.Domain.Cafe", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "AppUser")
                        .WithMany("Cafes")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.City", "City")
                        .WithMany("Cafes")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("City");
                });

            modelBuilder.Entity("App.Domain.CafeCategory", b =>
                {
                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("CafeCategories")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.CategoryOfCafe", "CategoryOfCafe")
                        .WithMany("CafeCategories")
                        .HasForeignKey("CategoryOfCafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("CategoryOfCafe");
                });

            modelBuilder.Entity("App.Domain.CafeOccasion", b =>
                {
                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("CafeOccasions")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.OccasionOfCafe", "OccasionOfCafe")
                        .WithMany("CafeOccasions")
                        .HasForeignKey("OccasionOfCafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("OccasionOfCafe");
                });

            modelBuilder.Entity("App.Domain.CafePhoto", b =>
                {
                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("CafePhotos")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("App.Domain.CafeType", b =>
                {
                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("CafeTypes")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.TypeOfCafe", "TypeOfCafe")
                        .WithMany("CafeTypes")
                        .HasForeignKey("TypeOfCafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("TypeOfCafe");
                });

            modelBuilder.Entity("App.Domain.Favourite", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "AppUser")
                        .WithMany("Favourites")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("Favourites")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("App.Domain.Identity.AppRefreshToken", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "AppUser")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("App.Domain.Identity.AppUserRole", b =>
                {
                    b.HasOne("App.Domain.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Menu", b =>
                {
                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("Menus")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("App.Domain.MenuItem", b =>
                {
                    b.HasOne("App.Domain.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.MenuItemCategory", "MenuItemCategory")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuItemCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("MenuItemCategory");
                });

            modelBuilder.Entity("App.Domain.MenuItemPhoto", b =>
                {
                    b.HasOne("App.Domain.MenuItem", "MenuItem")
                        .WithMany("MenuItemPhotos")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("App.Domain.Review", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "AppUser")
                        .WithMany("Reviews")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Cafe", "Cafe")
                        .WithMany("Reviews")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("App.Domain.ReviewPhoto", b =>
                {
                    b.HasOne("App.Domain.Review", "Review")
                        .WithMany("ReviewPhotos")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Cafe", b =>
                {
                    b.Navigation("CafeCategories");

                    b.Navigation("CafeOccasions");

                    b.Navigation("CafePhotos");

                    b.Navigation("CafeTypes");

                    b.Navigation("Favourites");

                    b.Navigation("Menus");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("App.Domain.CategoryOfCafe", b =>
                {
                    b.Navigation("CafeCategories");
                });

            modelBuilder.Entity("App.Domain.City", b =>
                {
                    b.Navigation("Cafes");
                });

            modelBuilder.Entity("App.Domain.Identity.AppUser", b =>
                {
                    b.Navigation("Cafes");

                    b.Navigation("Favourites");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("App.Domain.Menu", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("App.Domain.MenuItem", b =>
                {
                    b.Navigation("MenuItemPhotos");
                });

            modelBuilder.Entity("App.Domain.MenuItemCategory", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("App.Domain.OccasionOfCafe", b =>
                {
                    b.Navigation("CafeOccasions");
                });

            modelBuilder.Entity("App.Domain.Review", b =>
                {
                    b.Navigation("ReviewPhotos");
                });

            modelBuilder.Entity("App.Domain.TypeOfCafe", b =>
                {
                    b.Navigation("CafeTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
