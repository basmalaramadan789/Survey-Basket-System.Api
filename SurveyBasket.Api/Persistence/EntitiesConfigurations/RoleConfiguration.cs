
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Abstractions.Consts;

namespace SurveyBasket.Api.Persistence.EntitiesConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

            var PasswordHasher = new PasswordHasher<ApplicationUser>();
            // Defualt Data
            builder.HasData([
                new ApplicationRole
                {
                    Id= DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    NormalizedName= DefaultRoles.Admin.ToUpper(),
                    ConcurrencyStamp=DefaultRoles.AdminRoleConcurrencyStamp,

                },
                 new ApplicationRole
                {
                    Id= DefaultRoles.MemberRoleId,
                    Name = DefaultRoles.Member,
                    NormalizedName= DefaultRoles.Member.ToUpper(),
                    ConcurrencyStamp=DefaultRoles.AdminRoleConcurrencyStamp,
                    IsDefault= true

                }
                ]);


        }
    }
}
