using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableDateFormedToBand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_Bands_BandId",
                table: "Gigs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Gigs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "BandId",
                table: "Gigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFormed",
                table: "Bands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_Bands_BandId",
                table: "Gigs",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_Bands_BandId",
                table: "Gigs");

            migrationBuilder.DropColumn(
                name: "DateFormed",
                table: "Bands");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Gigs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BandId",
                table: "Gigs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_Bands_BandId",
                table: "Gigs",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
