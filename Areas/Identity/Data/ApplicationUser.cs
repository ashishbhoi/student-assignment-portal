using Microsoft.AspNetCore.Identity;

namespace StudentClassworkPortal.Areas.Identity.Data
{
    public enum StudentClass
    {
        VI,
        VII,
        VIII,
        IX,
        X,
        XI,
        XII
    }

    public enum StudentSection
    {
        A,
        B
    }

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public StudentClass? Class { get; set; }
        public StudentSection? Section { get; set; }
    }
}
