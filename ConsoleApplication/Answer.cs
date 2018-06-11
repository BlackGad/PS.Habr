namespace ConsoleApplication
{
    class Answer
    {
        #region Constructors

        public Answer(string message = null)
        {
            Message = message;
        }

        #endregion

        #region Properties

        public bool IsReady
        {
            get { return !string.IsNullOrEmpty(Message); }
        }

        public string Message { get; }

        #endregion
    }
}