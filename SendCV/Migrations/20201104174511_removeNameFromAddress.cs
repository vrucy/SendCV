using Microsoft.EntityFrameworkCore.Migrations;

namespace SendCV.Migrations
{
    public partial class removeNameFromAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CompanyAddresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CompanyAddresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
