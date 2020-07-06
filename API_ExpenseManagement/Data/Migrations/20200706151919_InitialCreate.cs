using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_ExpenseManagement.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id_Contact = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Contact = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id_Contact);
                });

            migrationBuilder.CreateTable(
                name: "CreateWallet",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(nullable: false),
                    Name_Wallet = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Id_Type_Wallet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateWallet", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "GetCategory",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetCategory", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "GetWallet",
                columns: table => new
                {
                    Userid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetWallet", x => x.Userid);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    User_Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.User_Name);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    idTransfers = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_chuyen = table.Column<int>(nullable: false),
                    id_nhan = table.Column<int>(nullable: false),
                    amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.idTransfers);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id_Trip = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Trip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id_Trip);
                });

            migrationBuilder.CreateTable(
                name: "TypeCategories",
                columns: table => new
                {
                    Id_type = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Type = table.Column<string>(nullable: true),
                    Image_Type = table.Column<string>(nullable: true),
                    TypeExpense = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCategories", x => x.Id_type);
                });

            migrationBuilder.CreateTable(
                name: "TypeWallets",
                columns: table => new
                {
                    Id_Type_Wallet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Type_Wallet = table.Column<string>(nullable: true),
                    Image_Type_Wallet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeWallets", x => x.Id_Type_Wallet);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Check_Wallet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id_Loan = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Loan = table.Column<string>(nullable: true),
                    Date_Pay = table.Column<string>(nullable: true),
                    ContactId_contact = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id_Loan);
                    table.ForeignKey(
                        name: "FK_Loans_Contacts_ContactId_contact",
                        column: x => x.ContactId_contact,
                        principalTable: "Contacts",
                        principalColumn: "Id_Contact",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id_Cate = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameCate = table.Column<string>(nullable: true),
                    ImageCate = table.Column<string>(nullable: true),
                    Id_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id_Cate);
                    table.ForeignKey(
                        name: "FK_Categories_TypeCategories_Id_type",
                        column: x => x.Id_type,
                        principalTable: "TypeCategories",
                        principalColumn: "Id_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id_Wallet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Wallet = table.Column<string>(nullable: true),
                    Amount_Wallet = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Id_Type_Wallet = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id_Wallet);
                    table.ForeignKey(
                        name: "FK_Wallets_TypeWallets_Id_Type_Wallet",
                        column: x => x.Id_Type_Wallet,
                        principalTable: "TypeWallets",
                        principalColumn: "Id_Type_Wallet",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCategory",
                columns: table => new
                {
                    Id_UserCategory = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId_Cate = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategory", x => x.Id_UserCategory);
                    table.ForeignKey(
                        name: "FK_UserCategory_Categories_CategoryId_Cate",
                        column: x => x.CategoryId_Cate,
                        principalTable: "Categories",
                        principalColumn: "Id_Cate",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategory_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Income_Outcomes",
                columns: table => new
                {
                    Id_come = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(nullable: false),
                    Date_come = table.Column<string>(nullable: true),
                    Description_come = table.Column<string>(nullable: true),
                    Is_Come = table.Column<bool>(nullable: false),
                    CategoryId_Cate = table.Column<int>(nullable: false),
                    LoanId_Loan = table.Column<int>(nullable: false),
                    TripId_Trip = table.Column<int>(nullable: false),
                    WalletId_Wallet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income_Outcomes", x => x.Id_come);
                    table.ForeignKey(
                        name: "FK_Income_Outcomes_Categories_CategoryId_Cate",
                        column: x => x.CategoryId_Cate,
                        principalTable: "Categories",
                        principalColumn: "Id_Cate",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Income_Outcomes_Loans_LoanId_Loan",
                        column: x => x.LoanId_Loan,
                        principalTable: "Loans",
                        principalColumn: "Id_Loan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Income_Outcomes_Trips_TripId_Trip",
                        column: x => x.TripId_Trip,
                        principalTable: "Trips",
                        principalColumn: "Id_Trip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Income_Outcomes_Wallets_WalletId_Wallet",
                        column: x => x.WalletId_Wallet,
                        principalTable: "Wallets",
                        principalColumn: "Id_Wallet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeContacts",
                columns: table => new
                {
                    Id_IncomeContact = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactId_Contact = table.Column<int>(nullable: true),
                    Income_OutcomeId_come = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeContacts", x => x.Id_IncomeContact);
                    table.ForeignKey(
                        name: "FK_IncomeContacts_Contacts_ContactId_Contact",
                        column: x => x.ContactId_Contact,
                        principalTable: "Contacts",
                        principalColumn: "Id_Contact",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncomeContacts_Income_Outcomes_Income_OutcomeId_come",
                        column: x => x.Income_OutcomeId_come,
                        principalTable: "Income_Outcomes",
                        principalColumn: "Id_come",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Id_type",
                table: "Categories",
                column: "Id_type");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Outcomes_CategoryId_Cate",
                table: "Income_Outcomes",
                column: "CategoryId_Cate");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Outcomes_LoanId_Loan",
                table: "Income_Outcomes",
                column: "LoanId_Loan");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Outcomes_TripId_Trip",
                table: "Income_Outcomes",
                column: "TripId_Trip");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Outcomes_WalletId_Wallet",
                table: "Income_Outcomes",
                column: "WalletId_Wallet");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeContacts_ContactId_Contact",
                table: "IncomeContacts",
                column: "ContactId_Contact");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeContacts_Income_OutcomeId_come",
                table: "IncomeContacts",
                column: "Income_OutcomeId_come");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ContactId_contact",
                table: "Loans",
                column: "ContactId_contact");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategory_CategoryId_Cate",
                table: "UserCategory",
                column: "CategoryId_Cate");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategory_User_Id",
                table: "UserCategory",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_Id_Type_Wallet",
                table: "Wallets",
                column: "Id_Type_Wallet");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_User_Id",
                table: "Wallets",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateWallet");

            migrationBuilder.DropTable(
                name: "GetCategory");

            migrationBuilder.DropTable(
                name: "GetWallet");

            migrationBuilder.DropTable(
                name: "IncomeContacts");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "UserCategory");

            migrationBuilder.DropTable(
                name: "Income_Outcomes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "TypeCategories");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "TypeWallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
