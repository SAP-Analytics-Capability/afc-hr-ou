using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace masterdata.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "client",
                schema: "public",
                columns: table => new
                {
                    client_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    application = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    expiration_date = table.Column<DateTime>(nullable: false),
                    enabled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "result",
                schema: "public",
                columns: table => new
                {
                    result_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    organizational_unit_code = table.Column<string>(nullable: true),
                    cost_center_description = table.Column<string>(nullable: true),
                    legal_entity_description = table.Column<string>(nullable: true),
                    organizational_unit_description = table.Column<string>(nullable: true),
                    legal_entity = table.Column<string>(nullable: true),
                    organizational_unit_type = table.Column<string>(nullable: true),
                    typology = table.Column<string>(nullable: true),
                    association_time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result", x => x.result_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client",
                schema: "public");

            migrationBuilder.DropTable(
                name: "result",
                schema: "public");
        }
    }
}
