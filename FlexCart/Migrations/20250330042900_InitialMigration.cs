using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexCart.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false),
                    company_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    mobile = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    addr = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    web = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    cus_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    mobile = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.cus_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    prod_code = table.Column<int>(type: "int", nullable: false),
                    prod_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    manf_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    barcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    prod_photo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    prod_type_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.prod_code);
                });

            migrationBuilder.CreateTable(
                name: "product_type",
                columns: table => new
                {
                    prod_type_id = table.Column<int>(type: "int", nullable: false),
                    prod_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product type", x => x.prod_type_id);
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    pur_code = table.Column<int>(type: "int", nullable: false),
                    purchaser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    purchase_date = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    net_amt = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    voucher_file = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase", x => x.pur_code);
                });

            migrationBuilder.CreateTable(
                name: "purchase_product",
                columns: table => new
                {
                    pur_prod_code = table.Column<int>(type: "int", nullable: false),
                    pur_code = table.Column<int>(type: "int", nullable: true),
                    batch_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    mfg_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase product", x => x.pur_prod_code);
                });

            migrationBuilder.CreateTable(
                name: "returns",
                columns: table => new
                {
                    return_id = table.Column<int>(type: "int", nullable: false),
                    return_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    return_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    product_code = table.Column<int>(type: "int", nullable: true),
                    sales_code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sales_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    return_amt = table.Column<decimal>(type: "numeric(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_returns", x => x.return_id);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    sales_code = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    sales_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    net_amt = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    paid_amt = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    user_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.sales_code);
                });

            migrationBuilder.CreateTable(
                name: "sales_product",
                columns: table => new
                {
                    sales_pro_code = table.Column<int>(type: "int", nullable: false),
                    sales_code = table.Column<int>(type: "int", nullable: true),
                    prod_code = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    rate = table.Column<decimal>(type: "numeric(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales product", x => x.sales_pro_code);
                });

            migrationBuilder.CreateTable(
                name: "stock_level",
                columns: table => new
                {
                    stk_id = table.Column<int>(type: "int", nullable: false),
                    available_stock = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    product_codes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_level", x => x.stk_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "purchase_product");

            migrationBuilder.DropTable(
                name: "returns");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "sales_product");

            migrationBuilder.DropTable(
                name: "stock_level");
        }
    }
}
