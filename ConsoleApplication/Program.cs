using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Contracts;

namespace ConsoleApplication
{
    class Program
    {
        #region Static members

        private static List<Type> DiscoverComputers()
        {
            var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var computerAssemblies = Directory.EnumerateFiles(outputDirectory, "Deep*.dll", SearchOption.AllDirectories).ToList();
            var result = new List<Type>();
            foreach (var computerAssembly in computerAssemblies)
            {
                result.AddRange(Assembly.LoadFile(computerAssembly)
                                        .GetTypes()
                                        .Where(t => typeof(IComputer).IsAssignableFrom(t) && !t.IsAbstract));
            }
            return result;
        }

        private static IComputer GetComputer(Type computerType)
        {
            return (IComputer)Activator.CreateInstance(computerType);
        }

        static void Main(string[] args)
        {
            var discoveredComputers = DiscoverComputers();

            Type selectedComputer = null;
            while (selectedComputer == null)
            {
                Console.Clear();
                Console.WriteLine("Choose computer: ");
                for (var index = 0; index < discoveredComputers.Count; index++)
                {
                    var discoveredComputer = discoveredComputers[index];
                    Console.WriteLine($"{index + 1}. {discoveredComputer.Name}");
                }

                var input = Console.ReadLine();
                int selectedIndex;
                if (int.TryParse(input, out selectedIndex) && selectedIndex > 0 && selectedIndex <= discoveredComputers.Count)
                {
                    selectedComputer = discoveredComputers[selectedIndex - 1];
                }
            }

            var computer = GetComputer(selectedComputer);
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