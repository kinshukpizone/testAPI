namespace Domain.SeedData
{
    public static class ApplicationRoleSeedData
    {
        public static Guid _idSuperAdmin
        {
            get
            {
                return Guid.Parse("EF5CA44F-7A2F-456C-B36E-0A7F066C9EC4");
            }
        }


    }

    public static class AppRoles
    {
        public const string SUPERADMIN = "superadmin";
    }
}
