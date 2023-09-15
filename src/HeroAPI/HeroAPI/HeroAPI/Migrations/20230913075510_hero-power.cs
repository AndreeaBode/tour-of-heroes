using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class heropower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroPower_Heroes_HeroId",
                table: "HeroPower");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroPower_Powers_PowerId",
                table: "HeroPower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroPower",
                table: "HeroPower");

            migrationBuilder.RenameTable(
                name: "HeroPower",
                newName: "HeroPowers");

            migrationBuilder.RenameIndex(
                name: "IX_HeroPower_PowerId",
                table: "HeroPowers",
                newName: "IX_HeroPowers_PowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroPowers",
                table: "HeroPowers",
                columns: new[] { "HeroId", "PowerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HeroPowers_Heroes_HeroId",
                table: "HeroPowers",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroPowers_Powers_PowerId",
                table: "HeroPowers",
                column: "PowerId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroPowers_Heroes_HeroId",
                table: "HeroPowers");

            migrationBuilder.DropForeignKey(
                name: "FK_HeroPowers_Powers_PowerId",
                table: "HeroPowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroPowers",
                table: "HeroPowers");

            migrationBuilder.RenameTable(
                name: "HeroPowers",
                newName: "HeroPower");

            migrationBuilder.RenameIndex(
                name: "IX_HeroPowers_PowerId",
                table: "HeroPower",
                newName: "IX_HeroPower_PowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroPower",
                table: "HeroPower",
                columns: new[] { "HeroId", "PowerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HeroPower_Heroes_HeroId",
                table: "HeroPower",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeroPower_Powers_PowerId",
                table: "HeroPower",
                column: "PowerId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
