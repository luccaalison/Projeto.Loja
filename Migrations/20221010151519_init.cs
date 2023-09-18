using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto.Loja.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_VendaProdutos_VendaProdutoId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_VendaProdutoId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "VendaProdutoId",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "Troco",
                table: "Vendas",
                newName: "QtdProduto");

            migrationBuilder.CreateTable(
                name: "VendaVendaProduto",
                columns: table => new
                {
                    ProdutosId = table.Column<int>(type: "int", nullable: false),
                    VendasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaVendaProduto", x => new { x.ProdutosId, x.VendasId });
                    table.ForeignKey(
                        name: "FK_VendaVendaProduto_VendaProdutos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "VendaProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendaVendaProduto_Vendas_VendasId",
                        column: x => x.VendasId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendaVendaProduto_VendasId",
                table: "VendaVendaProduto",
                column: "VendasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaVendaProduto");

            migrationBuilder.RenameColumn(
                name: "QtdProduto",
                table: "Vendas",
                newName: "Troco");

            migrationBuilder.AddColumn<int>(
                name: "VendaProdutoId",
                table: "Vendas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VendaProdutoId",
                table: "Vendas",
                column: "VendaProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_VendaProdutos_VendaProdutoId",
                table: "Vendas",
                column: "VendaProdutoId",
                principalTable: "VendaProdutos",
                principalColumn: "Id");
        }
    }
}
