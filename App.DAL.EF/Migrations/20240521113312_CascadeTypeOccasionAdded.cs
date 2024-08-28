using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class CascadeTypeOccasionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CafeId1",
                table: "CafeTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeOfCafeId1",
                table: "CafeTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CafeId1",
                table: "CafeOccasions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OccasionOfCafeId1",
                table: "CafeOccasions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CafeTypes_CafeId1",
                table: "CafeTypes",
                column: "CafeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CafeTypes_TypeOfCafeId1",
                table: "CafeTypes",
                column: "TypeOfCafeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CafeOccasions_CafeId1",
                table: "CafeOccasions",
                column: "CafeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CafeOccasions_OccasionOfCafeId1",
                table: "CafeOccasions",
                column: "OccasionOfCafeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CafeOccasions_Cafes_CafeId1",
                table: "CafeOccasions",
                column: "CafeId1",
                principalTable: "Cafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CafeOccasions_OccasionOfCafes_OccasionOfCafeId1",
                table: "CafeOccasions",
                column: "OccasionOfCafeId1",
                principalTable: "OccasionOfCafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CafeTypes_Cafes_CafeId1",
                table: "CafeTypes",
                column: "CafeId1",
                principalTable: "Cafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CafeTypes_TypeOfCafes_TypeOfCafeId1",
                table: "CafeTypes",
                column: "TypeOfCafeId1",
                principalTable: "TypeOfCafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CafeOccasions_Cafes_CafeId1",
                table: "CafeOccasions");

            migrationBuilder.DropForeignKey(
                name: "FK_CafeOccasions_OccasionOfCafes_OccasionOfCafeId1",
                table: "CafeOccasions");

            migrationBuilder.DropForeignKey(
                name: "FK_CafeTypes_Cafes_CafeId1",
                table: "CafeTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CafeTypes_TypeOfCafes_TypeOfCafeId1",
                table: "CafeTypes");

            migrationBuilder.DropIndex(
                name: "IX_CafeTypes_CafeId1",
                table: "CafeTypes");

            migrationBuilder.DropIndex(
                name: "IX_CafeTypes_TypeOfCafeId1",
                table: "CafeTypes");

            migrationBuilder.DropIndex(
                name: "IX_CafeOccasions_CafeId1",
                table: "CafeOccasions");

            migrationBuilder.DropIndex(
                name: "IX_CafeOccasions_OccasionOfCafeId1",
                table: "CafeOccasions");

            migrationBuilder.DropColumn(
                name: "CafeId1",
                table: "CafeTypes");

            migrationBuilder.DropColumn(
                name: "TypeOfCafeId1",
                table: "CafeTypes");

            migrationBuilder.DropColumn(
                name: "CafeId1",
                table: "CafeOccasions");

            migrationBuilder.DropColumn(
                name: "OccasionOfCafeId1",
                table: "CafeOccasions");
        }
    }
}
