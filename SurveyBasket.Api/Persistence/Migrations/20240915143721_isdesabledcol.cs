using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class isdesabledcol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDesabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "744b1d68-3c09-4bf4-953e-31b12da3cf31",
                columns: new[] { "IsDesabled", "PasswordHash" },
                values: new object[] { false, "AQAAAAIAAYagAAAAEBetjtLBsz8XRJf6d2QfYoNhCOM6LzMHdF2QFctARng3vhY1Br5D7Kvbi7l0pcGsJA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesabled",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "744b1d68-3c09-4bf4-953e-31b12da3cf31",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDzkMEw+LBZy/mTFnQYSa2EEgTVHQTEzEKoO/v0AOKY9tssDIiJAHLu5aMwA8XkL/Q==");
        }
    }
}
