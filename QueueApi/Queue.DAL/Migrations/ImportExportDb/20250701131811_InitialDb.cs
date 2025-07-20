using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Queue.DAL.Migrations.ImportExportDb
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchedulePeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    GroupScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulePeriods_GroupSchedules_GroupScheduleId",
                        column: x => x.GroupScheduleId,
                        principalTable: "GroupSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePeriods_GroupScheduleId",
                table: "SchedulePeriods",
                column: "GroupScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulePeriods");

            migrationBuilder.DropTable(
                name: "GroupSchedules");
        }
    }
}
