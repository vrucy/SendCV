using Microsoft.EntityFrameworkCore.Migrations;

namespace SendCV.Migrations
{
    public partial class addNavigationPropInCompanyCred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddresses_CompanyCredentials_CompanyCredentialsId",
                table: "CompanyAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAddresses_CompanyCredentialsId",
                table: "CompanyAddresses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "CompanyCredentials");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "CompanyCredentials");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyCredentialsId",
                table: "CompanyAddresses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddresses_CompanyCredentialsId",
                table: "CompanyAddresses",
                column: "CompanyCredentialsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddresses_CompanyCredentials_CompanyCredentialsId",
                table: "CompanyAddresses",
                column: "CompanyCredentialsId",
                principalTable: "CompanyCredentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddresses_CompanyCredentials_CompanyCredentialsId",
                table: "CompanyAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAddresses_CompanyCredentialsId",
                table: "CompanyAddresses");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CompanyCredentials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CompanyCredentials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyCredentialsId",
                table: "CompanyAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddresses_CompanyCredentialsId",
                table: "CompanyAddresses",
                column: "CompanyCredentialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddresses_CompanyCredentials_CompanyCredentialsId",
                table: "CompanyAddresses",
                column: "CompanyCredentialsId",
                principalTable: "CompanyCredentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
