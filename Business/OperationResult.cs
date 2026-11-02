using System;
namespace Business
{
    public class OperationResult
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime TimeStamp { get; set; }

        public OperationResult(string message, string title, bool isSuccess)
        {
            Message = message;
            Title = title;
            IsSuccess = isSuccess;
            TimeStamp = DateTime.Now;
        }
    }
}
