namespace Business
{
    public class Util
    {
        public static bool IsInputEmpty(string inputValue)
        {
            return string.IsNullOrWhiteSpace(inputValue);
        }
    }
}
