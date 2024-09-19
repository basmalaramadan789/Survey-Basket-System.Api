using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1eed8580-fda8-4780-af77-9885d9050998", "803b0f78-f34b-428f-8883-da6a2397654e", false, false, "Admin", "ADMIN" },
                    { "a06d81d5-465e-4900-ae6f-0a240e2f1090", "803b0f78-f34b-428f-8883-da6a2397654e", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "744b1d68-3c09-4bf4-953e-31b12da3cf31", 0, "c2424acf-28b9-4c91-96c7-f90e4cbd1e99", "admin@survey-pasket.com", true, "survey basket", "Admin", false, null, "ADMIN@SURVEY-PASKET.COM", "ADMIN@SURVEY-PASKET.COM", "AQAAAAIAAYagAAAAEDzkMEw+LBZy/mTFnQYSa2EEgTVHQTEzEKoO/v0AOKY9tssDIiJAHLu5aMwA8XkL/Q==", null, false, "71B8D54078B44B9189227C7182D1DC06", false, "admin@survey-pasket.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permissions", "polls:read", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 2, "Permissions", "polls:add", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 3, "Permissions", "polls:update", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 4, "Permissions", "polls:delete", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 5, "Permissions", "questions:read", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 6, "Permissions", "questions:add", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 7, "Permissions", "questions:update", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 8, "Permissions", "users:read", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 9, "Permissions", "users:add", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 10, "Permissions", "users:update", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 11, "Permissions", "roles:read", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 12, "Permissions", "roles:add", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 13, "Permissions", "roles:update", "1eed8580-fda8-4780-af77-9885d9050998" },
                    { 14, "Permissions", "results:read", "1eed8580-fda8-4780-af77-9885d9050998" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1eed8580-fda8-4780-af77-9885d9050998", "744b1d68-3c09-4bf4-953e-31b12da3cf31" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a06d81d5-465e-4900-ae6f-0a240e2f1090");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1eed8580-fda8-4780-af77-9885d9050998", "744b1d68-3c09-4bf4-953e-31b12da3cf31" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eed8580-fda8-4780-af77-9885d9050998");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "744b1d68-3c09-4bf4-953e-31b12da3cf31");
        }
    }
}
