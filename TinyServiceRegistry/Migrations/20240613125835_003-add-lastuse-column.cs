using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyServiceRegistry.Migrations
{
    /// <inheritdoc />
    public partial class _003addlastusecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LastUse",
                table: "ServiceInstance",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUse",
                table: "ServiceInstance");
        }
    }
}
