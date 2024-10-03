using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterPeiceBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddContactUsTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contactUs",
                table: "contactUs");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "contactUs",
                newName: "MessageID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "contactUs",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "contactUs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "contactUs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__ContactU__C87C037CAC1E4233",
                table: "contactUs",
                column: "MessageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__ContactU__C87C037CAC1E4233",
                table: "contactUs");

            migrationBuilder.RenameColumn(
                name: "MessageID",
                table: "contactUs",
                newName: "MessageId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "contactUs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "contactUs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "contactUs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_contactUs",
                table: "contactUs",
                column: "MessageId");
        }
    }
}
