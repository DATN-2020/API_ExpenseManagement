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
                name: "Trips",
                columns: table => new
                {
                    Id_Trip = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Trip = table.Column<string>(nullable: true),
                    Image_Trip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id_Trip);
                });

            migrationBuilder.CreateTable(
                name: "TypeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Type = table.Column<string>(nullable: true),
                    Image_Type = table.Column<string>(nullable: true),
                    TypeExpense = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCategories", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id_Wallet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Wallet = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Id_Type_Wallet = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id_Wallet);
                });

            migrationBuilder.CreateTable(
                name: "IncomeContacts",
                columns: table => new
                {
                    Id_IncomeContact = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactId_Contact = table.Column<int>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id_Loan = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Loan = table.Column<string>(nullable: true),
                    Date_Pay = table.Column<string>(nullable: true),
                    ContactId_Contact = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id_Loan);
                    table.ForeignKey(
                        name: "FK_Loans_Contacts_ContactId_Contact",
                        column: x => x.ContactId_Contact,
                        principalTable: "Contacts",
                        principalColumn: "Id_Contact",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id_Cate = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameCate = table.Column<string>(nullable: true),
                    ImageCate = table.Column<string>(nullable: true),
                    TypeCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id_Cate);
                    table.ForeignKey(
                        name: "FK_Categories_TypeCategories_TypeCategoryId",
                        column: x => x.TypeCategoryId,
                        principalTable: "TypeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    TypeCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income_Outcomes", x => x.Id_come);
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TypeCategoryId",
                table: "Categories",
                column: "TypeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Outcomes_LoanId_Loan",
                table: "Income_Outcomes",
                column: "LoanId_Loan");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Outcomes_TripId_Trip",
                table: "Income_Outcomes",
                column: "TripId_Trip");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeContacts_ContactId_Contact",
                table: "IncomeContacts",
                column: "ContactId_Contact");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ContactId_Contact",
                table: "Loans",
                column: "ContactId_Contact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Income_Outcomes");

            migrationBuilder.DropTable(
                name: "IncomeContacts");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "TypeWallets");

            migrationBuilder.DropTable(
                name: "UserCategory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "TypeCategories");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
