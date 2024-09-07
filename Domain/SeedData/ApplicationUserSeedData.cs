namespace Domain.SeedData
{
    public static class ApplicationUserSeedData
    {
        public static Guid Id
        {
            get
            {
                return Guid.Parse("B18F5FB4-033E-4A02-AE6A-8BA3A04AB8E8");
            }
        }

        public static string Email
        {
            get
            {
                return "admin@gmail.com";
            }
        }

        public static string Username
        {
            get
            {
                return "superadmin";
            }
        }

        public static string Password
        {
            get
            {
                /// ##################################->>> P@ssword123 <<<-###################################
                ///
                return "AQAAAAEAACcQAAAAEHy3kf7wmEhi9CKUFlxK8/Gf5MI+LuPK9QTOGxPA4sRRUPQrrujKT2NDjcHCss63vg==";
            }
        }

        public static string FirstName
        {
            get
            {
                return "SuperAdmin";
            }
        }

        public static string LastName
        {
            get
            {
                return "";
            }
        }

        public static Guid ConcurrencyStamp
        {
            get
            {
                return Guid.Parse("8335ABE9-BE93-41C6-AE78-5888FD0C42D5");
            }
        }

        //public static bool IsEmailConfirmed
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public static bool IsPhoneNumberConfirmed
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}


    }
}
