using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S_Clinical.Migrations
{
    /// <inheritdoc />
    public partial class PilotMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalCares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SequentialNumber = table.Column<int>(type: "int", nullable: false),
                    DateTimeArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusType = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalCares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalCares_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Triages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialityType = table.Column<int>(type: "int", nullable: false),
                    PriorityLevelType = table.Column<int>(type: "int", nullable: false),
                    ClinicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triages_ClinicalCares_ClinicalCareId",
                        column: x => x.ClinicalCareId,
                        principalTable: "ClinicalCares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalCares_PatientId",
                table: "ClinicalCares",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Triages_ClinicalCareId",
                table: "Triages",
                column: "ClinicalCareId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triages");

            migrationBuilder.DropTable(
                name: "ClinicalCares");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
