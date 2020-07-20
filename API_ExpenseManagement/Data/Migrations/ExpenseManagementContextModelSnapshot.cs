﻿// <auto-generated />
using System;
using API_ExpenseManagement.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_ExpenseManagement.Data.Migrations
{
    [DbContext(typeof(ExpenseManagementContext))]
    partial class ExpenseManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_ExpenseManagement.Models.Bank", b =>
                {
                    b.Property<int>("Id_Bank")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Interest");

                    b.Property<string>("Name_Bank");

                    b.HasKey("Id_Bank");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Bill", b =>
                {
                    b.Property<int>("Id_Bill")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount_Bill");

                    b.Property<string>("Desciption");

                    b.Property<string>("Id_Category");

                    b.Property<string>("Id_Type");

                    b.Property<string>("Id_Wallet");

                    b.Property<DateTime>("date_e");

                    b.Property<DateTime>("date_s");

                    b.Property<string>("id_Time");

                    b.Property<bool>("isEdit");

                    b.Property<bool>("isFinnish");

                    b.Property<bool>("isPay");

                    b.HasKey("Id_Bill");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Budget", b =>
                {
                    b.Property<int>("Id_Budget")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount_Budget");

                    b.Property<string>("Id_Cate");

                    b.Property<string>("Id_Wallet");

                    b.Property<string>("Id_type");

                    b.Property<float>("Remain");

                    b.Property<string>("id_Time");

                    b.Property<bool>("isFinnish");

                    b.Property<DateTime>("time_e");

                    b.Property<DateTime>("time_s");

                    b.HasKey("Id_Budget");

                    b.ToTable("Budget");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Category", b =>
                {
                    b.Property<int>("Id_Cate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id_type");

                    b.Property<string>("ImageCate");

                    b.Property<string>("NameCate");

                    b.HasKey("Id_Cate");

                    b.HasIndex("Id_type");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Contact", b =>
                {
                    b.Property<int>("Id_Contact")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name_Contact");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id_Contact");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.CreateWallet", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<string>("Description");

                    b.Property<int>("Id_Type_Wallet");

                    b.Property<string>("Name_Wallet");

                    b.HasKey("User_Id");

                    b.ToTable("CreateWallet");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Custom", b =>
                {
                    b.Property<int>("Id_Custom")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id_Custom");

                    b.ToTable("Custom");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.EndSavingWallet", b =>
                {
                    b.Property<int>("id_end")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("id_saving");

                    b.HasKey("id_end");

                    b.ToTable("EndSavingWallet");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.getBill", b =>
                {
                    b.Property<int>("id_getBill")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("amount_Bill");

                    b.Property<DateTime>("createDate");

                    b.Property<int>("id_cate");

                    b.Property<int>("id_type");

                    b.HasKey("id_getBill");

                    b.ToTable("getBill");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.getBudget", b =>
                {
                    b.Property<int>("id_getBudget")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("amount_budget");

                    b.Property<int>("id_cate");

                    b.Property<int>("id_type");

                    b.Property<float>("remain");

                    b.HasKey("id_getBudget");

                    b.ToTable("getBudget");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.GetCategory", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idType");

                    b.HasKey("userId");

                    b.ToTable("GetCategory");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.getIncome", b =>
                {
                    b.Property<int>("id_getIncome")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("id_cate");

                    b.Property<int>("id_type");

                    b.Property<float>("total_Icome");

                    b.Property<float>("total_Outcome");

                    b.HasKey("id_getIncome");

                    b.ToTable("getIncome");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.getPeriodic", b =>
                {
                    b.Property<int>("id_getBudget")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("amount_budget");

                    b.Property<int>("id_cate");

                    b.Property<int>("id_type");

                    b.HasKey("id_getBudget");

                    b.ToTable("getPeriodic");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.GetWallet", b =>
                {
                    b.Property<int>("Userid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Userid");

                    b.ToTable("GetWallet");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Income_Outcome", b =>
                {
                    b.Property<int>("Id_come")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<string>("CategoryId_Cate");

                    b.Property<string>("Date_come");

                    b.Property<string>("Description_come");

                    b.Property<string>("Id_Bill");

                    b.Property<string>("Id_Budget");

                    b.Property<string>("Id_Per");

                    b.Property<string>("Id_type");

                    b.Property<bool>("Is_Come");

                    b.Property<string>("LoanId_Loan");

                    b.Property<string>("TripId_Trip");

                    b.Property<string>("WalletId_Wallet");

                    b.HasKey("Id_come");

                    b.ToTable("Income_Outcomes");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.IncomeContact", b =>
                {
                    b.Property<int>("Id_IncomeContact")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id_IncomeContact");

                    b.ToTable("IncomeContacts");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Loan", b =>
                {
                    b.Property<int>("Id_Loan")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactId_contact");

                    b.Property<string>("Date_Pay");

                    b.Property<string>("Name_Loan");

                    b.HasKey("Id_Loan");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Login", b =>
                {
                    b.Property<string>("User_Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.HasKey("User_Name");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Periodic", b =>
                {
                    b.Property<int>("Id_Per")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount_Per");

                    b.Property<string>("Desciption");

                    b.Property<string>("Id_Cate");

                    b.Property<string>("Id_Type");

                    b.Property<string>("Id_Wallet");

                    b.Property<DateTime>("date_e");

                    b.Property<DateTime>("date_s");

                    b.Property<string>("id_Time");

                    b.Property<bool>("isComeback");

                    b.Property<bool>("isFinnish");

                    b.Property<bool>("isPay");

                    b.HasKey("Id_Per");

                    b.ToTable("Periodic");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.SavingWallet", b =>
                {
                    b.Property<int>("id_saving")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("date_e");

                    b.Property<string>("date_s");

                    b.Property<string>("id_bank");

                    b.Property<string>("id_user");

                    b.Property<bool>("is_Finnish");

                    b.Property<string>("name_saving");

                    b.Property<float>("price");

                    b.Property<float>("price_end");

                    b.HasKey("id_saving");

                    b.ToTable("SavingWallet");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Summary", b =>
                {
                    b.Property<int>("id_Summary")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("beginBalance");

                    b.Property<string>("date");

                    b.Property<float>("endBalance");

                    b.Property<string>("id_wallet");

                    b.Property<float>("netBalance");

                    b.Property<float>("totalBorrow");

                    b.Property<float>("totalIncome");

                    b.Property<float>("totalIncome_Outcome_1");

                    b.Property<float>("totalIncome_Outcome_10");

                    b.Property<float>("totalIncome_Outcome_11");

                    b.Property<float>("totalIncome_Outcome_12");

                    b.Property<float>("totalIncome_Outcome_2");

                    b.Property<float>("totalIncome_Outcome_3");

                    b.Property<float>("totalIncome_Outcome_4");

                    b.Property<float>("totalIncome_Outcome_5");

                    b.Property<float>("totalIncome_Outcome_6");

                    b.Property<float>("totalIncome_Outcome_7");

                    b.Property<float>("totalIncome_Outcome_8");

                    b.Property<float>("totalIncome_Outcome_9");

                    b.Property<float>("totalLoan");

                    b.Property<float>("totalOther");

                    b.Property<float>("totalOutcome");

                    b.HasKey("id_Summary");

                    b.ToTable("Summary");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Time_Periodic", b =>
                {
                    b.Property<int>("id_Time")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("desciption");

                    b.HasKey("id_Time");

                    b.ToTable("Time_Periodic");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Transactions", b =>
                {
                    b.Property<int>("id_trans")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("date_trans");

                    b.Property<string>("id_saving");

                    b.Property<bool>("is_End");

                    b.Property<bool>("is_Income");

                    b.Property<string>("name_trans");

                    b.Property<float>("price_trans");

                    b.HasKey("id_trans");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Transfers", b =>
                {
                    b.Property<int>("idTransfers")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Id_type");

                    b.Property<float>("amount");

                    b.Property<DateTime>("date");

                    b.Property<string>("desciption");

                    b.Property<int>("id_chuyen");

                    b.Property<int>("id_nhan");

                    b.HasKey("idTransfers");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Trip", b =>
                {
                    b.Property<int>("Id_Trip")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name_Trip");

                    b.Property<string>("id_user");

                    b.HasKey("Id_Trip");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.TypeCategory", b =>
                {
                    b.Property<int>("Id_type")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image_Type");

                    b.Property<string>("Name_Type");

                    b.Property<string>("TypeExpense");

                    b.HasKey("Id_type");

                    b.ToTable("TypeCategories");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.TypeWallet", b =>
                {
                    b.Property<int>("Id_Type_Wallet")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image_Type_Wallet");

                    b.Property<string>("Name_Type_Wallet");

                    b.HasKey("Id_Type_Wallet");

                    b.ToTable("TypeWallets");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Check_Wallet");

                    b.Property<string>("FullName");

                    b.Property<string>("Password");

                    b.Property<string>("User_Name");

                    b.HasKey("User_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.UserCategory", b =>
                {
                    b.Property<int>("Id_UserCategory")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId_Cate");

                    b.Property<int>("User_Id");

                    b.HasKey("Id_UserCategory");

                    b.ToTable("UserCategory");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Wallet", b =>
                {
                    b.Property<int>("Id_Wallet")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount_Wallet");

                    b.Property<float>("Amount_now");

                    b.Property<string>("Description");

                    b.Property<string>("Id_Type_Wallet");

                    b.Property<string>("Name_Wallet");

                    b.Property<string>("User_Id");

                    b.HasKey("Id_Wallet");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Category", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.TypeCategory")
                        .WithMany("Categories")
                        .HasForeignKey("Id_type")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
