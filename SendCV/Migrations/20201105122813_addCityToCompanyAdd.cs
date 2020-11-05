using Microsoft.EntityFrameworkCore.Migrations;

namespace SendCV.Migrations
{
    public partial class addCityToCompanyAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CompanyAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CompanyAddresses");
        }
    }
}
