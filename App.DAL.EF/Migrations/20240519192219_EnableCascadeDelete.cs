using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CafeId1",
                table: "CafeCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryOfCafeId1",
                table: "CafeCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CafeCategories_CafeId1",
                table: "CafeCategories",
                column: "CafeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CafeCategories_CategoryOfCafeId1",
                table: "CafeCategories",
                column: "CategoryOfCafeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CafeCategories_Cafes_CafeId1",
                table: "CafeCategories",
                column: "CafeId1",
                principalTable: "Cafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CafeCategories_CategoryOfCafes_CategoryOfCafeId1",
                table: "CafeCategories",
                column: "CategoryOfCafeId1",
                principalTable: "CategoryOfCafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CafeCategories_Cafes_CafeId1",
                table: "CafeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CafeCategories_CategoryOfCafes_CategoryOfCafeId1",
                table: "CafeCategories");

            migrationBuilder.DropIndex(
                name: "IX_CafeCategories_CafeId1",
                table: "CafeCategories");

            migrationBuilder.DropIndex(
                name: "IX_CafeCategories_CategoryOfCafeId1",
                table: "CafeCategories");

            migrationBuilder.DropColumn(
                name: "CafeId1",
                table: "CafeCategories");

            migrationBuilder.DropColumn(
                name: "CategoryOfCafeId1",
                table: "CafeCategories");
        }
    }
}
