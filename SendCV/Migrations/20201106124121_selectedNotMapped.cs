using Microsoft.EntityFrameworkCore.Migrations;

namespace SendCV.Migrations
{
    public partial class selectedNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "CompanyCredentials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "CompanyCredentials",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
