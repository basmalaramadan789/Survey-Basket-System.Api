using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Abstractions.Consts;

namespace SurveyBasket.Api.Persistence.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");

            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);



            var PasswordHasher = new PasswordHasher<ApplicationUser>();
            // Defualt Data
            builder.HasData(new ApplicationUser
            {
                Id = DefualtUsers.AdminId,
                FirstName = "survey basket",
                LastName = "Admin",
                UserName = DefualtUsers.AdminEmail,
                NormalizedUserName = DefualtUsers.AdminEmail.ToUpper(),
                Email = DefualtUsers.AdminEmail,
                NormalizedEmail = DefualtUsers.AdminEmail.ToUpper(),
                SecurityStamp = DefualtUsers.AdminSecurityStamp,
                ConcurrencyStamp = DefualtUsers.AdminConcarencyStamp,
                EmailConfirmed = true,
                PasswordHash = PasswordHasher.HashPassword(null!, DefualtUsers.AdminPassword)


            });
        }
    }
}
