using System.ComponentModel;

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
    }
}
