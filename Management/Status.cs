namespace MemoryGame.Management
{
    public class Status
    {
        #region Constructor

        public Status(string message = null, bool isError = false)
        {
            Message = message;
            IsError = isError;

        }

        #endregion
        
        #region Accesser

        public string Message { get; set; }
        public bool IsError { get; set; }

        #endregion
        
    }
}