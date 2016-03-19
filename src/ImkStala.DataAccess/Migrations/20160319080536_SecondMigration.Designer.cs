using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ImkStala.DataAccess;

namespace ImkStala.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160319080536_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImkStala.DataAccess.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AccountType");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ReservationCalendarReservationCalendarId");

                    b.Property<DateTime>("ReservationEndDateTime");

                    b.Property<DateTime>("ReservationStartDateTime");

                    b.HasKey("ReservationId");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.ReservationCalendar", b =>
                {
                    b.Property<int>("ReservationCalendarId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ReservationCalendarId");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Email");

                    b.Property<string>("Manager");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<double>("Rating");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("VatCode");

                    b.Property<string>("Website");

                    b.Property<string>("Workhours");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.RestaurantTable", b =>
                {
                    b.Property<int>("RestaurantTableId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ReservationCalendarReservationCalendarId");

                    b.Property<int?>("RestaurantId");

                    b.Property<int>("RestaurantTableSeats");

                    b.HasKey("RestaurantTableId");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.Visitor", b =>
                {
                    b.Property<int>("VisitorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Number");

                    b.HasKey("VisitorId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.Reservation", b =>
                {
                    b.HasOne("ImkStala.DataAccess.Entities.ReservationCalendar")
                        .WithMany()
                        .HasForeignKey("ReservationCalendarReservationCalendarId");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.Restaurant", b =>
                {
                    b.HasOne("ImkStala.DataAccess.Entities.ApplicationUser")
                        .WithOne()
                        .HasForeignKey("ImkStala.DataAccess.Entities.Restaurant", "ApplicationUserId");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.RestaurantTable", b =>
                {
                    b.HasOne("ImkStala.DataAccess.Entities.ReservationCalendar")
                        .WithMany()
                        .HasForeignKey("ReservationCalendarReservationCalendarId");

                    b.HasOne("ImkStala.DataAccess.Entities.Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("ImkStala.DataAccess.Entities.Visitor", b =>
                {
                    b.HasOne("ImkStala.DataAccess.Entities.ApplicationUser")
                        .WithOne()
                        .HasForeignKey("ImkStala.DataAccess.Entities.Visitor", "ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ImkStala.DataAccess.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ImkStala.DataAccess.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("ImkStala.DataAccess.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
