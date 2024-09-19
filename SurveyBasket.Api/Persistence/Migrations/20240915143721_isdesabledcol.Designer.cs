﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyBasket.Api.Persistence;

#nullable disable

namespace SurveyBasket.Api.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240915143721_isdesabledcol")]
    partial class isdesabledcol
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "Permissions",
                            ClaimValue = "polls:read",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "Permissions",
                            ClaimValue = "polls:add",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "Permissions",
                            ClaimValue = "polls:update",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "Permissions",
                            ClaimValue = "polls:delete",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 5,
                            ClaimType = "Permissions",
                            ClaimValue = "questions:read",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 6,
                            ClaimType = "Permissions",
                            ClaimValue = "questions:add",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 7,
                            ClaimType = "Permissions",
                            ClaimValue = "questions:update",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 8,
                            ClaimType = "Permissions",
                            ClaimValue = "users:read",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 9,
                            ClaimType = "Permissions",
                            ClaimValue = "users:add",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 10,
                            ClaimType = "Permissions",
                            ClaimValue = "users:update",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 11,
                            ClaimType = "Permissions",
                            ClaimValue = "roles:read",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 12,
                            ClaimType = "Permissions",
                            ClaimValue = "roles:add",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 13,
                            ClaimType = "Permissions",
                            ClaimValue = "roles:update",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        },
                        new
                        {
                            Id = 14,
                            ClaimType = "Permissions",
                            ClaimValue = "results:read",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "744b1d68-3c09-4bf4-953e-31b12da3cf31",
                            RoleId = "1eed8580-fda8-4780-af77-9885d9050998"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId", "Content")
                        .IsUnique();

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1eed8580-fda8-4780-af77-9885d9050998",
                            ConcurrencyStamp = "803b0f78-f34b-428f-8883-da6a2397654e",
                            IsDefault = false,
                            IsDeleted = false,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "a06d81d5-465e-4900-ae6f-0a240e2f1090",
                            ConcurrencyStamp = "803b0f78-f34b-428f-8883-da6a2397654e",
                            IsDefault = true,
                            IsDeleted = false,
                            Name = "Member",
                            NormalizedName = "MEMBER"
                        });
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDesabled")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "744b1d68-3c09-4bf4-953e-31b12da3cf31",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c2424acf-28b9-4c91-96c7-f90e4cbd1e99",
                            Email = "admin@survey-pasket.com",
                            EmailConfirmed = true,
                            FirstName = "survey basket",
                            IsDesabled = false,
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@SURVEY-PASKET.COM",
                            NormalizedUserName = "ADMIN@SURVEY-PASKET.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEBetjtLBsz8XRJf6d2QfYoNhCOM6LzMHdF2QFctARng3vhY1Br5D7Kvbi7l0pcGsJA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "71B8D54078B44B9189227C7182D1DC06",
                            TwoFactorEnabled = false,
                            UserName = "admin@survey-pasket.com"
                        });
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Poll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("EndsAt")
                        .HasColumnType("date");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("StartsAt")
                        .HasColumnType("date");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdateById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedById")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.HasIndex("UpdateById");

                    b.ToTable("polls");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("PollId")
                        .HasColumnType("int");

                    b.Property<string>("UpdateById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedById")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdateById");

                    b.HasIndex("PollId", "Content")
                        .IsUnique();

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PollId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmittedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("PollId", "UserId")
                        .IsUnique();

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.VoteAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("VoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("VoteId", "QuestionId")
                        .IsUnique();

                    b.ToTable("VoteAnswers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Answer", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.ApplicationUser", b =>
                {
                    b.OwnsMany("SurveyBasket.Api.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<DateTime>("CreatedOn")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("ExpiresOn")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("RevocedOn")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("RefreshTokens", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Poll", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Question", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.Poll", "Poll")
                        .WithMany("Questions")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Poll");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Vote", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.Poll", "Poll")
                        .WithMany("Votes")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Poll");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.VoteAnswer", b =>
                {
                    b.HasOne("SurveyBasket.Api.Entities.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.Question", "Question")
                        .WithMany("Votes")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurveyBasket.Api.Entities.Vote", "Vote")
                        .WithMany("VoteAnswers")
                        .HasForeignKey("VoteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("Question");

                    b.Navigation("Vote");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Poll", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SurveyBasket.Api.Entities.Vote", b =>
                {
                    b.Navigation("VoteAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
