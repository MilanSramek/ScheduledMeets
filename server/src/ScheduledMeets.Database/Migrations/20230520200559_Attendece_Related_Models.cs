using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduledMeets.Database.Migrations
{
    /// <inheritdoc />
    public partial class Attendece_Related_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meet",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    meets_id = table.Column<long>(type: "bigint", nullable: false),
                    from = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    to = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meet", x => x.id);
                    table.ForeignKey(
                        name: "fk_meet_meets_meets_id",
                        column: x => x.meets_id,
                        principalTable: "meets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendence",
                columns: table => new
                {
                    attendee_id = table.Column<long>(type: "bigint", nullable: false),
                    meet_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendence", x => new { x.attendee_id, x.meet_id });
                    table.ForeignKey(
                        name: "fk_attendence_attendee_attendee_id",
                        column: x => x.attendee_id,
                        principalTable: "attendee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_attendence_meet_meet_id",
                        column: x => x.meet_id,
                        principalTable: "meet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_attendence_attendee_id",
                table: "attendence",
                column: "attendee_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendence_meet_id",
                table: "attendence",
                column: "meet_id");

            migrationBuilder.CreateIndex(
                name: "ix_meet_meets_id",
                table: "meet",
                column: "meets_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendence");

            migrationBuilder.DropTable(
                name: "meet");
        }
    }
}
