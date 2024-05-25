using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Datalayer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    products_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    products_name = table.Column<string>(type: "longtext", nullable: false),
                    products_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    products_company = table.Column<string>(type: "longtext", nullable: false),
                    products_category = table.Column<string>(type: "longtext", nullable: false),
                    products_image = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.products_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    accounts_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    accounts_email = table.Column<string>(type: "longtext", nullable: false),
                    accounts_user_name = table.Column<string>(type: "longtext", nullable: false),
                    accounts_date_of_birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    accounts_password = table.Column<string>(type: "longtext", nullable: false),
                    accounts_perm_group = table.Column<string>(type: "longtext", nullable: false),
                    accounts_salt_column = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.accounts_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    pages_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    pages_type = table.Column<string>(type: "longtext", nullable: false),
                    pages_author_id = table.Column<int>(type: "int", nullable: false),
                    fkproducts_id = table.Column<int>(type: "int", nullable: true),
                    pages_tags = table.Column<string>(type: "longtext", nullable: true),
                    pages_content = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.pages_id);
                    table.ForeignKey(
                        name: "FK_Pages_Products_fkproducts_id",
                        column: x => x.fkproducts_id,
                        principalTable: "Products",
                        principalColumn: "products_id");
                    table.ForeignKey(
                        name: "FK_Pages_accounts_pages_author_id",
                        column: x => x.pages_author_id,
                        principalTable: "accounts",
                        principalColumn: "accounts_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_fkproducts_id",
                table: "Pages",
                column: "fkproducts_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_pages_author_id",
                table: "Pages",
                column: "pages_author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
