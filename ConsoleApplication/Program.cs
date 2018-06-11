using System;
using System.Threading;
using Contracts;

namespace ConsoleApplication
{
    class Program
    {
        #region Static members

        private static IComputer GetComputer()
        {
            var now = DateTimeOffset.Now;
            var pulsarPeriod = TimeSpan.FromSeconds(1.337302088331d);
            var fullCycles = (long)(now.Ticks/pulsarPeriod.TotalSeconds);

            return fullCycles%2 == 0
                ? (IComputer)new DeepThought1.DeepThought1()
                : new DeepThought2.DeepThought2();
        }

        static void Main(string[] args)
        {
            var computer = GetComputer();
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