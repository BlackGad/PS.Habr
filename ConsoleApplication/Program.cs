using System;
using System.Threading;

namespace ConsoleApplication
{
    class Program
    {
        #region Static members

        static void Main(string[] args)
        {
            var computer = new DeepThought();
            var question = "Answer to the Ultimate Question of Life, The Universe, and Everything";
            Console.WriteLine($"Asking {computer.Name} computer \"{question}\"");

            var questionTicket = computer.Ask(question);
            Console.WriteLine($"Question ticket is \"{questionTicket}\"");

            try
            {
                while (true)
                {
                    Console.Write("Trying to get an answer... ");
                    var answer = computer.Answer(questionTicket);
                    Console.Write(answer.IsReady ? answer.Message : "Not ready yet");
                    Console.Write(Environment.NewLine);

                    if (answer.IsReady) break;
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.GetBaseException().Message}");
            }

            Console.ReadLine();
        }

        #endregion
    }
}