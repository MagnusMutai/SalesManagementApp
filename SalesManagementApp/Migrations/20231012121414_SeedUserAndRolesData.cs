using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace SalesManagementApp.Migrations
{
    /// <inheritdoc />
    










    public partial class SeedUserAndRolesData : Migration
    {
        const string ADMIN_ROLE_GUID = "1ac93c24-5760-47da-a600-2e0bd8e87e2c";
        const string SM_ROLE_GUID = "0b25cab9-654a-47dd-b4f5-81c977736725";
        const string TL_ROLE_GUID = "27a87ddc-67ad-4bfc-9536-d540d0d0e76a";
        const string SR_ROLE_GUID = "3cfac255-741e-4369-ba86-837b981f678d";

        const string ADMIN_USER_GUID = "98460c43-5166-4d50-b743-08393117dcfc";
        const string SM_USER_GUID = "5dcc5505-a371-47cf-9145-f423eae0f645";
        const string TL_USER_GUID = "72930cb2-104b-4ade-ad05-bacc1752f6a8";
        const string SR1_USER_GUID = "daa26546-73e0-4ce5-855c-9609c411255e";
        const string SR2_USER_GUID = "a99c218f-c75d-42d1-a48a-9a13ba4de72b";
        const string SR3_USER_GUID = "d633c47e-1254-4605-9739-819b90d9732f";
        const string SR4_USER_GUID = "02736205-74cf-437c-adf9-b2dc3b215820";


        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var passwordHash = hasher.HashPassword(null, "Password1!");

            AddUser(migrationBuilder, "admin@oexl.com", passwordHash, ADMIN_USER_GUID);

            AddUser(migrationBuilder, "bob.jones@oexl.com", passwordHash, SM_USER_GUID);

            AddUser(migrationBuilder, "henry.andrews@oexl.com", passwordHash, TL_USER_GUID);

            AddUser(migrationBuilder, "olivia.mills@oexl.com", passwordHash, SR1_USER_GUID);
            AddUser(migrationBuilder, "noah.robinson@oexl.com", passwordHash, SR2_USER_GUID);
            AddUser(migrationBuilder, "benjamin.lucas@oexl.com", passwordHash, SR3_USER_GUID);
            AddUser(migrationBuilder, "sarah.henderson@oexl.com", passwordHash, SR4_USER_GUID);

            AddRole(migrationBuilder, "Admin", ADMIN_ROLE_GUID);
            AddRole(migrationBuilder, "SM", SM_ROLE_GUID);
            AddRole(migrationBuilder, "TL", TL_ROLE_GUID);
            AddRole(migrationBuilder, "SR", SR_ROLE_GUID);

            AddUserToRole(migrationBuilder, ADMIN_USER_GUID, ADMIN_ROLE_GUID);
            AddUserToRole(migrationBuilder, SM_USER_GUID, SM_ROLE_GUID);

            AddUserToRole(migrationBuilder, TL_USER_GUID, TL_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR1_USER_GUID, SR_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR2_USER_GUID, SR_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR3_USER_GUID, SR_ROLE_GUID);
            AddUserToRole(migrationBuilder, SR4_USER_GUID, SR_ROLE_GUID);
        }

        private void AddUser(MigrationBuilder migrationBuilder, string email, string passwordHash, string userGuid)
        {
            StringBuilder sb = new StringBuilder();

            string emailCaps = email.ToUpper();

            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{userGuid}'");
            sb.AppendLine($",'{email}'");
            sb.AppendLine($",'{emailCaps}'");
            sb.AppendLine($",'{email}'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine($",'{emailCaps}'");
            sb.AppendLine($", '{passwordHash}'");
            sb.AppendLine(", ''");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());
        }
        private void AddRole(MigrationBuilder migrationBuilder, string roleName, string roleGuid)
        {
            string roleNameCaps = roleName.ToUpper();

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{roleGuid}','{roleName}','{roleNameCaps}')");


        }

        private void AddUserToRole(MigrationBuilder migrationBuilder, string userGuid, string roleGuid)
        {
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{userGuid}','{roleGuid}')");


        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            RemoveUser(migrationBuilder, ADMIN_USER_GUID, ADMIN_ROLE_GUID);
            RemoveUser(migrationBuilder, SM_USER_GUID, SM_ROLE_GUID);
            RemoveUser(migrationBuilder, TL_USER_GUID, TL_ROLE_GUID);
            RemoveUser(migrationBuilder, SR1_USER_GUID, SR_ROLE_GUID);
            RemoveUser(migrationBuilder, SR2_USER_GUID, SR_ROLE_GUID);
            RemoveUser(migrationBuilder, SR3_USER_GUID, SR_ROLE_GUID);
            RemoveUser(migrationBuilder, SR4_USER_GUID, SR_ROLE_GUID);

            RemoveRole(migrationBuilder, ADMIN_ROLE_GUID);
            RemoveRole(migrationBuilder, SM_ROLE_GUID);
            RemoveRole(migrationBuilder, TL_ROLE_GUID);
            RemoveRole(migrationBuilder, SR_ROLE_GUID);
        }
        private void RemoveUser(MigrationBuilder migrationBuilder, string userGuid, string roleGuid)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{userGuid}' AND RoleId = '{roleGuid}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{userGuid}'");
        }
        private void RemoveRole(MigrationBuilder migrationBuilder, string roleGuid)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{roleGuid}'");
        }
    }
}
