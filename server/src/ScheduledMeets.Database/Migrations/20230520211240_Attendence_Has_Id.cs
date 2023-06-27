using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduledMeets.Database.Migrations
{
    /// <inheritdoc />
    public partial class Attendence_Has_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_attendence",
                table: "attendence");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "attendence",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddUniqueConstraint(
                name: "ak_attendence_attendee_id_meet_id",
                table: "attendence",
                columns: new[] { "attendee_id", "meet_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_attendence",
                table: "attendence",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "ak_attendence_attendee_id_meet_id",
                table: "attendence");

            migrationBuilder.DropPrimaryKey(
                name: "pk_attendence",
                table: "attendence");

            migrationBuilder.DropColumn(
                name: "id",
                table: "attendence");

            migrationBuilder.AddPrimaryKey(
                name: "pk_attendence",
                table: "attendence",
                columns: new[] { "attendee_id", "meet_id" });
        }
    }
}
