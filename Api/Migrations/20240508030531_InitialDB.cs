using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nchar(18)", fixedLength: true, maxLength: 18, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Cpf = table.Column<string>(type: "nchar(14)", fixedLength: true, maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operadora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operadora_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicoOperadora",
                columns: table => new
                {
                    MedicosAssociadosId = table.Column<int>(type: "int", nullable: false),
                    OperadorasAssociadasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoOperadora", x => new { x.MedicosAssociadosId, x.OperadorasAssociadasId });
                    table.ForeignKey(
                        name: "FK_MedicoOperadora_Medico_MedicosAssociadosId",
                        column: x => x.MedicosAssociadosId,
                        principalTable: "Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicoOperadora_Operadora_OperadorasAssociadasId",
                        column: x => x.OperadorasAssociadasId,
                        principalTable: "Operadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperadoraMedico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    OperadoraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperadoraMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperadoraMedico_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperadoraMedico_Operadora_OperadoraId",
                        column: x => x.OperadoraId,
                        principalTable: "Operadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicoOperadora_OperadorasAssociadasId",
                table: "MedicoOperadora",
                column: "OperadorasAssociadasId");

            migrationBuilder.CreateIndex(
                name: "IX_Operadora_EmpresaId",
                table: "Operadora",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_OperadoraMedico_MedicoId",
                table: "OperadoraMedico",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_OperadoraMedico_OperadoraId",
                table: "OperadoraMedico",
                column: "OperadoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicoOperadora");

            migrationBuilder.DropTable(
                name: "OperadoraMedico");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Operadora");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
