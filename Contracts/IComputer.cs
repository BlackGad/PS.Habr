using System;

namespace Contracts
{
    public interface IComputer
    {
        #region Properties

        string Name { get; }

        #endregion

        #region Members

        Answer Answer(Guid questionTicket);
        Guid Ask(string question);

        #endregion
    }
}