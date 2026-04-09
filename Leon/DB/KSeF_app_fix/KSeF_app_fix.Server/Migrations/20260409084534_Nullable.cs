using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KSeF_app_fix.Server.Migrations
{
    /// <inheritdoc />
    public partial class Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ksef");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GLN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Swift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBankOwnAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrier",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrier_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ksef",
                        principalTable: "Address",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SettlementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SettlementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KsefNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuePlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    FactorBankAccountId = table.Column<int>(type: "int", nullable: true),
                    SellerBankAccountId = table.Column<int>(type: "int", nullable: true),
                    FooterNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_BankAccount_FactorBankAccountId",
                        column: x => x.FactorBankAccountId,
                        principalSchema: "ksef",
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_BankAccount_SellerBankAccountId",
                        column: x => x.SellerBankAccountId,
                        principalSchema: "ksef",
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerPieceNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    PriceTotalNetto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ksef",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eori = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainAddressId = table.Column<int>(type: "int", nullable: true),
                    CorrespondenceAddressID = table.Column<int>(type: "int", nullable: true),
                    ContactInfoId = table.Column<int>(type: "int", nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Party_Address_CorrespondenceAddressID",
                        column: x => x.CorrespondenceAddressID,
                        principalSchema: "ksef",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Party_Address_MainAddressId",
                        column: x => x.MainAddressId,
                        principalSchema: "ksef",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Party_ContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalSchema: "ksef",
                        principalTable: "ContactInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Party_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ksef",
                        principalTable: "Invoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentInfo",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    IsPartial = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTermsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentInfo_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ksef",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settlement",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    TotalToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlement_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ksef",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxSummary",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    TaxRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Netto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PLNAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxSummary_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ksef",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTerms = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terms_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "ksef",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartialPayment",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartialPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartialPayment_PaymentInfo_PaymentInfoId",
                        column: x => x.PaymentInfoId,
                        principalSchema: "ksef",
                        principalTable: "PaymentInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderInfo",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderInfo_Terms_TermsId",
                        column: x => x.TermsId,
                        principalSchema: "ksef",
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportInfo",
                schema: "ksef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportType = table.Column<int>(type: "int", nullable: false),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    TransportOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoDescription = table.Column<int>(type: "int", nullable: false),
                    PackagingUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipFromId = table.Column<int>(type: "int", nullable: true),
                    ShipViaID = table.Column<int>(type: "int", nullable: true),
                    ShipToID = table.Column<int>(type: "int", nullable: true),
                    TermsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportInfo_Address_ShipFromId",
                        column: x => x.ShipFromId,
                        principalSchema: "ksef",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportInfo_Address_ShipToID",
                        column: x => x.ShipToID,
                        principalSchema: "ksef",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportInfo_Address_ShipViaID",
                        column: x => x.ShipViaID,
                        principalSchema: "ksef",
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportInfo_Carrier_CarrierId",
                        column: x => x.CarrierId,
                        principalSchema: "ksef",
                        principalTable: "Carrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportInfo_Terms_TermsId",
                        column: x => x.TermsId,
                        principalSchema: "ksef",
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrier_AddressId",
                schema: "ksef",
                table: "Carrier",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_SettlementId",
                schema: "ksef",
                table: "Charges",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_TermsId",
                schema: "ksef",
                table: "Contract",
                column: "TermsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_SettlementId",
                schema: "ksef",
                table: "Deductions",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BuyerId",
                schema: "ksef",
                table: "Invoice",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_FactorBankAccountId",
                schema: "ksef",
                table: "Invoice",
                column: "FactorBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SellerBankAccountId",
                schema: "ksef",
                table: "Invoice",
                column: "SellerBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SellerId",
                schema: "ksef",
                table: "Invoice",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                schema: "ksef",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_TermsId",
                schema: "ksef",
                table: "OrderInfo",
                column: "TermsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartialPayment_PaymentInfoId",
                schema: "ksef",
                table: "PartialPayment",
                column: "PaymentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_ContactInfoId",
                schema: "ksef",
                table: "Party",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_CorrespondenceAddressID",
                schema: "ksef",
                table: "Party",
                column: "CorrespondenceAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Party_InvoiceId",
                schema: "ksef",
                table: "Party",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_MainAddressId",
                schema: "ksef",
                table: "Party",
                column: "MainAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfo_InvoiceId",
                schema: "ksef",
                table: "PaymentInfo",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settlement_InvoiceId",
                schema: "ksef",
                table: "Settlement",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxSummary_InvoiceId",
                schema: "ksef",
                table: "TaxSummary",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_InvoiceId",
                schema: "ksef",
                table: "Terms",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportInfo_CarrierId",
                schema: "ksef",
                table: "TransportInfo",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportInfo_ShipFromId",
                schema: "ksef",
                table: "TransportInfo",
                column: "ShipFromId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportInfo_ShipToID",
                schema: "ksef",
                table: "TransportInfo",
                column: "ShipToID");

            migrationBuilder.CreateIndex(
                name: "IX_TransportInfo_ShipViaID",
                schema: "ksef",
                table: "TransportInfo",
                column: "ShipViaID");

            migrationBuilder.CreateIndex(
                name: "IX_TransportInfo_TermsId",
                schema: "ksef",
                table: "TransportInfo",
                column: "TermsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Settlement_SettlementId",
                schema: "ksef",
                table: "Charges",
                column: "SettlementId",
                principalSchema: "ksef",
                principalTable: "Settlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Terms_TermsId",
                schema: "ksef",
                table: "Contract",
                column: "TermsId",
                principalSchema: "ksef",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deductions_Settlement_SettlementId",
                schema: "ksef",
                table: "Deductions",
                column: "SettlementId",
                principalSchema: "ksef",
                principalTable: "Settlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Party_BuyerId",
                schema: "ksef",
                table: "Invoice",
                column: "BuyerId",
                principalSchema: "ksef",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Party_SellerId",
                schema: "ksef",
                table: "Invoice",
                column: "SellerId",
                principalSchema: "ksef",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_CorrespondenceAddressID",
                schema: "ksef",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_MainAddressId",
                schema: "ksef",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_BankAccount_FactorBankAccountId",
                schema: "ksef",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_BankAccount_SellerBankAccountId",
                schema: "ksef",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Party_BuyerId",
                schema: "ksef",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Party_SellerId",
                schema: "ksef",
                table: "Invoice");

            migrationBuilder.DropTable(
                name: "Charges",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Contract",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Deductions",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "InvoiceLine",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "OrderInfo",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "PartialPayment",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "TaxSummary",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "TransportInfo",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Settlement",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "PaymentInfo",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Carrier",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Terms",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Party",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "ContactInfo",
                schema: "ksef");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "ksef");
        }
    }
}
