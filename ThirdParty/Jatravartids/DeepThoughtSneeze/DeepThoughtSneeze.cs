using System;
using System.Collections.Generic;
using Contracts;

namespace DeepThoughtSneeze
{
    public class DeepThoughtSneeze : IComputer
    {
        #region Constants

        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        #endregion

        private readonly Dictionary<Guid, DateTimeOffset> _questions;

        #region Constructors

        public DeepThoughtSneeze()
        {
            _questions = new Dictionary<Guid, DateTimeOffset>();
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return "Deep thought beta"; }
        }

        #endregion

        #region Members

        public Answer Answer(Guid questionTicket)
        {
            if (!_questions.ContainsKey(questionTicket)) throw new InvalidOperationException("There is no answer to an unasked question");
            return _questions[questionTicket] < DateTimeOffset.Now ? new Answer("42") : new Answer();
        }

        public Guid Ask(string question)
        {
            var id = Guid.NewGuid();
            var answerAfter = TimeSpan.FromSeconds(Random.Next(2, 10));
            _questions.Add(id, DateTimeOffset.Now + answerAfter);
            return id;
        }

        #endregion
    }
}