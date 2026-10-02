using System.Configuration;

namespace Data_Access
{
    public class AccessString
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }
    }
}
