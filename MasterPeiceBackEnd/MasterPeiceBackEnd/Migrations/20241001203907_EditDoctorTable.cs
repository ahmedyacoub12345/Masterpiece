using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterPeiceBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class EditDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserID",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserID",
                table: "Doctors",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserID",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserID",
                table: "Doctors",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
