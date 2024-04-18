using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRent.DeliveryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDeliverymanChangeDriverLicenseImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DriverLicenseImage",
                table: "Deliverymen",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "DriverLicenseImage",
                table: "Deliverymen",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
