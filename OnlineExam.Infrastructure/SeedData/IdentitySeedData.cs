using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace OnlineExam.Infrastructure.SeedData
{
    internal static class IdentitySeedData
    {
        internal static IdentityRole[] IdentityRoles { get; set; }
        static IdentitySeedData()
        {
            IdentityRoles = new IdentityRole[] {
                new IdentityRole() {Name = "Admin", NormalizedName = "ADMIN", Id = "7A515A5C-EF58-4B4C-832D-E320DC412862", ConcurrencyStamp = "08bfe58a-071d-471f-a3ae-f0d64c4af71c"},
                new IdentityRole() {Name = "ExamCreator", NormalizedName = "EXAMCREATOR", Id = "41FB6182-8186-4E58-877C-7EE2C9690861", ConcurrencyStamp = "9869f560-c63d-4b6c-bd69-1d27638805ff"},
                new IdentityRole() {Name = "ExamUser", NormalizedName = "EXAMUSER", Id = "55693B5A-9729-484A-87B8-4AE24A06FB56", ConcurrencyStamp = "17066ecd-ee6b-40b5-a2aa-ca7cc429efee"}
            };
        }
    }

    public static class IdentityRoleNames 
    {
        public const string Admin = "Admin";
        public const string ExamCreator = "ExamCreator";
        public const string ExamUser = "ExamUser";
    }
}
