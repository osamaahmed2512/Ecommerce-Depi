using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_dpei.Migrations
{
    /// <inheritdoc />
    public partial class updateaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CustomerAddresses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CustomerAddresses");
        }
    }
}
