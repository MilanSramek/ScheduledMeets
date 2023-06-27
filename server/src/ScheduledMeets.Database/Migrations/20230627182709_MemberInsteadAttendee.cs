using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduledMeets.Database.Migrations
{
    /// <inheritdoc />
    public partial class MemberInsteadAttendee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendence");

            migrationBuilder.DropTable(
                name: "attendee");

            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    meets_id = table.Column<long>(type: "bigint", nullable: false),
                    is_owner = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_meets_meets_id",
                        column: x => x.meets_id,
                        principalTable: "meets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_member_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    meet_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendance", x => x.id);
                    table.UniqueConstraint("ak_attendance_member_id_meet_id", x => new { x.member_id, x.meet_id });
                    table.ForeignKey(
                        name: "fk_attendance_meet_meet_id",
                        column: x => x.meet_id,
                        principalTable: "meet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_attendance_member_member_id",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_attendance_meet_id",
                table: "attendance",
                column: "meet_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendance_member_id",
                table: "attendance",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_meets_id",
                table: "member",
                column: "meets_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_user_id",
                table: "member",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "member");

            migrationBuilder.CreateTable(
                name: "attendee",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    meets_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    is_owner = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendee", x => x.id);
                    table.ForeignKey(
                        name: "fk_attendee_meets_meets_id",
                        column: x => x.meets_id,
                        principalTable: "meets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_attendee_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendence",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    attendee_id = table.Column<long>(type: "bigint", nullable: false),
                    meet_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendence", x => x.id);
                    table.UniqueConstraint("ak_attendence_attendee_id_meet_id", x => new { x.attendee_id, x.meet_id });
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
                name: "ix_attendee_meets_id",
                table: "attendee",
                column: "meets_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendee_user_id",
                table: "attendee",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendence_attendee_id",
                table: "attendence",
                column: "attendee_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendence_meet_id",
                table: "attendence",
                column: "meet_id");
        }
    }
}
