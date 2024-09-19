
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Abstractions.Consts;

namespace SurveyBasket.Api.Persistence.EntitiesConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {

            var PasswordHasher = new PasswordHasher<ApplicationUser>();
            // Defualt Data
            builder.HasData(new IdentityUserRole<string>
            {
                UserId =DefualtUsers.AdminId,
                RoleId =DefaultRoles.AdminRoleId,


            });
                
                


        }
    }
}
