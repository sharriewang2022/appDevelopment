using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceWasmApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gl_bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillType = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    BillNo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BillName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    BillDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BillMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    BillYear = table.Column<int>(type: "INTEGER", nullable: false),
                    BillDirection = table.Column<int>(type: "INTEGER", nullable: false),
                    InputDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BillAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BillAbstract = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsAdd = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_bill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gl_billType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillTypeNo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BillTypeName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsAdd = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_billType", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gl_bill");

            migrationBuilder.DropTable(
                name: "gl_billType");
        }
    }
}
