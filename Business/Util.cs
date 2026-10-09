using System.Collections.Generic;

namespace Business
{
    public class Util
    {
        public static bool IsInputEmpty(string inputValue)
        {
            return string.IsNullOrWhiteSpace(inputValue);
        }

        public static void RefreshRecordsList<T>(List<T> records, T record, int recordIndex)
        {
            records[recordIndex] = record;
        }
    }
}
