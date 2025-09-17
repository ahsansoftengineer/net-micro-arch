using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBA.Job.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalLookupBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Desc = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Created_At = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Updated_At = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalLookupBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalLookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Desc = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GlobalLookupBaseId = table.Column<int>(type: "int", nullable: true),
                    Created_At = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Updated_At = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalLookups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalLookups_GlobalLookupBases_GlobalLookupBaseId",
                        column: x => x.GlobalLookupBaseId,
                        principalTable: "GlobalLookupBases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalLookups_GlobalLookupBaseId",
                table: "GlobalLookups",
                column: "GlobalLookupBaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalLookups");

            migrationBuilder.DropTable(
                name: "GlobalLookupBases");
        }
    }
}
