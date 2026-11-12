using AppEnums.cs;
using System.ComponentModel;
using System.Linq;

namespace Business
{
    public class Util
    {
        public static bool IsInputEmpty(string inputValue)
        {
            return string.IsNullOrWhiteSpace(inputValue);
        }
        public static void RefreshRecordsList<T>(BindingList<T> records, T record, int recordIndex)
        {
            if (recordIndex < records.Count && recordIndex != -1)
            {
                records[recordIndex] = record;
            }
            else
                records.Add(record);
        }
        public static bool IsRequired(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        public static bool IsValidId(int id)
        {
            return id > 0;
        }
        public static OperationResult CanAccessForm(params EnRole[] allowedRoles)
        {
            var currentStaff = Global.CurrentLoggedInStaff;

            if (currentStaff?.RoleInfo == null)
                return new OperationResult("User is not logged in.", "Access Denied", false);

            if (allowedRoles == null || allowedRoles.Count() == 0)
                return new OperationResult("No allowed roles specified.", "Access Denied", false);

            if (!allowedRoles.Contains(currentStaff.RoleInfo.RoleId))
                return new OperationResult("You do not have permission to access this form.", "Access Denied", false);

            return new OperationResult("Access granted.", "Access Granted", true);
        }

    }
}
