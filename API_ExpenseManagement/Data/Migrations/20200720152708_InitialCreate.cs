using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_ExpenseManagement.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id_Bank = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Bank = table.Column<string>(nullable: true),
                    Interest = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id_Bank);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id_Bill = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount_Bill = table.Column<float>(nullable: false),
                    Desciption = table.Column<string>(nullable: true),
                    date_s = table.Column<DateTime>(nullable: false),
                    date_e = table.Column<DateTime>(nullable: false),
                    isPay = table.Column<bool>(nullable: false),
                    isFinnish = table.Column<bool>(nullable: false),
                    isEdit = table.Column<bool>(nullable: false),
                    Id_Category = table.Column<string>(nullable: true),
                    Id_Type = table.Column<string>(nullable: true),
                    Id_Wallet = table.Column<string>(nullable: true),
                    id_Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id_Bill);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id_Budget = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount_Budget = table.Column<float>(nullable: false),
                    Remain = table.Column<float>(nullable: false),
                    time_s = table.Column<DateTime>(nullable: false),
                    time_e = table.Column<DateTime>(nullable: false),
                    isFinnish = table.Column<bool>(nullable: false),
                    Id_Cate = table.Column<string>(nullable: true),
                    Id_Wallet = table.Column<string>(nullable: true),
                    Id_type = table.Column<string>(nullable: true),
                    id_Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id_Budget);
                });

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
                name: "Custom",
                columns: table => new
                {
                    Id_Custom = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Custom", x => x.Id_Custom);
                });

            migrationBuilder.CreateTable(
                name: "EndSavingWallet",
                columns: table => new
                {
                    id_end = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_saving = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndSavingWallet", x => x.id_end);
                });

            migrationBuilder.CreateTable(
                name: "getBill",
                columns: table => new
                {
                    id_getBill = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_cate = table.Column<int>(nullable: false),
                    id_type = table.Column<int>(nullable: false),
                    amount_Bill = table.Column<float>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_getBill", x => x.id_getBill);
                });

            migrationBuilder.CreateTable(
                name: "getBudget",
                columns: table => new
                {
                    id_getBudget = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_cate = table.Column<int>(nullable: false),
                    id_type = table.Column<int>(nullable: false),
                    amount_budget = table.Column<float>(nullable: false),
                    remain = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_getBudget", x => x.id_getBudget);
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
                name: "getIncome",
                columns: table => new
                {
                    id_getIncome = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_cate = table.Column<int>(nullable: false),
                    id_type = table.Column<int>(nullable: false),
                    total_Icome = table.Column<float>(nullable: false),
                    total_Outcome = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_getIncome", x => x.id_getIncome);
                });

            migrationBuilder.CreateTable(
                name: "getPeriodic",
                columns: table => new
                {
                    id_getBudget = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_cate = table.Column<int>(nullable: false),
                    id_type = table.Column<int>(nullable: false),
                    amount_budget = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_getPeriodic", x => x.id_getBudget);
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
                name: "Income_Outcomes",
                columns: table => new
                {
                    Id_come = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(nullable: false),
                    Date_come = table.Column<string>(nullable: true),
                    Description_come = table.Column<string>(nullable: true),
                    Is_Come = table.Column<bool>(nullable: false),
                    CategoryId_Cate = table.Column<string>(nullable: true),
                    LoanId_Loan = table.Column<string>(nullable: true),
                    TripId_Trip = table.Column<string>(nullable: true),
                    Id_type = table.Column<string>(nullable: true),
                    WalletId_Wallet = table.Column<string>(nullable: true),
                    Id_Bill = table.Column<string>(nullable: true),
                    Id_Budget = table.Column<string>(nullable: true),
                    Id_Per = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income_Outcomes", x => x.Id_come);
                });

            migrationBuilder.CreateTable(
                name: "IncomeContacts",
                columns: table => new
                {
                    Id_IncomeContact = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeContacts", x => x.Id_IncomeContact);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id_Loan = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_Loan = table.Column<string>(nullable: true),
                    Date_Pay = table.Column<string>(nullable: true),
                    ContactId_contact = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id_Loan);
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
                name: "Periodic",
                columns: table => new
                {
                    Id_Per = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount_Per = table.Column<float>(nullable: false),
                    Desciption = table.Column<string>(nullable: true),
                    date_e = table.Column<DateTime>(nullable: false),
                    date_s = table.Column<DateTime>(nullable: false),
                    isComeback = table.Column<bool>(nullable: false),
                    isPay = table.Column<bool>(nullable: false),
                    isFinnish = table.Column<bool>(nullable: false),
                    Id_Cate = table.Column<string>(nullable: true),
                    Id_Type = table.Column<string>(nullable: true),
                    Id_Wallet = table.Column<string>(nullable: true),
                    id_Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodic", x => x.Id_Per);
                });

            migrationBuilder.CreateTable(
                name: "SavingWallet",
                columns: table => new
                {
                    id_saving = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name_saving = table.Column<string>(nullable: true),
                    date_s = table.Column<string>(nullable: true),
                    price = table.Column<float>(nullable: false),
                    price_end = table.Column<float>(nullable: false),
                    date_e = table.Column<string>(nullable: true),
                    id_bank = table.Column<string>(nullable: true),
                    is_Finnish = table.Column<bool>(nullable: false),
                    id_user = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingWallet", x => x.id_saving);
                });

            migrationBuilder.CreateTable(
                name: "Summary",
                columns: table => new
                {
                    id_Summary = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    beginBalance = table.Column<float>(nullable: false),
                    endBalance = table.Column<float>(nullable: false),
                    netBalance = table.Column<float>(nullable: false),
                    totalIncome = table.Column<float>(nullable: false),
                    totalOutcome = table.Column<float>(nullable: false),
                    totalLoan = table.Column<float>(nullable: false),
                    totalBorrow = table.Column<float>(nullable: false),
                    totalOther = table.Column<float>(nullable: false),
                    totalIncome_Outcome_1 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_2 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_3 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_4 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_5 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_6 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_7 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_8 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_9 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_10 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_11 = table.Column<float>(nullable: false),
                    totalIncome_Outcome_12 = table.Column<float>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    id_wallet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summary", x => x.id_Summary);
                });

            migrationBuilder.CreateTable(
                name: "Time_Periodic",
                columns: table => new
                {
                    id_Time = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    desciption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time_Periodic", x => x.id_Time);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id_trans = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name_trans = table.Column<string>(nullable: true),
                    price_trans = table.Column<float>(nullable: false),
                    date_trans = table.Column<string>(nullable: true),
                    is_Income = table.Column<bool>(nullable: false),
                    is_End = table.Column<bool>(nullable: false),
                    id_saving = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id_trans);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    idTransfers = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_chuyen = table.Column<int>(nullable: false),
                    id_nhan = table.Column<int>(nullable: false),
                    amount = table.Column<float>(nullable: false),
                    desciption = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    Id_type = table.Column<string>(nullable: true)
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
                    Name_Trip = table.Column<string>(nullable: true),
                    id_user = table.Column<string>(nullable: true)
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
                    FullName = table.Column<string>(nullable: true),
                    Check_Wallet = table.Column<bool>(nullable: false)
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
                    Amount_Wallet = table.Column<float>(nullable: false),
                    Amount_now = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Id_Type_Wallet = table.Column<string>(nullable: true),
                    User_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id_Wallet);
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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Id_type",
                table: "Categories",
                column: "Id_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "CreateWallet");

            migrationBuilder.DropTable(
                name: "Custom");

            migrationBuilder.DropTable(
                name: "EndSavingWallet");

            migrationBuilder.DropTable(
                name: "getBill");

            migrationBuilder.DropTable(
                name: "getBudget");

            migrationBuilder.DropTable(
                name: "GetCategory");

            migrationBuilder.DropTable(
                name: "getIncome");

            migrationBuilder.DropTable(
                name: "getPeriodic");

            migrationBuilder.DropTable(
                name: "GetWallet");

            migrationBuilder.DropTable(
                name: "Income_Outcomes");

            migrationBuilder.DropTable(
                name: "IncomeContacts");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Periodic");

            migrationBuilder.DropTable(
                name: "SavingWallet");

            migrationBuilder.DropTable(
                name: "Summary");

            migrationBuilder.DropTable(
                name: "Time_Periodic");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Trips");

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
        }
    }
}
