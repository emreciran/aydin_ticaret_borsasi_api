using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class UpdatedRequestSuggestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "RequestSuggestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reply",
                table: "RequestSuggestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReplyBy",
                table: "RequestSuggestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReplyDate",
                table: "RequestSuggestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RequestSuggestions");

            migrationBuilder.DropColumn(
                name: "Reply",
                table: "RequestSuggestions");

            migrationBuilder.DropColumn(
                name: "ReplyBy",
                table: "RequestSuggestions");

            migrationBuilder.DropColumn(
                name: "ReplyDate",
                table: "RequestSuggestions");
        }
    }
}
