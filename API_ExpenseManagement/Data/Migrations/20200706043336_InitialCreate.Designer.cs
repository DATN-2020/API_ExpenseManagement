﻿// <auto-generated />
using System;
using API_ExpenseManagement.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_ExpenseManagement.Data.Migrations
{
    [DbContext(typeof(ExpenseManagementContext))]
    [Migration("20200706043336_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_ExpenseManagement.Models.Category", b =>
                {
                    b.Property<int>("Id_Cate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageCate");

                    b.Property<string>("NameCate");

                    b.Property<int?>("TypeCategoryId");

                    b.HasKey("Id_Cate");

                    b.HasIndex("TypeCategoryId");

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

                    b.HasKey("User_Id");

                    b.ToTable("CreateWallet");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Income_Outcome", b =>
                {
                    b.Property<int>("Id_come")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<int>("CategoryId_Cate");

                    b.Property<string>("Date_come");

                    b.Property<string>("Description_come");

                    b.Property<bool>("Is_Come");

                    b.Property<int>("LoanId_Loan");

                    b.Property<int>("TripId_Trip");

                    b.Property<int>("WalletId_Wallet");

                    b.HasKey("Id_come");

                    b.HasIndex("CategoryId_Cate");

                    b.HasIndex("LoanId_Loan");

                    b.HasIndex("TripId_Trip");

                    b.HasIndex("WalletId_Wallet");

                    b.ToTable("Income_Outcomes");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.IncomeContact", b =>
                {
                    b.Property<int>("Id_IncomeContact")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContactId_Contact");

                    b.Property<int?>("Income_OutcomeId_come");

                    b.HasKey("Id_IncomeContact");

                    b.HasIndex("ContactId_Contact");

                    b.HasIndex("Income_OutcomeId_come");

                    b.ToTable("IncomeContacts");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Loan", b =>
                {
                    b.Property<int>("Id_Loan")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContactId_Contact");

                    b.Property<string>("Date_Pay");

                    b.Property<string>("Name_Loan");

                    b.HasKey("Id_Loan");

                    b.HasIndex("ContactId_Contact");

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

            modelBuilder.Entity("API_ExpenseManagement.Models.Trip", b =>
                {
                    b.Property<int>("Id_Trip")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image_Trip");

                    b.Property<string>("Name_Trip");

                    b.HasKey("Id_Trip");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.TypeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image_Type");

                    b.Property<string>("Name_Type");

                    b.Property<string>("TypeExpense");

                    b.HasKey("Id");

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

                    b.HasIndex("CategoryId_Cate");

                    b.HasIndex("User_Id");

                    b.ToTable("UserCategory");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Wallet", b =>
                {
                    b.Property<int>("Id_Wallet")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount_Wallet");

                    b.Property<string>("Description");

                    b.Property<int>("Id_Type_Wallet");

                    b.Property<string>("Name_Wallet");

                    b.Property<int>("User_Id");

                    b.HasKey("Id_Wallet");

                    b.HasIndex("Id_Type_Wallet");

                    b.HasIndex("User_Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Category", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.TypeCategory")
                        .WithMany("Categories")
                        .HasForeignKey("TypeCategoryId");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Income_Outcome", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.Category")
                        .WithMany("Income_Outcomes")
                        .HasForeignKey("CategoryId_Cate")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API_ExpenseManagement.Models.Loan")
                        .WithMany("Income_Outcomes")
                        .HasForeignKey("LoanId_Loan")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API_ExpenseManagement.Models.Trip")
                        .WithMany("Income_Outcomes")
                        .HasForeignKey("TripId_Trip")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API_ExpenseManagement.Models.Wallet")
                        .WithMany("Income_Outcomes")
                        .HasForeignKey("WalletId_Wallet")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.IncomeContact", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.Contact")
                        .WithMany("IncomeContacts")
                        .HasForeignKey("ContactId_Contact");

                    b.HasOne("API_ExpenseManagement.Models.Income_Outcome")
                        .WithMany("IncomeContacts")
                        .HasForeignKey("Income_OutcomeId_come");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Loan", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.Contact")
                        .WithMany("Loans")
                        .HasForeignKey("ContactId_Contact");
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.UserCategory", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.Category")
                        .WithMany("UserCategories")
                        .HasForeignKey("CategoryId_Cate")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API_ExpenseManagement.Models.User")
                        .WithMany("UserCategories")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API_ExpenseManagement.Models.Wallet", b =>
                {
                    b.HasOne("API_ExpenseManagement.Models.TypeWallet")
                        .WithMany("Wallets")
                        .HasForeignKey("Id_Type_Wallet")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API_ExpenseManagement.Models.User")
                        .WithMany("wallets")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
