using Data_Access;

namespace Business
{
    public class Global
    {
        public static Staff CurrentLoggedInStaff { get; set; }

        public static void SeedDefaultData()
        {
            AccessString.SeedDefaultData();
        }
    }
}
